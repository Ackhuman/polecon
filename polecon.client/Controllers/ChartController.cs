using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using polecon.service.ClientModel;
using polecon.service.Models;
using polecon.service.Service;

namespace polecon.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IChartService Service { get; }
        public ChartController(IChartService service)
        {
            Service = service;
        }

        [HttpGet, Route("[action]")]
        public async Task<List<Series>> GetSeries(int? id = null) => 
           await Service.GetSeries(id);

        [HttpGet, Route("[action]")]
        public async Task<List<DataPoint>> GetDataPoint(int? id = null) =>
            await Service.GetDataPoint(id);

        [HttpPost, Route("[action]")]
        public async Task<List<ScatterSeries<int, decimal?>>> GetData(ChartDataRequest request) =>
            await Service.GetDataSingle(request);

        [HttpPost, Route("[action]")]
        public List<LineSeries> GetLineSeries(ChartDataRequest request) =>
            Service.GetLineSeries(request);

        [HttpPost, Route("[action]")]
        public Task<List<string>> GetDateCategories(ChartDataRequest request) =>
            Service.GetDateCategories(request);
    }
}
