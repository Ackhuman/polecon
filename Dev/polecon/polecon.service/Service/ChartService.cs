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
        Task<List<DataPoint>> GetDataPoint(int? id = null);
        Task<List<ChartSeries>> GetData(int[] dataPointId);
    }

    public class ChartService : IChartService
    {
        private DataContext Db { get; }
        public ChartService(DataContext db)
        {
            Db = db;
        }

        public Task<List<DataPoint>> GetDataPoint(int? id = null)
        {
            var query = Db.DataPoint
                .Include(dp => dp.Unit)
                .AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(d => d.Id == id.Value);
            }
            return query.ToListAsync();
        }

        public Task<List<ChartSeries>> GetData(int[] dataPointIds) =>
            Db.DataPoint
                .Where(dp => dataPointIds.Contains(dp.Id))
                .GroupJoin(
                    Db.Observation
                        .Where(o => dataPointIds.Contains(o.DataPointId)),
                    dp => dp.Id,
                    d => d.DataPointId,
                    (dp, d) => new ChartSeries
                    {
                        DataPointId = dp.Id,
                        Name = dp.Name,
                        Data = d.Select(_d => _d.Value).ToList()
                    }
                ).ToListAsync();

        public Task<List<Series>> GetSeries(int? id = null)
        {
            var query = Db.Series.AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(s => s.Id == id.Value);
            }
            return query.ToListAsync();
        }
    }
}
