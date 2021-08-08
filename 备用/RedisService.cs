using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Service.Impl
{
    public class RedisService : IRedisService
    {
        public static RedisClient client = new RedisClient("127.0.0.1", 6397,"1207hcz");

        public string GetData(string key)
        {
            string res = client.Get<String>(key);
            return res;
        }

        public void AddData(string key, string value)
        {
            if (client.ContainsKey(key))
                throw new Exception();
            TimeSpan timeSpan = new TimeSpan(0, 0, 30);
            client.Add(key, value,timeSpan);
        }

        public void AddData(string key, string value, TimeSpan time)
        {
            if (client.ContainsKey(key))
                throw new Exception();
            client.Add(key, value, time);
        }
    }
}
