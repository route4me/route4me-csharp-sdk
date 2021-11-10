using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get an optimization problem by ID.
        /// </summary>
        /// <param name="optimizationProblemID">Optimization problem ID</param>
        public void GetOptimization(string optimizationProblemID = null)
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
                OptimizationProblemID = optimizationProblemID
            };

            // Run the query
            DataObject dataObject = route4Me.GetOptimization(
                optimizationParameters,
                out string errorString);

            PrintExampleOptimizationResult(dataObject, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
