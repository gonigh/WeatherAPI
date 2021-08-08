using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.Redis;
using WeatherAPI.Service;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisDemo : ControllerBase
    {
        private IRedisService _redisService;
        public RedisDemo(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        [Route("GetRedis")]
        public String SetRedis(string key)
        {
            return _redisService.GetData(key);
        }

        [HttpPost]
        [Route("SetRedis")]
        public void SetRedis(string key, string value)
        {
            _redisService.AddData(key, value);
        }
    }
}
