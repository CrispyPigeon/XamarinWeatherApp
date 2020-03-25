using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ApiItems
{
    public class StateOfWeather
    {
        public int dt { get; set; }
        public MainData main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public string dt_txt { get; set; }
    }
}
