using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class Series
    {
        public Series()
        {
            ObservationSeries = new HashSet<ObservationSeries>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int? SortOrder { get; set; }

        [InverseProperty("Series")]
        public ICollection<ObservationSeries> ObservationSeries { get; set; }
    }
}
