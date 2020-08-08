using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class Source
    {
        public Source()
        {
            DataSetSource = new HashSet<DataSetSource>();
        }

        public int SourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool? IsPrimary { get; set; }
        public DateTime? Date { get; set; }
        [StringLength(200)]
        public string Href { get; set; }

        [InverseProperty("Source")]
        public ICollection<DataSetSource> DataSetSource { get; set; }
    }
}
