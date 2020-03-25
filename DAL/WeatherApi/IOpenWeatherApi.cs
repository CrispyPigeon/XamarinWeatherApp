using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model.ApiItems;

namespace DAL.WeatherApi
{
    public interface IOpenWeatherApi
    {
        Task<WeathersResponse> GetWeathersAsync(string lang, double lat, double lon);
    }
}
