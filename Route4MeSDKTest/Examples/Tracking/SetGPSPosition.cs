using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of setting the GPS position of a device.
        /// </summary>
        /// <param name="routeId"></param>
        public void SetGPSPosition()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            // Create the gps parametes
            var gpsParameters = new GPSParameters()
            {
                Format = Format.Csv.Description(),
                RouteId = SD10Stops_route_id,
                Latitude = 33.14384,
                Longitude = -83.22466,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = 1,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = "2014-06-14 17:43:35"
            };

            // Run query
            var response = route4Me.SetGPS(gpsParameters, out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                string.IsNullOrEmpty(errorString) 
                ? String.Format("SetGps response: {0}", response.ToString()) 
                : String.Format("SetGps error: {0}", errorString)
            );

            RemoveTestOptimizations();
        }
    }
}
