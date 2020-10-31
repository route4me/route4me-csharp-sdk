using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void UpdateRouteCustomData(string routeId, int routeDestionationId, Dictionary<string, string> CustomData)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            // The example refers to the process of updating a route by sending custom data of an address with HTTP PUT method.

            // Run the query
            RouteParametersQuery parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                RouteDestinationId = routeDestionationId
            };

            string errorString;
            Address result = route4Me.UpdateRouteCustomData(parameters, CustomData, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UpdateRouteCustomData executed successfully");
                Console.WriteLine("Route ID: {0}", result.RouteId);
                Console.WriteLine("Route Destination ID: {0}", result.RouteDestinationId);
            }
            else
            {
                Console.WriteLine("UpdateRouteCustomData error {0}", errorString);
            }
        }
    }
}
