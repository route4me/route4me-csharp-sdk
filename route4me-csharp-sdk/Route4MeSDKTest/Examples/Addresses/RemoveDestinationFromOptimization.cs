using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove a destination from an optimization.
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="destinationId">Destination ID</param>
        /// <param name="andReOptimize">If true, re-optimize an optimization </param>
        public void RemoveDestinationFromOptimization(
            string optimizationId = null,
            int? destinationId = null,
            bool? andReOptimize = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = optimizationId == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                optimizationId = SD10Stops_optimization_problem_id;
                destinationId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;
                andReOptimize = true;
            }

            // Run the query
            bool removed = route4Me.RemoveDestinationFromOptimization(
                optimizationId,
                (int)destinationId,
                out string errorString);

            Console.WriteLine("");

            if (removed)
            {
                Console.WriteLine("RemoveAddressFromOptimization executed successfully");

                Console.WriteLine("Optimization Problem ID: {0}, Destination ID: {1}", optimizationId, destinationId);
            }
            else
            {
                Console.WriteLine("RemoveAddressFromOptimization error: {0}", errorString);
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
