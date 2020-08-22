using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using polecon.service.Models;

namespace polecon.service.Service
{
    public interface IMemDbDataImportService
    {
        Task ImportMetzData(List<MemDbEntry> data);
        Task ImportPosthumusData(List<MemDbEntry> data);
    }

    public class MemDbDataImportService : IMemDbDataImportService
    {
        public DataContext Db { get;}
        public MemDbDataImportService(DataContext db)
        {
            Db = db;
        }
        
        public async Task ImportMetzData(List<MemDbEntry> data)
        {
            await using var tran = Db.Database.BeginTransaction();
            try
            {
                var dataSet = await Db.DataSet.FirstOrDefaultAsync(d => d.Name == MetzPriceDataSetName)
                              ?? await CreateMetzDataSet();
                var dataPoints = InitReferences(dataSet.DataSetId);
                var units = dataPoints.Values
                    .Select(dp => dp.Unit)
                    .Distinct()
                    .ToDictionary(u => u.Name, u => u);
                await ImportMetzVolumeUnit(units);
                foreach (var entry in data)
                {
                    units = await ImportMoneyUnit(entry, units, "Money of Cologne");
                    units = await ImportVolumeUnit(units, entry.VolumeUnit, $"Unit of volume in Cologne in {entry.Year}");
                    dataPoints = await ImportMetzRowDataPoints(entry, dataSet, dataPoints, units);
                    await ImportRowObservations(entry, dataPoints);
                }
                await Db.SaveChangesAsync();
                await tran.CommitAsync();
            } 
            catch(Exception ex)
            {
                await tran.RollbackAsync();
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task ImportPosthumusData(List<MemDbEntry> data)
        {
            await using var tran = Db.Database.BeginTransaction();
            try
            {
                var dataSet = await Db.DataSet.FirstOrDefaultAsync(d => d.Name == PosthumusPriceDataSetName)
                              ?? await CreatePosthumusDataSet();
                var dataPoints = InitReferences(dataSet.DataSetId);
                var units = dataPoints.Values
                    .Select(dp => dp.Unit)
                    .Distinct()
                    .ToDictionary(u => u.Name, u => u);
                foreach (var entry in data)
                {
                    units = await ImportMoneyUnit(entry, units, "Money of Amsterdam");
                    units = await ImportVolumeUnit(units, entry.VolumeUnit, $"Unit of volume in Amsterdam in {entry.Year}");
                    dataPoints = await ImportPosthumusRowDataPoints(entry, dataSet, dataPoints, units);
                    await ImportRowObservations(entry, dataPoints);
                }
                await Db.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async Task ImportRowObservations(MemDbEntry entry, Dictionary<string, DataPoint> dataPoints)
        {
            await ImportObservation(entry, dataPoints, GetPriceDataPointName, e => e.Price);
            await ImportObservation(entry, dataPoints, GetVolumeDataPointName, e => e.NumSold);
        }

        private async Task<Dictionary<string, DataPoint>> ImportMetzRowDataPoints(
            MemDbEntry entry,
            DataSet dataSet,
            Dictionary<string, DataPoint> dataPoints,
            Dictionary<string, Unit> units
        ) {
            dataPoints = await ImportDataPoint(
                entry,
                dataPoints,
                dataSet,
                units[entry.CurrencyName],
                GetPriceDataPointName,
                GetMetzPriceDataPointDescription
            );
            dataPoints = await ImportDataPoint(
                entry,
                dataPoints,
                dataSet,
                units[MaltersUnitName],
                GetMaltersDataPointName,
                GetMaltersDataPointDescription
            );
            return dataPoints;
        }
        private async Task<Dictionary<string, DataPoint>> ImportPosthumusRowDataPoints(
            MemDbEntry entry,
            DataSet dataSet,
            Dictionary<string, DataPoint> dataPoints,
            Dictionary<string, Unit> units
        )
        {
            dataPoints = await ImportDataPoint(
                entry,
                dataPoints,
                dataSet,
                units[entry.CurrencyName],
                GetPriceDataPointName,
                GetPosthumusDataPointDescription
            );
            dataPoints = await ImportDataPoint(
                entry,
                dataPoints,
                dataSet,
                units[entry.VolumeUnit],
                GetVolumeDataPointName,
                GetPosthumusVolumeDataPointDescription
            );
            return dataPoints;
        }

        private Dictionary<string, DataPoint> InitReferences(int dataSetId)
        {
            var dataPoints = Db.DataPoint
                .Include(dp => dp.Unit)
                .Where(dp => dp.DataSetId == dataSetId)
                .AsEnumerable();
            
            return dataPoints.ToDictionary(dp => dp.Name, dp => dp);
        }

        private async Task<Observation> ImportObservation(
            MemDbEntry entry, 
            Dictionary<string, DataPoint> dataPoints,
            Func<MemDbEntry, string> unitSelector,
            Func<MemDbEntry, decimal?> valueSelector
        ) {
            var observation = new Observation
            {
                DataPoint = dataPoints[unitSelector(entry)],
                Date = entry.Date,
                Value = valueSelector(entry)
            };
            Db.Observation.Add(observation);
            return observation;
        }

        private async Task<Dictionary<string, DataPoint>> ImportDataPoint(
            MemDbEntry product,
            Dictionary<string, DataPoint> previousDataPoints,
            DataSet dataSet,
            Unit unit,
            Func<MemDbEntry, string> getDataPointName,
            Func<MemDbEntry, string> getDataPointDescription
        )
        {
            var dataPoint = new DataPoint
            {
                Name = getDataPointName(product),
                Description = getDataPointDescription(product),
                DataSet = dataSet,
                Unit = unit
            };
            if (!previousDataPoints.ContainsKey(dataPoint.Name))
            {
                previousDataPoints.Add(dataPoint.Name, dataPoint);
                Db.DataPoint.Add(dataPoint);
                await Db.SaveChangesAsync();
            }
            return previousDataPoints;
        }


        private async Task<Dictionary<string, Unit>> ImportMoneyUnit(MemDbEntry entry, Dictionary<string, Unit> previousUnits, string unitDescription)
        {
            var money = new Unit
            {
                Name = entry.CurrencyName,
                Description = unitDescription
            };
            if(!previousUnits.ContainsKey(money.Name))
            {
                previousUnits.Add(money.Name, money);
                Db.Unit.Add(money);
                await Db.SaveChangesAsync();
            }
            return previousUnits;
        }

        private async Task<Dictionary<string, Unit>> ImportMetzVolumeUnit(Dictionary<string, Unit> previousUnits) =>
            await ImportVolumeUnit(
                previousUnits,
                MaltersUnitName,
                "Medieval unit of volume that varies between 100-200 liters"
            );

        private async Task<Dictionary<string, Unit>> ImportVolumeUnit(
            Dictionary<string, Unit> previousUnits,
            string name,
            string description = ""
        ) {
            var volume = new Unit
            {
                Name = name,
                Description = "Medieval unit of volume that varies between 100-200 liters"
            };
            if (!previousUnits.ContainsKey(volume.Name))
            {
                previousUnits.Add(volume.Name, volume);
                Db.Unit.Add(volume);
                await Db.SaveChangesAsync();
            }

            return previousUnits;
        }

        private async Task<DataSet> CreateMetzDataSet()
        {
            var dataSet = new DataSet
            {
                Name = MetzPriceDataSetName,
                Description = @"For all quotations of grain prices and quantities, as well as bread prices and weights, see Dietrich Ebeling and Franz Irsigler, Getreideumsatz, Getreide- und Brotpreise in Köln, 1368-1797 (Köln, 1976), which provides a detailed explanation of the source material.

All data on price and quantity are drawn from sales of the weekly market in Cologne, which served as a trans-shipment hub for grain. Prices are weekly averages and quantities refer to volume sold during an entire week. The weeks in each month are numbered 1, 2, 3, 4, and 5. These data reflect, insofar as officials took care in recording them, the activities of a ""free"" market. The data from the seventeenth century should be used with special caution, because transactions from these years were often negligently recorded.

All prices are quoted in monies of account of Cologne, either in Gulden or Mark(1 Gulden = 4 Mark = 24 Albus = 288 Heller).For information about the bullion equivalents of these monies of account or their exchange rates, see the data set named Currency Exchanges(Metz)."
            };
            Db.DataSet.Add(dataSet);
            await Db.SaveChangesAsync();
            return dataSet;
        }

        private async Task<DataSet> CreatePosthumusDataSet()
        {
            var dataSet = new DataSet
            {
                Name = PosthumusPriceDataSetName,
                Description =
                    @"This data base contains prices taken from the tables of absolute prices contained in volume 1 of N. W. Posthumus's Nederlandsche Prijsgeschiedenis (Leiden, 1943). For a detailed explanation of the sources of this data, see his Introduction, pp. XVIII-LXXIX. Posthumus took most of the quotations from published listings of the Amsterdam commodity exchange. The listings appeared irregularly from 1585 to 1609, weekly from 1609 to 1796, and twice a week from 1796 to 1813. Of the approximately 12,000 price currents that were issued between 1585 and 1813, Posthumus was able to find 3,033, or about one-fourth. There are, moreover, large gaps among these. Only 138 years are represented, so that numerous years are lacking, particularly before 1624, and between the years 1654-64, 1694-1701,1710-18, and 1722-28.

Posthumus also supplemented the information drawn from the price currents with data contained in other, more specialized price lists, from various printed periodicals and newspapers, from miscellaneous documents published by other historians, and from archival sources. All quotations before 1585 come from such sources. If Posthumus gave the specific source of the price, the information has been included in the on-screen note of MEMDB (""BLARO"" refers to Brokers' Lists from the Amsterdam Record Office; for other abbreviations, see Posthumus).

Posthumus listed only monthly quotations, chosen from the list that appeared on the 15th day of each month, or as close as possible to this date. The quotations are given with the relevant weight or measure, and Posthumus indicated when the unit of measure changed, in some cases switching to a new series when one unit of measure replaced another. The prices were actually quoted in guilders, stuivers, and penningen(1 gulden = 20 stuivers = 320 penningen); or sometimes in Flemish pounds, shillings and pennies (1 pond = 20 schellingen = 240 groten); or, for grain prices, in ""goudguldens"" of 28 stuivers. Quotations in bank money ('banco'; currency of the Amsterdam Wisselbank) begin as early as 1634 and enter general use in 1683."
            };
            Db.DataSet.Add(dataSet);
            await Db.SaveChangesAsync();
            return dataSet;
        }
        private static string GetMaltersDataPointDescription(MemDbEntry product) =>
            $"Number of Malters sold of {product.ProductName} in Cologne";

        private static string GetMetzPriceDataPointDescription(MemDbEntry product) =>
            $"Weekly market price of {product.ProductName} in Cologne";

        private static string GetPosthumusDataPointDescription(MemDbEntry product) =>
            $"Monthly commodity price of {product.ProductName} in Amsterdam";

        private static string GetPosthumusVolumeDataPointDescription(MemDbEntry product) =>
            $"Monthly {product.VolumeUnit} sold of {product.ProductName} in Amsterdam";

        private static string GetPriceDataPointName(MemDbEntry product) => $"{product.ProductName}, Price";
        private static string GetMaltersDataPointName(MemDbEntry product) => $"{product.ProductName}, Malters Sold";
        private static string GetVolumeDataPointName(MemDbEntry product) => $"{product.ProductName}, {product.VolumeUnit} Sold";

        private const string MetzPriceDataSetName = "MEMDB: Metz price data";
        private const string PosthumusPriceDataSetName = "MEMDB: Posthumus price data";
        private const string MaltersUnitName = "Malters";
    }
}
