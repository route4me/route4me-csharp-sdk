using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Move a destination to a route after specified destination.
        /// </summary>
        /// <param name="toRouteId">Route ID of a destination route</param>
        /// <param name="routeDestinationId">Source destination ID</param>
        /// <param name="afterDestinationId">A destination id in a destination route. </param>
        public void MoveDestinationToRoute(
            string toRouteId = null,
            int? routeDestinationId = null,
            int? afterDestinationId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = toRouteId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();

                routeDestinationId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;

                RunSingleDriverRoundTrip();

                OptimizationsToRemove = new List<string>()
                {
                    SD10Stops_optimization_problem_id,
                    SDRT_optimization_problem_id
                };

                toRouteId = SDRT_route_id;

                if (toRouteId == null || toRouteId.Length != 32)
                {
                    Console.WriteLine("Cannot get a route to move the destination");
                    RemoveTestOptimizations();
                    return;
                }

                afterDestinationId = (int)SDRT_route.Addresses[3].RouteDestinationId;
            }

            // Run the query
            bool success = route4Me.MoveDestinationToRoute(
                toRouteId,
                (int)routeDestinationId,
                (int)afterDestinationId,
                out string errorString);

            Console.WriteLine("");

            if (success)
            {
                Console.WriteLine("MoveDestinationToRoute executed successfully");

                Console.WriteLine("Destination {0} moved to Route {1} after Destination {2}", routeDestinationId, toRouteId, afterDestinationId);
            }
            else
            {
                Console.WriteLine("MoveDestinationToRoute error: {0}", errorString);
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
