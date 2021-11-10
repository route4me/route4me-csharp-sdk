using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example refers to the process of searching for the specified text 
        /// throughout all routes names belonging to the user's account.
        /// </summary>
        /// <param name="query">Query text</param>
        public void SearchRoutesForText()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops("Nonstandard route name");
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var routeParameters = new RouteParametersQuery()
            {
                Query = "Nonstandard"
            };

            // Run the query
            DataObjectRoute[] dataObjects = route4Me.GetRoutes(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObjects, errorString);

            RemoveTestOptimizations();
        }
    }
}
