using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Share a route by email.
        /// </summary>
        public void ShareRoute()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_route_id
            };

            string routeId = SD10Stops_route_id;


            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            string email = "regression.autotests+testcsharp123@gmail.com";

            // Run the query
            var result = route4Me.RouteSharing(
                routeParameters,
                email,
                out string errorString
            );

            Console.WriteLine(
                result
                ? String.Format("The route {0} shared successfully", routeId)
                : String.Format("Cannot share the route." + Environment.NewLine + errorString)
            );

            RemoveTestOptimizations();
        }
    }
}
