using Glass.Mapper.Sc.Web.Mvc;
using Ignium.Foundation.GoogleTagManager.Models;
using Ignium.Foundation.GoogleTagManager.Services;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ignium.Foundation.GoogleTagManager.Controllers
{
    public class GoalController : Controller
    {
        private readonly ITrackerService _trackerService;
        private readonly IMvcContext _mvcContext;

        public GoalController(ITrackerService trackerService, IMvcContext mvcContext)
        {
            _trackerService = trackerService;
            _mvcContext = mvcContext;
        }

        [Route("api/googletagmanager/goals/track")]
        [HttpGet]
        public async Task<ActionResult> TrackGoal(Guid goalId, dynamic data, string text)
        {
            if (goalId == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!Tracker.IsActive)
            {
                Tracker.StartTracking();
            }

            ResultModel result = await _trackerService.TrackCurrentGoal(Tracker.Current, _mvcContext, goalId, data, text);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}