using System.Collections.Generic;
using System.Threading.Tasks;

namespace WitnessReport.Services.Interfaces
{
    public interface IBaseHttpClient
    {
        Task<T> Get<T>(string url, IDictionary<string, string> headers = null);
    }
}
