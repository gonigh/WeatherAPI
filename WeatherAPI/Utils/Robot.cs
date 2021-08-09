using DingtalkChatbotSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Utils
{
    public class Robot
    {
        private static string WebHookUrl = "https://oapi.dingtalk.com/robot/send?access_token=ebe844809eb10752031e4468041bafa2cbfa0ac5b033bd1085a2edf7471d7074";
        public void SendMessage(String msg)
        {
            string SendMsg = "靓仔通知：\n" + msg;
            var res = DingDingClient.SendMessageAsync(WebHookUrl, SendMsg).Result;
        }
    }
}
