using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using polecon.console.ForeignApi;
using polecon.console.Scraper;
using polecon.service;
using polecon.service.Service;

namespace polecon.console
{
    class Program
    {
        private const int ExpectedNumResults = 103780;
        static async Task Main(string[] args)
        {
            var serviceProvider = InitializeDependencyInjection();
            var importService = serviceProvider.GetService<IMemDbDataImportService>();
            try
            {
                var i = 0;
                var nextLoopStart = 100 * 100;
                while (i < ExpectedNumResults)
                {
                    var scraper = new PosthumusPriceScraper(importService, i, ExpectedNumResults);
                    Console.WriteLine($"Starting scrape of MEMDB Posthumus Prices ({i:N0} of {i + nextLoopStart:N0})...");
                    await scraper.StartAsync();
                    i += nextLoopStart;
                }
                Console.WriteLine("Scrape complete.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //var json = await OnsApi.TestQuery();
            //File.WriteAllText("output.json", json);
        }

        private static ServiceProvider InitializeDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>()
                .AddScoped<IMemDbDataImportService, MemDbDataImportService>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
