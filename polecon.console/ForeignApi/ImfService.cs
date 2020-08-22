using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ImfApi;

namespace polecon.console.ForeignApi
{
    public class ImfService
    {
        public async Task<string> TestQuery()
        {
            var client = new ASMXSDMXServiceSoapClient(ASMXSDMXServiceSoapClient.EndpointConfiguration.ASMXSDMXServiceSoap12);
            var response = await client.GetDataflowAsync();
            return response.Body.GetDataflowResult.OuterXml;
        }
    }
}
