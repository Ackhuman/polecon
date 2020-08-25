using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace polecon.console.Scraper
{
    public interface IBatchScraper
    {
        int ExpectedNumResults { get; }
    }
}
