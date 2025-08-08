using Lumen.Modules.GRDF.Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace Lumen.Modules.GRDF.Module.Controllers {
    public class Payload {
        public string cookie { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class GRDFDataController(ILogger<GRDFDataController> logger, IGrdfApi grdfapi) : ControllerBase {
        private static DateTime? LastRun = null;

        [HttpPost("queryDataFromGRDF")]
        public async Task<IActionResult> QueryDataFromGRDF([FromBody] Payload payload) {
            if (LastRun is not null && LastRun.Value.AddHours(1) > DateTime.UtcNow) {
                logger.LogInformation("Skipping QueryDataFromGRDF() to prevent spamming GRDF API");
                return NoContent();
            }

            logger.BeginScope("Getting data from the GRDF API ...");

            try {
                await grdfapi.QueryConsumptionData(payload.cookie, GRDFModule.PCENumber);

                LastRun = DateTime.UtcNow;

                return Accepted();
            } catch (Exception ex) {
                logger.LogError(ex, "Error when querying GRDF data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
