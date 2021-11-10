using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove a destination from a route.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="destinationId">Destination ID</param>
        public void RemoveRouteDestination(string routeId = null, int? destinationId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                routeId = SD10Stops_route_id;
                destinationId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;
            }

            // Run the query
            bool deleted = route4Me.RemoveRouteDestination(
                routeId,
                (int)destinationId,
                out string errorString);

            PrintExampleDestination(deleted, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
