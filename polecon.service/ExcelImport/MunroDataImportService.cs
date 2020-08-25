using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;

namespace polecon.service.ExcelImport
{
    public interface IMunroDataImportService
    {
        Task ImportWageTables(string filePath);
        bool TestOnly { get; set; }

    }
    public class MunroDataImportService : IMunroDataImportService
    {
        public MunroDataImportService(IExcelImportService importService)
        {
            ImportService = importService;
        }

        private IExcelImportService ImportService { get; }
        public bool TestOnly { get; set; }

        private const string YearStr = "Year";
        public async Task ImportWageTables(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();
            foreach(DataTable dataTable in result.Tables)
            {
                var yearIndex = GetYearRowIndex(dataTable);
                if (yearIndex.HasValue)
                {
                    var dataSetName = GetDataSetName(dataTable, yearIndex.Value);
                    var columnHeaders = GetColumnHeaders(dataTable, yearIndex.Value);
                    var yearMap = GetYears(dataTable, yearIndex.Value);
                    var dataMap = GetData(dataTable, yearMap, columnHeaders);
                    var units = ExtractUnits(columnHeaders.Values.ToList());
                    var flatData = FlattenData(yearMap, dataMap);
                    await ImportService.ImportData(dataSetName, columnHeaders.Values.ToList(), units, flatData.years, flatData.data, !TestOnly);
                }
            }
            // The result of each spreadsheet is in result.Tables
        }

        private (List<int> years, List<List<decimal?>> data) FlattenData(
            Dictionary<int, List<int>> yearMap,
            Dictionary<string, List<decimal?>> dataMap)
        {
            //we may have multiple years in each row, so duplicate the data points for those rows
            //  according to the number of years in that row.
            //  e.g. if we have 65.52 for 1640-45, we should create 5 instances of 65.52 in individual rows.
            var data = yearMap.Values.Zip(dataMap.Values, (y, d) => new {Years = y, Data = d})
                .SelectMany(yearData => yearData.Years, (yearData, year) => yearData.Data)
                .ToList();
            var years = yearMap.Values.SelectMany(y => y).ToList();
            return (years, data);
        }

        private List<string> ExtractUnits(
            List<string> columnHeaders
        )
        {
            //this is hard and likely to be error-prone, but let's try it anyway
            var blankInBlank = SplitByStringSeparator(columnHeaders, " in ");
            var blankColonBlank = SplitByStringSeparator(columnHeaders, ":");
            var indices = columnHeaders.Select(h =>
            {
                var split = new List<string> {h};
                if (h.Contains("Index"))
                {
                    split.Add("Index");
                }
                return split;
            }).ToList();
            var headersAndUnits = indices
                .Zip(blankInBlank, (i, b) => i.Count > 1 ? i : b)
                .Zip(blankColonBlank, (i, b) => i.Count > 1 ? i : b)
                .ToList();
            return headersAndUnits.Select(h => h.Last()).ToList();
        }

        private List<List<string>> SplitByStringSeparator(List<string> items, string separator)
        {
            return items
                .Select(h => 
                    h.Split(new[] { separator }, StringSplitOptions.None)
                        .Select(p => p.Trim())
                        .ToList()
                ).ToList();
        }

        private Dictionary<string, List<decimal?>> GetData(
            DataTable worksheet,
            Dictionary<int, List<int>> yearMap,
            Dictionary<int, string> columnHeaders
        )
        {
            var i = 0;
            var dataMap = new Dictionary<string, List<decimal?>>();
            foreach(DataRow row in worksheet.Rows)
            {
                if (yearMap.ContainsKey(i))
                {
                    var j = 0;
                    foreach (var cell in row.ItemArray)
                    {
                        var hasValue = decimal.TryParse(cell.ToString(), out var cellValue);
                        if (columnHeaders.ContainsKey(j))
                        {
                            var columnName = columnHeaders[j];
                            if (!dataMap.ContainsKey(columnName))
                            {
                                dataMap.Add(columnName, new List<decimal?>());
                            }
                            dataMap[columnName].Add(hasValue ? cellValue : (decimal?)null);
                        }
                        j++;
                    }
                }
                i++;
            }
            return dataMap;
        }

        private string GetDataSetName(DataTable worksheet, int yearIndex)
        {
            var dataSetParts = worksheet.Rows.Cast<DataRow>()
                .Take(yearIndex - 1)
                .SelectMany(
                    row => row.ItemArray, 
                    (row, cell) => cell.ToString().Trim()
                ).Where(cellString => !string.IsNullOrWhiteSpace(cellString))
                .ToList();
            return string.Join(" ", dataSetParts);
        }

        private Dictionary<int, List<int>> GetYears(DataTable worksheet, int yearRowIndex)
        {
            var i = 0;
            var years = new Dictionary<int, List<int>>();
            foreach(DataRow row in worksheet.Rows)
            {
                var yearStr = row.ItemArray[0].ToString().Trim();
                if (i >= yearRowIndex && !string.IsNullOrEmpty(yearStr))
                {
                    var rowYears = ParseRowYears(yearStr);
                    if (rowYears.Any())
                    {
                        years.Add(i, rowYears);
                    }
                }
                i++;
            }
            return years;
        }

        private static List<int> ParseRowYears(string yearStr)
        {
            var rowYears = new List<int>();
            if (int.TryParse(yearStr, out var year))
            {
                rowYears.Add(year);
            }
            //year range, e.g. "1561-65
            if (yearStr.Contains("-"))
            {
                var yearRange = yearStr.Split('-');
                var century = yearRange[0].Substring(0, 2);
                yearRange[1] = century + yearRange[1];
                foreach (var _yearStr in yearRange)
                {
                    if (int.TryParse(_yearStr, out var _year))
                    {
                        rowYears.Add(_year);
                    }
                }
            }

            return rowYears;
        }

        private Dictionary<int, string> GetColumnHeaders(DataTable worksheet, int yearRowIndex)
        {
            var headers = new Dictionary<int, string>();
            foreach (var index in Enumerable.Range(yearRowIndex - 1, 5))
            {
                var cells = worksheet.Rows[index].ItemArray;
                var i = 0;
                foreach (var cell in cells)
                {
                    var cellString = cell.ToString().Trim();
                    //ignore year column for now
                    if (i > 0 && !string.IsNullOrEmpty(cellString))
                    {
                        AddOrAppendCell(cellString, headers, cells, i);
                    }
                    i++;
                }
            }
            return headers;
        }

        private static void AddOrAppendCell(string cellString, Dictionary<int, string> headers, object[] cells, int i)
        {
            if (!headers.ContainsKey(i))
            {
                headers.Add(i, cellString);
            }
            else
            {
                headers[i] = $"{headers[i]} {cellString}";
            }
        }

        private int? GetYearRowIndex(DataTable worksheet)
        {
            var i = 0;
            foreach (DataRow row in worksheet.Rows)
            {
                if (row.ItemArray[0].ToString().StartsWith(YearStr))
                {
                    return i;
                }
                i++;
            }
            return null;
        }
    }
}