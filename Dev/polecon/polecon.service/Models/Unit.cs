using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polecon.service.Models
{
    public partial class Unit
    {
        public Unit()
        {
            DataPoint = new HashSet<DataPoint>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(10)]
        public string Sigil { get; set; }
        public bool IsPrefix { get; set; }
        public int DefaultMagnitude { get; set; }
        public string FormulaJson { get; set; }

        [InverseProperty("Unit")]
        public ICollection<DataPoint> DataPoint { get; set; }
    }
}
