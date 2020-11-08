using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Asynchronous batch geocoding
        /// </summary>
        /// <param name="geoParams">Geocoding parameters</param>
        public void BatchGeocodingForwardAsync(GeocodingParameters geoParams = null)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            string result = route4Me.BatchGeocodingAsync(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString);
        }
    }
}
