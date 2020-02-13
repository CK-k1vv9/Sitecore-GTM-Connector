using Glass.Mapper.Sc.Web.Mvc;
using Ignium.Foundation.GoogleTagManager.Models;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Marketing.Definitions.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Ignium.Foundation.GoogleTagManager.Services
{
    public class TrackerService : ITrackerService
    {
        private readonly ITracker _tracker;
        private readonly IMvcContext _context;
        public TrackerService(ITracker currentTracker, IMvcContext mvcContext)
        {
            _tracker = currentTracker;
            _context = mvcContext;
        }

        public async Task<ResultModel> TrackCurrentGoal(Guid goalId, dynamic data, string text)
        {
            var model = new ResultModel();

            EnsureTrackerActive(_tracker);

            if (_tracker == null)
            {
                model.Success = false;
                model.Message = "Tracker is null and can't be started!";

                return model;
            }

            if (_tracker.CurrentPage != null)
            {
                Sitecore.Data.Items.Item goalItem = _context.SitecoreService.Database.GetItem(new ID(goalId));

                if (goalItem != null)
                {
                    IGoalDefinition goalTrigger = Tracker.MarketingDefinitions.Goals[goalItem.ID.ToGuid()];

                    if (goalTrigger == null)
                    {
                        model.Success = false;
                        model.Message = "Goal with ID was not found";

                        return model;
                    }

                    await Task.Run(() =>
                    {
                        var goalEventData = _tracker.CurrentPage.RegisterGoal(goalTrigger);

                        goalEventData.Data = data;
                        goalEventData.ItemId = goalItem.ID.ToGuid();
                        goalEventData.DataKey = goalItem.Paths.Path;
                        goalEventData.Text = text;

                        _tracker.Interaction.AcceptModifications();
                    });
                }
            }

            return model;
        }

        public async Task<bool> TrackCurrentOutcome(Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive(_tracker);

            await Task.Run(() => {
                _tracker.Interaction.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        public async Task<bool> TrackCurrentPageOutcome(Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive(_tracker);

            await Task.Run(() => {
                _tracker.CurrentPage.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        private void EnsureTrackerActive(ITracker currentTracker)
        {
            if (_tracker == null)
                _tracker.StartTracking();
        }
    }
}