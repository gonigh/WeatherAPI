using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.BO;
using WeatherAPI.IBO;
using WeatherAPI.Models;
using WeatherAPI.Service;
using WeatherAPI.Service.Impl;
using WeatherAPI.Service.Interface;
using WeatherAPI.Utils;

namespace WeatherAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyWeather : ControllerBase
    {
        private IRequestAPI api;
        private IMemoryCache _cache;
        private Robot _robot;
        public ReplyWeather(
            IMemoryCache cache,
            IRequestAPI Iapi)
        {
            api = Iapi;
            _cache = cache;
            _robot = new Robot();
        }

        [HttpPost("Reply")]
        public async Task<IActionResult> Reply(DingTalkRequest ding)
        {
            //处理输入信息
            String msg = ding.text.content.Trim();
            String citycode = api.GetCityCode(msg);
            if (citycode.Equals("0"))
            {
                _robot.SendMessage("中国有 " + msg + " 这地方吗");
                return Ok();
            }
            else if (citycode.Equals("2"))
            {
                _robot.SendMessage("我找到了的好多个" + msg + ",麻烦说的具体些\n" +
                    "示例：浙江/金华/浦江县\n" +
                    "省市区之间记得用用/隔开哦");
                return Ok();
            }

            //获取天气
            Weather weather = new Weather();

            var cache = _cache.Get("city_" + citycode);
            if (cache != null)
            {
                weather = _cache.Get<Weather>("city_" + citycode);
            }
            else
            {
                weather = api.GetWeatherByCode(citycode);
                _cache.Set<Weather>("city_" + citycode, weather, TimeSpan.FromSeconds(30));
            }

            //处理信息
            WeatherBO bo = WeatherBO.ToWeatherBO(weather);

            //发送信息
            _robot.SendMessage(WeatherBO.FormatBO(bo));
            return Ok();
        }

       
    }
}
