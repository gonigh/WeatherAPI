using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;
using WeatherAPI.Utils;

namespace WeatherAPI.Service.Interface
{
    public interface IRequestAPI
    {
        //private static readonly SetHeader _setHeader;

        List<City> GetCities();
        Weather GetWeatherByCode(String citycode = "");

        String GetCityCode(String msg);
    }
}
