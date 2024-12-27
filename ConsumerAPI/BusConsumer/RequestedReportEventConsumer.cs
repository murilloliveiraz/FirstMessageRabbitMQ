using ConsumerAPI.Models;
using MassTransit;

namespace ConsumerAPI.BusConsumer
{
    public class RequestedReportEventConsumer : IConsumer<RequestedReportEvent>
    {
        private readonly ILogger<RequestedReportEventConsumer> _logger;

        public RequestedReportEventConsumer(ILogger<RequestedReportEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RequestedReportEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Processing Report, Id:{message.Id}, Nome:{message.Name}", message.Id, message.Name);
            await Task.Delay(10000);
            var report = Reports.ReportsList.FirstOrDefault(report => report.Id == message.Id);
            if(report != null)
            {
                report.Status = "Finished";
                report.ProcessedTime = DateTime.Now;
            }
            _logger.LogInformation($"Report Finished, Id:{message.Id}, Nome:{message.Name}", message.Id, message.Name);
        }
    }
}
