using System;
using System.Collections.Generic;
using System.Text;
using Model.ApiItems;
using Model.DbItems.Base;
using SQLite;

namespace Model.DbItems
{
    public class CountryDbItem : IDbModel<int>
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Index { get; set; }
        public double Lontitude { get; set; }
        public double Latitude { get; set; }

        public CountryDbItem() { }

        public CountryDbItem(City city)
        {
            Id = city.id;
            Name = city.name;
            Index = city.country;
            Lontitude = city.coord.lon;
            Latitude = city.coord.lat;
        }
    }
}
