using Ignium.Foundation.GoogleTagManager.Services;
using Sitecore.Data;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ignium.Foundation.GoogleTagManager.Controllers
{
    public class GoalController : ServicesApiController
    {
        private readonly ITrackerService _trackerService;
        public GoalController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }

        [Route("api/googletagmanager/goals/track")]
        [HttpGet]
        public async Task<IHttpActionResult> TrackGoal(Guid goalId, dynamic data, string text)
        {
            if (goalId == Guid.Empty)
                return BadRequest();

            bool result = await _trackerService.TrackCurrentGoal(goalId, data, text);

            return Ok(result);
        }
    }
}