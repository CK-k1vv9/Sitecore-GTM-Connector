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
        public TrackerService()
        {
        }

        public async Task<ResultModel> TrackCurrentGoal(ITracker currentTracker, IMvcContext context, Guid goalId, dynamic data, string text)
        {
            var model = new ResultModel();

            EnsureTrackerActive(currentTracker);

            if (currentTracker == null)
            {
                model.Success = false;
                model.Message = "Tracker is null and can't be started!";

                return model;
            }

            if (currentTracker.CurrentPage != null)
            {
                Sitecore.Data.Items.Item goalItem = context.SitecoreService.Database.GetItem(new ID(goalId));

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
                        var goalEventData = currentTracker.CurrentPage.RegisterGoal(goalTrigger);

                        goalEventData.Data = data;
                        goalEventData.ItemId = goalItem.ID.ToGuid();
                        goalEventData.DataKey = goalItem.Paths.Path;
                        goalEventData.Text = text;

                        currentTracker.Interaction.AcceptModifications();
                    });
                }
            }

            return model;
        }

        public async Task<bool> TrackCurrentOutcome(ITracker currentTracker, IMvcContext context, Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive(currentTracker);

            await Task.Run(() => {
                currentTracker.Interaction.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        public async Task<bool> TrackCurrentPageOutcome(ITracker currentTracker, IMvcContext context, Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive(currentTracker);

            await Task.Run(() => {
                currentTracker.CurrentPage.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        private void EnsureTrackerActive(ITracker currentTracker)
        {
            if (currentTracker == null)
                currentTracker.StartTracking();
        }
    }
}