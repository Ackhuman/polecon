using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using polecon.service.ClientModel;
using polecon.service.Models;

namespace polecon.service.Service
{
    public interface IChartService
    {
        Task<List<Series>> GetSeries(int? id = null);
        Task<List<DataSet>> GetDataSets();
        Task<List<DataPoint>> GetDataPoint(int? id = null);
        Task<List<ScatterSeries<int, decimal?>>> GetDataSingle(ChartDataRequest request);
        List<ScatterSeries<int[], decimal?[]>> GetDataPaired(int[][] dataPointIds);
        List<LineSeries> GetLineSeries(ChartDataRequest request);
        Task<List<string>> GetDateCategories(ChartDataRequest request);
    }

    public class ChartService : IChartService
    {
        private DataContext Db { get; }
        public ChartService(DataContext db)
        {
            Db = db;
        }

        public Task<List<DataSet>> GetDataSets() => Db.DataSet.ToListAsync();

        public Task<List<DataPoint>> GetDataPoint(int? dataSetId = null)
        {
            var query = Db.DataPoint
                .Include(dp => dp.Unit)
                .AsQueryable();
            if (dataSetId.HasValue)
            {
                query = query.Where(d => d.DataSetId == dataSetId);
            }
            return query.ToListAsync();
        }

        public async Task<List<ScatterSeries<int, decimal?>>> GetDataSingle(ChartDataRequest request)
        {
            try
            {
                return (await DataQuery(request)
                    .ToListAsync())
                    .GroupBy(o => o.DataPoint, o => o.Value)
                    .Select(o => new ScatterSeries<int, decimal?> {
                        Id = o.Key.Id,
                        Name = o.Key.Name,
                        Data = o.ToList()
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ScatterSeries<int[], decimal?[]>> GetDataPaired(int[][] dataPointIds) =>
            dataPointIds.Select(pair =>
                new ScatterSeries<int[], decimal?[]>
                {
                    Id = pair,
                    Data = Db.Observation.Include(o => o.DataPoint)
                        .Where(o => o.DataPointId == pair[0])
                        .Select(o => o.Value)
                        .Zip(
                            Db.Observation
                                .Where(o => o.DataPointId == pair[1])
                                .Select(o => o.Value),
                            (item1, item2) => new[] {item1, item2}
                        ).ToList()
                }).ToList();

        public Task<List<Series>> GetSeries(int? id = null)
        {
            var query = Db.Series.AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(s => s.Id == id.Value);
            }
            return query.ToListAsync();
        }

        public Task<List<string>> GetDateCategories(ChartDataRequest request)
        {
            var observationQuery = DataQuery(request);
            return observationQuery
                .Select(o => o.Date.Value.Year)
                .Distinct()
                .OrderBy(o => o)
                .Select(o => o.ToString())
                .ToListAsync();
        }

        public List<LineSeries> GetLineSeries(ChartDataRequest request)
        {
            return DataQuery(request)
                .OrderBy(o => o.Date.Value)
                .AsEnumerable()
                .GroupBy(
                    o => o.DataPoint
                ).Select(o => new LineSeries
                {
                    Name = $"{o.Key.Name}, {o.Key.Unit.Name} (D:{o.Key.Id})",
                    Data = AggregateObservations(request, o)
                }).ToList();
        }

        private List<decimal?> AggregateObservations(ChartDataRequest request, IGrouping<DataPoint, Observation> observations)
        {
            //todo: support more than just annual observations
            var annualObservations = observations
                .GroupBy(o => o.Date.Value.Year)
                .Select(o => o.Average(_o => _o.Value));
            if (request.MovingAveragePeriod.HasValue)
            {
                annualObservations = MovingAverage(annualObservations.ToList(), request.MovingAveragePeriod.Value);
            }

            return annualObservations.ToList();
        }

        private List<decimal?> MovingAverage(List<decimal?> values, int period)
        {
            var window = new Queue<decimal?>(period);
            var results = new List<decimal?>();
            foreach (var value in values)
            {
                if (window.Count == period)
                {
                    var average = window.Average();
                    results.Add(average);
                    window.Dequeue();
                }
                window.Enqueue(value);
            }
            return results;
        }

        private IQueryable<Observation> DataQuery(ChartDataRequest request)
        {
            var observationQuery = Db.Observation
                .Include(o => o.DataPoint)
                .ThenInclude(d => d.Unit)
                .Where(o =>
                    request.DataPointIds.Contains(o.DataPointId)
                );
            if (request.YearMin.HasValue)
            {
                observationQuery = observationQuery.Where(o => o.Date.Value.Year > request.YearMin);
            }
            if (request.YearMax.HasValue)
            {
                observationQuery = observationQuery.Where(o => o.Date.Value.Year < request.YearMax);
            }
            if (request.IncludeNulls != true)
            {
                observationQuery = observationQuery.Where(o => o.Value.HasValue);
            }
            return observationQuery;
        }
    }
}
