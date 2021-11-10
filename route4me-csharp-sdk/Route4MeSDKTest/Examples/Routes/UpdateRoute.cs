using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Update Route Parameters
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        public void UpdateRoute(string routeId = null)
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
            }

            var parametersNew = new RouteParameters()
            {
                RouteName = "New name of the route"
            };

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = parametersNew
            };

            // Run the query
            DataObjectRoute dataObject = route4Me.UpdateRoute(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
