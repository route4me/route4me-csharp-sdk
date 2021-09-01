using System;
using System.Collections.Generic;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Resequence/Reoprimize All Route Destinations
        /// </summary>
        /// <param name="routeId">Route ID</param>
        public void ResequenceReoptimizeRoute(string routeId = null)
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

            var queryParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                DisableOptimization = false,
                Optimize  = DataTypes.Optimize.Distance.Description()
            };

            // Run the query
            var result = route4Me.ReoptimizeRoute(
                queryParameters, 
                out string errorString
            );

            Console.WriteLine(
                result!=null && result.GetType()==typeof(Route4MeSDK.DataTypes.DataObjectRoute)  
                ? "ResequenceReoptimizeRoute executed successfully"
                : String.Format("ResequenceReoptimizeRoute error: {0}", errorString)
            );

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
