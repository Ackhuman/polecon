using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class DataPoint
    {
        public DataPoint()
        {
            Observation = new HashSet<Observation>();
            ObservationSeries = new HashSet<ObservationSeries>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UnitId { get; set; }
        public int? DataSetId { get; set; }

        [ForeignKey("DataSetId")]
        [InverseProperty("DataPoint")]
        public DataSet DataSet { get; set; }
        [ForeignKey("UnitId")]
        [InverseProperty("DataPoint")]
        public Unit Unit { get; set; }
        [InverseProperty("DataPoint")]
        public ICollection<Observation> Observation { get; set; }
        [InverseProperty("DataPoint")]
        public ICollection<ObservationSeries> ObservationSeries { get; set; }
    }
}
