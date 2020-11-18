using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get origin route of the duplicated route.
        /// </summary>
        public void RouteOriginParameter()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new System.Collections.Generic.List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            #region Duplicate Route

            var duplParameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id
            };

            // Run the query
            string duplicatedRouteId = route4Me.DuplicateRoute(
                duplParameters,
                out string errorString
             );

            if (duplicatedRouteId!=null)
            {
                RoutesToRemove = new List<string>()
                {
                    duplicatedRouteId
                };
            }
            else
            {
                Console.WriteLine("Cannot duplicate the route");
                return;
            }

            #endregion

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = duplicatedRouteId,
                Original = true
            };

            var route = route4Me.GetRoute(routeParameters, out errorString);

            PrintExampleRouteResult((route?.OriginalRoute ?? null), errorString);

            RemoveTestRoutes();
            RemoveTestOptimizations();
        }
    }
}
