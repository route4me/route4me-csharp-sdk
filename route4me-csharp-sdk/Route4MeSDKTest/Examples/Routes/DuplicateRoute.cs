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
                DuplicateRoutesId = new string[] { routeId }
            };

            // Run the query
            var result = route4Me.DuplicateRoute(routeParameters, out string errorString);

            if (((result?.Status ?? false) && (result?.RouteIDs?.Length ?? 0)>0))
            {
                RoutesToRemove = new List<string>() { result.RouteIDs[0] };
            }

            Console.WriteLine(
                (result?.Status ?? false) && (result?.RouteIDs?.Length ?? 0) > 0 
                ? String.Format(
                    "DuplicateRoute executed successfully, duplicated route ID: {0}",
                    result.RouteIDs[0]
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
