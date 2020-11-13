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
        public void GetOptimization(string optimizationProblemID)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = SD10Stops_optimization_problem_id
            };

            // Run the query
            DataObject dataObject = route4Me.GetOptimization(
                optimizationParameters, 
                out string errorString);

            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
