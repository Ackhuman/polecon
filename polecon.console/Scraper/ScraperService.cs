using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IronWebScraper;
using polecon.service.Service;

namespace polecon.console.Scraper
{
    public interface IScraperService
    {
        Task RunScraper<T>() where T : WebScraper, IBatchScraper;
    }

    public class ScraperService : IScraperService
    {
        public ScraperService(IMemDbDataImportService service)
        {
            Service = service;
        }

        private IMemDbDataImportService Service { get; }

        public async Task RunScraper<T>() where T : WebScraper, IBatchScraper
        {
            try
            {
                var i = 0;
                var nextLoopStart = 100 * 100;
                var expectedNumResults = nextLoopStart + 1;
                while (i < expectedNumResults)
                {
                    var scraper = Activator.CreateInstance(typeof(T), Service, i);
                    Console.WriteLine($"Starting scrape ({i:N0} of {i + nextLoopStart:N0})...");
                    await ((WebScraper)scraper).StartAsync();
                    i += nextLoopStart;
                    expectedNumResults = ((IBatchScraper) scraper).ExpectedNumResults;
                }
                Console.WriteLine("Scrape complete.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
