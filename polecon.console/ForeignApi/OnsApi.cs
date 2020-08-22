using ONS.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace polecon.console.ForeignApi
{
    public class OnsApi
    {
        public static async Task<string> TestQuery()
        {
            using var http = new HttpClient();
            var api = new swaggerClient(http);
            var dataSets = await api.SearchAsync("rent", null, null);
            return JsonConvert.SerializeObject(dataSets, Formatting.Indented);
        }
    }
}
