using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Team Activities on a Route
        /// </summary>
        public void GetRouteTeamActivities()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var activityParameters = new ActivityParameters()
            {
                RouteId = routeId,
                Team = "true",
                Limit = 10,
                Offset = 0
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);

            RemoveTestOptimizations();
        }
    }
}
