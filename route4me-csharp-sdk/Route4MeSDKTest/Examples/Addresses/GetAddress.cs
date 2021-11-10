using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get destination from a route.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="routeDestinationId">A route destination ID</param>
        public void GetAddress(string routeId = null, int? routeDestinationId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };
            }

            var addressParameters = new AddressParameters()
            {
                RouteId = (routeId == null) ? SD10Stops_route_id : routeId,
                RouteDestinationId = (routeDestinationId == null)
                                    ? (int)SD10Stops_route.Addresses[2].RouteDestinationId
                                    : (int)routeDestinationId,
                Notes = true
            };

            // Run the query
            Address destination = route4Me.GetAddress(addressParameters, out string errorString);

            PrintExampleDestination(destination, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
