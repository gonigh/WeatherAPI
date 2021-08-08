using DingtalkChatbotSdk;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotDemo : ControllerBase
    {
        public static string WebHookUrl = "https://oapi.dingtalk.com/robot/send?access_token=63ee4cf8fc7f8b3a404f283c1fd3bb7e4d30d0f896d3a6b3279f6a3ab2c4bdbd";

        [HttpPost]
        [Route("Send")]
        public void SendMessage(string msg)
        {
            string SendMsg = "靓仔通知：\n" + msg;
            var res = DingDingClient.SendMessageAsync(WebHookUrl, SendMsg).Result;
        }

        [HttpPost]
        [Route("Get")]
        public void GetMessage()
        {

        }
    }
}
