using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ApiItems
{
    public class MainData
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public int temp_kf { get; set; }
    }
}
