using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Re-optimize an optimization
        /// </summary>
        /// <param name="optimizationProblemID">Optimization problem ID</param>
        public void ReOptimization(string optimizationProblemID = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = optimizationProblemID == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                optimizationProblemID = SD10Stops_optimization_problem_id;
                OptimizationsToRemove = new List<string>();
                OptimizationsToRemove.Add(optimizationProblemID);
            }

            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = optimizationProblemID,
                ReOptimize = true
            };

            // Run the query
            DataObject dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out string errorString);

            PrintExampleOptimizationResult(dataObject, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
