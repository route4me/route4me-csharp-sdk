using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Resequence/Reoprimize All Route Destinations
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        public void ResequenceReoptimizeRoute(string routeId)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            Dictionary<string, string> roParameters = new Dictionary<string, string>()
            {
                {"route_id","CA902292134DBC134EAF8363426BD247"},
                {"disable_optimization","0"},
                {"optimize","Distance"},
            };

            // Run the query
            string errorString;
            bool result = route4Me.ResequenceReoptimizeRoute(roParameters, out errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("ResequenceReoptimizeRoute executed successfully");
            }
            else
            {
                Console.WriteLine("ResequenceReoptimizeRoute error: {0}", errorString);
            }
        }
    }
}
