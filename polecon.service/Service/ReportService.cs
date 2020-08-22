using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace polecon.service.Service
{
    public interface IReportService
    {
        decimal Covariance(int dataPointIdX, int dataPointIdY);
    }

    public class ReportService : IReportService
    {
        private DataContext Db { get; }
        public ReportService(DataContext db)
        {
            Db = db;
        }

        public decimal Covariance(int dataPointIdX, int dataPointIdY)
        {
            var data = GetCorrelationData(dataPointIdX, dataPointIdY);
            var n = data.Count;
            var sumX = data.Sum(d => d.DataPointValueX);
            var sumY = data.Sum(d => d.DataPointValueY);
            var sumXY = data.Sum(d => d.DataPointValueX * d.DataPointValueY);
            var covariance = (sumXY - sumX * sumY / n) / n;
            return covariance;
        }

        public List<CorrelationDatum> GetCorrelationData(int dataPointIdX, int dataPointIdY)
        {
            var data = Db.Observation
                .Where(o =>
                    o.DataPointId == dataPointIdX
                    || o.DataPointId == dataPointIdY
                ).AsEnumerable()
                .GroupBy(o => o.Date,
                    (date, o) => new CorrelationDatum
                    {
                        Year = date.Value.Year,
                        DataPointIdX = o.First(_o => _o.DataPointId == dataPointIdX).DataPointId,
                        DataPointValueX = o.First(_o => _o.DataPointId == dataPointIdX).Value.Value,
                        DataPointIdY = o.First(_o => _o.DataPointId == dataPointIdY).DataPointId,
                        DataPointValueY = o.First(_o => _o.DataPointId == dataPointIdY).Value.Value
                    }
                ).ToList();
            return data;
        }
    }

    public class CorrelationDatum
    {
        public int DataPointIdX { get; set; }
        public decimal DataPointValueX { get; set; }
        public int DataPointIdY { get; set; }
        public decimal DataPointValueY { get; set; }
        public int Year { get; set; }
        public int SortOrder { get; set; }
    }
}
