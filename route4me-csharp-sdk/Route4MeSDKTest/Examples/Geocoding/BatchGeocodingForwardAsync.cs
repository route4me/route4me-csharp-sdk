using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Asynchronous batch geocoding
        /// </summary>
        /// <param name="geoParams">Geocoding parameters</param>
        public void BatchGeocodingForwardAsync()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "4171 Woodland Dr, Howell, Howell, MI, 48855" + Environment.NewLine +
                            "6689 N LAKE ROAD, LENA, LENA, IL, 61048",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.BatchGeocodingAsync(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString);
        }
    }
}
