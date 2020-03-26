using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.DbItems;
using Model.FinallyItems;

namespace WeatherMobileApp.Core.Services.Converters
{
    public static class DbItemsToFinallyItemsConverters
    {
        public static List<WeatherItem> WeatherResponseToWeatherDbItems(List<WeatherDbItem> weatherDbItems, List<CountryDbItem> countries)
        {
            return weatherDbItems.Select(x => new WeatherItem(x, countries.FirstOrDefault(y => y.Id == x.CountryId))).ToList();
        }

        public static List<WeatherItem> WeatherResponseToWeatherDbItems(List<WeatherDbItem> weatherDbItems, CountryDbItem country)
        {
            return weatherDbItems.Select(x => new WeatherItem(x, country)).ToList();
        }
    }
}
