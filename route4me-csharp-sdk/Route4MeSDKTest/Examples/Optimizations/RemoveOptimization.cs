using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void RemoveOptimization(string[] optimizationProblemIDs = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = optimizationProblemIDs == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                optimizationProblemIDs = new string[] { SD10Stops_optimization_problem_id };
            }

            // Run the query
            bool removed = route4Me.RemoveOptimization(
                optimizationProblemIDs,
                out string errorString);

            Console.WriteLine("");

            if (removed)
            {
                Console.WriteLine("RemoveOptimization executed successfully");

                foreach (string optid in optimizationProblemIDs)
                {
                    Console.WriteLine("Removed Optimization Problem ID: {0}", optid);
                }
            }
            else
            {
                Console.WriteLine("RemoveOptimization error: {0}", errorString);
            }
        }
    }
}
