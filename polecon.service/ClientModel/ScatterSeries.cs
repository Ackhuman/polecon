using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace polecon.service.ClientModel
{
    [ClientModel]
    public class ScatterSeries<TKey, TValue>
    {
        public string Name { get; set; }
        public TKey Id { get; set; }
        public List<TValue> Data { get; set; }
    }

    [ClientModel]
    public class LineSeries
    {
        public string Name { get; set; }
        public List<decimal?> Data { get; set; }
    }
}
