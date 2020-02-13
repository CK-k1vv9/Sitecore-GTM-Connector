using Glass.Mapper.Sc.Web.Mvc;
using Ignium.Foundation.GoogleTagManager.Models;
using Sitecore.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Ignium.Foundation.GoogleTagManager.Services
{
    public interface ITrackerService
    {
        Task<ResultModel> TrackCurrentGoal(ITracker currentTracker, IMvcContext context, Guid goalId, dynamic data, string text);

        Task<bool> TrackCurrentOutcome(ITracker currentTracker, IMvcContext context, Guid outcomeDefinitionId, string currencyCode, decimal amount);

        Task<bool> TrackCurrentPageOutcome(ITracker currentTracker, IMvcContext context, Guid outcomeDefinitionId, string currencyCode, decimal amount);
    }
}