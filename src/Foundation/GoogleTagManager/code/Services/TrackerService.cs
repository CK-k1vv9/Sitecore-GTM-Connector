using Sitecore.Analytics;
using Sitecore.Data;
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
        public TrackerService(ITracker tracker)
        {
            _tracker = tracker;
        }

        public async Task<bool> TrackCurrentGoal(Guid goalId, dynamic data, string text)
        {
            EnsureTrackerActive();

            if (_tracker.CurrentPage != null)
            {
                Sitecore.Data.Items.Item goalItem = Sitecore.Context.Database.GetItem(new ID(goalId));

                if (goalItem != null)
                {
                    var goalTrigger = Tracker.MarketingDefinitions.Goals[goalItem.ID.ToGuid()];

                    if (goalTrigger == null)
                        return false;

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

            return true;
        }

        public async Task<bool> TrackCurrentOutcome(Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive();

            await Task.Run(() => {
                _tracker.Interaction.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        public async Task<bool> TrackCurrentPageOutcome(Guid outcomeDefinitionId, string currencyCode, decimal amount)
        {
            EnsureTrackerActive();

            await Task.Run(() => {
                _tracker.CurrentPage.RegisterOutcome(Tracker.MarketingDefinitions.Outcomes[outcomeDefinitionId], currencyCode, amount);
            });

            return true;
        }

        private void EnsureTrackerActive()
        {
            if (_tracker.IsActive)
                _tracker.StartTracking();
        }
    }
}