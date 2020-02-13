using Ignium.Foundation.GoogleTagManager.Controllers;
using Ignium.Foundation.GoogleTagManager.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Analytics;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ignium.Foundation.GoogleTagManager
{
    public class DependencyConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITrackerService, TrackerService>();
            serviceCollection.AddTransient<ITracker>(provider => Tracker.Current);
            serviceCollection.AddTransient<GoalController>();
            serviceCollection.AddTransient<OutcomeController>();
        }
    }
}