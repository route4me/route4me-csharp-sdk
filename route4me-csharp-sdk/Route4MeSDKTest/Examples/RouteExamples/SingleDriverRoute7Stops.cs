using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void SingleDriverRoute7Stops()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
        #region Addresses

        new Address() { AddressString = "128 Woodland Dr, Stafford, VA 22556",
                        IsDepot = true,
                        Alias = "HQ",
                        Latitude = 38.5022586,
                        Longitude = -77.5402276
        },

        new Address() { AddressString = "2232 Aquia Dr, Stafford, VA 22554",
                        Latitude = 38.4613311,
                        Longitude = -77.3733942,
                        IsDepot = false,
                        Alias = "1",
                        TimeWindowStart = 32400,
                        TimeWindowEnd = 82800
        },

       new Address() { AddressString = "94 The Vance Way, Fredericksburg, VA 22405",
                        Latitude = 38.343827,
                        Longitude = -77.358127,
                        IsDepot = false,
                        Alias = "2",
                        TimeWindowStart = 32400,
                        TimeWindowEnd = 82800
                        },

        new Address() { AddressString = "3 Edgewood Circle, Fredericksburg, VA 22405",
                        Latitude = 38.3560299,
                        Longitude = -77.44275,
                        IsDepot = false,
                        Alias = "3",
                        TimeWindowStart = 32400,
                        TimeWindowEnd = 82800
                         },

        new Address() { AddressString =  "609 Jett St, Fredericksburg, VA 22405",
                        Latitude = 38.321677,
                        Longitude = -77.434507,
                        IsDepot = false,
                        Alias = "4",
                        TimeWindowStart = 39600,
                        TimeWindowEnd = 82800
                        },

        new Address() { AddressString =  "1120 Potomac Ave, Fredericksburg, VA 22405",
                        Latitude = 38.3115498,
                        Longitude = -77.4349647,
                        Alias = "5",
                        TimeWindowStart = 39600,
                        TimeWindowEnd = 82800
                         },

        new Address() { AddressString =  "10809 Stacy Run, Fredericksburg, VA 22408",
                        Latitude = 38.258764,
                        Longitude = -77.425318,
                         Alias = "6",
                        TimeWindowStart = 39600,
                        TimeWindowEnd = 82800
                        }
        #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "Test for equal sequences Single Driver Route 7 Stops",
                DisableOptimization = false,
                MemberId = 403634,
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 24 * 3600,
                TravelMode = TravelMode.Driving.Description(),
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,
                RT = false,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description()
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(
                                                optimizationParameters,
                                                out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
