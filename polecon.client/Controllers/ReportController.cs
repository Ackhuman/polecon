using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using polecon.service.Service;

namespace polecon.client.Controllers
{
    public class ReportController : Controller
    {
        private IReportService Service { get; }

        public ReportController(IReportService service)
        {
            Service = service;
        }

        [HttpGet, Route("[action]")]
        public IActionResult DebasementReport()
        {
            return null;
        }
    }
}
