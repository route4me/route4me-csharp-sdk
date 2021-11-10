using Route4MeSDK.QueryTypes;
using System.Collections;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Rapid Street Service All
        /// </summary>
        public void RapidStreetServiceAll()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters()
            {
                Zipcode = "00601",
                Housenumber = "17"
            };

            // Run the query
            ArrayList result = route4Me.RapidStreetService(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetService, errorString);
        }
    }
}
