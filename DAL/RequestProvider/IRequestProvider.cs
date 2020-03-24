using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;

namespace DAL.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> MakeApiGetCall<TResult>(string url,
                                              List<(string,string)> parametersList = null)
                                              where TResult : class;

        Task<TResult> MakeApiPostCall<TResult>(string url,
                                               List<(string, string)> parametersList = null,
                                               object data = null)
                                               where TResult : class;
    }
}
