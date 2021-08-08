using DingtalkChatbotSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Service.Interface
{
    public interface IRobotService
    {

        public void SendMessage(string msg);
        
    }
}
