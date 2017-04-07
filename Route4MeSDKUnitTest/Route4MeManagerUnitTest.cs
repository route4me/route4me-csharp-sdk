using Moq;
using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDKUnitTest
{
    [TestClass]
    public class Route4MeManagerUnitTest
    {
        DataObject dataObject1;
        static DataObjectRoute routeSingleDriverRoute10Stops;
        static string routeId_SingleDriverRoute10Stops;
        static int removeRouteDestinationID;

        static string api_key { get; set; }

        [TestMethod]
        public void RunOptimizationSingleDriverRoute10StopsTest()
        {
            api_key = "11111111111111111111111111111111";
            Route4MeManager r4mm = new Route4MeManager(api_key);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
            #region Addresses

            new Address() { AddressString = "151 Arbor Way Milledgeville GA 31061",
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
                            CustomFields = new Dictionary<string, string>() {{"color", "red"}, {"size", "huge"}}
            },

            new Address() { AddressString = "230 Arbor Way Milledgeville GA 31061",
                            Latitude = 33.129695892334,
                            Longitude = -83.24577331543,
                            Time = 0 },

            new Address() { AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                            Latitude = 33.143497,
                            Longitude = -83.224487,
                            Time = 0 },

            new Address() { AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141784667969,
                            Longitude = -83.237518310547,
                            Time = 0 },

            new Address() { AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141086578369,
                            Longitude = -83.238258361816,
                            Time = 0 },

            new Address() { AddressString =  "131 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142036437988,
                            Longitude = -83.238845825195,
                            Time = 0 },

            new Address() { AddressString =  "138 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.14307,
                            Longitude = -83.239334,
                            Time = 0 },

            new Address() { AddressString =  "139 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142734527588,
                            Longitude = -83.237442016602,
                            Time = 0 },

            new Address() { AddressString =  "145 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.143871307373,
                            Longitude = -83.237342834473,
                            Time = 0 },

            new Address() { AddressString =  "221 Blake Cir Milledgeville GA 31061",
                            Latitude = 33.081462860107,
                            Longitude = -83.208511352539,
                            Time = 0 }

            #endregion
            };

            // Set parameters
            RouteParameters parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                StoreRoute = false,
                RouteName = "Single Driver Route 10 Stops",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description()
            };

            OptimizationParameters optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            string errorString;
            dataObject1 = r4mm.RunOptimization(optimizationParameters, out errorString);

            Assert.IsNotNull(dataObject1, "Run optimization test with Single Driver Route 10 Stops failed...");

            routeSingleDriverRoute10Stops = (dataObject1 != null && dataObject1.Routes != null && dataObject1.Routes.Length > 0) ? dataObject1.Routes[0] : null;

            Assert.IsNotNull(routeSingleDriverRoute10Stops, "Run optimization test with Single Driver Route 10 Stops failed...");
            
            routeId_SingleDriverRoute10Stops = (routeSingleDriverRoute10Stops != null) ? routeSingleDriverRoute10Stops.RouteID : null;

            Assert.IsInstanceOfType(routeId_SingleDriverRoute10Stops,typeof(System.String));

            Assert.IsNotNull(routeId_SingleDriverRoute10Stops, "The optimization problem ID is wrong in Single Driver Route 10 Stops test...");
            
        }

        [TestMethod]
        public void ResequenceRouteDestinationsTest( )
        {
            DataObjectRoute route = routeSingleDriverRoute10Stops;
            Assert.IsNotNull(route, "Route for the test Route Destinations Resequence is null...");

            Route4MeManager route4Me = new Route4MeManager(api_key);

            AddressesOrderInfo addressesOrderInfo = new AddressesOrderInfo();
            addressesOrderInfo.RouteId = route.RouteID;
            addressesOrderInfo.Addresses = new AddressInfo[0];
            for (int i = 0; i < route.Addresses.Length; i++)
            {
                Address address = route.Addresses[i];
                AddressInfo addressInfo = new AddressInfo();
                addressInfo.DestinationId = address.RouteDestinationId.Value;
                addressInfo.SequenceNo = i;
                if (i == 1)
                    addressInfo.SequenceNo = 2;
                else if (i == 2)
                    addressInfo.SequenceNo = 1;
                addressInfo.IsDepot = (addressInfo.SequenceNo == 0);
                List<AddressInfo> addressesList = new List<AddressInfo>(addressesOrderInfo.Addresses);
                addressesList.Add(addressInfo);
                addressesOrderInfo.Addresses = addressesList.ToArray();
            }

            string errorString1 = "";
            DataObjectRoute route1 = route4Me.GetJsonObjectFromAPI<DataObjectRoute>(addressesOrderInfo,
                                                                                                R4MEInfrastructureSettings.RouteHost,
                                                                                                HttpMethodType.Put,
                                                                                                out errorString1);
            Assert.IsNotNull(route1, "ResequenceRouteDestinationsTest failed...");
        }

        [TestMethod]
        public void ResequenceReoptimizeRouteTest()
        {
            Route4MeManager route4Me = new Route4MeManager(api_key);

            string route_id = routeId_SingleDriverRoute10Stops;

            Assert.IsNotNull(route_id, "rote_id is null...");

            Dictionary<string, string> roParameters = new Dictionary<string, string>()
            {
                {"route_id",route_id},
                {"disable_optimization","0"},
                {"optimize","Distance"},
            };

            // Run the query
            string errorString;
            bool result = route4Me.ResequenceReoptimizeRoute(roParameters, out errorString);

            Assert.IsTrue(result, "ResequenceReoptimizeRouteTest failed...");
        }

        [TestMethod]
        public void AddRouteDestinationsTest()
        {
            Route4MeManager route4Me = new Route4MeManager(api_key);

            string route_id = routeId_SingleDriverRoute10Stops;

            Assert.IsNotNull(route_id, "rote_id is null...");

            // Prepare the addresses
            #region Addresses
            Address[] addresses = new Address[]
            {
                new Address() { AddressString =  "146 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude = 	33.143526,
                                Longitude = -83.240354,
                                Time = 0 },

                new Address() { AddressString =  "222 Blake Cir Milledgeville GA 31061",
                                Latitude = 	33.177852,
                                Longitude = -83.263535,
                                Time = 0 }
            };
            #endregion

            // Run the query
            bool optimalPosition = true;
            string errorString;
            int[] destinationIds = route4Me.AddRouteDestinations(route_id, addresses, optimalPosition, out errorString);

            Assert.IsInstanceOfType(destinationIds, typeof(System.Int32[]), "AddRouteDestinationsTest failed...");

            removeRouteDestinationID = destinationIds[0];
        }

        [TestMethod]
        public void RemoveRouteDestinationTest()
        {
            Route4MeManager route4Me = new Route4MeManager(api_key);

            string route_id = routeId_SingleDriverRoute10Stops;
            Assert.IsNotNull(route_id, "rote_id is null...");

            int destination_id = removeRouteDestinationID;
            Assert.IsNotNull(destination_id, "destination_id is null...");

            // Run the query
            string errorString;
            bool deleted = route4Me.RemoveRouteDestination(route_id, destination_id, out errorString);

            Assert.IsTrue(deleted, "RemoveRouteDestinationTest");
        }
    }

    [DataContract]
    class AddressInfo : GenericParameters
    {
        [DataMember(Name = "route_destination_id")]
        public int DestinationId { get; set; }

        [DataMember(Name = "sequence_no")]
        public int SequenceNo { get; set; }

        [DataMember(Name = "is_depot")]
        public bool IsDepot { get; set; }
    }

    [DataContract]
    class AddressesOrderInfo : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        [DataMember(Name = "addresses")]
        public AddressInfo[] Addresses { get; set; }
    }
}
