using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.BO
{
    public class WeatherBO
    {
        public String city { get; set; }
        public int temp { get; set; }
        public int temphigh { get; set; }
        public int templow { get; set; }
        public String weather { get; set; }
        public int curtemp { get; set; }

        public static WeatherBO ToWeatherBO(Weather weather)
        {
            WeatherBO bo = new WeatherBO();
            bo.city = weather.city;
            bo.temp = weather.temp;
            bo.temphigh = weather.temphigh;
            bo.templow = weather.templow;
            bo.weather = weather.weather;
            int time = DateTime.Now.Hour;
            bo.curtemp = weather.hourly.Where(h => h.time == time).ToList()[0].temp;
            return bo;
        }

        public static String FormatBO(WeatherBO bo)
        {
            string res = bo.city + "天气\n";
            res += "今日天气:" + bo.weather + "\n";
            res += "今日气温：" + bo.templow + "-" + bo.temphigh + "\n";
            res += "平均气温" + bo.temp + "\n";
            res += "此时气温" + bo.temp + "\n";
            return res;
        }
    }
}
