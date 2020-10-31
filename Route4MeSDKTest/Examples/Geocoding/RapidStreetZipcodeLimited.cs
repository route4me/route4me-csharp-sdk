using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Rapid Street Zipcode Limited
        /// </summary>
        public void RapidStreetZipcodeLimited()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            GeocodingParameters geoParams = new GeocodingParameters()
            {
                Zipcode = "00601",
                Offset = 1,
                Limit = 10
            };
            // Run the query
            string errorString = "";
            ArrayList result = route4Me.RapidStreetZipcode(geoParams, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("RapidStreetZipcodeLimited executed successfully");
                foreach (Dictionary<string, string> res1 in result)
                {

                    Console.WriteLine("Zipcode: " + res1["zipcode"]);
                    Console.WriteLine("Street name: " + res1["street_name"]);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine("RapidStreetZipcodeLimited error: {0}", errorString);
            }
        }
    }
}

