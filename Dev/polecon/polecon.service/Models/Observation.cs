using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class Observation
    {
        public int Id { get; set; }
        [Column(TypeName = "numeric(16, 8)")]
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }
        public int? SortOrder { get; set; }
        public int DataPointId { get; set; }

        [ForeignKey("DataPointId")]
        [InverseProperty("Observation")]
        public DataPoint DataPoint { get; set; }
    }
}
