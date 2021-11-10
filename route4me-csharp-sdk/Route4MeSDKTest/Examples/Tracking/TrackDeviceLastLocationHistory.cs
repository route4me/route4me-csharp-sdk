using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting the last location history of a GPS device.
        /// </summary>
        public void TrackDeviceLastLocationHistory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Create GPS event record

            var tsp2days = new TimeSpan(2, 0, 0, 0);
            DateTime dtNow = DateTime.Now;

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            double lat = SD10Stops_route.Addresses.Length > 1
                ? SD10Stops_route.Addresses[1].Latitude
                : 33.14384;
            double lng = SD10Stops_route.Addresses.Length > 1
                ? SD10Stops_route.Addresses[1].Longitude
                : -83.22466;

            var gpsParameters = new GPSParameters
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

            var response = route4Me.SetGPS(gpsParameters, out string errorString);

            if (!string.IsNullOrEmpty(errorString))
            {
                Console.WriteLine("SetGps error: {0}", errorString);
                return;
            }

            Console.WriteLine("SetGps response: {0}", response.Status.ToString());

            #endregion

            var trParameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                DeviceTrackingHistory = true
            };

            var dataObject = route4Me.GetLastLocation(trParameters, out errorString);

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("TrackDeviceLastLocationHistory executed successfully");
                Console.WriteLine("");

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                Console.WriteLine("");

                dataObject.TrackingHistory.ForEach(th =>
                {
                    Console.WriteLine("Speed: {0}", th.Speed);
                    Console.WriteLine("Longitude: {0}", th.Longitude);
                    Console.WriteLine("Latitude: {0}", th.Latitude);
                    Console.WriteLine("Time Stamp: {0}", th.TimeStampFriendly);
                    Console.WriteLine("");
                });
            }
            else
            {
                Console.WriteLine("TrackDeviceLastLocationHistory error: {0}", errorString);
            }

            RemoveTestOptimizations();
        }
    }
}
