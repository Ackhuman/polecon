using System;

namespace polecon.service.Models
{
    public class MemDbEntry
    {
        public int Num { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateTime Date => new DateTime(Year, Month, Day);
        public string ProductName { get; set; }
        public int? NumSold { get; set; }
        public string VolumeUnit { get; set;}
        public string CurrencyName { get; set; }
        public decimal? Price { get; set; }
    }
}