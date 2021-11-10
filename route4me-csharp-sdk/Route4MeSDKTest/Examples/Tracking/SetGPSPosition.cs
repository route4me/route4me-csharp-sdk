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
        public void SetGPSPosition()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            double lat = SD10Stops_route.Addresses.Length > 1
                ? SD10Stops_route.Addresses[1].Latitude
                : 33.14384;
            double lng = SD10Stops_route.Addresses.Length > 1
                ? SD10Stops_route.Addresses[1].Longitude
                : -83.22466;
            
            // Create the gps parameters
            var gpsParameters = new GPSParameters()
            {
                Format = Format.Csv.Description(),
                RouteId = SD10Stops_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = (int)SD10Stops_route.Addresses[1].MemberId,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
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
