using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get optimizations scheduled to date from the specified date range.
        /// </summary>
        public void GetOptimizationsFromDateRange()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            DateTime today = DateTime.Now;

            TimeSpan days3 = new TimeSpan(3, 0, 0, 0);

            var queryParameters = new OptimizationParameters()
            {
                StartDate = (today - days3).ToString("yyyy-MM-dd"),
                EndDate = today.ToString("yyyy-MM-dd")
            };

            // Run the query
            var dataObjects = route4Me.GetOptimizations(
                queryParameters,
                out string errorString);

            PrintExampleOptimizationResult(dataObjects, errorString);
        }
    }
}
