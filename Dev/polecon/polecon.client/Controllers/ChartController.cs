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

        [HttpGet, Route("[action]")]
        public Task<List<ChartSeries>> GetData(params int[] dataPointIds) =>
            Service.GetData(dataPointIds);
        
    }
}
