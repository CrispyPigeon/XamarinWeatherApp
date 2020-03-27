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

        public List<WeatherItem> Get24HWeathers(List<WeatherDbItem> weathers, List<CountryDbItem> countries)
        {
            var increasedDateTime = DateTime.Now.AddDays(1);
            var todayWeathers = weathers.Where(x => x.DateTime < increasedDateTime && x.DateTime > DateTime.Now).ToList();
            return DbItemsToFinallyItemsConverters.WeatherResponseToWeatherDbItems(todayWeathers, countries);
        }
    }
}
