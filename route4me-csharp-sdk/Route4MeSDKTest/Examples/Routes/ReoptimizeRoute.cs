using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Reoptimize a route
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        public void ReoptimizeRoute(string routeId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new System.Collections.Generic.List<string>()
                {
                    SD10Stops_optimization_problem_id
                };

                routeId = SD10Stops_route_id;
            }

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ReOptimize = true
            };

            // Run the query
            DataObjectRoute dataObject = route4Me.UpdateRoute(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}

