using System;
using System.Collections.Generic;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Forward batch geocoding
        /// </summary>
        /// <param name="geoParams">Geocoding parameters</param>
        public void BatchGeocodingForward(GeocodingParameters geoParams = null)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if (geoParams == null)
            {
                geoParams = new GeocodingParameters()
                {
                    Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                    ExportFormat = "json"
                };
            }

            //Run the query
            string result = route4Me.BatchGeocoding(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString);
        }
    }
}
