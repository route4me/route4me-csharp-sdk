using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Destination Marked as Departed
        /// </summary>
        public void SearchDestinationMarkedAsDeparted()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            int addressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;

            var addressVisitedParams = new AddressParameters()
            {
                RouteId = routeId,
                AddressId = addressId,
                IsVisited = true
            };

            int visitedStatus = route4Me.MarkAddressVisited(addressVisitedParams, out string errorString);

            if (visitedStatus != 1)
            {
                Console.WriteLine(
                    "Cannot mark the test destination as visited." +
                    Environment.NewLine +
                    errorString);

                RemoveTestOptimizations();
                return;
            }

            var addressDepartParams = new AddressParameters()
            {
                RouteId = routeId,
                AddressId = addressId,
                IsDeparted = true
            };

            int departedStatus = route4Me.MarkAddressDeparted(addressDepartParams, out errorString);

            if (departedStatus != 1)
            {
                Console.WriteLine(
                    "Cannot mark the test destination as departed." +
                    Environment.NewLine +
                    errorString);

                RemoveTestOptimizations();
                return;
            }

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed",
                RouteId = routeId
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out errorString);

            PrintExampleActivities(activities, errorString);

            RemoveTestOptimizations();
        }
    }
}
