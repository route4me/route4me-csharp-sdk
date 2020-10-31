using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Forward Geocoding
        /// </summary>
        /// <returns> xml object </returns>
        public void GeocodingForward(GeocodingParameters geoParams)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            //Run the query
            string errorString = "";
            string result = route4Me.Geocoding(geoParams, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("GeocodingForward executed successfully");
            }
            else
            {
                Console.WriteLine("GeocodingForward error: {0}", errorString);
            }
        }
    }
}
