using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using polecon.service.Models;

namespace polecon.service.ExcelImport
{
    public interface IExcelImportService
    {
        Task<List<Observation>> ImportData(string dataSetName,
            List<string> dataPointNames,
            List<string> dataPointUnits,
            List<int> years,
            List<List<decimal?>> data,
            bool commit = true);
    }

    public class ExcelImportService : IExcelImportService
    {
        public ExcelImportService(DataContext db)
        {
            Db = db;
        }

        private DataContext Db { get; }

        public async Task<List<Observation>> ImportData(string dataSetName,
            List<string> dataPointNames,
            List<string> dataPointUnits,
            List<int> years,
            List<List<decimal?>> data,
            bool commit = true
        ) {
            await using var tran = await Db.Database.BeginTransactionAsync();
            try
            {

                var dataSet = await ImportDataSet(dataSetName);
                var units = await ImportUnits(dataPointUnits, dataPointNames);
                var dataPoints = await ImportDataPoints(dataPointNames, units, dataSet);
                var observations = await ImportData(years, data, dataPoints);
                if (commit)
                {
                    await tran.CommitAsync();
                } else
                {
                    await tran.RollbackAsync();
                }
                return observations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await tran.RollbackAsync();
                throw;
            }

        }

        private async Task<DataSet> ImportDataSet(string dataSetName, string description = null)
        {
            var existingDataSet = await Db.DataSet.FirstOrDefaultAsync(ds => ds.Name == dataSetName);
            if (existingDataSet != null)
            {
                return existingDataSet;
            }

            var dataSet = new DataSet
            {
                Name = dataSetName,
                Description = description
            };
            Db.DataSet.Add(dataSet);
            await Db.SaveChangesAsync();
            return dataSet;
        }

        private async Task<List<Unit>> ImportUnits(List<string> unitNames, List<string> dataPoints)
        {
            var existingUnits = await Db.Unit.Where(u => unitNames.Contains(u.Name)).ToListAsync();
            //create separate list because we will join with the original list to preserve order
            var newUnitNames = unitNames.Except(existingUnits.Select(u => u.Name)).ToList();
            var newUnits = newUnitNames.Select((name, i) => new Unit
            {
                Name = name,
                Description = name == "Index" ? dataPoints[i] : null
            }).ToList();
            Db.Unit.AddRange(newUnits);
            await Db.SaveChangesAsync();
            return unitNames.Join(
                existingUnits.Concat(newUnits),
                name => name,
                u => u.Name,
                (name, u) => u
            ).ToList();
        }

        private async Task<List<DataPoint>> ImportDataPoints(List<string> columnNames, List<Unit> units, DataSet dataSet)
        {
            AssertMappingExists(units, columnNames);
            var existingDataPoints = await Db.DataPoint.Where(u => columnNames.Contains(u.Name)).ToListAsync();
            //create separate list because we will join with the original list to preserve order
            var newDataPointNames = columnNames.Except(existingDataPoints.Select(u => u.Name)).ToList();
            var newDataPoints = newDataPointNames.Select((name, i) => new DataPoint
            {
                Name = name,
                Unit = units.Count == 1 ? units[0] : units[i],
                DataSet = dataSet
            }).ToList();
            Db.DataPoint.AddRange(newDataPoints);
            await Db.SaveChangesAsync();
            return columnNames.Join(
                existingDataPoints.Concat(newDataPoints),
                name => name,
                u => u.Name,
                (name, u) => u
            ).ToList();
        }

        private async Task<List<Observation>> ImportData(
            List<int> years, 
            List<List<decimal?>> data,
            List<DataPoint> dataPoints
        ) {
            AssertMappingExists(years, data.First());
            AssertMappingExists(dataPoints, data);
            var colIndex = 0;
            var observations = new List<Observation>();
            foreach(var column in data)
            {
                var rowIndex = 0;
                var dataPoint = dataPoints[colIndex];
                foreach (var row in column)
                {
                    var date = new DateTime(years[rowIndex], 1, 1);
                    var observation = new Observation
                    {
                        DataPoint = dataPoint,
                        Date = date,
                        Value = row
                    };
                    Db.Observation.Add(observation);
                    observations.Add(observation);
                    rowIndex++;
                }

                colIndex++;
            }
            await Db.SaveChangesAsync();
            return observations;
        }

        private void AssertMappingExists<T1, T2>(List<T1> first, List<T2> second)
        {
            if (first.Count != 1 && first.Count != second.Count)
            {
                throw new ImportMismatchException(
                    "A mapping between the first and second collection cannot be automatically determined. " +
                    "For this import method, there needs to be either one item in the first collection or the sizes of the collections must match."
                );
            }
        }
    }

    internal class ImportMismatchException : Exception
    {
        public ImportMismatchException() { }
        public ImportMismatchException(string message) : base(message) { }
    }
}
