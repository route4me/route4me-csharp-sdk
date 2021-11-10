using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get limited number of the optimizations.
        /// </summary>
        public void GetOptimizations()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var queryParameters = new OptimizationParameters()
            {
                Limit = 10,
                Offset = 5
            };

            // Run the query
            DataObject[] dataObjects = route4Me.GetOptimizations(
                queryParameters,
                out string errorString);

            PrintExampleOptimizationResult(dataObjects, errorString);
        }
    }
}
