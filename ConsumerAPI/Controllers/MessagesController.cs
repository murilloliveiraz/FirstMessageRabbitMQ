using ConsumerAPI.Extensions;
using ConsumerAPI.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IPublisherBus bus;

        public MessagesController(IPublisherBus bus)
        {
            this.bus = bus;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> RequestReport(string name, CancellationToken cancellation = default)
        {
            var request = new RequestReport()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = "Pending",
                ProcessedTime = null,
            };

            Reports.ReportsList.Add(request);

            var eventRequest = new RequestedReportEvent(request.Id, request.Name);

            await bus.PublishAsync(eventRequest, cancellation);

            return Ok(request);
        }

        [HttpGet()]
        public IActionResult GetReports()
        {
            return Ok(Reports.ReportsList);
        }
    }
}
