using DingtalkChatbotSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Service.Interface;

namespace WeatherAPI.Service.Impl
{
    public class RobotService : IRobotService
    {
        private static string WebHookUrl = "https://oapi.dingtalk.com/robot/send?access_token=e9c58d7d5d6ab44b51fefce5e435c843bea21cb7bf476a59db4de1a0669342be";
        public void SendMessage(String msg)
        {
            string SendMsg = "靓仔通知：\n" + msg;
            var res = DingDingClient.SendMessageAsync(WebHookUrl, SendMsg).Result;
        }
        
    }
}
