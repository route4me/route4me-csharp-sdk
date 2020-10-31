using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Departed
        /// </summary>
        /// <returns> status </returns>
        public void MarkAddressDeparted(AddressParameters aParams)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            // Run the query

            string errorString = "";
            int result = route4Me.MarkAddressVisited(aParams, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                if (result == 1)
                {
                    Console.WriteLine("MarkAddressDeparted executed successfully");
                }
                else
                {
                    Console.WriteLine("MarkAddressDeparted error: {0}", errorString);
                }
            }
            else
            {
                Console.WriteLine("MarkAddressVisited error: {0}", errorString);
            }
        }
    }
}
