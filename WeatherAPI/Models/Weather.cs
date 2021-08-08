using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class Weather
    {
        public String citycode { get; set; }
        public String city { get; set; }
        public int temp { get; set; }
        public int temphigh { get; set; }
        public int templow { get; set; }
        public String weather { get; set; }
        public List<WeatherHours> hourly { get; set; }

        public class WeatherHours
        {
            public int time { get; set; }
            public String weather{ get; set; }
            public int temp { get; set; }
        }

        public Weather()
        {
            hourly = new List<WeatherHours>();
        }

    }
}
