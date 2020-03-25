using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Services.RequestProvider;
using Model.ApiItems;

namespace DAL.WeatherApi
{
    public class OpenWeatherApi : IOpenWeatherApi
    {
        private readonly IRequestProvider _requestProvider;

        public OpenWeatherApi(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<WeathersResponse> GetWeathersAsync(string lang, double lat, double lon)
        {
            var request = Consts.Forecast;

            var parameters = new List<(string, string)>
            {
                (Consts.AppId, Consts.ApiKey),
                (Consts.Latitude, lat.ToString()),
                (Consts.Longitude, lon.ToString()),
                (Consts.Units, Consts.UnitCel),
                (Consts.Language, lang)
            };

            return await _requestProvider.MakeApiGetCall<WeathersResponse>(request, parameters)
                    ?? throw new Exception();            
        }
    }
}
