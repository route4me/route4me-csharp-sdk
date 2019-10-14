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
        /// Rapid Street Data Single
        /// </summary>
        public void RapidStreetDataSingle()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            GeocodingParameters geoParams = new GeocodingParameters()
            {
                Pk =4
            };
            // Run the query
            string errorString = "";
            ArrayList result = route4Me.RapidStreetData(geoParams, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("RapidStreetDataSingle executed successfully");
                foreach (Dictionary<string, string> res1 in result)
                {

                    Console.WriteLine("Zipcode: " + res1["zipcode"]);
                    Console.WriteLine("Street name: " + res1["street_name"]);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine("RapidStreetDataSingle error: {0}", errorString);
            }
        }
    }
}

