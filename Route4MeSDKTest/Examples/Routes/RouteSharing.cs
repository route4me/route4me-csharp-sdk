using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void RouteSharing(string routeId, string Email)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            // Example refers to the process of sharing a route by email

            // Run the query
            RouteParametersQuery parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            string errorString;
            bool result = route4Me.RouteSharing(parameters, Email, out errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("RouteSharing executed successfully");
            }
            else
            {
                Console.WriteLine("RouteSharing error {0}", errorString);
            }
        }
    }
}

