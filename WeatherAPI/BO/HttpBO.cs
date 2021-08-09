using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.BO
{
    public class HttpBO
    {
        public int templow { get; set; }
        public int temphigh { get; set; }
        public String wind { get; set; }
        public String dayweather { get; set; }
        public String nightweather { get; set; }

        public static String FormatBO(HttpBO bo)
        {
            String res = "天气预警\n";
            res += "当天气温为：" + bo.templow + "-" + bo.temphigh + "℃\n";
            res += "风力等级为：" + bo.wind + "级\n";
            res += "白天天气为：" + bo.dayweather + "\n";
            res += "夜晚天气为：" + bo.nightweather + "\n";
            return res;
        }
    }
}
