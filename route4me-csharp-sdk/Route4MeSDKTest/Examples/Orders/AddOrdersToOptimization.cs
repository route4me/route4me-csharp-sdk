using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
	public sealed partial class Route4MeExamples
	{
        /// <summary>
        /// Add Orders to an Optimization Problem object
        /// </summary>
        /// <returns> Optimization Problem object </returns>
        public void AddOrdersToOptimization()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var rQueryParams = new OptimizationParameters()
            {
                OptimizationProblemID = SD10Stops_optimization_problem_id,
                Redirect = false
            };

            var lsTimeWindowStart = new List<int>();

            var dtCurDate = DateTime.Now + (new TimeSpan(1, 0, 0, 0));
            dtCurDate = new DateTime(dtCurDate.Year, dtCurDate.Month, dtCurDate.Day, 8, 0, 0);

            var tsp1000sec = new TimeSpan(0, 0, 1000);
            var tsp7days = new TimeSpan(7, 0, 0, 0);

            lsTimeWindowStart.Add((int)R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int)R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int)R4MeUtils.ConvertToUnixTimestamp(dtCurDate));

            #region Addresses
            Address[] addresses = new Address[] {
            new Address {
                AddressString = "273 Canal St, New York, NY 10013, USA",
                Latitude = 40.7191558,
                Longitude = -74.0011966,
                Alias = "",
                CurbsideLatitude = 40.7191558,
                CurbsideLongitude = -74.0011966,
                IsDepot = true
            },
            new Address {
                AddressString = "106 Liberty St, New York, NY 10006, USA",
                Alias = "BK Restaurant #: 2446",
                Latitude = 40.709637,
                Longitude = -74.011912,
                CurbsideLatitude = 40.709637,
                CurbsideLongitude = -74.011912,
                Email = "",
                Phone = "(917) 338-1887",
                FirstName = "",
                LastName = "",
                CustomFields = new Dictionary<string, string> { {"icon", null} },
                Time = 0,
                TimeWindowStart = lsTimeWindowStart[0],
                TimeWindowEnd = lsTimeWindowStart[0]+300,
                OrderId = 7205705
            },
            new Address {
                AddressString = "325 Broadway, New York, NY 10007, USA",
                Alias = "BK Restaurant #: 20333",
                Latitude = 40.71615,
                Longitude = -74.00505,
                CurbsideLatitude = 40.71615,
                CurbsideLongitude = -74.00505,
                Email = "",
                Phone = "(212) 227-7535",
                FirstName = "",
                LastName = "",
                CustomFields = new Dictionary<string, string> { {"icon", null} },
                Time = 0,
                TimeWindowStart = lsTimeWindowStart[1],
                TimeWindowEnd = lsTimeWindowStart[1]+300,
                OrderId = 7205704
            },
            new Address {
                AddressString = "106 Fulton St, Farmingdale, NY 11735, USA",
                Alias = "BK Restaurant #: 17871",
                Latitude = 40.73073,
                Longitude = -73.459283,
                CurbsideLatitude = 40.73073,
                CurbsideLongitude = -73.459283,
                Email = "",
                Phone = "(212) 566-5132",
                FirstName = "",
                LastName = "",
                CustomFields = new Dictionary<string, string> { {"icon", null} },
                Time = 0,
                TimeWindowStart = lsTimeWindowStart[2],
                TimeWindowEnd = lsTimeWindowStart[2]+300,
                OrderId = 7205703
            }
        };
            #endregion

            var rParams = new RouteParameters()
            {
                RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now + tsp7days),
                RouteTime = 14400,
                Optimize = "Time",
                AlgorithmType = AlgorithmType.TSP,
                RT = false,
                LockLast = false,
                VehicleId = "",
                DisableOptimization = false
            };

            var dataObject = route4Me.AddOrdersToOptimization(
                rQueryParams,
                addresses,
                rParams, out string errorString);

            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
