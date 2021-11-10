using System;
using System.Collections.Generic;

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

            var roParameters = new Dictionary<string, string>()
            {
                {"route_id",routeId},
                {"disable_optimization","0"},
                {"optimize","Distance"},
            };

            // Run the query
            bool result = route4Me.ResequenceReoptimizeRoute(
                roParameters,
                out string errorString
            );

            Console.WriteLine(
                result
                ? "ResequenceReoptimizeRoute executed successfully"
                : String.Format("ResequenceReoptimizeRoute error: {0}", errorString)
            );

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
