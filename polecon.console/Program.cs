using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using polecon.console.ForeignApi;
using polecon.console.Scraper;
using polecon.service;
using polecon.service.ExcelImport;
using polecon.service.Service;

namespace polecon.console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = InitializeDependencyInjection();
            var filePath = "../../../../tmp/wages.xls";
            await ImportExcel(serviceProvider, filePath, true);
        }

        private static async Task ImportExcel(ServiceProvider serviceProvider, string fileName, bool testOnly)
        {
            var service = serviceProvider.GetService<IMunroDataImportService>();
            service.TestOnly = true;
            await service.ImportWageTables(fileName);
        }

        private static async Task Scrape(ServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService<IScraperService>();
            await service.RunScraper<MetzPriceScraper>();
        }


        private static ServiceProvider InitializeDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>()
                .AddScoped<IMemDbDataImportService, MemDbDataImportService>()
                .AddScoped<IScraperService, ScraperService>()
                .AddScoped<IExcelImportService, ExcelImportService>()
                .AddScoped<IMunroDataImportService, MunroDataImportService>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
