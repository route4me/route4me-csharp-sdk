using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetOptimizationsByText()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            string queryText = "SD Route 10 Stops Test";

            var queryParameters = new OptimizationParameters()
            {
                Limit = 3,
                Offset = 0,
                Query = queryText
            };

            // Run the query
            var dataObjects = route4Me.GetOptimizations(
                queryParameters,
                out string errorString);

            int foundOptimizations = dataObjects
                .Where(x => x.Parameters.RouteName.Contains(queryText))
                .Count();

            Console.WriteLine(
                foundOptimizations > 0
                    ? "Found the optimizations searched by text: " + foundOptimizations
                    : "Cannot found the optimizations searched by text"
                );

            RemoveTestOptimizations();
        }
    }
}