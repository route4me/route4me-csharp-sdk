using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Unlink a route from an optimization.
        /// </summary>
        public void UnlinkRouteFromOptimization()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            #region Duplicate the route

            var routeDuplicateParameters = new RouteParametersQuery()
            {
                DuplicateRoutesId = new string[] { SD10Stops_route_id }
            };

            // Run the query
            var duplicatedResult = route4Me.DuplicateRoute(routeDuplicateParameters, out string errorString);

            if (!(duplicatedResult?.Status ?? false) || (duplicatedResult?.RouteIDs?.Length ?? 0)<1)
            {
                Console.WriteLine($"Cannot duplicate the route. {errorString}");
                return;
            }

            Thread.Sleep(5000);

            var duplicatedRoute = route4Me.GetRoute(
                new RouteParametersQuery() { RouteId = duplicatedResult.RouteIDs[0] },
                out errorString);

            if (duplicatedRoute == null && duplicatedRoute.GetType() != typeof(DataObjectRoute))
            {
                Console.WriteLine($"Cannot retrieve the duplicated route. {errorString}");
                return;
            }

            RoutesToRemove = new List<string>() { duplicatedResult.RouteIDs[0] };

            #endregion

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = duplicatedResult.RouteIDs[0],
                UnlinkFromMasterOptimization = true
            };

            // Run the query
            var unlinkedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            PrintExampleRouteResult(unlinkedRoute, errorString);

            RemoveTestRoutes();
            RemoveTestOptimizations();
        }
    }
}
