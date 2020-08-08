using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class ObservationSeries
    {
        public int SeriesId { get; set; }
        public int DataPointId { get; set; }

        [ForeignKey("DataPointId")]
        [InverseProperty("ObservationSeries")]
        public DataPoint DataPoint { get; set; }
        [ForeignKey("SeriesId")]
        [InverseProperty("ObservationSeries")]
        public Series Series { get; set; }
    }
}
