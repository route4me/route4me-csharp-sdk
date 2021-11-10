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
            var duplicateResult = route4Me.DuplicateRoute(duplParameters, out string errorString);

            if ((duplicateResult?.RouteIDs?.Length ?? 0) > 0)
            {
                RoutesToRemove = new List<string>()
                {
                    duplicateResult.RouteIDs[0]
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
                RouteId = duplicateResult.RouteIDs[0],
                Original = true
            };

            var route = route4Me.GetRoute(routeParameters, out errorString);

            PrintExampleRouteResult((route?.OriginalRoute ?? null), errorString);

            RemoveTestRoutes();
            RemoveTestOptimizations();
        }
    }
}
