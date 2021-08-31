using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Forward Geocoding
        /// </summary>
        /// <returns> json/xml object </returns>
        public void GeocodingForward()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters()
            {
                Addresses = "Los Angeles International Airport, CA",
                Format = "json"
            };

            //Run the query
            string result = route4Me.Geocoding(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString);
        }
    }
}
