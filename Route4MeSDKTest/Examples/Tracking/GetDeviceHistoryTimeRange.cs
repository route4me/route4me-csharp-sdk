using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Device History from Time Range
        /// </summary>
        public void GetDeviceHistoryTimeRange(string routeId)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            int uStartTime = 0;
            int uEndTime = 0;
            uStartTime = (int)(new DateTime(2016, 10, 20, 0, 0, 0) - (new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;
            uEndTime = (int)(new DateTime(2016, 10, 26, 23, 59, 59) - (new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;

            GPSParameters gpsParameters = new GPSParameters
            {
                Format = "csv",
                RouteId = routeId,
                TimePeriod = "custom",
                StartDate = uStartTime,
                EndDate = uEndTime
            };

            string errorString = "";
            var response = route4Me.SetGPS(gpsParameters, out errorString);

            if (!string.IsNullOrEmpty(errorString))
            {
                Console.WriteLine("SetGps error: {0}", errorString);
                return;
            }

            Console.WriteLine("SetGps response: {0}", response.Status.ToString());

            GenericParameters genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("route_id", routeId);
            genericParameters.ParametersCollection.Add("device_tracking_history", "1");

            var dataObject = route4Me.GetLastLocation(genericParameters, out errorString);

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("GetDeviceHistoryTimeRange executed successfully");
                Console.WriteLine("");

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                Console.WriteLine("");
                foreach (TrackingHistory th in dataObject.TrackingHistory)
                {
                    Console.WriteLine("Speed: {0}", th.Speed);
                    Console.WriteLine("Longitude: {0}", th.Longitude);
                    Console.WriteLine("Latitude: {0}", th.Latitude);
                    Console.WriteLine("Time Stamp: {0}", th.TimeStampFriendly);
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("GetDeviceHistoryTimeRange error: {0}", errorString);
            }
        }
    }
}

