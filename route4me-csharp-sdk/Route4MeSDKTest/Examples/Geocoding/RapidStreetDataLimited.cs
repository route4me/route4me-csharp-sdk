﻿using Route4MeSDK.QueryTypes;
using System.Collections;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Rapid Street Data Limited
        /// </summary>
        public void RapidStreetDataLimited()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var geoParams = new GeocodingParameters()
            {
                Offset = 10,
                Limit = 10
            };

            // Run the query
            ArrayList result = route4Me.RapidStreetData(geoParams, out string errorString);

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetData, errorString);
        }
    }
}
