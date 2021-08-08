using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientDemo
    {

        [HttpGet]
        [Route("GetData")]
        public async Task<string> GetData()
        {
            String citycode = "101210101";
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Host",)
            string rspText = client.GetStringAsync("http://www.weather.com.cn/weather1d/"+citycode+".shtml").Result;
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(rspText);
            
            
            return ($"Serializing the (original) document: {doc.QuerySelector("script").OuterHtml}");
        }
    }
}
