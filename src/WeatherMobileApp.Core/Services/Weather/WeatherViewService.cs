using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.DbItems;
using Model.FinallyItems;
using WeatherMobileApp.Core.Services.Converters;

namespace WeatherMobileApp.Core.Services.Weather
{
    public class WeatherViewService : IWeatherViewService
    {
        public WeatherItem GetCurrentWeather(List<WeatherItem> weathers)
        {
            var specifiedDate = DateTime.Now;

            var minDistance = weathers.Min(x => (x.Weather.DateTime - specifiedDate).Duration());
            return weathers.First(x => (x.Weather.DateTime - specifiedDate).Duration() == minDistance);
        }

        public List<WeatherItem> GetTodayWeathers(List<WeatherDbItem> weathers, List<CountryDbItem> countries)
        {
            var todayWeathers = weathers.Where(x => x.DateTime.Date == DateTime.Today.Date).ToList();
            return DbItemsToFinallyItemsConverters.WeatherResponseToWeatherDbItems(todayWeathers, countries);
        }

        public List<WeatherItem> GetTodayWeathers(List<WeatherDbItem> weathers, CountryDbItem country)
        {
            var todayWeathers = weathers.Where(x => x.DateTime.Date == DateTime.Today.Date).ToList();
            return DbItemsToFinallyItemsConverters.WeatherResponseToWeatherDbItems(weathers, country);
        }
    }
}
