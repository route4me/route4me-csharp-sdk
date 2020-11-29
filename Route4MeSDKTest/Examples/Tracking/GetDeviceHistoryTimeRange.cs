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
        public void GetDeviceHistoryTimeRange()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            DateTime zeroTime = new DateTime(1970, 1, 1, 0, 0, 0);

            int uStartTime = (int)(new DateTime(2016, 10, 20, 0, 0, 0) - zeroTime).TotalSeconds;
            int uEndTime = (int)(new DateTime(2026, 10, 26, 23, 59, 59) - zeroTime).TotalSeconds;

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            var gpsParameters = new GPSParameters
            {
                Format = "csv",
                RouteId = SD10Stops_route_id,
                TimePeriod = "custom",
                StartDate = uStartTime,
                EndDate = uEndTime
            };

            var response = route4Me.SetGPS(gpsParameters, out string errorString);

            if (!string.IsNullOrEmpty(errorString))
            {
                Console.WriteLine("SetGps error: {0}", errorString);
                return;
            }

            Console.WriteLine("SetGps response: {0}", response.Status.ToString());

            GenericParameters genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("route_id", SD10Stops_route_id);
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

            RemoveTestOptimizations();
        }
    }
}

