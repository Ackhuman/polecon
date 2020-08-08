using System.Collections.Generic;
using System.Threading.Tasks;

namespace polecon.service.ClientModel
{
    public class ChartSeries
    {
        public string Name { get; set; }
        public int DataPointId { get; set; }
        public List<decimal?> Data { get; set; }
    }
}
