using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Destination Deleted
        /// </summary>
        public void SearchDestinationDeleted()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            int addressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;

            bool removed = route4Me.RemoveRouteDestination(routeId, addressId, out string errorString);

            if (!removed)
            {
                Console.WriteLine(
                    "Cannot remove the test destination." +
                    Environment.NewLine +
                    errorString);
                return;
            }

            var activityParameters = new ActivityParameters
            {
                ActivityType = "delete-destination",
                RouteId = routeId
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out errorString);

            PrintExampleActivities(activities, errorString);

            RemoveTestOptimizations();
        }
    }
}
