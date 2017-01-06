using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Find Asset (Asset tracking)
        /// </summary>
        public void FindAsset()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            string query = "2121541";
            // Run the query
            string errorString = "";
            Dictionary<string, string> result = route4Me.FindAsset(query, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("FindAsset executed successfully");
                Console.WriteLine("route_id: " + result["route_id"]);
                Console.WriteLine("sequence_no: " + result["sequence_no"]);
                Console.WriteLine("datetime: " + result["mDateTime"]);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("FindAsset error: {0}", errorString);
            }
        }
    }
}
