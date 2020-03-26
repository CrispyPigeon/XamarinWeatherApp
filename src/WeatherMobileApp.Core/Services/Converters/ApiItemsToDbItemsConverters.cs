using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ApiItems;
using Model.DbItems;

namespace WeatherMobileApp.Core.Services.Converters
{
    public static class ApiItemsToDbItemsConverters
    {
        public static CountryDbItem WeatherResponseToCountryDbItem(WeathersResponse weathersResponse)
        {
            return new CountryDbItem(weathersResponse.City);
        }

        public static List<WeatherDbItem> WeatherResponseToWeatherDbItems(WeathersResponse weathersResponse, int countryId)
        {
            return weathersResponse.list.Select(x => new WeatherDbItem(x, countryId)).ToList();
        }
    }
}
