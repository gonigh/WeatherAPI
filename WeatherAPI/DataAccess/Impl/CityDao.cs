using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.DataAccess.Interface;
using WeatherAPI.DataAccess.Base;
using WeatherAPI.Models;

namespace WeatherAPI.DataAccess.Impl
{
    public class CityDao : ICityDao
    {
        public SQLContext Context;

        public CityDao(SQLContext context)
        {
            this.Context = context;
        }

        public bool CreateCity(City city)
        {
            Context.Cities.Add(city);
            return Context.SaveChanges() > 0;
        }

        //取全部记录
        public List<City> GetCities()
        {
            return Context.Cities.ToList();
        }

        //取某id记录
        public City GetCityByID(int id)
        {
            return Context.Cities.SingleOrDefault(s => s.cityid == id);
        }

        //某城市是否已在数据库
        public bool IsContainCity(City city)
        {
            return Context.Cities.Contains<City>(city);
        }

        //根据名字查询城市
        public List<City> GetCitiesByName(String city)
        {
            return Context.Cities.Where<City>(c => c.city == city).ToList();
            
        }
    }
}
