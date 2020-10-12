using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ppsha.Helper;
using ppsha.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ppsha.Services
{
    public class PPSHAService : BackgroundService
    {
        private readonly ILogger _logger;
        public PPSHAService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Program>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int timeInterval = Convert.ToInt32(GlobalStatic._MySettings.Service_Thread_Sleep);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running PPSHA Service.");
                ServiceBusiness _service = new ServiceBusiness(_logger);
                bool result = await _service.CreateClaim();
                await Task.Delay(10000 * timeInterval * 1, stoppingToken);
            }
        }
    }
}
