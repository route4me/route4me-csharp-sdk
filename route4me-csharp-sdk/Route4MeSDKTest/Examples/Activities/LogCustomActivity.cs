using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create User Activity
        /// </summary>
        public void LogCustomActivity()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            string routeId = SD10Stops_route_id;

            var activity = new Activity()
            {
                ActivityType = "user_message",
                ActivityMessage = "Test User Activity " + DateTime.Now.ToString(),
                RouteId = routeId
            };

            // Run the query
            bool added = route4Me.LogCustomActivity(activity, out string errorString);

            Console.WriteLine("");

            if (added)
            {
                Console.WriteLine("LogCustomActivity executed successfully");
            }
            else
            {
                Console.WriteLine("LogCustomActivity error: {0}", errorString);
            }

            RemoveTestOptimizations();
        }
    }
}
