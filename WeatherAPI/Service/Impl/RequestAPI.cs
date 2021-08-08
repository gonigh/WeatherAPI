using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.DataAccess.Impl;
using WeatherAPI.DataAccess.Interface;
using WeatherAPI.Models;
using WeatherAPI.Service.Interface;
using WeatherAPI.Utils;

namespace WeatherAPI.Service.Impl
{
    public class RequestAPI : IRequestAPI
    {
        private static String TOKEN = null;
        private static String apiKey = "329ec8d2a65847f8960aa2e3d1ddf182";
        private static String secretKey = "d0c75d03278e43489cc8aef1695f8801";
        private static String appCode = "c876a755ce9c4c12884c54f26db872ac";
        private static String getUrl = "https://jisuweather.api.bdymkt.com/weather/city";
        private static String postUrl = "https://jisuweather.api.bdymkt.com/weather/query";

        private ICityDao _cityDao;

        private readonly SetHeader _setHeader = new SetHeader();

        
        public RequestAPI(ICityDao cityDao)
        {
            _cityDao = cityDao;
        }

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
                if(!_cityDao.IsContainCity(city))
                    _cityDao.CreateCity(city);
            }
            return cities;
        }
        public Weather GetWeatherByCode(String citycode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "post";
            _setHeader.SetHeaderValue(request.Headers, "X-Bce-Signature", "AppCode/" + appCode);
            _setHeader.SetHeaderValue(request.Headers, "Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            String param = "";
            if (citycode != "")
            {
                param = "citycode=" + citycode;
            }
            else
            {
                IHttpContextAccessor httpContext = new HttpContextAccessor();
                String ip = httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                param = "ip=" + ip;
            }
            
            byte[] data = Encoding.UTF8.GetBytes(param);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
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
            weather.city = j["result"]["city"].ToString();
            weather.weather = j["result"]["weather"].ToString();
            weather.temp = Convert.ToInt32(j["result"]["temp"]);
            weather.templow = Convert.ToInt32(j["result"]["templow"]);
            weather.temphigh = Convert.ToInt32(j["result"]["temphigh"]);
            for (int i = 0; i < j["result"]["hourly"].Count(); i++)
            {
                Weather.WeatherHours hours = new Weather.WeatherHours();
                hours.weather = j["result"]["hourly"][i]["weather"].ToString();
                hours.temp = Convert.ToInt32(j["result"]["hourly"][i]["temp"]);
                String time_tmp = j["result"]["hourly"][i]["time"].ToString();
                hours.time = Convert.ToInt32(time_tmp.Trim().Split(":")[0]);
                
                weather.hourly.Add(hours);
            }

            return weather;
        }

        public String GetCityCode(String msg)
        {
            //解析传入消息
            String[] cityName = msg.Split("/");
            List<City> cities = new List<City>();
            if(cityName.Length == 1)
            {
                cities = _cityDao.GetCitiesByName(cityName[0]);
            }
            else if(cityName.Length == 2)
            {
                //市级
                List<City> cities1 = _cityDao.GetCitiesByName(cityName[0]);
                if (cities1.Count > 1)
                {
                    return "2";
                }
                //地级市、县
                List<City> cities2 = _cityDao.GetCitiesByName(cityName[1]);
                foreach(var c in cities2)
                {
                    if (c.parentid == cities1[0].cityid)
                        cities.Add(c);
                }
            
            }
            else if(cityName.Length == 3)
            {
                //省级(不存在多个情况)
                List<City> cities1 = _cityDao.GetCitiesByName(cityName[0]);
                //市级
                List<City> cities2 = _cityDao.GetCitiesByName(cityName[1]).Where(c=>c.parentid == cities1[0].cityid).ToList();
                foreach(var c2 in cities2)
                {
                    foreach(var c3 in _cityDao.GetCitiesByName(cityName[2]).Where(c => c.parentid == c2.cityid).ToList())
                    {
                        cities.Add(c3);
                    }
                }
            }
            if (cities.Count > 1)
            {
                return "2";
            }
            if (cities.Count == 0)
            {
                return "0";
            }
            return cities[0].citycode;
        }

    }
}
