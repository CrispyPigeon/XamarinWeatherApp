using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ApiItems
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coordinate coord { get; set; }
        public string country { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
}
