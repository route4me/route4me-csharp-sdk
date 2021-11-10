using Route4MeSDK.QueryTypes;
using System.Collections;

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
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters()
            {
                Zipcode = "00601",
                Offset = 1,
                Limit = 10
            };

            // Run the query
            ArrayList result = route4Me.RapidStreetZipcode(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetZipCode, errorString);
        }
    }
}
