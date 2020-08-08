using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class DataSetSource
    {
        public int DataSetId { get; set; }
        public int SourceId { get; set; }

        [ForeignKey("DataSetId")]
        [InverseProperty("DataSetSource")]
        public DataSet DataSet { get; set; }
        [ForeignKey("SourceId")]
        [InverseProperty("DataSetSource")]
        public Source Source { get; set; }
    }
}
