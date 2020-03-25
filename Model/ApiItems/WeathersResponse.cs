using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ApiItems
{
    public class WeathersResponse
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<StateOfWeather> list { get; set; }
        public City City { get; set; }
    }
}
