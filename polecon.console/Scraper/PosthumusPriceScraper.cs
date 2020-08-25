using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronWebScraper;
using polecon.service.Models;
using polecon.service.Service;

namespace polecon.console.Scraper
{
    public class PosthumusPriceScraper : WebScraper, IBatchScraper
    {
        public int ExpectedNumResults { get; private set; } = 103780;
        public PosthumusPriceScraper(IMemDbDataImportService service, int start)
        {
            Service = service;
            Start = start;
            Stopwatch = new Stopwatch();
            End = Math.Min(ExpectedNumResults, start + RecordInterval);
        }

        private IMemDbDataImportService Service { get; }
        private List<MemDbEntry> Data { get; } = new List<MemDbEntry>();
        private int Start { get; set; }
        private int End { get; }
        private Stopwatch Stopwatch { get; }
        private const string SearchPage = "http://www2.scc.rutgers.edu/memdb/result_postpr.php";
        private const int RecordInterval = 100 * 100;

        private const int minYear = 1585;
        private const int maxYear = 1813;

        public override void Init()
        {
            var formData = GetFormData();
            var url = Start > 0 ? $"{SearchPage}?start={Start}" : SearchPage; 
            Stopwatch.Start();
            PostRequest(url, Parse, formData, UserAgent);
        }

        private static HttpIdentity UserAgent =>
            new HttpIdentity
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36"
            };

        private void GetExpectedResults(Response response)
        {
            var resultsNode = response.QuerySelectorAll("b")
                .FirstOrDefault(node => node.TextContent.Contains("matches were found for your query"));
            if(resultsNode == null)
            {
                return;
            }
            var numResultsStr = resultsNode
                .TextContentClean
                .Replace("matches were found for your query", "")
                .Trim();
            if (int.TryParse(numResultsStr, out var numResults))
            {
                ExpectedNumResults = numResults;
            }
        }

        public override void Parse(Response response)
        {
            if (response.Html.Contains("Sorry, no matches were found for the following query"))
            {
                throw new ArgumentException("No matches were found for the query. Try fixing the form data.");
            } 
            if (response.Html.Contains("matches were found for your query"))
            {
                GetExpectedResults(response);
            }
            Stopwatch.Restart();
            //this is an old web page that uses tables to do layout, so there will be some extra rows
            var dataTable = response.QuerySelectorAll("tr");
            var data = dataTable
                .Select(ParseRow)
                .Where(row => row != null)
                .ToList();
            Data.AddRange(data);
            Console.Write($"\rParse complete for page {Start/100:N0} in {Stopwatch.ElapsedMilliseconds:N0}ms.                     ");
            Start += 100;
            if (Start < End)
            {
                var url = $"{SearchPage}?start={Start}";
                var formData = GetFormData();
                PostRequest(url, Parse, formData, UserAgent);
            } else
            {
                //done, import data
                Console.WriteLine();
                Console.WriteLine($"Beginning data import for records up to {End}");
                Stopwatch.Restart();
                var importTask = Service.ImportPosthumusData(Data);
                Task.WaitAll(importTask);
                Stopwatch.Stop();
                Console.WriteLine($"Import of records up to {End} complete in {Stopwatch.ElapsedMilliseconds:N0}ms.");
            }
        }

        private Dictionary<string, string> GetFormData()
        {
            var years = Enumerable.Range(minYear, maxYear - minYear)
                .Select(year => $"\"{year}\"");
            var formData = new Dictionary<string, string>
            {
                { "table", "postpr" },
                { "product", "" },
                { "year", string.Join(" ", years) },
                { "month", "" },
                { "week", "" }
            };
            return formData;
        }

        private MemDbEntry ParseRow(HtmlNode row)
        {
            var cells = row.GetElementsByTagName("td[align]");
            if(cells.All(cell => string.IsNullOrWhiteSpace(cell.TextContent)))
            {
                return null;
            }

            if (row.GetElementsByTagName("th").Any())
            {
                return null;
            }

            var entry = new MemDbEntry
            {
                Day = 15
            };
            foreach (var mapping in cells.Zip(RowMap))
            {
                mapping.Second(entry, mapping.First.TextContent.Trim());
            }

            return entry;
        }
        //each item in the list corresponds to a cell in the table
        private static List<Action<MemDbEntry, string>> RowMap =>
            new List<Action<MemDbEntry, string>>
            {
                (i, value) => i.Num = int.Parse(value),
                (i, value) => i.Year = int.Parse(value),
                (i, value) => i.Month = int.Parse(value),
                //(i, value) => i.Day = int.Parse(value),
                (i, value) => i.ProductName = value,
                (i, value) => { }, //product name in Dutch, which we don't need
                (i, value) =>
                {
                    var numSoldParts = value.Split(' ');
                    if(numSoldParts.Length == 1)
                    {
                        i.NumSold = 1;
                        i.VolumeUnit = $"{value}s";
                    } else
                    {
                        if (int.TryParse(numSoldParts.First(), out var parsed))
                        {
                            i.NumSold = string.IsNullOrWhiteSpace(value)
                                ? (int?) null
                                : int.Parse(numSoldParts.First());
                        }
                        else
                        {
                            i.NumSold = 1;
                        }
                        i.VolumeUnit = string.Join(' ', numSoldParts.Skip(1));
                    }
                },
                (i, value) => i.CurrencyName = value,
                (i, value) => i.Price = string.IsNullOrWhiteSpace(value) ? (decimal?)null : decimal.Parse(value),
                (i, value) => { }, //notes
            };
    }
}
