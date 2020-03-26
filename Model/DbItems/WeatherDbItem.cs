using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ApiItems;
using Model.DbItems.Base;
using SQLite;

namespace Model.DbItems
{
    public class WeatherDbItem : IDbModel<int>
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        public string Condition { get; set; }
        public double Temperature { get; set; }
        public int CountryId { get; set; }

        public WeatherDbItem() { }

        public WeatherDbItem(StateOfWeather stateOfWeather, int countryId)
        {
            DateTime = Convert.ToDateTime(stateOfWeather.dt_txt);
            Condition = stateOfWeather.weather?.First().description;
            Temperature = stateOfWeather.main.temp;
            CountryId = countryId;
        }
    }
}
