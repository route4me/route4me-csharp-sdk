using Route4MeSDK.QueryTypes;
using System.Collections;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Rapid Street Data All
        /// </summary>
        public void RapidStreetDataAll()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters();

            // Run the query
            ArrayList result = route4Me.RapidStreetData(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetData, errorString);
        }
    }
}
