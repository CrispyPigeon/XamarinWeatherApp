using System;
using System.Collections.Generic;
using System.Text;
using Model.DbItems;

namespace Model.FinallyItems
{
    public class WeatherItem
    {
        public WeatherDbItem Weather { get; set; }
        public CountryDbItem Country { get; set; }

        public WeatherItem(WeatherDbItem weather, CountryDbItem country)
        {
            Weather = weather;
            Country = country;
        }
    }
}
