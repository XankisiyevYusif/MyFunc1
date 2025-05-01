using System;
using System.Threading.Tasks;
using FunctionApp1.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;

        public Function1(ILoggerFactory loggerFactory, IQueueService queueService)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _queueService = queueService;
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            await _queueService.SendMessageAsync($"Hello from Timer Trigger!");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }

        [Function("Function2")]
        public async Task Run2([TimerTrigger("*/7 * * * * *")] TimerInfo myTimer)
        {
            var message = await _queueService.ReceiveMessageAsync();

            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Message from queue: {message}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
