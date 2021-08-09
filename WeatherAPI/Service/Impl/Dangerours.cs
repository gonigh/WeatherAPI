using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.BO;
using WeatherAPI.Service.Interface;

namespace WeatherAPI.Service.Impl
{
    public class Dangerours : IDangerours
    {
        public class Limit
        {
            public static int templow = 5;
            public static int temphigh = 15;
            public static String[] weather = { "暴雨" };

            
        }
        public bool IsDangerours(HttpBO bo)
        {
            if (bo.templow < Limit.templow || bo.temphigh > Limit.temphigh ||
                Limit.weather.Contains<String>(bo.dayweather) || Limit.weather.Contains<String>(bo.nightweather))
                return true;
            return false;
        }
    }
}
