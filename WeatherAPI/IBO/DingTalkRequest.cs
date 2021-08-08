using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.IBO
{
    public class DingTalkRequest
    {
        public Text text { get; set; }
    }

    public class Text
    {
        public string content { get; set; }
    }

}
