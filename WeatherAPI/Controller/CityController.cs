using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;
using WeatherAPI.Service.Interface;

namespace WeatherAPI.Controller
{
    public class CityController:ControllerBase
    {
        private readonly IRequestAPI api;

        public CityController(IRequestAPI Api)
        {
            api = Api;
        }

        public List<City> InitCity()
        {
            return api.GetCities();
        }
    }
}
