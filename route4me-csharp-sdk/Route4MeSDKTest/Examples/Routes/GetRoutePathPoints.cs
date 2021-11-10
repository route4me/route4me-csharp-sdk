using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get route path points
        /// </summary>
        public void GetRoutePathPoints()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                RoutePathOutput = RoutePathOutput.Points.ToString()
            };

            // Run the query
            var dataObject = route4Me.GetRoute(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
