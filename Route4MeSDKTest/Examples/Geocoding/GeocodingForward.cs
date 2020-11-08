using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Forward Geocoding
        /// </summary>
        /// <returns> json/xml object </returns>
        public void GeocodingForward(GeocodingParameters geoParams = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            if (geoParams==null)
            {
                geoParams = new GeocodingParameters()
                {
                    Addresses = "Los Angeles International Airport, CA||3495 Purdue St, Cuyahoga Falls, OH 44221",
                    Format = "json"
                };
            }

            //Run the query
            string result = route4Me.Geocoding(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString);
        }
    }
}
