using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace DAL.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        private RestClient restClient;

        public RequestProvider()
        {
            restClient = new RestClient(Consts.BaseUrl);
        }

        public async Task<TResult> MakeApiGetCall<TResult>(string url,
                                                           List<(string, string)> parametersList = null)
                                                           where TResult : class
        {
            var request = new RestRequest(url, Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = Consts.ContentTypeJson; }
            };

            if (parametersList != null)
            {
                foreach ((string, string) parameter in parametersList)
                {
                    request.AddParameter(parameter.Item1, parameter.Item2);
                }
            }

            IRestResponse<TResult> response = await restClient.ExecuteAsync<TResult>(request);

            return response.Data;
        }
    }
}
