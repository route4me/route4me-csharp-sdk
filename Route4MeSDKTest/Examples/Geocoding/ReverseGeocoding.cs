using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Reverse Geocoding
        /// </summary>
        /// <returns> xml object </returns>
        public void ReverseGeocoding()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            GeocodingParameters geoParams = new GeocodingParameters { Addresses = "42.35863,-71.05670" };
            // Run the query
            string errorString = "";
            string result = route4Me.Geocoding(geoParams, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("ReverseGeocoding executed successfully");
            }
            else
            {
                Console.WriteLine("ReverseGeocoding error: {0}", errorString);
            }
        }
    }
}
