using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

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
                RouteId = SD10Stops_route_id
            };

            // Run the query
            var duplicatedRouteId = route4Me.DuplicateRoute(
                routeDuplicateParameters, 
                out string errorString);

            if (duplicatedRouteId == null)
            {
                Console.WriteLine("Cannot duplicate the route");
                return;
            }

            var duplicatedRoute = route4Me.GetRoute(
                new RouteParametersQuery() { RouteId = duplicatedRouteId },
                out errorString);

            if (duplicatedRoute==null && duplicatedRoute.GetType()!= typeof(DataObjectRoute))
            {
                Console.WriteLine("Cannot retrieve the duplicated route.");
                return;
            }

            RoutesToRemove = new List<string>() { duplicatedRouteId };

            #endregion

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = duplicatedRouteId,
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
