using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Duplicate a route
        /// </summary>
        /// <param name="routeId">Route ID</param>
        public void DuplicateRoute(string routeId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerEample = routeId == null ? true : false;

            if (isInnerEample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>()
                {
                    SD10Stops_optimization_problem_id
                };

                routeId = SD10Stops_route_id;
            }

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId
            };

            // Run the query
            string duplicatedRouteId = route4Me.DuplicateRoute(
                routeParameters,
                out string errorString
             );

            if (duplicatedRouteId != null) RoutesToRemove = new List<string>()
            {
                duplicatedRouteId
            };

            Console.WriteLine(
                duplicatedRouteId != null
                ? String.Format(
                    "DuplicateRoute executed successfully, duplicated route ID: {0}", 
                    duplicatedRouteId
                  )
                : String.Format(
                    "DuplicateRoute error {0}", 
                    errorString
                  )
             );

            if (isInnerEample)
            {
                RemoveTestRoutes();
                RemoveTestOptimizations();
            }
        }
    }
}
