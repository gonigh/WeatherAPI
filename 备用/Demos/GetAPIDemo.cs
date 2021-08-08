using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.DataAccess.Base;
using WeatherAPI.DataAccess.Impl;
using WeatherAPI.DataAccess.Interface;
using WeatherAPI.Models;
using WeatherAPI.Service.Impl;
using WeatherAPI.Service.Interface;
using WeatherAPI.Utils;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAPIDemo : ControllerBase
    {
        public static String TOKEN = null;
        public String apiKey = "329ec8d2a65847f8960aa2e3d1ddf182";
        public String secretKey = "d0c75d03278e43489cc8aef1695f8801";
        public String appCode = "c876a755ce9c4c12884c54f26db872ac";
        public String getUrl = "https://jisuweather.api.bdymkt.com/weather/city";
        public String postUrl = "https://jisuweather.api.bdymkt.com/weather/query";

        private readonly SetHeader _setHeader = new SetHeader();
        private readonly SQLContext _context;
        public GetAPIDemo(SQLContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        [Route("GetCity")]
        public List<City> GetCities()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
            request.Method = "get";
            _setHeader.SetHeaderValue(request.Headers, "X-Bce-Signature", "AppCode/" + appCode);
            _setHeader.SetHeaderValue(request.Headers, "Content-Type", "application/json;charset=UTF-8");
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseText = response.GetResponseStream();
            String text = new StreamReader(responseText).ReadToEnd();
            JObject j = JObject.Parse(text);
            List<City> cities = new List<City>();
            for (int i = 0; i < j["result"].Count(); i++)
            {
                City city = new City();
                city.cityid = Convert.ToInt32(j["result"][i]["cityid"]);
                city.parentid = Convert.ToInt32(j["result"][i]["parentid"]);
                city.citycode = j["result"][i]["citycode"].ToString();
                city.city = j["result"][i]["city"].ToString();
                cities.Add(city);
            }
            return cities;
        }

        [HttpPost]
        [Route("GetCityWeather")]
        public Weather GetCityWeather()
        {
            String citycode = "101210101";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "post";
            _setHeader.SetHeaderValue(request.Headers, "X-Bce-Signature", "AppCode/" + appCode);
            _setHeader.SetHeaderValue(request.Headers, "Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");

            String param = "city=杭州";
            byte[] data = Encoding.UTF8.GetBytes(param);
            request.ContentLength = data.Length;
            using(Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseText = response.GetResponseStream();
            String text = new StreamReader(responseText).ReadToEnd();
            JObject j = JObject.Parse(text);
            Weather weather = new Weather();
            weather.citycode = param;
            weather.weather = j["result"]["weather"].ToString();
            weather.temp = Convert.ToInt32(j["result"]["temp"]);
            weather.templow = Convert.ToInt32(j["result"]["templow"]);
            weather.temphigh = Convert.ToInt32(j["result"]["temphigh"]);
            for(int i = 0; i < j["result"]["hourly"].Count(); i++)
            {
                WeatherHours hours = new WeatherHours();
                hours.weather = j["result"]["hourly"][i]["weather"].ToString();
                hours.temp = Convert.ToInt32(j["result"]["hourly"][i]["temp"]);
                String time_tmp = j["result"]["hourly"][i]["time"].ToString();
                int hour = Convert.ToInt32(time_tmp.Trim().Split(":")[0]);
                int min = Convert.ToInt32(time_tmp.Trim().Split(":")[1]);
                hours.time = new TimeSpan(hour, min, 0);
                weather.hourly.Add(hours);
            }


            return weather;
        }

        [HttpPost]
        [Route("interface")]
        public List<City> getInterface()
        {
            IRequestAPI api = new RequestAPI(new CityDao(_context));
            List<City> cities = api.GetCities();
            return cities;
        }


    }
}
