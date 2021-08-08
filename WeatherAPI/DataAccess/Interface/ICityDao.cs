using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.DataAccess.Base;
using WeatherAPI.Models;

namespace WeatherAPI.DataAccess.Interface
{
    public interface ICityDao
    {
        //插入数据
        bool CreateCity(City city);

        //取全部记录
        List<City> GetCities();

        //取某id记录
        City GetCityByID(int id);

        //是否存在某个城市
        bool IsContainCity(City city);

        List<City> GetCitiesByName(String city);
    }
}
