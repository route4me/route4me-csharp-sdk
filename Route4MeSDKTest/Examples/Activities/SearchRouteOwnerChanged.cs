using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Route Owner Changed
        /// </summary>
        public void SearchRouteOwnerChanged()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-owner-changed",
                RouteId = routeId
            };

            // Run the query
            Activity[] activities = route4Me.GetActivityFeed(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);
        }
    }
}
