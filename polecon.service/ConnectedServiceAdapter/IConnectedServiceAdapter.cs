using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace polecon.service.ConnectedServiceAdapter
{
    public interface IConnectedServiceAdapter
    {
        string ServiceName { get; }
        Task<Dictionary<string, string>> DataSets();

        Task<string> GetData(string dataSetId, params object[] args);
    }
}
