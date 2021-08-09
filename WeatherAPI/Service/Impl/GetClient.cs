using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAPI.BO;
using WeatherAPI.Service.Interface;

namespace WeatherAPI.Service.Impl
{
    public class GetClient : IGetClient
    {
        

        public HttpBO GetWeather(String citycode)
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Host",)
            string rspText = client.GetStringAsync("http://www.weather.com.cn/weather1d/" + citycode + ".shtml").Result;
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(rspText);
            var tem = doc.QuerySelectorAll(".tem span");
         
            var wea = doc.QuerySelectorAll(".clearfix p");
            List<String> weas = new List<string>();
            foreach(IElement item in wea)
            {
                if (item.ClassName == "wea")
                    weas.Add(item.Text());
            }
            HttpBO bo = new HttpBO();
            bo.temphigh = Convert.ToInt32(tem[0].TextContent.Trim());
            bo.templow = Convert.ToInt32(tem[1].TextContent.Trim());
            bo.dayweather = weas[0];
            bo.nightweather = weas[1];

            return bo;
        }
    }
}
