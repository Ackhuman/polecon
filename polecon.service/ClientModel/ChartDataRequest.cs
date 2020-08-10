using System.Collections.Generic;

namespace polecon.service.ClientModel
{
    [ClientModel]
    public class ChartDataRequest
    {
        public ChartDataRequest()
        {
            DataPointIds = new List<int>();
        }

        public List<int> DataPointIds { get; set; }
        public bool? IncludeNulls { get; set; } = true;
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public int? MovingAveragePeriod { get; set; }
    }
}
