using System;
using System.Collections.Generic;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5
{
    public class ApiKeys
    {
        public static string ActualApiKey = R4MeUtils.ReadSetting("actualApiKey");
        public static string DemoApiKey = R4MeUtils.ReadSetting("demoApiKey");
    }

    public class TestDataRepository
    {
        private readonly string c_ApiKey = ApiKeys.ActualApiKey;

        public TestDataRepository()
        {
            c_ApiKey = ApiKeys.ActualApiKey;
        }

        public DataObject dataObjectSD10Stops { get; set; }
        public string SD10Stops_optimization_problem_id { get; set; }
        public DataObjectRoute SD10Stops_route { get; set; }
        public string SD10Stops_route_id { get; set; }
        public DataObject dataObjectSDRT { get; set; }
        public string SDRT_optimization_problem_id { get; set; }
        public DataObjectRoute SDRT_route { get; set; }
        public string SDRT_route_id { get; set; }
        public DataObject dataObjectMDMD24 { get; set; }
        public string MDMD24_optimization_problem_id { get; set; }
        public DataObjectRoute MDMD24_route { get; set; }
        public string MDMD24_route_id { get; set; }

        public bool RunOptimizationSingleDriverRoute10Stops()
        {
            var r4mm = new Route4MeManagerV5(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "151 Arbor Way Milledgeville GA 31061",
                    //indicate that this is a departure stop
                    //single depot routes can only have one departure depot 
                    IsDepot = true,

                    //required coordinates for every departure and stop on the route
                    Latitude = 33.132675170898,
                    Longitude = -83.244743347168,

                    //the expected time on site, in seconds. this value is incorporated into the optimization engine
                    //it also adjusts the estimated and dynamic eta's for a route
                    Time = 0,


                    //input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest
                    CustomFields = new Dictionary<string, string> {{"color", "red"}, {"size", "huge"}}
                },

                new Address
                {
                    AddressString = "230 Arbor Way Milledgeville GA 31061",
                    Latitude = 33.129695892334,
                    Longitude = -83.24577331543,
                    Time = 0
                },

                new Address
                {
                    AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                    Latitude = 33.143497,
                    Longitude = -83.224487,
                    Time = 0
                },

                new Address
                {
                    AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.141784667969,
                    Longitude = -83.237518310547,
                    Time = 0
                },

                new Address
                {
                    AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.141086578369,
                    Longitude = -83.238258361816,
                    Time = 0
                },

                new Address
                {
                    AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.142036437988,
                    Longitude = -83.238845825195,
                    Time = 0
                },

                new Address
                {
                    AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.14307,
                    Longitude = -83.239334,
                    Time = 0
                },

                new Address
                {
                    AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.142734527588,
                    Longitude = -83.237442016602,
                    Time = 0
                },

                new Address
                {
                    AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.143871307373,
                    Longitude = -83.237342834473,
                    Time = 0
                },

                new Address
                {
                    AddressString = "221 Blake Cir Milledgeville GA 31061",
                    Latitude = 33.081462860107,
                    Longitude = -83.208511352539,
                    Time = 0
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.TSP,
                //StoreRoute = false,
                RouteName = "Single Driver Route 10 Stops Test",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description()
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            //string errorString;

            try
            {
                dataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, out var resultResponse);

                SD10Stops_optimization_problem_id = dataObjectSD10Stops.OptimizationProblemId;
                SD10Stops_route = dataObjectSD10Stops != null &&
                                  dataObjectSD10Stops.Routes != null &&
                                  dataObjectSD10Stops.Routes.Length > 0
                    ? dataObjectSD10Stops.Routes[0]
                    : null;
                SD10Stops_route_id = SD10Stops_route != null ? SD10Stops_route.RouteID : null;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Route 10 Stops generation failed. " + ex.Message);
                return false;
            }
        }

        public bool RunSingleDriverRoundTrip()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new Address
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new Address
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    Alias = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                new Address
                {
                    AddressString = "1011 Madison Ave New York, NY 10075",
                    Alias = "Yigal Azrou'l",
                    Latitude = 40.7772129,
                    Longitude = -73.9669,
                    Time = 0
                },

                new Address
                {
                    AddressString = "440 Columbus Ave New York, NY 10024",
                    Alias = "Frank Stella Clothier",
                    Latitude = 40.7808364,
                    Longitude = -73.9732729,
                    Time = 0
                },

                new Address
                {
                    AddressString = "324 Columbus Ave #1 New York, NY 10023",
                    Alias = "Liana",
                    Latitude = 40.7803123,
                    Longitude = -73.9793079,
                    Time = 0
                },

                new Address
                {
                    AddressString = "110 W End Ave New York, NY 10023",
                    Alias = "Toga Bike Shop",
                    Latitude = 40.7753077,
                    Longitude = -73.9861529,
                    Time = 0
                },

                new Address
                {
                    AddressString = "555 W 57th St New York, NY 10019",
                    Alias = "BMW of Manhattan",
                    Latitude = 40.7718005,
                    Longitude = -73.9897716,
                    Time = 0
                },

                new Address
                {
                    AddressString = "57 W 57th St New York, NY 10019",
                    Alias = "Verizon Wireless",
                    Latitude = 40.7558695,
                    Longitude = -73.9862019,
                    Time = 0
                },

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.TSP,
                //StoreRoute = false,
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,
                RT = true,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            //string errorString;

            try
            {
                dataObjectSDRT = route4Me.RunOptimization(optimizationParameters, out var resultResponse);

                SDRT_optimization_problem_id = dataObjectSDRT.OptimizationProblemId;

                SDRT_route = dataObjectSDRT != null &&
                             dataObjectSDRT.Routes != null &&
                             dataObjectSDRT.Routes.Length > 0
                    ? dataObjectSDRT.Routes[0]
                    : null;

                SDRT_route_id = SDRT_route != null ? SDRT_route.RouteID : null;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Round Trip generation failed. " + ex.Message);
                return false;
            }
        }

        public bool RemoveOptimization(string[] optimizationProblemIDs)
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            // Run the query
            try
            {
                var removed = route4Me.RemoveOptimization(
                    optimizationProblemIDs,
                    out var resultResponse);
                return removed;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Removing of an optimization failed." + ex.Message);
                return false;
                throw;
            }
        }

        public bool MultipleDepotMultipleDriverWith24StopsTimeWindowTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "3634 W Market St, Fairlawn, OH 44333",
                    IsDepot = true,
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,
                    Time = 300,
                    TimeWindowStart = 28800,
                    TimeWindowEnd = 29465
                },

                new Address
                {
                    AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                    Latitude = 41.143505096435,
                    Longitude = -81.46549987793,
                    Time = 300,
                    TimeWindowStart = 29465,
                    TimeWindowEnd = 30529
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 300,
                    TimeWindowStart = 30529,
                    TimeWindowEnd = 33479
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    IsDepot = true,
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 300,
                    TimeWindowStart = 33479,
                    TimeWindowEnd = 33944
                },

                new Address
                {
                    AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                    Latitude = 41.162971496582,
                    Longitude = -81.479049682617,
                    Time = 300,
                    TimeWindowStart = 33944,
                    TimeWindowEnd = 34801
                },

                new Address
                {
                    AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                    Latitude = 41.194505989552,
                    Longitude = -81.443351581693,
                    Time = 300,
                    TimeWindowStart = 34801,
                    TimeWindowEnd = 36366
                },

                new Address
                {
                    AddressString = "2705 N River Rd, Stow, OH 44224",
                    Latitude = 41.145240783691,
                    Longitude = -81.410247802734,
                    Time = 300,
                    TimeWindowStart = 36366,
                    TimeWindowEnd = 39173
                },

                new Address
                {
                    AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                    Latitude = 41.340042114258,
                    Longitude = -81.421226501465,
                    Time = 300,
                    TimeWindowStart = 39173,
                    TimeWindowEnd = 41617
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
                    Time = 300,
                    TimeWindowStart = 41617,
                    TimeWindowEnd = 43660
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148579,
                    Longitude = -81.42923,
                    Time = 300,
                    TimeWindowStart = 43660,
                    TimeWindowEnd = 46392
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 300,
                    TimeWindowStart = 46392,
                    TimeWindowEnd = 48089
                },

                new Address
                {
                    AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                    Latitude = 41.315116882324,
                    Longitude = -81.558746337891,
                    Time = 300,
                    TimeWindowStart = 48089,
                    TimeWindowEnd = 48449
                },

                new Address
                {
                    AddressString = "3933 Klein Ave, Stow, OH 44224",
                    Latitude = 41.169467926025,
                    Longitude = -81.429420471191,
                    Time = 300,
                    TimeWindowStart = 48449,
                    TimeWindowEnd = 50152
                },

                new Address
                {
                    AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                    Latitude = 41.136692047119,
                    Longitude = -81.493492126465,
                    Time = 300,
                    TimeWindowStart = 50152,
                    TimeWindowEnd = 51682
                },

                new Address
                {
                    AddressString = "3731 Osage St, Stow, OH 44224",
                    Latitude = 41.161357879639,
                    Longitude = -81.42293548584,
                    Time = 300,
                    TimeWindowStart = 51682,
                    TimeWindowEnd = 54379
                },

                new Address
                {
                    AddressString = "3862 Klein Ave, Stow, OH 44224",
                    Latitude = 41.167895123363,
                    Longitude = -81.429973393679,
                    Time = 300,
                    TimeWindowStart = 54379,
                    TimeWindowEnd = 54879
                },

                new Address
                {
                    AddressString = "138 Northwood Ln, Tallmadge, OH 44278",
                    Latitude = 41.085464134812,
                    Longitude = -81.447411775589,
                    Time = 300,
                    TimeWindowStart = 54879,
                    TimeWindowEnd = 56613
                },

                new Address
                {
                    AddressString = "3401 Saratoga Blvd, Stow, OH 44224",
                    Latitude = 41.148849487305,
                    Longitude = -81.407363891602,
                    Time = 300,
                    TimeWindowStart = 56613,
                    TimeWindowEnd = 57052
                },

                new Address
                {
                    AddressString = "5169 Brockton Dr, Stow, OH 44224",
                    Latitude = 41.195003509521,
                    Longitude = -81.392700195312,
                    Time = 300,
                    TimeWindowStart = 57052,
                    TimeWindowEnd = 59004
                },

                new Address
                {
                    AddressString = "5169 Brockton Dr, Stow, OH 44224",
                    Latitude = 41.195003509521,
                    Longitude = -81.392700195312,
                    Time = 300,
                    TimeWindowStart = 59004,
                    TimeWindowEnd = 60027
                },

                new Address
                {
                    AddressString = "458 Aintree Dr, Munroe Falls, OH 44262",
                    Latitude = 41.1266746521,
                    Longitude = -81.445808410645,
                    Time = 300,
                    TimeWindowStart = 60027,
                    TimeWindowEnd = 60375
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 300,
                    TimeWindowStart = 60375,
                    TimeWindowEnd = 63891
                },

                new Address
                {
                    AddressString = "2299 Tyre Dr, Hudson, OH 44236",
                    Latitude = 41.250511169434,
                    Longitude = -81.420433044434,
                    Time = 300,
                    TimeWindowStart = 63891,
                    TimeWindowEnd = 65277
                },

                new Address
                {
                    AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                    Latitude = 41.136692047119,
                    Longitude = -81.493492126465,
                    Time = 300,
                    TimeWindowStart = 65277,
                    TimeWindowEnd = 68545
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window",
                //StoreRoute = false,

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 5,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Matrix
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            try
            {
                dataObjectMDMD24 = route4Me.RunOptimization(optimizationParameters, out var resultResponse);

                MDMD24_route_id = dataObjectMDMD24 != null &&
                                  dataObjectMDMD24.Routes != null &&
                                  dataObjectMDMD24.Routes.Length > 0
                    ? dataObjectMDMD24.Routes[0].RouteID
                    : null;

                MDMD24_optimization_problem_id = dataObjectMDMD24.OptimizationProblemId;

                MDMD24_route = dataObjectMDMD24 != null &&
                               dataObjectMDMD24.Routes != null &&
                               dataObjectMDMD24.Routes.Length > 0
                    ? dataObjectMDMD24.Routes[0]
                    : null;

                MDMD24_route_id = MDMD24_route != null ? MDMD24_route.RouteID : null;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Generation of the Multiple Depot, Multiple Driver with 24 Stops optimization problem failed. " +
                    ex.Message);
                return false;
            }
        }

        public bool RemoveAddressBookContacts(List<int> lsRemLocations, string ApiKey)
        {
            var route4Me = new Route4MeManagerV5(ApiKey);

            if (lsRemLocations.Count > 0)
            {
                var removed = route4Me.RemoveAddressBookContacts(
                    lsRemLocations.ToArray(),
                    out var resultResponse);

                return removed;
            }

            return false;
        }

        public bool RemoveOrders(List<string> lsOrders, string ApiKey)
        {
            var route4Me = new Route4MeManager(ApiKey);

            // Run the query
            var removed = route4Me.RemoveOrders(lsOrders.ToArray(), out var errorString);

            return removed;
        }
    }
}