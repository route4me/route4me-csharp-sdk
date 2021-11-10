using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of tracking a specified asset.
        /// </summary>
        public void FindAsset()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            string tracking = SD10Stops_route.Addresses[1].TrackingNumber;

            // Run the query
            FindAssetResponse result = route4Me.FindAsset(tracking, out string errorString);

            DateTime nDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("FindAsset executed successfully");
                Console.WriteLine("tracking_number: " + result.TrackingNumber);

                foreach (FindAssetResponseLocations loc1 in result.Locations)
                {
                    Console.WriteLine("lat: " + loc1.Latitude);
                    Console.WriteLine("lng: " + loc1.Longitude);
                    Console.WriteLine("icon: " + loc1.Icon);
                }

                if ((result?.CustomData?.Count ?? 0) > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in result.CustomData)
                    {
                        Console.WriteLine(kvp.Key + ": " + kvp.Value);
                    }
                }

                foreach (FindAssetResponseArrival arriv1 in result.Arrival)
                {
                    Console.WriteLine(
                        "from_unix_timestamp: " + nDateTime.AddSeconds(arriv1.FromUnixTimestamp >= 0
                        ? (double)arriv1.FromUnixTimestamp
                        : 0)
                    );
                    Console.WriteLine(
                        "to_unix_timestamp: " + nDateTime.AddSeconds(arriv1.ToUnixTimestamp >= 0
                        ? (double)arriv1.ToUnixTimestamp
                        : 0)
                    );
                }

                Console.WriteLine("delivered: " + result.Delivered);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("FindAsset error: {0}", errorString);
            }

            RemoveTestOptimizations();
        }
    }
}
