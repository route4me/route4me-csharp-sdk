using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using static Route4MeSDK.Route4MeManager;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Device History from Time Range
        /// </summary>
        public void GetDeviceHistoryTimeRange()
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

            var trParameters = new GPSParameters
            {
                Format = "json",
                RouteId = SD10Stops_route_id,
                TimePeriod = "custom",
                StartDate = R4MeUtils.ConvertToUnixTimestamp(dtNow - tsp2days),
                EndDate = R4MeUtils.ConvertToUnixTimestamp(dtNow + tsp2days)
            };

            var result = route4Me.GetDeviceLocationHistory(trParameters, out errorString);

            Console.WriteLine(
                    result != null && result.GetType() == typeof(DeviceLocationHistoryResponse )
                    ? "GetDeviceHistoryTimeRangeTest executed successfully"
                    : "GetDeviceHistoryTimeRangeTest failed. " + errorString
                );

            if (result != null && result.GetType() == typeof(DeviceLocationHistoryResponse))
            {
                Console.WriteLine("");

                var locationHistoryResul = (DeviceLocationHistoryResponse)result;

                if ((locationHistoryResul.Data?.Length ?? 0) > 0)
                {
                    foreach (var locationHistory in locationHistoryResul.Data)
                    {
                        Console.WriteLine("Location: {0}, {1}", locationHistory.Latitude, locationHistory.Longitude);
                    }
                }
            }

            RemoveTestOptimizations();
        }
    }
}
