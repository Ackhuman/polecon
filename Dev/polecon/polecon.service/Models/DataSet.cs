using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class DataSet
    {
        public DataSet()
        {
            DataPoint = new HashSet<DataPoint>();
            DataSetSource = new HashSet<DataSetSource>();
        }

        public int DataSetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("DataSet")]
        public ICollection<DataPoint> DataPoint { get; set; }
        [InverseProperty("DataSet")]
        public ICollection<DataSetSource> DataSetSource { get; set; }
    }
}
