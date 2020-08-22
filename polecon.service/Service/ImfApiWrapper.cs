using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ImfService;
using polecon.service.ConnectedServiceAdapter;
using polecon.service.Xml;

namespace polecon.service.Service
{
    public interface IIMfApi : IConnectedServiceAdapter, ASMXSDMXServiceSoap
    {

    }
    public class ImfApiWrapper : ASMXSDMXServiceSoapClient, IIMfApi
    {
        public ImfApiWrapper() : base(EndpointConfiguration.ASMXSDMXServiceSoap)
        {

        }

        public string ServiceName => "IMF API";

        public async Task<Dictionary<string, string>> DataSets()
        {
            var response = await GetDataflowAsync();
            var xml = response.Body.GetDataflowResult;
            return xml.SelectNodes("//Dataflow").AsQueryable()
                .Cast<XmlNode>()
                .Select(n => new KeyValuePair<string, string>(
                    n.SelectSingleNode("//KeyFamilyID").InnerText,
                    n.SelectSingleNode("//Name").InnerText
                    )
                ).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value
                );
        }

        public async Task<string> GetData(string dataSetId, params object[] args)
        {
            string query = $@"<QueryMessage xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message"">
              <Query>
                <KeyFamilyWhere xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/query"">
                  <KeyFamily>{dataSetId}</KeyFamily>
                </KeyFamilyWhere>
              </Query>
            </QueryMessage>";
            var queryXml = XmlHelper.FromString(query);
            var result = await GetDataStructureAsync(queryXml);
            return result.Body.GetDataStructureResult.ToString();
        }

    }
}
