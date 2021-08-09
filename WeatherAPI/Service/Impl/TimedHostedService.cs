using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherAPI.BO;
using WeatherAPI.Service.Interface;
using WeatherAPI.Utils;

namespace WeatherAPI.Service.Impl
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private readonly IGetClient client;
        private readonly IDangerours dangerours;
        private readonly Robot robot;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
            client = new GetClient();
            dangerours = new Dangerours();
            robot = new Robot();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            //30秒测试一次
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            //输入地区码
            HttpBO bo = client.GetWeather("101210101");

            if (dangerours.IsDangerours(bo))
            {
                robot.SendMessage(HttpBO.FormatBO(bo));
            } 
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

     
    }
}
