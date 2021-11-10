using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove specified routes.
        /// </summary>
        /// <param name="routeIds">An array of the route IDs</param>
        public void DeleteRoutes(string[] routeIds = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeIds == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>()
                {
                    SD10Stops_optimization_problem_id
                };

                RunSingleDriverRoundTrip();
                OptimizationsToRemove.Add(SDRT_optimization_problem_id);

                routeIds = new string[]
                {
                    SD10Stops_route_id,
                    SDRT_route_id
                };
            }

            // Run the query
            string[] deletedRouteIds = route4Me.DeleteRoutes(
                routeIds,
                out string errorString
            );

            Console.WriteLine("");

            Console.WriteLine(
                deletedRouteIds != null
                ? String.Format(
                    "DeleteRoutes executed successfully, {0} routes deleted",
                    deletedRouteIds.Length
                  )
                : String.Format(
                    "DeleteRoutes error {0}",
                    errorString
                  )
            );

            RemoveTestOptimizations();
        }
    }
}
