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

            IRestResponse<TResult> response = await restClient.ExecuteTaskAsync<TResult>(request);

            return response.Data;
        }

        public async Task<TResult> MakeApiPostCall<TResult>(string url,
                                                            List<(string, string)> parametersList = null,
                                                            object data = null) where TResult : class
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };

            if (parametersList != null)
            {
                foreach ((string, string) parameter in parametersList)
                {
                    request.AddQueryParameter(parameter.Item1, parameter.Item2);
                }
            }

            request.AddHeader("Content-Type", Consts.ContentTypeJson);
            request.AddParameter(Consts.ContentTypeJson, JsonConvert.SerializeObject(data), ParameterType.RequestBody);

            IRestResponse<TResult> response = await client.ExecuteTaskAsync<TResult>(request);

            return response.Data;
        }
    }
}
