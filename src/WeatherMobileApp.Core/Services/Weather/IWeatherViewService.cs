using System;
using System.Collections.Generic;
using System.Text;
using Model.DbItems;
using Model.FinallyItems;

namespace WeatherMobileApp.Core.Services.Weather
{
    public interface IWeatherViewService
    {
        WeatherItem GetCurrentWeather(List<WeatherItem> weathers);

        List<WeatherItem> Get24HWeathers(List<WeatherDbItem> weathers, List<CountryDbItem> countries);
    }
}
