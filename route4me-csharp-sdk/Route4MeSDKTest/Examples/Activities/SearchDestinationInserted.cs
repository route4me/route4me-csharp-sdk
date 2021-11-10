using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Destination Inserted
        /// </summary>
        public void SearchDestinationInserted()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var newAddress = new Address()
            {
                AddressString = "118 Bill Johnson Rd NE Milledgeville GA 31061",
                Latitude = 33.141784667969,
                Longitude = -83.237518310547,
                Time = 0,
                SequenceNo = 4
            };

            int[] insertedDestinations = route4Me.AddRouteDestinations(
                routeId,
                new Address[] { newAddress },
                out string errorString);

            if (insertedDestinations == null || insertedDestinations.Length < 1)
            {
                Console.WriteLine(
                    "Cannot insert the test destination." +
                    Environment.NewLine +
                    errorString);

                RemoveTestOptimizations();
                return;
            }

            ActivityParameters activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination",
                RouteId = routeId
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out errorString);

            PrintExampleActivities(activities, errorString);

            RemoveTestOptimizations();
        }
    }
}
