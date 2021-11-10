using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Orders to a Route
        /// </summary>
        /// <returns> Route object </returns>
        public void AddOrdersToRoute()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var rQueryParams = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                Redirect = false
            };

            #region Addresses
            Address[] addresses = new Address[] {
                new Address {
                    AddressString = "273 Canal St, New York, NY 10013, USA",
                    Latitude = 40.7191558,
                    Longitude = -74.0011966,
                    Alias = "",
                    CurbsideLatitude = 40.7191558,
                    CurbsideLongitude = -74.0011966
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
                    CustomFields = new Dictionary<string, string> { {
                        "icon",
                        null
                    } },
                    Time = 0,
                    OrderId = 7205705
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
                    CustomFields = new Dictionary<string, string> { {
                        "icon",
                        null
                    } },
                    Time = 0,
                    OrderId = 7205703
                }
            };
            #endregion

            var rParams = new RouteParameters()
            {
                RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
                RouteDate = 1465948800,
                RouteTime = 14400,
                Optimize = "Time",
                AlgorithmType = AlgorithmType.TSP,
                RT = false,
                LockLast = false,
                VehicleId = "",
                DisableOptimization = false
            };

            var result = route4Me.AddOrdersToRoute(
                rQueryParams,
                addresses,
                rParams, out string errorString);

            PrintExampleRouteResult(result, errorString);

            RemoveTestOptimizations();
        }
    }
}
