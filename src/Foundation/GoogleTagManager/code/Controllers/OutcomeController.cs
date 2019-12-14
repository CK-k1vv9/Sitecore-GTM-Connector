using Ignium.Foundation.GoogleTagManager.Services;
using Sitecore.Analytics;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ignium.Foundation.GoogleTagManager.Controllers
{
    [AllowAnonymous]
    public class OutcomeController : ServicesApiController
    {
        private readonly ITrackerService _trackerService;
        public OutcomeController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }

        [HttpGet]
        [Route("api/googletagmanager/outcomes/track")]
        public async Task<IHttpActionResult> TrackPageOutcome(Guid outcomeDefinitionId, decimal amount, string currencyCode = "USD")
        {
            bool result = await _trackerService.TrackCurrentPageOutcome(outcomeDefinitionId, currencyCode, amount);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/googletagmanager/outcomes/pages/track")]
        public async Task<IHttpActionResult> TrackOutcome(Guid outcomeDefinitionId, decimal amount, string currencyCode = "USD")
        {
            bool result = await _trackerService.TrackCurrentOutcome(outcomeDefinitionId, currencyCode, amount);

            return Ok(result);
        }
    }
}