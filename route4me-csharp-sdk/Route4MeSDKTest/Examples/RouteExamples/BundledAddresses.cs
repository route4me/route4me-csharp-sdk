using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of creating an optimization 
        /// with bundled addresses.
        /// </summary>
        public void BundledAddresses()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if (!route4Me.MemberHasCommercialCapability(ActualApiKey, DemoApiKey, out string errorString0)) return;

            // Prepare the addresses
            Address[] addresses = new Address[]
              {
                #region Addresses

                new Address() { AddressString   = "3634 W Market St, Fairlawn, OH 44333",
                                //all possible originating locations are depots, should be marked as true
                                //stylistically we recommend all depots should be at the top of the destinations list
                                IsDepot          = true,
                                Latitude         = 41.135762259364,
                                Longitude        = -81.629313826561,

                                TimeWindowStart  = null,
                                TimeWindowEnd    = null,
                                TimeWindowStart2 = null,
                                TimeWindowEnd2   = null,
                                Time             = null
                },

                new Address() { AddressString   = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                                Latitude        = 41.135762259364,
                                Longitude       = -81.629313826561,

                                //together these two specify the time window of a destination
                                //seconds offset relative to the route start time for the open availability of a destination
                                TimeWindowStart  = 6 * 3600 + 00 * 60,
                                //seconds offset relative to the route end time for the open availability of a destination
                                TimeWindowEnd    = 6 * 3600 + 30 * 60,

                                // Second 'TimeWindowStart'
                                TimeWindowStart2 = 7 * 3600 + 00 * 60,
                                // Second 'TimeWindowEnd'
                                TimeWindowEnd2   = 7 * 3600 + 20 * 60,

                                //the number of seconds at destination
                                Time             = 300
                },

                new Address() { AddressString    = "512 Florida Pl, Barberton, OH 44203",
                                Latitude         = 41.003671512008,
                                Longitude        = -81.598461046815,
                                TimeWindowStart  = 7 * 3600 + 30 * 60,
                                TimeWindowEnd    = 7 * 3600 + 40 * 60,
                                TimeWindowStart2 = 8 * 3600 + 00 * 60,
                                TimeWindowEnd2   = 8 * 3600 + 10 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "512 Florida Pl, Barberton, OH 44203",
                                Latitude         = 41.003671512008,
                                Longitude        = -81.598461046815,
                                TimeWindowStart  = 8 * 3600 + 30 * 60,
                                TimeWindowEnd    = 8 * 3600 + 40 * 60,
                                TimeWindowStart2 = 8 * 3600 + 50 * 60,
                                TimeWindowEnd2   = 9 * 3600 + 00 * 60,
                                Time             = 100
                },

                new Address() { AddressString    = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                                Latitude         = 41.162971496582,
                                Longitude        = -81.479049682617,
                                TimeWindowStart  = 9 * 3600 + 00 * 60,
                                TimeWindowEnd    = 9 * 3600 + 15 * 60,
                                TimeWindowStart2 = 9 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 9 * 3600 + 45 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "1659 Hibbard Dr, Stow, OH 44224",
                                Latitude         = 41.194505989552,
                                Longitude        = -81.443351581693,
                                TimeWindowStart  = 10 * 3600 + 00 * 60,
                                TimeWindowEnd    = 10 * 3600 + 15 * 60,
                                TimeWindowStart2 = 10 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 10 * 3600 + 45 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "2705 N River Rd, Stow, OH 44224",
                                Latitude         = 41.145240783691,
                                Longitude        = -81.410247802734,
                                TimeWindowStart  = 11 * 3600 + 00 * 60,
                                TimeWindowEnd    = 11 * 3600 + 15 * 60,
                                TimeWindowStart2 = 11 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 11 * 3600 + 45 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "10159 Bissell Dr, Twinsburg, OH 44087",
                                Latitude         = 41.340042114258,
                                Longitude        = -81.421226501465,
                                TimeWindowStart  = 12 * 3600 + 00 * 60,
                                TimeWindowEnd    = 12 * 3600 + 15 * 60,
                                TimeWindowStart2 = 12 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 12 * 3600 + 45 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "367 Cathy Dr, Munroe Falls, OH 44262",
                                Latitude         = 41.148578643799,
                                Longitude        = -81.429229736328,
                                TimeWindowStart  = 13 * 3600 + 00 * 60,
                                TimeWindowEnd    = 13 * 3600 + 15 * 60,
                                TimeWindowStart2 = 13 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 13 * 3600 + 45 * 60,
                                Time             = 300,
                                Cube             = 3
                },

                new Address() { AddressString    = "367 Cathy Dr, Munroe Falls, OH 44262",
                                Latitude         = 41.148578643799,
                                Longitude        = -81.429229736328,
                                TimeWindowStart  = 14 * 3600 + 00 * 60,
                                TimeWindowEnd    = 14 * 3600 + 15 * 60,
                                TimeWindowStart2 = 14 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 14 * 3600 + 45 * 60,
                                Time             = 300,
                                Cube             = 2
                },

                new Address() { AddressString    = "512 Florida Pl, Barberton, OH 44203",
                                Latitude         = 41.003671512008,
                                Longitude        = -81.598461046815,
                                TimeWindowStart  = 15 * 3600 + 00 * 60,
                                TimeWindowEnd    = 15 * 3600 + 15 * 60,
                                TimeWindowStart2 = 15 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 15 * 3600 + 45 * 60,
                                Time             = 300
                },

                new Address() { AddressString    = "559 W Aurora Rd, Northfield, OH 44067",
                                Latitude         = 41.315116882324,
                                Longitude        = -81.558746337891,
                                TimeWindowStart  = 16 * 3600 + 00 * 60,
                                TimeWindowEnd    = 16 * 3600 + 15 * 60,
                                TimeWindowStart2 = 16 * 3600 + 30 * 60,
                                TimeWindowEnd2   = 17 * 3600 + 00 * 60,
                                Time             = 50
                }

                #endregion
              };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "SD Multiple TW Address Bundling " + DateTime.Now.ToString("yyyy-MM-dd"),

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 5 * 3600 + 30 * 60,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                Bundling = new AddressBundling()
                {
                    Mode = AddressBundlingMode.Address,
                    MergeMode = AddressBundlingMergeMode.KeepAsSeparateDestinations,
                    ServiceTimeRules = new ServiceTimeRulesClass()
                    {
                        FirstItemMode = AddressBundlingFirstItemMode.KeepOriginal,
                        AdditionalItemsMode = AddressBundlingAdditionalItemsMode.KeepOriginal
                    }
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunOptimization(
                                        optimizationParameters, 
                                        out string errorString);
            
            OptimizationsToRemove = new List<string>()
            {
                dataObject.OptimizationProblemId
            };

            if (dataObject == null && dataObject.GetType() != typeof(DataObject))
            {
                Console.WriteLine(
                    "BundledAddresses failed" +
                    Environment.NewLine +
                    "Cannot create the optimization. " +
                    Environment.NewLine +
                    errorString
                );

                return;
            }

            if ((dataObject?.Routes?.Length ?? 0) < 1)
            {
                Console.WriteLine("The optimization doesn't contain route");
                RemoveTestOptimizations();
                return;
            }

            var routeId = dataObject.Routes[0].RouteId;

            if ((routeId?.Length ?? 0) < 32)
            {
                Console.WriteLine("The route ID is not valid");
                RemoveTestOptimizations();
                return;
            }

            var routeQueryParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                BundlingItems = true
            };

            var routeBundled = route4Me.GetRoute(routeQueryParameters, out errorString);

            if ((routeBundled?.BundleItems ?? null) == null)
            {
                Console.WriteLine("Cannot retrieve bundled items in the route response.");
                RemoveTestOptimizations();
                return;
            }

            PrintExampleRouteResult(routeBundled, errorString);

            RemoveTestOptimizations();
        }
    }
}