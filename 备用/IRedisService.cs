using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Service
{
    public interface IRedisService
    {
        //private RedisClient client => new RedisClient("127.0.0.1", 6397,"1207hcz");

        public string GetData(string key);
        public void AddData(string key, string value);
    }
}
