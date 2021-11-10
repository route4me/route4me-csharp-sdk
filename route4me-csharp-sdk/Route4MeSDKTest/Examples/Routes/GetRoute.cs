using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get a route by route ID
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="getRouteDirections">If true, the directions included in the response</param>
        /// <param name="getRoutePathPoints">If true, the path points included in the response</param>
        public void GetRoute(string routeId = null,
                             bool? getRouteDirections = null,
                             bool? getRoutePathPoints = null)
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
                getRouteDirections = true;
                getRoutePathPoints = true;
            }

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Directions = getRouteDirections,
                RoutePathOutput = getRoutePathPoints == true
                    ? RoutePathOutput.Points.ToString()
                    : ""
            };

            // Run the query
            DataObjectRoute dataObject = route4Me.GetRoute(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
