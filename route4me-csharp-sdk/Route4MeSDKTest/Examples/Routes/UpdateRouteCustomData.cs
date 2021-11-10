using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a route 
        /// by sending custom data of an address with HTTP PUT method.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="routeDestionationId">Route destination ID</param>
        /// <param name="CustomData">Custom data</param>
        public void UpdateRouteCustomData(string routeId = null,
                                          int? routeDestionationId = null,
                                          Dictionary<string, string> CustomData = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>()
                {
                    SD10Stops_optimization_problem_id
                };

                routeId = SD10Stops_route_id;

                routeDestionationId = SD10Stops_route.Addresses[1].RouteDestinationId;
            }

            // Run the query
            var parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                RouteDestinationId = routeDestionationId
            };

            Address result = route4Me.UpdateRouteCustomData(
                parameters,
                CustomData,
                out string errorString
            );

            PrintExampleDestination(result, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
