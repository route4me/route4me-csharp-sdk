using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.FastProcessing;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using static Route4MeSDK.Route4MeManager;
using Address = Route4MeSDK.DataTypes.Address;
using AddressBookContact = Route4MeSDK.DataTypes.AddressBookContact;
using AddressBookContactsResponse = Route4MeSDK.DataTypes.AddressBookContactsResponse;
using AddressBundlingAdditionalItemsMode = Route4MeSDK.DataTypes.AddressBundlingAdditionalItemsMode;
using AddressBundlingFirstItemMode = Route4MeSDK.DataTypes.AddressBundlingFirstItemMode;
using AddressBundlingMergeMode = Route4MeSDK.DataTypes.AddressBundlingMergeMode;
using AddressBundlingMode = Route4MeSDK.DataTypes.AddressBundlingMode;
using AddressStopType = Route4MeSDK.DataTypes.AddressStopType;
using AlgorithmType = Route4MeSDK.DataTypes.AlgorithmType;
using DataObject = Route4MeSDK.DataTypes.DataObject;
using DataObjectRoute = Route4MeSDK.DataTypes.DataObjectRoute;
using DeviceType = Route4MeSDK.DataTypes.DeviceType;
using DistanceUnit = Route4MeSDK.DataTypes.DistanceUnit;
using Format = Route4MeSDK.DataTypes.Format;
using Metric = Route4MeSDK.DataTypes.Metric;
using Optimize = Route4MeSDK.DataTypes.Optimize;
using RouteParameters = Route4MeSDK.DataTypes.RouteParameters;
using RoutePathOutput = Route4MeSDK.DataTypes.RoutePathOutput;
using SlowdownParams = Route4MeSDK.DataTypes.SlowdownParams;
using StatusResponse = Route4MeSDK.DataTypes.StatusResponse;
using StatusUpdateType = Route4MeSDK.DataTypes.StatusUpdateType;
using TerritoryType = Route4MeSDK.DataTypes.TerritoryType;
using TravelMode = Route4MeSDK.DataTypes.TravelMode;

namespace Route4MeSDKUnitTest
{
    public class ApiKeys
    {
        public static string ActualApiKey = R4MeUtils.ReadSetting("actualApiKey");
        public static string DemoApiKey = R4MeUtils.ReadSetting("demoApiKey");
    }

    [TestClass]
    public class RoutesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository tdr;
        private static TestDataRepository tdr2;
        private static List<string> lsOptimizationIDs;
        private static List<string> lsVehicleIDs;

        [ClassInitialize]
        public static void RoutesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(context, "Initialization of the class RoutesGroup failed");

            lsOptimizationIDs = new List<string>();
            lsVehicleIDs = new List<string>();

            tdr = new TestDataRepository();
            tdr2 = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();
            var result2 = tdr2.RunOptimizationSingleDriverRoute10Stops();
            var result3 = tdr2.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");
            Assert.IsTrue(result2, "Single Driver 10 Stops generation failed.");

            Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.IsTrue(tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id);
            lsOptimizationIDs.Add(tdr2.SD10Stops_optimization_problem_id);
            lsOptimizationIDs.Add(tdr2.SDRT_optimization_problem_id);

            var result1 = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result1, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id);
        }

        [TestMethod]
        public void GetRoutesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 20,
                Offset = 0
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsInstanceOfType(
                dataObjects,
                typeof(DataObjectRoute[]),
                "GetRoutesTest failed. " + errorString);
        }

        [TestMethod]
        public void GetRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                RouteId = tdr.SD10Stops_route_id
            };

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void GetRoutesByIDsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            #region // Retrieve first 3 routes

            var routesParameters = new RouteParametersQuery
            {
                Offset = 0,
                Limit = 3
            };

            var threeRoutes = route4Me.GetRoutes(routesParameters, out var errorString);

            #endregion

            #region // Retrieve 2 route by their IDs

            var routeParameters = new RouteParametersQuery
            {
                RouteId = threeRoutes[0].RouteId + "," + threeRoutes[1].RouteId
            };

            var twoRoutes = route4Me.GetRoutes(routeParameters, out errorString);

            #endregion

            Assert.IsInstanceOfType(
                twoRoutes,
                typeof(DataObjectRoute[]),
                "GetRoutesByIDsTest failed");

            Assert.IsTrue(twoRoutes.Length == 2, "GetRoutesByIDsTest failed");
        }

        [TestMethod]
        public void GetRoutesFromDateRangeTest()
        {
            if (c_ApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                StartDate = "2019-08-01",
                EndDate = "2019-08-05"
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsInstanceOfType(
                dataObjects,
                typeof(DataObjectRoute[]),
                "GetRoutesFromDateRangeTest failed. " + errorString);
        }

        [TestMethod]
        public void GetRouteDirectionsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Directions = true
            };

            routeParameters.Directions = true;

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRouteDirectionsTest failed. " + errorString);
        }

        [TestMethod]
        public void GetRoutePathPointsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId
            };

            routeParameters.RoutePathOutput = RoutePathOutput.Points.ToString();

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRoutePathPointsTest failed. " + errorString);
        }

        [TestMethod]
        public void ResequenceRouteDestinationsTest()
        {
            var route = tdr.SD10Stops_route;
            Assert.IsNotNull(
                route,
                "Route for the test Route Destinations Resequence is null.");

            var route4Me = new Route4MeManager(c_ApiKey);

            var rParams = new RouteParametersQuery
            {
                RouteId = route.RouteId
            };

            var lsAddresses = new List<Address>();
            var address1 = route.Addresses[2];
            var address2 = route.Addresses[3];

            address1.SequenceNo = 4;
            address2.SequenceNo = 3;

            lsAddresses.Add(address1);
            lsAddresses.Add(address2);

            var route1 = route4Me.ManuallyResequenceRoute(
                rParams,
                lsAddresses.ToArray(),
                out var errorString);

            Assert.IsNotNull(route1, "ResequenceRouteDestinationsTest failed. " + errorString);
        }

        [TestMethod]
        public void ResequenceReoptimizeRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = tdr.SD10Stops_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            var routeParams = new RouteParametersQuery
            {
                RouteId = route_id,
                ReOptimize = true,
                Remaining = false,
                DeviceType = DeviceType.Web.Description()
            };

            // Run the query
            var result = route4Me.ReoptimizeRoute(routeParams, out var errorString);

            Assert.IsNotNull(result, "ResequenceReoptimizeRouteTest failed.");

            Assert.IsInstanceOfType(
                result,
                typeof(DataObjectRoute),
                "ResequenceReoptimizeRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void ReoptimizeRemainingStopsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route = tdr2.SDRT_route;

            var visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int) route.Addresses[1].RouteDestinationId,
                IsVisited = true
            };

            var result = route4Me.MarkAddressVisited(visitedParams, out var errorString);

            Assert.IsNotNull(result, "MarkAddressVisitedTest. " + errorString);

            visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int) route.Addresses[2].RouteDestinationId,
                IsVisited = true
            };

            result = route4Me.MarkAddressVisited(visitedParams, out errorString);

            Assert.IsNotNull(result, "MarkAddressVisitedTest. " + errorString);

            var routeParameters = new RouteParametersQuery
            {
                RouteId = route.RouteId,
                ReOptimize = true,
                Remaining = true
            };

            var updatedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            Assert.IsNotNull(updatedRoute, "Cannot update the route " + route.RouteId);
        }

        [TestMethod]
        public void UnlinkRouteFromOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr2.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeDuplicateParameters = new RouteParametersQuery
            {
                DuplicateRoutesId = new[] {routeId}
            };

            // Run the query
            var duplicateResult = route4Me.DuplicateRoute(routeDuplicateParameters, out var errorString);

            Assert.IsNotNull(duplicateResult, $"Cannot duplicate a route {routeId ?? "nll"}. " + errorString);
            Assert.IsTrue(duplicateResult.Status, $"Cannot duplicate a route {routeId ?? "nll"}.");
            Assert.IsTrue((duplicateResult?.RouteIDs?.Length ?? 0) > 0,
                $"Cannot duplicate a route {routeId ?? "nll"}.");

            Thread.Sleep(5000);

            var duplicatedRoute = route4Me.GetRoute(
                new RouteParametersQuery {RouteId = duplicateResult.RouteIDs[0]},
                out errorString);

            Assert.IsNotNull(duplicatedRoute, "Cannot retrieve the duplicated route.");
            Assert.IsInstanceOfType(
                duplicatedRoute,
                typeof(DataObjectRoute),
                "Cannot retrieve the duplicated route.");
            //Assert.IsNotNull(duplicatedRoute.OptimizationProblemId, "Optimization problem ID of the duplicated route is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = duplicateResult.RouteIDs[0],
                UnlinkFromMasterOptimization = true
            };

            // Run the query
            var unlinkedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            Assert.IsNotNull(
                unlinkedRoute,
                "UnlinkRouteFromOptimizationTest failed. " + errorString + Environment.NewLine + "Route ID: " +
                routeId);
            Assert.IsNull(
                unlinkedRoute?.OptimizationProblemId ?? null,
                "Optimization problem ID of the unlinked route is not null.");
        }

        [TestMethod]
        public void UpdateRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var parametersNew = new RouteParameters
            {
                RouteName = "New name of the route",
                Metric = Metric.Manhattan
            };

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = parametersNew
            };

            var dataObject = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "UpdateRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateWholeRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr2.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone(tdr2.SD10Stops_route);

            #region // Notes, Custom Type Notes, Note File Uploading

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out var errorString5);

            var allCustomNotes =
                customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[])
                    ? (CustomNoteType[]) customNotesResponse
                    : null;

            string tempFilePath = null;

            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    Console.WriteLine(tempFilePath);

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            tdr2.SD10Stops_route.Addresses[1].Notes = new[]
            {
                new AddressNote
                {
                    NoteId = -1,
                    RouteId = tdr2.SD10Stops_route.RouteId,
                    Latitude = tdr2.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = tdr2.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content",
                    CustomTypes = allCustomNotes.Length > 0
                        ? new[]
                        {
                            new AddressCustomNote
                            {
                                NoteCustomTypeID = allCustomNotes[0].NoteCustomTypeID.ToString(),
                                NoteCustomValue = allCustomNotes[0].NoteCustomTypeValues[0]
                            }
                        }
                        : null,
                    UploadUrl = tempFilePath
                }
            };

            var updatedRoute0 = route4Me.UpdateRoute(
                tdr2.SD10Stops_route,
                initialRoute,
                out var errorString0);

            Assert.IsTrue(
                updatedRoute0.Addresses[1].Notes.Length == 1,
                "UpdateRouteTest failed: cannot create a note");

            if (allCustomNotes.Length > 0)
                Assert.IsTrue(
                    updatedRoute0.Addresses[1].Notes[0].CustomTypes.Length == 1,
                    "UpdateRouteTest failed: cannot create a custom type note");

            Assert.IsTrue(
                updatedRoute0.Addresses[1].Notes[0].UploadId.Length == 32,
                "UpdateRouteTest failed: cannot create a custom type note");

            #endregion

            tdr2.SD10Stops_route.ApprovedForExecution = true;
            tdr2.SD10Stops_route.Parameters.RouteName += " Edited";
            tdr2.SD10Stops_route.Parameters.Metric = Metric.Manhattan;

            tdr2.SD10Stops_route.Addresses[1].AddressString += " Edited";
            tdr2.SD10Stops_route.Addresses[1].Group = "Example Group";
            tdr2.SD10Stops_route.Addresses[1].CustomerPo = "CPO 456789";
            tdr2.SD10Stops_route.Addresses[1].InvoiceNo = "INO 789654";
            tdr2.SD10Stops_route.Addresses[1].ReferenceNo = "RNO 313264";
            tdr2.SD10Stops_route.Addresses[1].OrderNo = "ONO 654878";
            tdr2.SD10Stops_route.Addresses[1].Notes = new[]
            {
                new AddressNote
                {
                    RouteDestinationId = -1,
                    RouteId = tdr.SD10Stops_route.RouteId,
                    Latitude = tdr.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = tdr.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content"
                }
            };

            tdr2.SD10Stops_route.Addresses[2].SequenceNo = 5;
            var addressID = tdr2.SD10Stops_route.Addresses[2].RouteDestinationId;

            var dataObject = route4Me.UpdateRoute(tdr2.SD10Stops_route, initialRoute, out var errorString);

            Assert.IsTrue(dataObject.Addresses.Where(x => x.RouteDestinationId == addressID)
                .FirstOrDefault()
                .SequenceNo == 5, "UpdateWholeRouteTest failed  Cannot resequence addresses");

            Assert.IsTrue(
                tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to true");
            Assert.IsNotNull(
                dataObject,
                "UpdateRouteTest failed. " + errorString);
            Assert.IsTrue(
                dataObject.Parameters.RouteName.Contains("Edited"),
                "UpdateRouteTest failed, the route name not changed.");
            Assert.IsTrue(
                dataObject.Addresses[1].AddressString.Contains("Edited"),
                "UpdateRouteTest failed, second address name not changed.");

            Assert.IsTrue(
                dataObject.Addresses[1].Group == "Example Group",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].CustomerPo == "CPO 456789",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].InvoiceNo == "INO 789654",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].ReferenceNo == "RNO 313264",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].OrderNo == "ONO 654878",
                "UpdateWholeRouteTest failed.");

            initialRoute = R4MeUtils.ObjectDeepClone(tdr2.SD10Stops_route);

            tdr2.SD10Stops_route.ApprovedForExecution = false;
            tdr2.SD10Stops_route.Addresses[1].Group = null;
            tdr2.SD10Stops_route.Addresses[1].CustomerPo = null;
            tdr2.SD10Stops_route.Addresses[1].InvoiceNo = null;
            tdr2.SD10Stops_route.Addresses[1].ReferenceNo = null;
            tdr2.SD10Stops_route.Addresses[1].OrderNo = null;

            dataObject = route4Me.UpdateRoute(tdr2.SD10Stops_route, initialRoute, out errorString);

            Assert.IsFalse(
                tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to false");
            Assert.IsNull(
                dataObject.Addresses[1].Group,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].CustomerPo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].InvoiceNo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].ReferenceNo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].OrderNo,
                "UpdateWholeRouteTest failed.");
        }

        [TestMethod]
        public void ChangeRouteDepoteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr2.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone(tdr2.SDRT_route);

            Assert.IsTrue(
                tdr2.SDRT_route.Addresses[0].IsDepot == true,
                "First address is not depot");
            Assert.IsTrue(
                tdr2.SDRT_route.Addresses[1].IsDepot == false,
                "Second address is depot");

            tdr2.SDRT_route.Addresses[0].IsDepot = false;
            var addressId0 = tdr2.SDRT_route.Addresses[0].RouteDestinationId;
            tdr2.SDRT_route.Addresses[0].Alias = addressId0.ToString();
            initialRoute.Addresses[0].Alias = addressId0.ToString();
            tdr2.SDRT_route.Addresses[1].IsDepot = true;
            var addressId1 = tdr2.SDRT_route.Addresses[1].RouteDestinationId;
            tdr2.SDRT_route.Addresses[1].Alias = addressId1.ToString();
            initialRoute.Addresses[1].Alias = addressId1.ToString();

            var dataObject = route4Me.UpdateRoute(
                tdr2.SDRT_route, initialRoute,
                out var errorString);

            var address0 = dataObject.Addresses
                .Where(x => x.Alias == addressId0.ToString())
                .FirstOrDefault();

            var address1 = dataObject.Addresses
                .Where(x => x.Alias == addressId1.ToString())
                .FirstOrDefault();

            Assert.IsTrue(address0.IsDepot == false, "First address is depot");
            Assert.IsTrue(address1.IsDepot == true, "Second address is not depot");
        }

        [TestMethod]
        public void AssignVehicleToRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleGroup = new VehiclesGroup();
            var vehicles = vehicleGroup.GetVehiclesList();

            if ((vehicles?.Length ?? 0) < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleName = "Ford Transit Test 6",
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = vehicleGroup.CreateVehicle(newVehicle);
                lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var vehicleId = (vehicles?.Length ?? 0) > 0
                ? vehicles[new Random().Next(0, vehicles.Length - 1)].VehicleId
                : lsVehicleIDs[0];

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    VehicleId = vehicleId
                }
            };

            var route = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.IsInstanceOfType(
                route.Vehilce,
                typeof(VehicleV4Response),
                "AssignVehicleToRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void AssignMemberToRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var members = route4Me.GetUsers(new GenericParameters(), out var errorString);

            var randomNumber = new Random().Next(0, members.Results.Length - 1);
            var memberId = members.Results[randomNumber].MemberId != null
                ? Convert.ToInt32(members.Results[randomNumber].MemberId)
                : -1;

            Assert.IsTrue(
                memberId != -1,
                "AssignMemberToRouteTest failed - cannot retrieve random member ID");

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(
                routeId,
                "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    MemberId = memberId
                }
            };

            route4Me.UpdateRoute(routeParameters, out errorString);

            var route = route4Me.GetRoute(
                new RouteParametersQuery {RouteId = routeId},
                out errorString);

            Assert.IsTrue(
                route.MemberId == memberId,
                "AssignMemberToRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateRouteCustomDataTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            var routeDestionationId = tdr.SD10Stops_route.Addresses[3].RouteDestinationId;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var parameters = new RouteParametersQuery
            {
                RouteId = routeId,
                RouteDestinationId = routeDestionationId
            };

            var customData = new Dictionary<string, string>
            {
                {"animal", "lion"},
                {"bird", "budgie"}
            };

            var result = route4Me.UpdateRouteCustomData(parameters, customData, out var errorString);

            Assert.IsNotNull(result, "UpdateRouteCustomDataTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateRouteAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null..");

            var parameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    AvoidanceZones = new[]
                    {
                        "FAA49711A0F1144CE4E203DC18ABDFFB",
                        "9C48E8008E9865006336B99D3595E66A"
                    }
                }
            };

            var result = route4Me.UpdateRoute(parameters, out var errorString);

            Assert.IsNotNull(result, "UpdateRouteAvoidanceZonesTest failed. " + errorString);

            Assert.IsTrue(
                result.Parameters.AvoidanceZones.Length == 2,
                "UpdateRouteAvoidanceZonesTest failed. " + errorString);
        }

        [TestMethod]
        public void RouteOriginParameterTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Original = true
            };

            var route = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(
                route,
                "RouteOriginParameterTest failed. " + errorString);
            Assert.IsNotNull(
                route.OriginalRoute,
                "RouteOriginParameterTest failed. " + errorString);
            Assert.IsInstanceOfType(
                route.OriginalRoute,
                typeof(DataObjectRoute),
                "RouteOriginParameterTest failed. " + errorString);
        }

        [TestMethod]
        public void ReoptimizeRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                ReOptimize = true
            };

            // Run the query
            var dataObject = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "ReoptimizeRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void DuplicateRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId is null.");

            var routeParameters = new RouteParametersQuery
            {
                DuplicateRoutesId = new[] {routeId}
            };

            // Run the query
            var result = route4Me.DuplicateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(result, "DuplicateRouteTest failed. " + errorString);
            Assert.IsInstanceOfType(
                result,
                typeof(DuplicateRouteResponse),
                "DeleteRoutesTest failed. " + errorString);
            Assert.IsTrue(result.Status, "DuplicateRouteTest failed");

            var routeIdsToDelete = new List<string>();

            foreach (var id in result.RouteIDs) routeIdsToDelete.Add(id);

            route4Me.DeleteRoutes(routeIdsToDelete.ToArray(), out var errorString2);
        }

        [TestMethod]
        public void ShareRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            var email = "regression.autotests+testcsharp123@gmail.com";

            // Run the query
            var result = route4Me.RouteSharing(routeParameters, email, out var errorString);

            Assert.IsTrue(result, "ShareRouteTest failed. " + errorString);
        }

        [TestMethod]
        public void DeleteRoutesTest()
        {
            var routeIdsToDelete = new List<string>();

            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            if (tdr.SDRT_route_id != null)
                routeIdsToDelete.Add(tdr.SDRT_route_id);

            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var deletedRouteIds = route4Me.DeleteRoutes(
                routeIdsToDelete.ToArray(),
                out var errorString);

            Assert.IsInstanceOfType(
                deletedRouteIds,
                typeof(string[]),
                "DeleteRoutesTest failed. " + errorString);
        }

        [TestMethod]
        public void RunRouteSlowdownTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

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
                    //Alias         = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new Address
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    //Alias         = "Ralph Lauren Women's and Home",
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
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),

                Slowdowns = new SlowdownParams(15, 20)
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObject,
                "RunSingleDriverRoundTripfailed. " + errorString);

            Assert.AreEqual(
                dataObject.Parameters.RouteServiceTimeMultiplier,
                15,
                "Cannot set service time slowdown");
            Assert.AreEqual(
                dataObject.Parameters.RouteTimeMultiplier,
                20,
                "Cannot set route travel time slowdown");

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [ClassCleanup]
        public static void AddressGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");

            if (lsVehicleIDs.Count > 0)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                foreach (var vehId in lsVehicleIDs)
                {
                    var vehicleParams = new VehicleV4Parameters
                    {
                        VehicleId = vehId
                    };

                    // Run the query
                    var vehicles = route4Me.DeleteVehicle(vehicleParams, out var errorString);
                }

                lsVehicleIDs.Clear();
            }
        }
    }

    [TestClass]
    public class NotesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;

        private static int lastCustomNoteTypeID;
        //static int firstCustomNoteTypeID; // rezerved for future.

        [ClassInitialize]
        public static void NotesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(context, "Initialization of the class NotesGroup failed.");

            var route4Me = new Route4MeManager(c_ApiKey);

            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();
            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id);

            var routeIdToMoveTo = tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId = tdr.DataObjectSDRT != null &&
                            tdr.SDRT_route != null &&
                            tdr.SDRT_route.Addresses.Length > 1 &&
                            tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                Format = "json"
            };

            // Run the query
            var contents = "Test Note Contents " + DateTime.Now;
            var note = route4Me.AddAddressNote(
                noteParameters,
                contents,
                out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);

            var response = route4Me.GetAllCustomNoteTypes(out errorString);

            Assert.IsTrue(response.GetType() == typeof(CustomNoteType[]), errorString);

            var notesGroup = new NotesGroup();

            if (((CustomNoteType[]) response).Length < 2)
            {
                notesGroup.AddCustomNoteType(
                    "Conditions at Site",
                    new[] {"safe", "mild", "dangerous", "slippery"}
                );
                notesGroup.AddCustomNoteType(
                    "To Do",
                    new[] {"Pass a package", "Pickup package", "Do a service"}
                );

                response = route4Me.GetAllCustomNoteTypes(out errorString);
            }

            Assert.IsTrue(
                ((CustomNoteType[]) response).Length > 0,
                "Can not find custom note type in the account. " + errorString);

            lastCustomNoteTypeID =
                ((CustomNoteType[]) response)[((CustomNoteType[]) response).Length - 1].NoteCustomTypeID;
        }

        [TestMethod]
        public void AddComplexAddressNoteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeIdToMoveTo = tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId =
                tdr.DataObjectSDRT != null && tdr.SDRT_route != null && tdr.SDRT_route.Addresses.Length > 1 &&
                tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                    ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                    : 0;

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out var errorString0);

            Dictionary<string, string> customNotes = null;

            if (customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[]))
            {
                var allCustomNotes = (CustomNoteType[]) customNotesResponse;

                if (allCustomNotes.Length > 0)
                    customNotes = new Dictionary<string, string>
                    {
                        {
                            "custom_note_type[" + allCustomNotes[0].NoteCustomTypeID + "]",
                            allCustomNotes[0].NoteCustomTypeValues[0]
                        }
                    };
            }

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                StrNoteContents = "Test Note Contents " + DateTime.Now
            };

            if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

            string tempFilePath = null;

            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    Console.WriteLine(tempFilePath);

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            noteParameters.StrFileName = tempFilePath;

            var note = route4Me.AddAddressNote(noteParameters, out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);
        }

        [TestMethod]
        public void AddAddressNoteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeIdToMoveTo = tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId = tdr.DataObjectSDRT != null &&
                            tdr.SDRT_route != null &&
                            tdr.SDRT_route.Addresses.Length > 1 &&
                            tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            // Run the query
            var contents = "Test Note Contents " + DateTime.Now;
            var note = route4Me.AddAddressNote(noteParameters, contents, out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);
        }

        [TestMethod]
        public void AddAddressNoteWithFileTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SDRT_route_id;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null.");

            var addressId = tdr.DataObjectSDRT != null &&
                            tdr.SDRT_route != null &&
                            tdr.SDRT_route.Addresses.Length > 1 &&
                            tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeId,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png");

            using (var stream = resourceStream)
            {
                string tempFilePath = null;

                using (var tempFiles = new TempFileCollection())
                {
                    {
                        tempFilePath = tempFiles.AddExtension("png");
                        Console.WriteLine(tempFilePath);
                        using (Stream fileStream = File.OpenWrite(tempFilePath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                    // Run the query
                    var contents = "Test Note Contents with Attachment " + DateTime.Now;
                    var note = route4Me.AddAddressNote(
                        noteParameters,
                        contents,
                        tempFilePath,
                        out var errorString);

                    Assert.IsNotNull(note, "AddAddressNoteWithFileTest failed. " + errorString);
                }

                ;
            }
        }

        [TestMethod]
        public void GetAddressNotesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null.");

            var routeDestinationId = tdr.DataObjectSDRT != null &&
                                     tdr.SDRT_route != null &&
                                     tdr.SDRT_route.Addresses.Length > 1 &&
                                     tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var noteParameters = new NoteParameters
            {
                RouteId = routeId,
                AddressId = routeDestinationId
            };

            // Run the query
            var notes = route4Me.GetAddressNotes(noteParameters, out var errorString);

            Assert.IsInstanceOfType(
                notes,
                typeof(AddressNote[]),
                "GetAddressNotesTest failed. " + errorString);
        }

        [TestMethod]
        public void AddCustomNoteTypeTest()
        {
            var response = AddCustomNoteType(
                "To Do",
                new[] {"Pass a package", "Pickup package", "Do a service"});

            Assert.IsTrue(response.GetType() != typeof(string), response.ToString());

            Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not create new custom note type");
        }

        public object AddCustomNoteType(string customType, string[] customValues)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var response = route4Me.AddCustomNoteType(customType, customValues, out var errorString);

            return response ?? errorString;
        }

        [TestMethod]
        public void RemoveCustomNoteTypeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var response = route4Me.RemoveCustomNoteType(lastCustomNoteTypeID, out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not remove the custom note type");
        }

        [TestMethod]
        public void GetAllCustomNoteTypesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            //string errorString;
            var response = route4Me.GetAllCustomNoteTypes(out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(response.GetType() == typeof(CustomNoteType[]));
        }

        [TestMethod]
        public void AddCustomNoteToRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var noteParameters = new NoteParameters
            {
                RouteId = tdr.SDRT_route.RouteId,
                AddressId = tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                    ? (int) tdr.SDRT_route.Addresses[1].RouteDestinationId
                    : 0,
                Format = "json",
                Latitude = tdr.SDRT_route.Addresses[1].Latitude,
                Longitude = tdr.SDRT_route.Addresses[1].Longitude
            };

            var customNotes = new Dictionary<string, string>
            {
                {"custom_note_type[11]", "slippery"},
                {"custom_note_type[10]", "Backdoor"},
                {"strUpdateType", "dropoff"},
                {"strNoteContents", "test1111"}
            };

            var response = route4Me.AddCustomNoteToRoute(
                noteParameters,
                customNotes,
                out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(response.GetType() == typeof(AddressNote));
        }

        [ClassCleanup]
        public static void NotesGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the optimization with 24 stops failed.");
        }
    }

    [TestClass]
    public class RouteTypesGroup
    {
        private static string skip;

        private static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        private static readonly TestDataRepository tdr = new TestDataRepository();

        private static DataObject dataObject, dataObjectMDMD24;

        [ClassInitialize]
        public static void RouteTypesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(context, "Initialization of the class RouteTypesGroup failed.");

            skip = c_ApiKey == c_ApiKey_1 ? "yes" : "no";
        }

        [TestMethod]
        public void MultipleDepotMultipleDriverTest()
        {
            if (skip == "yes") return;
            // Create the manager with the api key
            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "3634 W Market St, Fairlawn, OH 44333",
                    //all possible originating locations are depots, should be marked as true
                    //stylistically we recommend all depots should be at the top of the destinations list
                    IsDepot = true,
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,

                    //the number of seconds at destination
                    Time = 300,

                    //together these two specify the time window of a destination
                    //seconds offset relative to the route start time for the open availability of a destination
                    TimeWindowStart = 28800,

                    //seconds offset relative to the route end time for the open availability of a destination
                    TimeWindowEnd = 29465
                },

                new Address
                {
                    AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,
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
                    TimeWindowEnd = 33779
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 100,
                    TimeWindowStart = 33779,
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
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
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
                    TimeWindowEnd = 48389
                },

                new Address
                {
                    AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                    Latitude = 41.315116882324,
                    Longitude = -81.558746337891,
                    Time = 50,
                    TimeWindowStart = 48389,
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
                    TimeWindowEnd = 51982
                },

                new Address
                {
                    AddressString = "3731 Osage St, Stow, OH 44224",
                    Latitude = 41.161357879639,
                    Longitude = -81.42293548584,
                    Time = 100,
                    TimeWindowStart = 51982,
                    TimeWindowEnd = 52180
                },

                new Address
                {
                    AddressString = "3731 Osage St, Stow, OH 44224",
                    Latitude = 41.161357879639,
                    Longitude = -81.42293548584,
                    Time = 300,
                    TimeWindowStart = 52180,
                    TimeWindowEnd = 54379
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                //specify capacitated vehicle routing with time windows and multiple depots, with multiple drivers
                AlgorithmType = AlgorithmType.CVRP_TW_MD,

                //set an arbitrary route name
                //this value shows up in the website, and all the connected mobile device
                RouteName = "Multiple Depot, Multiple Driver",

                //the route start date in UTC, unix timestamp seconds (Tomorrow)
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                //the time in UTC when a route is starting (7AM)
                RouteTime = 60 * 60 * 7,

                //the maximum duration of a route
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(dataObject, "MultipleDepotMultipleDriverTest failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void MultipleSeparateDepostMultipleDriverTest()
        {
            if (skip == "yes") return;
            // Create the manager with the api key
            var route4Me = new Route4MeManager(c_ApiKey);

            #region Depots

            Address[] depots =
            {
                new Address
                {
                    AddressString = "3634 W Market St, Fairlawn, OH 44333",
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561
                },

                new Address
                {
                    AddressString = "2705 N River Rd, Stow, OH 44224",
                    Latitude = 41.145240783691,
                    Longitude = -81.410247802734
                }
            };

            #endregion

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,
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
                    TimeWindowEnd = 33779
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    Time = 100,
                    TimeWindowStart = 33779,
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
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
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
                    TimeWindowEnd = 48389
                },

                new Address
                {
                    AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                    Latitude = 41.315116882324,
                    Longitude = -81.558746337891,
                    Time = 50,
                    TimeWindowStart = 48389,
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
                    TimeWindowEnd = 51982
                },

                new Address
                {
                    AddressString = "3731 Osage St, Stow, OH 44224",
                    Latitude = 41.161357879639,
                    Longitude = -81.42293548584,
                    Time = 100,
                    TimeWindowStart = 51982,
                    TimeWindowEnd = 52180
                },

                new Address
                {
                    AddressString = "3731 Osage St, Stow, OH 44224",
                    Latitude = 41.161357879639,
                    Longitude = -81.42293548584,
                    Time = 300,
                    TimeWindowStart = 52180,
                    TimeWindowEnd = 54379
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                //specify capacitated vehicle routing with time windows and multiple depots, with multiple drivers
                AlgorithmType = AlgorithmType.CVRP_TW_MD,

                //set an arbitrary route name
                //this value shows up in the website, and all the connected mobile device
                RouteName = "Multiple Separate Depots, Multiple Driver",

                //the route start date in UTC, unix timestamp seconds (Tomorrow)
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                //the time in UTC when a route is starting (7AM)
                RouteTime = 60 * 60 * 7,

                //the maximum duration of a route
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters,
                Depots = depots
            };

            // Run the query
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(
                dataObject,
                "MultipleSeparateDepostMultipleDriverTest failed. " + errorString);

            var optimizationDepots = dataObject.Addresses.Where(x => x.IsDepot == true);

            Assert.IsTrue(
                optimizationDepots.Count() == depots.Length,
                "The depots number is not " + depots.Length);

            var depotAddresses = depots.Select(x => x.AddressString).ToList();

            foreach (var depot in optimizationDepots)
                Assert.IsTrue(
                    depotAddresses.Contains(depot.AddressString),
                    "The address " + depot.AddressString + " is not depot");

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void MultipleDepotMultipleDriverTimeWindowTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "455 S 4th St, Louisville, KY 40202",
                    IsDepot = true,
                    Latitude = 38.251698,
                    Longitude = -85.757308,
                    Time = 300,
                    TimeWindowStart = 28800,
                    TimeWindowEnd = 30477
                },

                new Address
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300,
                    TimeWindowStart = 30477,
                    TimeWindowEnd = 33406
                },

                new Address
                {
                    AddressString = "1407 א53MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    TimeWindowStart = 33406,
                    TimeWindowEnd = 36228
                },
                new Address
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300,
                    TimeWindowStart = 36228,
                    TimeWindowEnd = 37518
                },

                new Address
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    TimeWindowStart = 37518,
                    TimeWindowEnd = 39550
                },

                new Address
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300,
                    TimeWindowStart = 39550,
                    TimeWindowEnd = 41348
                },

                new Address
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    TimeWindowStart = 41348,
                    TimeWindowEnd = 42261
                },

                new Address
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    TimeWindowStart = 42261,
                    TimeWindowEnd = 45195
                },

                new Address
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    TimeWindowStart = 45195,
                    TimeWindowEnd = 46549
                },

                new Address
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    TimeWindowStart = 46549,
                    TimeWindowEnd = 47353
                },

                new Address
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    TimeWindowStart = 47353,
                    TimeWindowEnd = 50924
                },

                new Address
                {
                    AddressString = "1661 W HILL ST, Louisville, KY, 40210",
                    Latitude = 38.229584,
                    Longitude = -85.783966,
                    Time = 300,
                    TimeWindowStart = 50924,
                    TimeWindowEnd = 51392
                },

                new Address
                {
                    AddressString = "3222 KINGSWOOD WAY, Louisville, KY, 40216",
                    Latitude = 38.210606,
                    Longitude = -85.822594,
                    Time = 300,
                    TimeWindowStart = 51392,
                    TimeWindowEnd = 52451
                },

                new Address
                {
                    AddressString = "1922 PALATKA RD, Louisville, KY, 40214",
                    Latitude = 38.153767,
                    Longitude = -85.796783,
                    Time = 300,
                    TimeWindowStart = 52451,
                    TimeWindowEnd = 55631
                },

                new Address
                {
                    AddressString = "1314 SOUTH 26TH STREET, Louisville, KY, 40210",
                    Latitude = 38.235847,
                    Longitude = -85.796852,
                    Time = 300,
                    TimeWindowStart = 55631,
                    TimeWindowEnd = 58516
                },

                new Address
                {
                    AddressString = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210",
                    Latitude = 38.218662,
                    Longitude = -85.789032,
                    Time = 300,
                    TimeWindowStart = 58516,
                    TimeWindowEnd = 61080
                },

                new Address
                {
                    AddressString = "1409 PHYLLIS AVE, Louisville, KY, 40215",
                    Latitude = 38.206154,
                    Longitude = -85.781387,
                    Time = 100,
                    TimeWindowStart = 61080,
                    TimeWindowEnd = 61504
                },

                new Address
                {
                    AddressString = "4504 SUNFLOWER AVE, Louisville, KY, 40216",
                    Latitude = 38.187511,
                    Longitude = -85.839149,
                    Time = 300,
                    TimeWindowStart = 61504,
                    TimeWindowEnd = 62061
                },

                new Address
                {
                    AddressString = "2512 GREENWOOD AVE, Louisville, KY, 40210",
                    Latitude = 38.241405,
                    Longitude = -85.795059,
                    Time = 300,
                    TimeWindowStart = 62061,
                    TimeWindowEnd = 65012
                },

                new Address
                {
                    AddressString = "5500 WILKE FARM AVE, Louisville, KY, 40216",
                    Latitude = 38.166065,
                    Longitude = -85.863319,
                    Time = 300,
                    TimeWindowStart = 65012,
                    TimeWindowEnd = 67541
                },

                new Address
                {
                    AddressString = "3640 LENTZ AVE, Louisville, KY, 40215",
                    Latitude = 38.193283,
                    Longitude = -85.786201,
                    Time = 300,
                    TimeWindowStart = 67541,
                    TimeWindowEnd = 69120
                },

                new Address
                {
                    AddressString = "1020 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.17952,
                    Longitude = -85.780037,
                    Time = 300,
                    TimeWindowStart = 69120,
                    TimeWindowEnd = 70572
                },

                new Address
                {
                    AddressString = "123 NORTH 40TH ST, Louisville, KY, 40212",
                    Latitude = 38.26498,
                    Longitude = -85.814156,
                    Time = 300,
                    TimeWindowStart = 70572,
                    TimeWindowEnd = 73177
                },

                new Address
                {
                    AddressString = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214",
                    Latitude = 38.151072,
                    Longitude = -85.802867,
                    Time = 300,
                    TimeWindowStart = 73177,
                    TimeWindowEnd = 75231
                },

                new Address
                {
                    AddressString = "3210 POPLAR VIEW DR, Louisville, KY, 40216",
                    Latitude = 38.182594,
                    Longitude = -85.849937,
                    Time = 300,
                    TimeWindowStart = 75231,
                    TimeWindowEnd = 77663
                },

                new Address
                {
                    AddressString = "4519 LOUANE WAY, Louisville, KY, 40216",
                    Latitude = 38.1754,
                    Longitude = -85.811447,
                    Time = 300,
                    TimeWindowStart = 77663,
                    TimeWindowEnd = 79796
                },

                new Address
                {
                    AddressString = "6812 MANSLICK RD, Louisville, KY, 40214",
                    Latitude = 38.161839,
                    Longitude = -85.798279,
                    Time = 300,
                    TimeWindowStart = 79796,
                    TimeWindowEnd = 80813
                },

                new Address
                {
                    AddressString = "1524 HUNTOON AVENUE, Louisville, KY, 40215",
                    Latitude = 38.172031,
                    Longitude = -85.788353,
                    Time = 300,
                    TimeWindowStart = 80813,
                    TimeWindowEnd = 83956
                },

                new Address
                {
                    AddressString = "1307 LARCHMONT AVE, Louisville, KY, 40215",
                    Latitude = 38.209663,
                    Longitude = -85.779816,
                    Time = 300,
                    TimeWindowStart = 83956,
                    TimeWindowEnd = 84365
                },

                new Address
                {
                    AddressString = "434 N 26TH STREET #2, Louisville, KY, 40212",
                    Latitude = 38.26844,
                    Longitude = -85.791962,
                    Time = 300,
                    TimeWindowStart = 84365,
                    TimeWindowEnd = 85367
                },

                new Address
                {
                    AddressString = "678 WESTLAWN ST, Louisville, KY, 40211",
                    Latitude = 38.250397,
                    Longitude = -85.80629,
                    Time = 300,
                    TimeWindowStart = 85367,
                    TimeWindowEnd = 86400
                },

                new Address
                {
                    AddressString = "2308 W BROADWAY, Louisville, KY, 40211",
                    Latitude = 38.248882,
                    Longitude = -85.790421,
                    Time = 300,
                    TimeWindowStart = 86400,
                    TimeWindowEnd = 88703
                },

                new Address
                {
                    AddressString = "2332 WOODLAND AVE, Louisville, KY, 40210",
                    Latitude = 38.233579,
                    Longitude = -85.794257,
                    Time = 300,
                    TimeWindowStart = 88703,
                    TimeWindowEnd = 89320
                },

                new Address
                {
                    AddressString = "1706 WEST ST. CATHERINE, Louisville, KY, 40210",
                    Latitude = 38.239697,
                    Longitude = -85.783928,
                    Time = 300,
                    TimeWindowStart = 89320,
                    TimeWindowEnd = 90054
                },

                new Address
                {
                    AddressString = "1699 WATHEN LN, Louisville, KY, 40216",
                    Latitude = 38.216465,
                    Longitude = -85.792397,
                    Time = 300,
                    TimeWindowStart = 90054,
                    TimeWindowEnd = 91150
                },

                new Address
                {
                    AddressString = "2416 SUNSHINE WAY, Louisville, KY, 40216",
                    Latitude = 38.186245,
                    Longitude = -85.831787,
                    Time = 300,
                    TimeWindowStart = 91150,
                    TimeWindowEnd = 91915
                },

                new Address
                {
                    AddressString = "6925 MANSLICK RD, Louisville, KY, 40214",
                    Latitude = 38.158466,
                    Longitude = -85.798355,
                    Time = 300,
                    TimeWindowStart = 91915,
                    TimeWindowEnd = 93407
                },

                new Address
                {
                    AddressString = "2707 7TH ST, Louisville, KY, 40215",
                    Latitude = 38.212438,
                    Longitude = -85.785082,
                    Time = 300,
                    TimeWindowStart = 93407,
                    TimeWindowEnd = 95992
                },

                new Address
                {
                    AddressString = "2014 KENDALL LN, Louisville, KY, 40216",
                    Latitude = 38.179394,
                    Longitude = -85.826668,
                    Time = 300,
                    TimeWindowStart = 95992,
                    TimeWindowEnd = 99307
                },

                new Address
                {
                    AddressString = "612 N 39TH ST, Louisville, KY, 40212",
                    Latitude = 38.273354,
                    Longitude = -85.812012,
                    Time = 300,
                    TimeWindowStart = 99307,
                    TimeWindowEnd = 102906
                },

                new Address
                {
                    AddressString = "2215 ROWAN ST, Louisville, KY, 40212",
                    Latitude = 38.261703,
                    Longitude = -85.786781,
                    Time = 300,
                    TimeWindowStart = 102906,
                    TimeWindowEnd = 106021
                },

                new Address
                {
                    AddressString = "1826 W. KENTUCKY ST, Louisville, KY, 40210",
                    Latitude = 38.241611,
                    Longitude = -85.78653,
                    Time = 300,
                    TimeWindowStart = 106021,
                    TimeWindowEnd = 107276
                },

                new Address
                {
                    AddressString = "1810 GREGG AVE, Louisville, KY, 40210",
                    Latitude = 38.224716,
                    Longitude = -85.796211,
                    Time = 300,
                    TimeWindowStart = 107276,
                    TimeWindowEnd = 107948
                },

                new Address
                {
                    AddressString = "4103 BURRRELL DRIVE, Louisville, KY, 40216",
                    Latitude = 38.191753,
                    Longitude = -85.825836,
                    Time = 300,
                    TimeWindowStart = 107948,
                    TimeWindowEnd = 108414
                },

                new Address
                {
                    AddressString = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212",
                    Latitude = 38.259903,
                    Longitude = -85.823463,
                    Time = 200,
                    TimeWindowStart = 108414,
                    TimeWindowEnd = 108685
                },

                new Address
                {
                    AddressString = "2407 W CHESTNUT ST, Louisville, KY, 40211",
                    Latitude = 38.252781,
                    Longitude = -85.792109,
                    Time = 300,
                    TimeWindowStart = 108685,
                    TimeWindowEnd = 110109
                },

                new Address
                {
                    AddressString = "225 S 22ND ST, Louisville, KY, 40212",
                    Latitude = 38.257616,
                    Longitude = -85.786658,
                    Time = 300,
                    TimeWindowStart = 110109,
                    TimeWindowEnd = 111375
                },

                new Address
                {
                    AddressString = "1404 MCCOY AVE, Louisville, KY, 40215",
                    Latitude = 38.202122,
                    Longitude = -85.786072,
                    Time = 300,
                    TimeWindowStart = 111375,
                    TimeWindowEnd = 112120
                },

                new Address
                {
                    AddressString = "117 FOUNT LANDING CT, Louisville, KY, 40212",
                    Latitude = 38.270061,
                    Longitude = -85.799438,
                    Time = 300,
                    TimeWindowStart = 112120,
                    TimeWindowEnd = 114095
                },

                new Address
                {
                    AddressString = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214",
                    Latitude = 38.145851,
                    Longitude = -85.7798,
                    Time = 300,
                    TimeWindowStart = 114095,
                    TimeWindowEnd = 115743
                },

                new Address
                {
                    AddressString = "1406 CENTRAL AVE, Louisville, KY, 40208",
                    Latitude = 38.211025,
                    Longitude = -85.780251,
                    Time = 300,
                    TimeWindowStart = 115743,
                    TimeWindowEnd = 117716
                },

                new Address
                {
                    AddressString = "901 W WHITNEY AVE, Louisville, KY, 40215",
                    Latitude = 38.194115,
                    Longitude = -85.77494,
                    Time = 300,
                    TimeWindowStart = 117716,
                    TimeWindowEnd = 119078
                },

                new Address
                {
                    AddressString = "2109 SCHAFFNER AVE, Louisville, KY, 40210",
                    Latitude = 38.219699,
                    Longitude = -85.779363,
                    Time = 300,
                    TimeWindowStart = 119078,
                    TimeWindowEnd = 121147
                },

                new Address
                {
                    AddressString = "2906 DIXIE HWY, Louisville, KY, 40216",
                    Latitude = 38.209278,
                    Longitude = -85.798653,
                    Time = 300,
                    TimeWindowStart = 121147,
                    TimeWindowEnd = 124281
                },

                new Address
                {
                    AddressString = "814 WWHITNEY AVE, Louisville, KY, 40215",
                    Latitude = 38.193596,
                    Longitude = -85.773521,
                    Time = 300,
                    TimeWindowStart = 124281,
                    TimeWindowEnd = 124675
                },

                new Address
                {
                    AddressString = "1610 ALGONQUIN PWKY, Louisville, KY, 40210",
                    Latitude = 38.222153,
                    Longitude = -85.784187,
                    Time = 300,
                    TimeWindowStart = 124675,
                    TimeWindowEnd = 127148
                },

                new Address
                {
                    AddressString = "3524 WHEELER AVE, Louisville, KY, 40215",
                    Latitude = 38.195293,
                    Longitude = -85.788643,
                    Time = 300,
                    TimeWindowStart = 127148,
                    TimeWindowEnd = 130667
                },

                new Address
                {
                    AddressString = "5009 NEW CUT RD, Louisville, KY, 40214",
                    Latitude = 38.165905,
                    Longitude = -85.779701,
                    Time = 300,
                    TimeWindowStart = 130667,
                    TimeWindowEnd = 131980
                },

                new Address
                {
                    AddressString = "3122 ELLIOTT AVE, Louisville, KY, 40211",
                    Latitude = 38.251213,
                    Longitude = -85.804199,
                    Time = 300,
                    TimeWindowStart = 131980,
                    TimeWindowEnd = 134402
                },

                new Address
                {
                    AddressString = "911 GAGEL AVE, Louisville, KY, 40216",
                    Latitude = 38.173512,
                    Longitude = -85.807854,
                    Time = 300,
                    TimeWindowStart = 134402,
                    TimeWindowEnd = 136787
                },

                new Address
                {
                    AddressString = "4020 GARLAND AVE #lOOA, Louisville, KY, 40211",
                    Latitude = 38.246181,
                    Longitude = -85.818901,
                    Time = 300,
                    TimeWindowStart = 136787,
                    TimeWindowEnd = 138073
                },

                new Address
                {
                    AddressString = "5231 MT HOLYOKE DR, Louisville, KY, 40216",
                    Latitude = 38.169369,
                    Longitude = -85.85704,
                    Time = 300,
                    TimeWindowStart = 138073,
                    TimeWindowEnd = 141407
                },

                new Address
                {
                    AddressString = "1339 28TH S #2, Louisville, KY, 40211",
                    Latitude = 38.235275,
                    Longitude = -85.800156,
                    Time = 300,
                    TimeWindowStart = 141407,
                    TimeWindowEnd = 143561
                },

                new Address
                {
                    AddressString = "836 S 36TH ST, Louisville, KY, 40211",
                    Latitude = 38.24651,
                    Longitude = -85.811234,
                    Time = 300,
                    TimeWindowStart = 143561,
                    TimeWindowEnd = 145941
                },

                new Address
                {
                    AddressString = "2132 DUNCAN STREET, Louisville, KY, 40212",
                    Latitude = 38.262135,
                    Longitude = -85.785172,
                    Time = 300,
                    TimeWindowStart = 145941,
                    TimeWindowEnd = 148296
                },

                new Address
                {
                    AddressString = "3529 WHEELER AVE, Louisville, KY, 40215",
                    Latitude = 38.195057,
                    Longitude = -85.787949,
                    Time = 300,
                    TimeWindowStart = 148296,
                    TimeWindowEnd = 150177
                },

                new Address
                {
                    AddressString = "2829 DE MEL #11, Louisville, KY, 40214",
                    Latitude = 38.171662,
                    Longitude = -85.807271,
                    Time = 300,
                    TimeWindowStart = 150177,
                    TimeWindowEnd = 150981
                },

                new Address
                {
                    AddressString = "1325 EARL AVENUE, Louisville, KY, 40215",
                    Latitude = 38.204556,
                    Longitude = -85.781555,
                    Time = 300,
                    TimeWindowStart = 150981,
                    TimeWindowEnd = 151854
                },

                new Address
                {
                    AddressString = "3632 MANSLICK RD #10, Louisville, KY, 40215",
                    Latitude = 38.193542,
                    Longitude = -85.801147,
                    Time = 300,
                    TimeWindowStart = 151854,
                    TimeWindowEnd = 152613
                },

                new Address
                {
                    AddressString = "637 S 41ST ST, Louisville, KY, 40211",
                    Latitude = 38.253632,
                    Longitude = -85.81897,
                    Time = 300,
                    TimeWindowStart = 152613,
                    TimeWindowEnd = 156131
                },

                new Address
                {
                    AddressString = "3420 VIRGINIA AVENUE, Louisville, KY, 40211",
                    Latitude = 38.238693,
                    Longitude = -85.811386,
                    Time = 300,
                    TimeWindowStart = 156131,
                    TimeWindowEnd = 157212
                },

                new Address
                {
                    AddressString = "3501 MALIBU CT APT 6, Louisville, KY, 40216",
                    Latitude = 38.166481,
                    Longitude = -85.825928,
                    Time = 300,
                    TimeWindowStart = 157212,
                    TimeWindowEnd = 158655
                },

                new Address
                {
                    AddressString = "4912 DIXIE HWY, Louisville, KY, 40216",
                    Latitude = 38.170728,
                    Longitude = -85.826817,
                    Time = 300,
                    TimeWindowStart = 158655,
                    TimeWindowEnd = 159145
                },

                new Address
                {
                    AddressString = "7720 DINGLEDELL RD, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    TimeWindowStart = 159145,
                    TimeWindowEnd = 161831
                },

                new Address
                {
                    AddressString = "2123 RATCLIFFE AVE, Louisville, KY, 40210",
                    Latitude = 38.21978,
                    Longitude = -85.797615,
                    Time = 300,
                    TimeWindowStart = 161831,
                    TimeWindowEnd = 163705
                },

                new Address
                {
                    AddressString = "1321 OAKWOOD AVE, Louisville, KY, 40215",
                    Latitude = 38.17704,
                    Longitude = -85.783829,
                    Time = 300,
                    TimeWindowStart = 163705,
                    TimeWindowEnd = 164953
                },

                new Address
                {
                    AddressString = "2223 WEST KENTUCKY STREET, Louisville, KY, 40210",
                    Latitude = 38.242516,
                    Longitude = -85.790695,
                    Time = 300,
                    TimeWindowStart = 164953,
                    TimeWindowEnd = 166189
                },

                new Address
                {
                    AddressString = "8025 GLIMMER WAY #3308, Louisville, KY, 40214",
                    Latitude = 38.131981,
                    Longitude = -85.77935,
                    Time = 300,
                    TimeWindowStart = 166189,
                    TimeWindowEnd = 166640
                },

                new Address
                {
                    AddressString = "1155 S 28TH ST, Louisville, KY, 40211",
                    Latitude = 38.238621,
                    Longitude = -85.799911,
                    Time = 300,
                    TimeWindowStart = 166640,
                    TimeWindowEnd = 168147
                },

                new Address
                {
                    AddressString = "840 IROQUOIS AVE, Louisville, KY, 40214",
                    Latitude = 38.166355,
                    Longitude = -85.779396,
                    Time = 300,
                    TimeWindowStart = 168147,
                    TimeWindowEnd = 170385
                },

                new Address
                {
                    AddressString = "5573 BRUCE AVE, Louisville, KY, 40214",
                    Latitude = 38.145222,
                    Longitude = -85.779205,
                    Time = 300,
                    TimeWindowStart = 170385,
                    TimeWindowEnd = 171096
                },

                new Address
                {
                    AddressString = "1727 GALLAGHER, Louisville, KY, 40210",
                    Latitude = 38.239334,
                    Longitude = -85.784882,
                    Time = 300,
                    TimeWindowStart = 171096,
                    TimeWindowEnd = 171951
                },

                new Address
                {
                    AddressString = "1309 CATALPA ST APT 204, Louisville, KY, 40211",
                    Latitude = 38.236524,
                    Longitude = -85.801619,
                    Time = 300,
                    TimeWindowStart = 171951,
                    TimeWindowEnd = 172393
                },

                new Address
                {
                    AddressString = "1330 ALGONQUIN PKWY, Louisville, KY, 40208",
                    Latitude = 38.219846,
                    Longitude = -85.777344,
                    Time = 300,
                    TimeWindowStart = 172393,
                    TimeWindowEnd = 175337
                },

                new Address
                {
                    AddressString = "823 SUTCLIFFE, Louisville, KY, 40211",
                    Latitude = 38.246956,
                    Longitude = -85.811569,
                    Time = 300,
                    TimeWindowStart = 175337,
                    TimeWindowEnd = 176867
                },

                new Address
                {
                    AddressString = "4405 CHURCHMAN AVENUE #2, Louisville, KY, 40215",
                    Latitude = 38.177768,
                    Longitude = -85.792545,
                    Time = 300,
                    TimeWindowStart = 176867,
                    TimeWindowEnd = 178051
                },

                new Address
                {
                    AddressString = "3211 DUMESNIL ST #1, Louisville, KY, 40211",
                    Latitude = 38.237789,
                    Longitude = -85.807968,
                    Time = 300,
                    TimeWindowStart = 178051,
                    TimeWindowEnd = 179083
                },

                new Address
                {
                    AddressString = "3904 WEWOKA AVE, Louisville, KY, 40212",
                    Latitude = 38.270367,
                    Longitude = -85.813118,
                    Time = 300,
                    TimeWindowStart = 179083,
                    TimeWindowEnd = 181543
                },

                new Address
                {
                    AddressString = "660 SO. 42ND STREET, Louisville, KY, 40211",
                    Latitude = 38.252865,
                    Longitude = -85.822624,
                    Time = 300,
                    TimeWindowStart = 181543,
                    TimeWindowEnd = 184193
                },

                new Address
                {
                    AddressString = "3619  LENTZ  AVE, Louisville, KY, 40215",
                    Latitude = 38.193249,
                    Longitude = -85.785492,
                    Time = 300,
                    TimeWindowStart = 184193,
                    TimeWindowEnd = 185853
                },

                new Address
                {
                    AddressString = "4305  STOLTZ  CT, Louisville, KY, 40215",
                    Latitude = 38.178707,
                    Longitude = -85.787292,
                    Time = 300,
                    TimeWindowStart = 185853,
                    TimeWindowEnd = 187252
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Multiple Depot, Multiple Driver, Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RT = true,
                RouteMaxDuration = 86400 * 3,
                VehicleCapacity = 99,
                VehicleMaxDistanceMI = 99999,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(
                dataObject,
                "MultipleDepotMultipleDriverTimeWindowTest failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void SingleDepotMultipleDriverNoTimeWindowTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "40 Mercer st, New York, NY",
                    IsDepot = true,
                    Latitude = 40.7213583,
                    Longitude = -74.0013082,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Manhatten Island NYC",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "503 W139 St, NY,NY",
                    Latitude = 40.7109062,
                    Longitude = -74.0091848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "203 grand st, new york, ny",
                    Latitude = 40.7188990,
                    Longitude = -73.9967320,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "119 Church Street",
                    Latitude = 40.7137757,
                    Longitude = -74.0088238,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "broadway street, new york",
                    Latitude = 40.7191551,
                    Longitude = -74.0020849,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038",
                    Latitude = 40.7233126,
                    Longitude = -74.0116602,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "226 ilyssa way staten lsland ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "185 franklin st.",
                    Latitude = 40.7192099,
                    Longitude = -74.0097670,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city,",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "11 e. broaway 11038",
                    Latitude = 40.7132060,
                    Longitude = -73.9974019,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Brooklyn Bridge, NY",
                    Latitude = 40.7053804,
                    Longitude = -73.9962503,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World Trade Center Site, NY",
                    Latitude = 40.7114980,
                    Longitude = -74.0122990,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York Stock Exchange, NY",
                    Latitude = 40.7074242,
                    Longitude = -74.0116342,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Wall Street, NY",
                    Latitude = 40.7079825,
                    Longitude = -74.0079781,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Trinity Church, NY",
                    Latitude = 40.7081426,
                    Longitude = -74.0120511,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World Financial Center, NY",
                    Latitude = 40.7104750,
                    Longitude = -74.0154930,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Federal Hall, NY",
                    Latitude = 40.7073034,
                    Longitude = -74.0102734,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Flatiron Building, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "South Street Seaport, NY",
                    Latitude = 40.7069210,
                    Longitude = -74.0036380,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Rockefeller Center, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "FAO Schwarz, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Woolworth Building, NY",
                    Latitude = 40.7123903,
                    Longitude = -74.0083309,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Met Life Building, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "SOHO/Tribeca, NY",
                    Latitude = 40.7185650,
                    Longitude = -74.0120170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "MacyГўв‚¬в„ўs, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "City Hall, NY, NY",
                    Latitude = 40.7127047,
                    Longitude = -74.0058663,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Macy&amp;acirc;в‚¬в„ўs, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "1452 potter blvd bayshore ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "55 Church St. New York, NY",
                    Latitude = 40.7112320,
                    Longitude = -74.0102680,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "55 Church St, New York, NY",
                    Latitude = 40.7112320,
                    Longitude = -74.0102680,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "79 woodlawn dr revena ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "135 main st revena ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "250 greenwich st, new york, ny",
                    Latitude = 40.7131590,
                    Longitude = -74.0118890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "79 grand, new york, ny",
                    Latitude = 40.7216958,
                    Longitude = -74.0024352,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World trade center\n",
                    Latitude = 40.7116260,
                    Longitude = -74.0107140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World trade centern",
                    Latitude = 40.7132910,
                    Longitude = -74.0118350,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "391 broadway new york",
                    Latitude = 40.7183693,
                    Longitude = -74.0027800,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Fletcher street",
                    Latitude = 40.7063954,
                    Longitude = -74.0056353,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "2 Plum LanenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "50 Kennedy drivenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "7 Crestwood DrivenPlainview New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "85 west street nyc",
                    Latitude = 40.7096460,
                    Longitude = -74.0146140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York, New York",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "89 Reade St, New York City, New York 10013",
                    Latitude = 40.7142970,
                    Longitude = -74.0059660,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "100 white st",
                    Latitude = 40.7172477,
                    Longitude = -74.0014351,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "100 white st\n33040",
                    Latitude = 40.7172477,
                    Longitude = -74.0014351,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Canal st and mulberry",
                    Latitude = 40.7170880,
                    Longitude = -73.9986025,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "91-83 111st st\nRichmond hills ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "122-09 liberty avenOzone park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "80-16 101 avenOzone park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "6302 woodhaven blvdnRego park ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "39-02 64th stnWoodside ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York City, NY,",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Pine st",
                    Latitude = 40.7069754,
                    Longitude = -74.0089557,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Wall st",
                    Latitude = 40.7079825,
                    Longitude = -74.0079781,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "32 avenue of the Americas, NY, NY",
                    Latitude = 40.7201140,
                    Longitude = -74.0050920,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "260 west broadway, NY, NY",
                    Latitude = 40.7206210,
                    Longitude = -74.0055670,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Long island, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "27 Carley ave\nHuntington ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "17 west neck RdnHuntington ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "206 washington st",
                    Latitude = 40.7131577,
                    Longitude = -74.0126091,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Cipriani new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Byshnell Basin. NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "89 Reade St, New York, New York 10013",
                    Latitude = 40.7142970,
                    Longitude = -74.0059660,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "250 Greenwich St, New York, New York 10007",
                    Latitude = 40.7133000,
                    Longitude = -74.0120000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "64 Bowery, New York, New York 10013",
                    Latitude = 40.7165540,
                    Longitude = -73.9962700,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "142-156 Mulberry St, New York, New York 10013",
                    Latitude = 40.7192764,
                    Longitude = -73.9973096,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "80 Spring St, New York, New York 10012",
                    Latitude = 40.7226590,
                    Longitude = -73.9981820,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "182 Duane street ny",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "182 Duane St, New York, New York 10013",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "462 broome street nyc",
                    Latitude = 40.7225800,
                    Longitude = -74.0008980,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "117 mercer street nyc",
                    Latitude = 40.7239679,
                    Longitude = -73.9991585,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Lucca antiques\n182 Duane St, New York, New York 10013",
                    Latitude = 40.7167516,
                    Longitude = -74.0087482,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Room and board\n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Lucca antiquesn182 Duane St, New York, New York 10013",
                    Latitude = 40.7167516,
                    Longitude = -74.0087482,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Room and boardn105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Lucca antiques 182 Duane st new York ny",
                    Latitude = 40.7170879,
                    Longitude = -74.0101210,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Property\n14 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "101 Crosby street nyc",
                    Latitude = 40.7235730,
                    Longitude = -73.9969540,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Room and board \n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Propertyn14 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Room and board n105 Wooster street nyc",
                    Latitude = 40.7229097,
                    Longitude = -74.0021852,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Mecox gardens\n926 Lexington nyc",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "25 sybil&apos;s crossing Kent lakes, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString =
                        "10149 ASHDALE LANE\t67\t67393253\t\t\tSANTEE\tCA\t92071\t\t280501691\t67393253\tIFI\t280501691\t05-JUN-10\t67393253",
                    Latitude = 40.7143000,
                    Longitude = -74.0067000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "193 Lakebridge Dr, Kings Paark, NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "219 west creek",
                    Latitude = 40.7198564,
                    Longitude = -74.0121098,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "14 North Moore Street\nNew York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "14 North Moore StreetnNew York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "14 North Moore Street New York, ny",
                    Latitude = 40.7196970,
                    Longitude = -74.0066100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "30-38 Fulton St, New York, New York 10038",
                    Latitude = 40.7077737,
                    Longitude = -74.0043299,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "73 Spring Street Ny NY",
                    Latitude = 40.7225378,
                    Longitude = -73.9976742,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "119 Mercer Street Ny NY",
                    Latitude = 40.7241390,
                    Longitude = -73.9993110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "525 Broadway Ny NY",
                    Latitude = 40.7230410,
                    Longitude = -73.9991650,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Church St",
                    Latitude = 40.7154338,
                    Longitude = -74.0075430,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "135 union stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "21101 coffeen stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "215 Washington stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "619 mill stnWatertown ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "3 canel st, new York, ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "50 grand street",
                    Latitude = 40.7225780,
                    Longitude = -74.0038019,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Orient ferry, li ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Hilton hotel river head li ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "116 park pl",
                    Latitude = 40.7140565,
                    Longitude = -74.0110155,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "long islans new york",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "1 prospect pointe niagra falls ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York City\tNY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "pink berry ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York City\t NY",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "10108",
                    Latitude = 40.7143000,
                    Longitude = -74.0067000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ann st",
                    Latitude = 40.7105937,
                    Longitude = -74.0073715,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Hok 620 ave of Americas new York ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Som 14 wall st nyc",
                    Latitude = 40.7076179,
                    Longitude = -74.0107630,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York ,ny",
                    Latitude = 40.7142691,
                    Longitude = -74.0059729,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "52 prince st. 10012",
                    Latitude = 40.7235840,
                    Longitude = -73.9961170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "451 broadway 10013",
                    Latitude = 40.7205177,
                    Longitude = -74.0009557,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Dover street",
                    Latitude = 40.7087886,
                    Longitude = -74.0008644,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Murray st",
                    Latitude = 40.7148929,
                    Longitude = -74.0113349,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "85 West St, New York, New York",
                    Latitude = 40.7096460,
                    Longitude = -74.0146140,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },
                //125left
                new Address
                {
                    AddressString = "NYC",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "64 trinity place, ny, ny",
                    Latitude = 40.7081649,
                    Longitude = -74.0127168,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "150 broadway ny ny",
                    Latitude = 40.7091850,
                    Longitude = -74.0100330,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Pinegrove Dude Ranch 31 cherrytown Rd Kerhinkson Ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Front street",
                    Latitude = 40.7063990,
                    Longitude = -74.0045493,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "234 canal St new York, NY 10013",
                    Latitude = 40.7177010,
                    Longitude = -73.9999570,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "72 spring street, new york ny 10012",
                    Latitude = 40.7225093,
                    Longitude = -73.9976540,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "150 spring street, new york, ny 10012",
                    Latitude = 40.7242393,
                    Longitude = -74.0014922,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "580 broadway street, new york, ny 10012",
                    Latitude = 40.7244210,
                    Longitude = -73.9970260,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "42 trinity place, new york, ny 10007",
                    Latitude = 40.7074000,
                    Longitude = -74.0135510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "baco ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Micro Tel Inn Alburn New York",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "20 Cedar Close",
                    Latitude = 40.7068734,
                    Longitude = -74.0078613,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "South street",
                    Latitude = 40.7080184,
                    Longitude = -73.9999414,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "47 Lafayette street",
                    Latitude = 40.7159204,
                    Longitude = -74.0027332,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Newyork",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ground Zero, NY",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "GROUND ZERO NY",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "33400 SE Harrison",
                    Latitude = 40.7188400,
                    Longitude = -74.0103330,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york, new york",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "8 Greene St, New York, 10013",
                    Latitude = 40.7206160,
                    Longitude = -74.0027600,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "226 w 44st new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "s street seaport 11 fulton st new york city",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "30 Rockefeller Plaza w 49th St New York City",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "30 Rockefeller Plaza 50th St New York City",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "S. Street Seaport 11 Fulton St. New York City",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "30 rockefeller plaza w 49th st, new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "30 rockefeller plaza 50th st, new york city",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "11 fulton st, new york city",
                    Latitude = 40.7069150,
                    Longitude = -74.0033215,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Big apple",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ny",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York new York",
                    Latitude = 40.7143528,
                    Longitude = -74.0059731,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "83-85 Chambers St, New York, New York 10007",
                    Latitude = 40.7148130,
                    Longitude = -74.0068890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "102 North End Ave NY, NY",
                    Latitude = 40.7147980,
                    Longitude = -74.0159690,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "57 Thompson St, New York, New York 10012",
                    Latitude = 40.7241400,
                    Longitude = -74.0035860,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "nyc, ny",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York NY",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "285 West Broadway New York, NY 10013",
                    Latitude = 40.7208750,
                    Longitude = -74.0046310,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "100 avenue of the americas New York, NY 10013",
                    Latitude = 40.7233120,
                    Longitude = -74.0043950,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "270 Lafeyette st New York, NY 10012",
                    Latitude = 40.7238790,
                    Longitude = -73.9965270,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "560 Broadway New York, NY 10012",
                    Latitude = 40.7238540,
                    Longitude = -73.9974980,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "42 Wooster St New York, NY 10013",
                    Latitude = 40.7223860,
                    Longitude = -74.0024220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "42 Wooster StreetNew York, NY 10013-2230",
                    Latitude = 40.7223633,
                    Longitude = -74.0026240,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "504 Broadway, New York, NY 10012",
                    Latitude = 40.7221444,
                    Longitude = -73.9992714,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "426 Broome Street, New York, NY 10013",
                    Latitude = 40.7213295,
                    Longitude = -73.9987121,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "City hall, nyc",
                    Latitude = 40.7122066,
                    Longitude = -74.0055026,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "South street seaport, nyc",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ground zero, nyc",
                    Latitude = 40.7116410,
                    Longitude = -74.0122530,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Ground zero",
                    Latitude = 40.7116410,
                    Longitude = -74.0122530,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Mulberry and canal, NYC",
                    Latitude = 40.7170900,
                    Longitude = -73.9985900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World Trade Center, NYC",
                    Latitude = 40.7116670,
                    Longitude = -74.0125000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "South Street Seaport",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Wall Street and Nassau Street, NYC",
                    Latitude = 40.7071400,
                    Longitude = -74.0106900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Trinity Church, NYC",
                    Latitude = 40.7081269,
                    Longitude = -74.0125691,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Federal Hall National Memorial",
                    Latitude = 40.7069515,
                    Longitude = -74.0101638,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Little Italy, NYC",
                    Latitude = 40.7196920,
                    Longitude = -73.9977650,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York, NY",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York City, NY,",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york,ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Odeon cinema",
                    Latitude = 40.7168300,
                    Longitude = -74.0080300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York City",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "52 broadway, ny,ny 1004",
                    Latitude = 40.7065000,
                    Longitude = -74.0123000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "52 broadway, ny,ny 10004",
                    Latitude = 40.7065000,
                    Longitude = -74.0123000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "22 beaver st, ny,ny 10004",
                    Latitude = 40.7048200,
                    Longitude = -74.0121800,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "54 pine st,ny,ny 10005",
                    Latitude = 40.7068600,
                    Longitude = -74.0084900,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "114 liberty st, ny,ny 10006",
                    Latitude = 40.7097700,
                    Longitude = -74.0122000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "215 canal st,ny,ny 10013",
                    Latitude = 40.7174700,
                    Longitude = -73.9989500,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "World Trade Center, New York, NY",
                    Latitude = 40.7116700,
                    Longitude = -74.0125000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Chinatown, New York, NY",
                    Latitude = 40.7159600,
                    Longitude = -73.9974100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "101 murray street new york, ny",
                    Latitude = 40.7152600,
                    Longitude = -74.0125100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "nyc",
                    Latitude = 40.7145500,
                    Longitude = -74.0071200,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "510 broadway new york",
                    Latitude = 40.7223400,
                    Longitude = -73.9990160,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "nyc",
                    Latitude = 40.7145502,
                    Longitude = -74.0071249,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Little Italy",
                    Latitude = 40.7196920,
                    Longitude = -73.9977647,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "463 Broadway, New York, NY",
                    Latitude = 40.7210590,
                    Longitude = -74.0006880,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "222 West Broadway, New York, NY",
                    Latitude = 40.7193520,
                    Longitude = -74.0064170,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "270 Lafayette street new York new york",
                    Latitude = 40.7238790,
                    Longitude = -73.9965270,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "New York, NY USA",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "97 Kenmare Street, New York, NY 10012",
                    Latitude = 40.7214370,
                    Longitude = -73.9969110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "19 Beekman St, New York, New York 10038",
                    Latitude = 40.7107540,
                    Longitude = -74.0062870,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Soho",
                    Latitude = 40.7241404,
                    Longitude = -74.0020213,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Bergen, New York",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "478 Broadway, NY, NY",
                    Latitude = 40.7213360,
                    Longitude = -73.9997710,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "555 broadway, ny, ny",
                    Latitude = 40.7238830,
                    Longitude = -73.9982960,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "375 West Broadway, NY, NY",
                    Latitude = 40.7235000,
                    Longitude = -74.0026020,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "35 howard st, NY, NY",
                    Latitude = 40.7195240,
                    Longitude = -74.0010300,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Pier 17 NYC",
                    Latitude = 40.7063660,
                    Longitude = -74.0026890,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "120 Liberty St NYC",
                    Latitude = 40.7097740,
                    Longitude = -74.0124510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "80 White Street, NY, NY",
                    Latitude = 40.7178340,
                    Longitude = -74.0020520,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Manhattan, NY",
                    Latitude = 40.7144300,
                    Longitude = -74.0061000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "22 read st, ny",
                    Latitude = 40.7142010,
                    Longitude = -74.0044910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "130 Mulberry St, New York, NY 10013-5547",
                    Latitude = 40.7182880,
                    Longitude = -73.9977110,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "new york city, ny",
                    Latitude = 40.7145500,
                    Longitude = -74.0071250,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "10038",
                    Latitude = 40.7092119,
                    Longitude = -74.0033631,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "11 Wall St, New York, NY 10005-1905",
                    Latitude = 40.7072900,
                    Longitude = -74.0112010,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "89 Reade St, New York, New York 10007",
                    Latitude = 40.7134560,
                    Longitude = -74.0034990,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "265 Canal St, New York, NY 10013-6010",
                    Latitude = 40.7188850,
                    Longitude = -74.0009000,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "39 Broadway, New York, NY 10006-3003",
                    Latitude = 40.7133450,
                    Longitude = -73.9961320,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "25 beaver street new york ny",
                    Latitude = 40.7051110,
                    Longitude = -74.0120070,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "100 church street new york ny",
                    Latitude = 40.7130430,
                    Longitude = -74.0096370,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "69 Mercer St, New York, NY 10012-4440",
                    Latitude = 40.7226490,
                    Longitude = -74.0006100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "111 Worth St, New York, NY 10013-4008",
                    Latitude = 40.7159210,
                    Longitude = -74.0034100,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "240-248 Broadway, New York, New York 10038",
                    Latitude = 40.7127690,
                    Longitude = -74.0076810,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "12 Maiden Ln, New York, NY 10038-4002",
                    Latitude = 40.7094460,
                    Longitude = -74.0095760,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "291 Broadway, New York, NY 10007-1814",
                    Latitude = 40.7150000,
                    Longitude = -74.0061340,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "55 Liberty St, New York, NY 10005-1003",
                    Latitude = 40.7088430,
                    Longitude = -74.0093840,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "Brooklyn Bridge, NY",
                    Latitude = 40.7063440,
                    Longitude = -73.9974390,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "wall street",
                    Latitude = 40.7063889,
                    Longitude = -74.0094444,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "south street seaport, ny",
                    Latitude = 40.7069501,
                    Longitude = -74.0030848,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "little italy, ny",
                    Latitude = 40.7196920,
                    Longitude = -73.9977647,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "47 Pine St, New York, NY 10005-1513",
                    Latitude = 40.7067340,
                    Longitude = -74.0089280,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "22 cortlandt street new york ny",
                    Latitude = 40.7100820,
                    Longitude = -74.0102510,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "105 reade street new york ny",
                    Latitude = 40.7156330,
                    Longitude = -74.0085220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "2 lafayette street new york ny",
                    Latitude = 40.7140310,
                    Longitude = -74.0038910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "53 crosby street new york ny",
                    Latitude = 40.7219770,
                    Longitude = -73.9982450,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "2 Lafayette St, New York, NY 10007-1307",
                    Latitude = 40.7140310,
                    Longitude = -74.0038910,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "105 Reade St, New York, NY 10013-3840",
                    Latitude = 40.7156330,
                    Longitude = -74.0085220,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "chinatown, ny",
                    Latitude = 40.7159556,
                    Longitude = -73.9974133,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "250 Broadway, New York, NY 10007-2516",
                    Latitude = 40.7130180,
                    Longitude = -74.0074700,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "156 William St, New York, NY 10038-2609",
                    Latitude = 40.7097970,
                    Longitude = -74.0055770,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "100 Church St, New York, NY 10007-2601",
                    Latitude = 40.7130430,
                    Longitude = -74.0096370,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                },

                new Address
                {
                    AddressString = "33 Beaver St, New York, NY 10004-2736",
                    Latitude = 40.7050980,
                    Longitude = -74.0117200,
                    Time = 0,
                    TimeWindowStart = null,
                    TimeWindowEnd = null
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Single Depot, Multiple Driver, No Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RT = true,
                RouteMaxDuration = 86400,
                VehicleCapacity = 20,
                VehicleMaxDistanceMI = 99999,
                Parts = 4,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            //Note: The addresses of this test aren't permited for api_key=11111111111111111111111111111111. 
            // If you put in the parameter api_key your valid key, the test will be finished successfuly.

            //Assert.IsNull(dataObject, "SingleDepotMultipleDriverNoTimeWindowTest failed... " + errorString);
            if (skip == "yes")
            {
            }
            else
            {
                // Run the query
                dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

                Assert.IsNotNull(
                    dataObject,
                    "SingleDepotMultipleDriverNoTimeWindowTest failed. " + errorString);

                tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
            }
        }

        [TestMethod]
        public void MultipleDepotMultipleDriverWith24StopsTimeWindowTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

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

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400 * 3,
                VehicleCapacity = 5,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.TimeWithTraffic.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            dataObjectMDMD24 = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObjectMDMD24,
                "MultipleDepotMultipleDriverWith24StopsTimeWindowTest failed.. " + errorString);

            MoveDestinationToRouteTest();

            MergeRoutesTest();
        }

        public void MoveDestinationToRouteTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            Assert.IsNotNull(dataObjectMDMD24, "dataObjectMDMD24 is null.");

            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = dataObjectMDMD24.OptimizationProblemId
            };

            var dataObjectDetails = route4Me.GetOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObjectDetails,
                "Retrieving of the optimizations failed. " + errorString);

            Assert.IsTrue(
                dataObjectDetails.Routes.Length >= 2,
                "There is no 2 routes for moving a destination to other route.");

            var route1 = dataObjectDetails.Routes[0];

            Assert.IsTrue(
                route1.Addresses.Length >= 2,
                "There is less than 2 addresses in the generated route..");

            var routeDestinationIdToMove = route1.Addresses[2].RouteDestinationId != null
                ? Convert.ToInt32(route1.Addresses[1].RouteDestinationId)
                : -1;

            Assert.IsTrue(routeDestinationIdToMove > 0, "Wrong destination_id to move: " + routeDestinationIdToMove);

            var route2 = dataObjectDetails.Routes[1];

            Assert.IsTrue(route1.Addresses.Length >= 2, "There is less than 2 addresses in the generated route...");

            var afterDestinationIdToMoveAfter = route2.Addresses[1].RouteDestinationId != null
                ? Convert.ToInt32(route2.Addresses[1].RouteDestinationId)
                : -1;

            Assert.IsTrue(afterDestinationIdToMoveAfter > 0,
                "Wrong destination_id to move after: " + afterDestinationIdToMoveAfter);

            var result = route4Me.MoveDestinationToRoute(route2.RouteId, routeDestinationIdToMove,
                afterDestinationIdToMoveAfter, out errorString);

            Assert.IsTrue(result, "MoveDestinationToRouteTest failed." + errorString);
        }

        [TestMethod]
        public void MergeRoutesTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            tdr.MultipleDepotMultipleDriverWith24StopsTimeWindowTest();
            dataObjectMDMD24 = tdr.DataObjectMDMD24;

            Assert.IsNotNull(dataObjectMDMD24, "dataObjectMDMD24 is null.");

            Assert.IsTrue(
                dataObjectMDMD24.Routes.Length >= 2,
                "There is no 2 routes for moving a destination to other route.");

            var route1 = dataObjectMDMD24.Routes[0];

            Assert.IsTrue(
                route1.Addresses.Length >= 2,
                "There is less than 2 addresses in the generated route.");

            var route2 = dataObjectMDMD24.Routes[1];

            var mergeRoutesParameters = new MergeRoutesQuery
            {
                RouteIds = route1.RouteId + "," + route2.RouteId,
                DepotAddress = route1.Addresses[0].AddressString,
                RemoveOrigin = false,
                DepotLat = route1.Addresses[0].Latitude,
                DepotLng = route1.Addresses[0].Longitude
            };

            var result = route4Me.MergeRoutes(mergeRoutesParameters, out var errorString);

            Assert.IsTrue(result, "MergeRoutesTest failed. " + errorString);

            //tdr.RemoveOptimization(new string[] { dataObjectMDMD24.OptimizationProblemId });
        }

        [TestMethod]
        public void TruckingSingleDriverMultipleTimeWindowsTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "455 S 4th St, Louisville, KY 40202",
                    //all possible originating locations are depots, should be marked as true
                    //stylistically we recommend all depots should be at the top of the destinations list
                    IsDepot = true,
                    Latitude = 38.251698,
                    Longitude = -85.757308
                },

                new Address
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Latitude = 38.141598,
                    Longitude = -85.7938461,

                    //together these two specify the time window of a destination
                    //seconds offset relative to the route start time for the open availability of a destination
                    TimeWindowStart = 7 * 3600 + 30 * 60,
                    //seconds offset relative to the route end time for the open availability of a destination
                    TimeWindowEnd = 7 * 3600 + 40 * 60,

                    // Second 'TimeWindowStart'
                    TimeWindowStart2 = 8 * 3600 + 00 * 60,
                    // Second 'TimeWindowEnd'
                    TimeWindowEnd2 = 8 * 3600 + 10 * 60,

                    //the number of seconds at destination
                    Time = 300
                },

                new Address
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    TimeWindowStart = 8 * 3600 + 30 * 60,
                    TimeWindowEnd = 8 * 3600 + 40 * 60,
                    TimeWindowStart2 = 8 * 3600 + 50 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 0 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    TimeWindowStart = 9 * 3600 + 0 * 60,
                    TimeWindowEnd = 9 * 3600 + 15 * 60,
                    TimeWindowStart2 = 9 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 45 * 60,
                    Time = 100
                },

                new Address
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    TimeWindowStart = 10 * 3600 + 00 * 60,
                    TimeWindowEnd = 10 * 3600 + 15 * 60,
                    TimeWindowStart2 = 10 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 10 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    TimeWindowStart = 11 * 3600 + 00 * 60,
                    TimeWindowEnd = 11 * 3600 + 15 * 60,
                    TimeWindowStart2 = 11 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 11 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    TimeWindowStart = 12 * 3600 + 00 * 60,
                    TimeWindowEnd = 12 * 3600 + 15 * 60,
                    TimeWindowStart2 = 12 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 12 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    TimeWindowStart = 13 * 3600 + 00 * 60,
                    TimeWindowEnd = 13 * 3600 + 15 * 60,
                    TimeWindowStart2 = 13 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 13 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    TimeWindowStart = 14 * 3600 + 00 * 60,
                    TimeWindowEnd = 14 * 3600 + 15 * 60,
                    TimeWindowStart2 = 14 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 14 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    TimeWindowStart = 15 * 3600 + 00 * 60,
                    TimeWindowEnd = 15 * 3600 + 15 * 60,
                    TimeWindowStart2 = 15 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 15 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    TimeWindowStart = 16 * 3600 + 00 * 60,
                    TimeWindowEnd = 16 * 3600 + 15 * 60,
                    TimeWindowStart2 = 16 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 16 * 3600 + 45 * 60,
                    Time = 300
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = "Trucking SD Multiple TW from c# SDK " + DateTime.Now.ToString("yymMddHHmmss"),
                OptimizationQuality = 3,
                DeviceType = DeviceType.Web.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                Dirm = 3,
                DM = 6,
                Optimize = Optimize.TimeWithTraffic.Description(),
                RouteMaxDuration = 8 * 3600 + 30 * 60,
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 7 * 3600 + 00 * 60,
                TravelMode = TravelMode.Driving.Description(),
                VehicleMaxCargoWeight = 30,
                VehicleCapacity = 10,
                VehicleMaxDistanceMI = 10000,
                TruckHeightMeters = 4,
                TruckLengthMeters = 12,
                TruckWidthMeters = 3,
                TrailerWeightT = 10,
                WeightPerAxleT = 10,
                LimitedWeightT = 20,
                RT = true
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            dataObject = route4Me.RunAsyncOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(dataObject, "SingleDriverMultipleTimeWindowsTest failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void SingleDriverMultipleTimeWindowsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "3634 W Market St, Fairlawn, OH 44333",
                    //all possible originating locations are depots, should be marked as true
                    //stylistically we recommend all depots should be at the top of the destinations list
                    IsDepot = true,
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,

                    TimeWindowStart = null,
                    TimeWindowEnd = null,
                    TimeWindowStart2 = null,
                    TimeWindowEnd2 = null,
                    Time = null
                },

                new Address
                {
                    AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,

                    //together these two specify the time window of a destination
                    //seconds offset relative to the route start time for the open availability of a destination
                    TimeWindowStart = 6 * 3600 + 00 * 60,
                    //seconds offset relative to the route end time for the open availability of a destination
                    TimeWindowEnd = 6 * 3600 + 30 * 60,

                    // Second 'TimeWindowStart'
                    TimeWindowStart2 = 7 * 3600 + 00 * 60,
                    // Second 'TimeWindowEnd'
                    TimeWindowEnd2 = 7 * 3600 + 20 * 60,

                    //the number of seconds at destination
                    Time = 300
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 7 * 3600 + 30 * 60,
                    TimeWindowEnd = 7 * 3600 + 40 * 60,
                    TimeWindowStart2 = 8 * 3600 + 00 * 60,
                    TimeWindowEnd2 = 8 * 3600 + 10 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 8 * 3600 + 30 * 60,
                    TimeWindowEnd = 8 * 3600 + 40 * 60,
                    TimeWindowStart2 = 8 * 3600 + 50 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 00 * 60,
                    Time = 100
                },

                new Address
                {
                    AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                    Latitude = 41.162971496582,
                    Longitude = -81.479049682617,
                    TimeWindowStart = 9 * 3600 + 00 * 60,
                    TimeWindowEnd = 9 * 3600 + 15 * 60,
                    TimeWindowStart2 = 9 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                    Latitude = 41.194505989552,
                    Longitude = -81.443351581693,
                    TimeWindowStart = 10 * 3600 + 00 * 60,
                    TimeWindowEnd = 10 * 3600 + 15 * 60,
                    TimeWindowStart2 = 10 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 10 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "2705 N River Rd, Stow, OH 44224",
                    Latitude = 41.145240783691,
                    Longitude = -81.410247802734,
                    TimeWindowStart = 11 * 3600 + 00 * 60,
                    TimeWindowEnd = 11 * 3600 + 15 * 60,
                    TimeWindowStart2 = 11 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 11 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                    Latitude = 41.340042114258,
                    Longitude = -81.421226501465,
                    TimeWindowStart = 12 * 3600 + 00 * 60,
                    TimeWindowEnd = 12 * 3600 + 15 * 60,
                    TimeWindowStart2 = 12 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 12 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
                    TimeWindowStart = 13 * 3600 + 00 * 60,
                    TimeWindowEnd = 13 * 3600 + 15 * 60,
                    TimeWindowStart2 = 13 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 13 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
                    TimeWindowStart = 14 * 3600 + 00 * 60,
                    TimeWindowEnd = 14 * 3600 + 15 * 60,
                    TimeWindowStart2 = 14 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 14 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 15 * 3600 + 00 * 60,
                    TimeWindowEnd = 15 * 3600 + 15 * 60,
                    TimeWindowStart2 = 15 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 15 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                    Latitude = 41.315116882324,
                    Longitude = -81.558746337891,
                    TimeWindowStart = 16 * 3600 + 00 * 60,
                    TimeWindowEnd = 16 * 3600 + 15 * 60,
                    TimeWindowStart2 = 16 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 17 * 3600 + 00 * 60,
                    Time = 50
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "Single Driver Multiple TimeWindows 12 Stops",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 5 * 3600 + 30 * 60,
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
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(dataObject, "SingleDriverMultipleTimeWindowsTest failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void OptimizationByOrderTerritoriesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = "Optimization by order territories, " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                is_dynamic_start_time = false,
                Optimize = "Time",
                IgnoreTw = false,
                Parts = 10,
                RT = false,
                LockLast = false,
                DisableOptimization = false,
                VehicleId = ""
            };

            var orderTerritories = new OrderTerritories
            {
                SplitTerritories = true,
                TerritoriesId = new[] {"5E66A5AFAB087B08E690DFAE4F8B151B", "6160CFC4CC3CD508409D238E04D6F6C4"},
                filters = new FilterDetails
                {
                    // Specified as 'all' for test purpose - after the first optimization, the orders become routed and the test is failing.
                    // For real tasks should be specified as 'unrouted'
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new[] {"2021-09-21"}
                }
            };

            var depots = new Address[1]
            {
                new Address
                {
                    Alias = "HQ1",
                    AddressString = "1010 N Florida ave, Tampa, FL",
                    IsDepot = true,
                    Latitude = 27.952941,
                    Longitude = -82.459493,
                    Time = 0
                }
            };

            var optimizationParameters = new OptimizationParameters
            {
                Redirect = false,
                OrderTerritories = orderTerritories,
                Parameters = parameters,
                Depots = depots
            };

            // Run the query
            var dataObjects = route4Me.RunOptimizationByOrderTerritories(optimizationParameters, out var errorString);


            Assert.IsNotNull(dataObjects, "OptimizationByOrderTerritoriesTest failed. " + errorString);

            var returnedOptimizations = dataObjects.Length;

            foreach (var optProblem in dataObjects)
                tdr.RemoveOptimization(new[] {optProblem.OptimizationProblemId});

            Assert.IsTrue(returnedOptimizations == 2,
                "OptimizationByOrderTerritoriesTest failed - smart optimization ID not returned.");
        }

        [TestMethod]
        public void BundledAddressesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            if (!route4Me.MemberHasCommercialCapability(
                c_ApiKey,
                ApiKeys.DemoApiKey,
                out var errorString0)) return;

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "3634 W Market St, Fairlawn, OH 44333",
                    //all possible originating locations are depots, should be marked as true
                    //stylistically we recommend all depots should be at the top of the destinations list
                    IsDepot = true,
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,

                    TimeWindowStart = null,
                    TimeWindowEnd = null,
                    TimeWindowStart2 = null,
                    TimeWindowEnd2 = null,
                    Time = null
                },

                new Address
                {
                    AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                    Latitude = 41.135762259364,
                    Longitude = -81.629313826561,

                    //together these two specify the time window of a destination
                    //seconds offset relative to the route start time for the open availability of a destination
                    TimeWindowStart = 6 * 3600 + 00 * 60,
                    //seconds offset relative to the route end time for the open availability of a destination
                    TimeWindowEnd = 6 * 3600 + 30 * 60,

                    // Second 'TimeWindowStart'
                    TimeWindowStart2 = 7 * 3600 + 00 * 60,
                    // Second 'TimeWindowEnd'
                    TimeWindowEnd2 = 7 * 3600 + 20 * 60,

                    //the number of seconds at destination
                    Time = 300
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 7 * 3600 + 30 * 60,
                    TimeWindowEnd = 7 * 3600 + 40 * 60,
                    TimeWindowStart2 = 8 * 3600 + 00 * 60,
                    TimeWindowEnd2 = 8 * 3600 + 10 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 8 * 3600 + 30 * 60,
                    TimeWindowEnd = 8 * 3600 + 40 * 60,
                    TimeWindowStart2 = 8 * 3600 + 50 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 00 * 60,
                    Time = 100
                },

                new Address
                {
                    AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                    Latitude = 41.162971496582,
                    Longitude = -81.479049682617,
                    TimeWindowStart = 9 * 3600 + 00 * 60,
                    TimeWindowEnd = 9 * 3600 + 15 * 60,
                    TimeWindowStart2 = 9 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 9 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                    Latitude = 41.194505989552,
                    Longitude = -81.443351581693,
                    TimeWindowStart = 10 * 3600 + 00 * 60,
                    TimeWindowEnd = 10 * 3600 + 15 * 60,
                    TimeWindowStart2 = 10 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 10 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "2705 N River Rd, Stow, OH 44224",
                    Latitude = 41.145240783691,
                    Longitude = -81.410247802734,
                    TimeWindowStart = 11 * 3600 + 00 * 60,
                    TimeWindowEnd = 11 * 3600 + 15 * 60,
                    TimeWindowStart2 = 11 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 11 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                    Latitude = 41.340042114258,
                    Longitude = -81.421226501465,
                    TimeWindowStart = 12 * 3600 + 00 * 60,
                    TimeWindowEnd = 12 * 3600 + 15 * 60,
                    TimeWindowStart2 = 12 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 12 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
                    TimeWindowStart = 13 * 3600 + 00 * 60,
                    TimeWindowEnd = 13 * 3600 + 15 * 60,
                    TimeWindowStart2 = 13 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 13 * 3600 + 45 * 60,
                    Time = 300,
                    Cube = 3
                },

                new Address
                {
                    AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                    Latitude = 41.148578643799,
                    Longitude = -81.429229736328,
                    TimeWindowStart = 14 * 3600 + 00 * 60,
                    TimeWindowEnd = 14 * 3600 + 15 * 60,
                    TimeWindowStart2 = 14 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 14 * 3600 + 45 * 60,
                    Time = 300,
                    Cube = 2
                },

                new Address
                {
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    Latitude = 41.003671512008,
                    Longitude = -81.598461046815,
                    TimeWindowStart = 15 * 3600 + 00 * 60,
                    TimeWindowEnd = 15 * 3600 + 15 * 60,
                    TimeWindowStart2 = 15 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 15 * 3600 + 45 * 60,
                    Time = 300
                },

                new Address
                {
                    AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                    Latitude = 41.315116882324,
                    Longitude = -81.558746337891,
                    TimeWindowStart = 16 * 3600 + 00 * 60,
                    TimeWindowEnd = 16 * 3600 + 15 * 60,
                    TimeWindowStart2 = 16 * 3600 + 30 * 60,
                    TimeWindowEnd2 = 17 * 3600 + 00 * 60,
                    Time = 50
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "SD Multiple TW Address Bundling " + DateTime.Now.ToString("yyyy-MM-dd"),

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 5 * 3600 + 30 * 60,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                Bundling = new AddressBundling
                {
                    Mode = AddressBundlingMode.Address,
                    MergeMode = AddressBundlingMergeMode.KeepAsSeparateDestinations,
                    ServiceTimeRules = new ServiceTimeRulesClass
                    {
                        FirstItemMode = AddressBundlingFirstItemMode.KeepOriginal,
                        AdditionalItemsMode = AddressBundlingAdditionalItemsMode.KeepOriginal
                    }
                }
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(dataObject, "SingleDriverMultipleTimeWindowsTest failed. " + errorString);
            Assert.IsTrue(dataObject.Routes.Length > 0, "The optimization doesn't contain route");
            var routeId = dataObject.Routes[0].RouteId;

            Assert.IsTrue(routeId != null && routeId.Length == 32, "The route ID is not valid");

            var routeQueryParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                BundlingItems = true
            };

            var routeBundled = route4Me.GetRoute(routeQueryParameters, out errorString);

            Assert.IsNotNull(routeBundled, "Cannot retrieve the route. " + errorString);
            Assert.IsNotNull(
                routeBundled.BundleItems,
                "Cannot retrieve bundled items in the route response.");

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void GetScheduleCalendarTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            if (!route4Me.MemberHasCommercialCapability(c_ApiKey, ApiKeys.DemoApiKey, out var errorString0)) return;

            var days5 = new TimeSpan(5, 0, 0, 0);

            var calendarQuery = new ScheduleCalendarQuery
            {
                DateFromString = (DateTime.Now - days5).ToString("yyyy-MM-dd"),
                DateToString = (DateTime.Now + days5).ToString("yyyy-MM-dd"),
                TimezoneOffsetMinutes = 4 * 60,
                ShowOrders = true,
                ShowContacts = true,
                RoutesCount = true
            };

            var scheduleCalendar = route4Me.GetScheduleCalendar(calendarQuery, out var errorString);

            Assert.IsNotNull(scheduleCalendar, "The test GetScheduleCalendarTest failed");

            Assert.IsNotNull(scheduleCalendar.AddressBook, "The test GetScheduleCalendarTest failed");
            Assert.IsNotNull(scheduleCalendar.Orders, "The test GetScheduleCalendarTest failed");
            Assert.IsNotNull(scheduleCalendar.RoutesCount, "The test GetScheduleCalendarTest failed");
        }

        [TestMethod]
        public void SingleDriverRoundTripGenericTest()
        {
            var myApiKey = ApiKeys.ActualApiKey;

            // Create the manager with the api key
            var route4Me = new Route4MeManager(myApiKey);

            // Prepare the addresses
            // Using the defined class, can use user-defined class instead
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
            // Using the defined class, can use user-defined class instead
            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

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
            var dataObject0 = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(dataObject0, "SingleDriverRoundTripGenericTest failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject0.OptimizationProblemId});
        }

        [TestMethod]
        public void RunSingleDriverRoundTrip()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

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
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

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
            dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(dataObject, "RunSingleDriverRoundTrip failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject.OptimizationProblemId});
        }

        [TestMethod]
        public void RunOptimizationSingleDriverRoute10StopsTest()
        {
            var r4mm = new Route4MeManager(c_ApiKey);

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
            var dataObject1 = r4mm.RunOptimization(optimizationParameters, out var errorString);

            Assert.IsNotNull(
                dataObject1,
                "Run optimization test with Single Driver Route 10 Stops failed. " + errorString);

            tdr.RemoveOptimization(new[] {dataObject1.OptimizationProblemId});
        }

        [TestMethod]
        public void Route300StopsTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "449 Schiller st Elizabeth, NJ, 07206",
                    Alias = "449 Schiller st Elizabeth, NJ, 07206",
                    IsDepot = true,
                    Latitude = 40.6608211,
                    Longitude = -74.1827578,
                    Time = 0
                },

                new Address
                {
                    AddressString = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
                    Alias = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.355035,
                    Longitude = -74.441433,
                    Time = 0
                },

                new Address
                {
                    AddressString = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.361713,
                    Longitude = -74.428145,
                    Time = 0
                },

                new Address
                {
                    AddressString = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
                    Alias = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
                    IsDepot = false,
                    Latitude = 40.314719,
                    Longitude = -74.248578,
                    Time = 0
                },

                new Address
                {
                    AddressString = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
                    Alias = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
                    IsDepot = false,
                    Latitude = 40.756385,
                    Longitude = -74.187419,
                    Time = 0
                },

                new Address
                {
                    AddressString = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
                    Alias = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
                    IsDepot = false,
                    Latitude = 40.301426,
                    Longitude = -74.259929,
                    Time = 0
                },

                new Address
                {
                    AddressString = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
                    Alias = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.368782,
                    Longitude = -74.438739,
                    Time = 0
                },

                new Address
                {
                    AddressString = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
                    Alias = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
                    IsDepot = false,
                    Latitude = 40.095338,
                    Longitude = -74.144739,
                    Time = 0
                },

                new Address
                {
                    AddressString = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
                    Alias = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
                    IsDepot = false,
                    Latitude = 40.763848,
                    Longitude = -74.228196,
                    Time = 0
                },

                new Address
                {
                    AddressString = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
                    Alias = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.351864,
                    Longitude = -74.448293,
                    Time = 0
                },

                new Address
                {
                    AddressString = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
                    Alias = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.357207,
                    Longitude = -74.440922,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
                    Alias = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.864225,
                    Longitude = -74.139027,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
                    Alias = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.708466,
                    Longitude = -74.201882,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
                    Alias = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.36423,
                    Longitude = -74.414019,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
                    Alias = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
                    IsDepot = false,
                    Latitude = 40.37812,
                    Longitude = -74.305547,
                    Time = 0
                },

                new Address
                {
                    AddressString = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.362037,
                    Longitude = -74.427806,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
                    Alias = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
                    IsDepot = false,
                    Latitude = 40.76518,
                    Longitude = -74.211008,
                    Time = 0
                },

                new Address
                {
                    AddressString = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
                    Alias = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
                    IsDepot = false,
                    Latitude = 40.263924,
                    Longitude = -74.040861,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
                    Alias = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
                    IsDepot = false,
                    Latitude = 40.922167,
                    Longitude = -74.163824,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
                    Alias = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
                    IsDepot = false,
                    Latitude = 40.524289,
                    Longitude = -74.287035,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
                    Alias = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
                    IsDepot = false,
                    Latitude = 40.441791,
                    Longitude = -74.133082,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
                    Alias = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
                    IsDepot = false,
                    Latitude = 40.302707,
                    Longitude = -73.987299,
                    Time = 0
                },

                new Address
                {
                    AddressString = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
                    Alias = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
                    IsDepot = false,
                    Latitude = 40.918104,
                    Longitude = -74.151194,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
                    Alias = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.721587,
                    Longitude = -74.201352,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
                    Alias = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
                    IsDepot = false,
                    Latitude = 40.371677,
                    Longitude = -73.999631,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
                    Alias = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.366843,
                    Longitude = -74.08326,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
                    Alias = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
                    IsDepot = false,
                    Latitude = 40.366843,
                    Longitude = -74.08326,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
                    Alias = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
                    IsDepot = false,
                    Latitude = 40.4138,
                    Longitude = -74.037514,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
                    Alias = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
                    IsDepot = false,
                    Latitude = 39.403741,
                    Longitude = -74.511303,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
                    Alias = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
                    IsDepot = false,
                    Latitude = 39.380853,
                    Longitude = -74.495093,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
                    Alias = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.351437,
                    Longitude = -74.455519,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
                    Alias = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
                    IsDepot = false,
                    Latitude = 39.941312,
                    Longitude = -74.074916,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
                    Alias = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
                    IsDepot = false,
                    Latitude = 40.430159,
                    Longitude = -74.251723,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
                    Alias = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.462466,
                    Longitude = -74.402632,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
                    Alias = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.462783,
                    Longitude = -74.327999,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
                    Alias = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
                    IsDepot = false,
                    Latitude = 40.348985,
                    Longitude = -74.073624,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
                    Alias = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
                    IsDepot = false,
                    Latitude = 40.91279,
                    Longitude = -74.138676,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
                    Alias = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.782701,
                    Longitude = -74.166163,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
                    Alias = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
                    IsDepot = false,
                    Latitude = 40.442036,
                    Longitude = -74.116429,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
                    Alias = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.452799,
                    Longitude = -74.299858,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
                    Alias = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.920487,
                    Longitude = -74.166298,
                    Time = 0
                },

                new Address
                {
                    AddressString = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
                    Alias = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
                    IsDepot = false,
                    Latitude = 40.893342,
                    Longitude = -74.107102,
                    Time = 0
                },

                new Address
                {
                    AddressString = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
                    Alias = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
                    IsDepot = false,
                    Latitude = 40.905749,
                    Longitude = -74.147813,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
                    Alias = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
                    IsDepot = false,
                    Latitude = 40.436711,
                    Longitude = -74.111739,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
                    Alias = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
                    IsDepot = false,
                    Latitude = 40.374892,
                    Longitude = -74.013428,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
                    Alias = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
                    IsDepot = false,
                    Latitude = 40.045475,
                    Longitude = -74.094392,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
                    Alias = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.891322,
                    Longitude = -74.149694,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
                    Alias = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
                    IsDepot = false,
                    Latitude = 40.380837,
                    Longitude = -74.388253,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
                    Alias = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
                    IsDepot = false,
                    Latitude = 40.499122,
                    Longitude = -74.451908,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
                    Alias = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.734721,
                    Longitude = -74.223831,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Café Sical, 56 Obert Street, South River, NJ, 08882",
                    Alias = "Café Sical, 56 Obert Street, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.45067,
                    Longitude = -74.380567,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.35569,
                    Longitude = -74.444721,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
                    Alias = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
                    IsDepot = false,
                    Latitude = 40.888972,
                    Longitude = -74.045214,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
                    Alias = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.361182,
                    Longitude = -74.437285,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
                    Alias = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
                    IsDepot = false,
                    Latitude = 40.76121,
                    Longitude = -74.169224,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
                    Alias = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.074549,
                    Longitude = -74.215903,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
                    Alias = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
                    IsDepot = false,
                    Latitude = 39.887461,
                    Longitude = -74.156648,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.368863,
                    Longitude = -74.416528,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
                    Alias = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
                    IsDepot = false,
                    Latitude = 39.338153,
                    Longitude = -74.482597,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.360264,
                    Longitude = -74.432264,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
                    Alias = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
                    IsDepot = false,
                    Latitude = 40.441981,
                    Longitude = -74.12276,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
                    Alias = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.365509,
                    Longitude = -74.429001,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
                    Alias = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.449313,
                    Longitude = -74.236787,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
                    Alias = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
                    IsDepot = false,
                    Latitude = 40.400419,
                    Longitude = -73.984715,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
                    Alias = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
                    IsDepot = false,
                    Latitude = 40.006828,
                    Longitude = -74.242188,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
                    Alias = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
                    IsDepot = false,
                    Latitude = 39.916714,
                    Longitude = -74.15386,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
                    Alias = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
                    IsDepot = false,
                    Latitude = 40.260143,
                    Longitude = -74.409921,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
                    Alias = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
                    IsDepot = false,
                    Latitude = 40.918597,
                    Longitude = -74.154093,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
                    Alias = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
                    IsDepot = false,
                    Latitude = 40.887559,
                    Longitude = -74.041441,
                    Time = 0
                },

                new Address
                {
                    AddressString = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
                    Alias = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
                    IsDepot = false,
                    Latitude = 40.933369,
                    Longitude = -74.172208,
                    Time = 0
                },

                new Address
                {
                    AddressString = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
                    Alias = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
                    IsDepot = false,
                    Latitude = 39.391235,
                    Longitude = -74.522571,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
                    Alias = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.735982,
                    Longitude = -74.169955,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
                    Alias = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
                    IsDepot = false,
                    Latitude = 39.491728,
                    Longitude = -74.503715,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Community Deli, 546 Market St, East Orange, NJ, 07042",
                    Alias = "Community Deli, 546 Market St, East Orange, NJ, 07042",
                    IsDepot = false,
                    Latitude = 40.911747,
                    Longitude = -74.155516,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
                    Alias = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
                    IsDepot = false,
                    Latitude = 40.2842,
                    Longitude = -74.02012,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
                    Alias = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.906704,
                    Longitude = -74.158671,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
                    Alias = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
                    IsDepot = false,
                    Latitude = 40.90601,
                    Longitude = -74.150362,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
                    Alias = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
                    IsDepot = false,
                    Latitude = 40.135683,
                    Longitude = -74.062333,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
                    Alias = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
                    IsDepot = false,
                    Latitude = 40.200035,
                    Longitude = -74.019095,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
                    Alias = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
                    IsDepot = false,
                    Latitude = 39.416868,
                    Longitude = -74.569141,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
                    Alias = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
                    IsDepot = false,
                    Latitude = 40.079909,
                    Longitude = -74.083889,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
                    Alias = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
                    IsDepot = false,
                    Latitude = 39.973935,
                    Longitude = -74.137087,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
                    Alias = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
                    IsDepot = false,
                    Latitude = 39.882705,
                    Longitude = -74.164435,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
                    Alias = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
                    IsDepot = false,
                    Latitude = 40.258843,
                    Longitude = -74.398019,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
                    Alias = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
                    IsDepot = false,
                    Latitude = 40.383938,
                    Longitude = -74.241525,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
                    Alias = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.458048,
                    Longitude = -74.305937,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
                    Alias = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
                    IsDepot = false,
                    Latitude = 40.316134,
                    Longitude = -74.440031,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
                    Alias = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
                    IsDepot = false,
                    Latitude = 40.253485,
                    Longitude = -74.000852,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
                    Alias = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.863711,
                    Longitude = -74.116357,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
                    Alias = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.091107,
                    Longitude = -74.216751,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
                    Alias = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.358413,
                    Longitude = -74.462155,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
                    Alias = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
                    IsDepot = false,
                    Latitude = 40.768005,
                    Longitude = -74.232605,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
                    Alias = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
                    IsDepot = false,
                    Latitude = 40.915152,
                    Longitude = -74.173859,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                    Alias = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.332559,
                    Longitude = -74.074423,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
                    Alias = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
                    IsDepot = false,
                    Latitude = 40.219227,
                    Longitude = -74.003708,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
                    Alias = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.349847,
                    Longitude = -74.457832,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
                    Alias = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.713875,
                    Longitude = -74.229677,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
                    Alias = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.438094,
                    Longitude = -74.199867,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
                    Alias = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
                    IsDepot = false,
                    Latitude = 40.913352,
                    Longitude = -74.143493,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
                    Alias = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.867712,
                    Longitude = -74.122705,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
                    Alias = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.906332,
                    Longitude = -74.153318,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
                    Alias = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
                    IsDepot = false,
                    Latitude = 39.985037,
                    Longitude = -74.20969,
                    Time = 0
                },

                new Address
                {
                    AddressString = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
                    Alias = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.949274,
                    Longitude = -74.149605,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
                    Alias = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.720118,
                    Longitude = -74.186768,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
                    Alias = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
                    IsDepot = false,
                    Latitude = 40.401578,
                    Longitude = -74.228494,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
                    Alias = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
                    IsDepot = false,
                    Latitude = 40.713666,
                    Longitude = -74.214332,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
                    Alias = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.851854,
                    Longitude = -74.234064,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
                    Alias = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
                    IsDepot = false,
                    Latitude = 40.079245,
                    Longitude = -74.087066,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
                    Alias = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.872671,
                    Longitude = -74.192393,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
                    Alias = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
                    IsDepot = false,
                    Latitude = 40.353539,
                    Longitude = -74.306081,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
                    Alias = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
                    IsDepot = false,
                    Latitude = 40.637435,
                    Longitude = -74.265105,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
                    Alias = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.460145,
                    Longitude = -74.360907,
                    Time = 0
                },

                new Address
                {
                    AddressString = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
                    Alias = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.872287,
                    Longitude = -74.12229,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
                    Alias = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.372044,
                    Longitude = -74.421096,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
                    Alias = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.914935,
                    Longitude = -74.156819,
                    Time = 0
                },

                new Address
                {
                    AddressString = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
                    Alias = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
                    IsDepot = false,
                    Latitude = 40.413219,
                    Longitude = -74.037804,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
                    Alias = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
                    IsDepot = false,
                    Latitude = 40.437628,
                    Longitude = -74.141357,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
                    Alias = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.369874,
                    Longitude = -74.412611,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
                    Alias = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.358182,
                    Longitude = -74.443172,
                    Time = 0
                },

                new Address
                {
                    AddressString = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
                    Alias = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
                    IsDepot = false,
                    Latitude = 40.9144,
                    Longitude = -74.140202,
                    Time = 0
                },

                new Address
                {
                    AddressString = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
                    Alias = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
                    IsDepot = false,
                    Latitude = 40.495678,
                    Longitude = -74.444192,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
                    Alias = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
                    IsDepot = false,
                    Latitude = 40.158248,
                    Longitude = -74.099694,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
                    Alias = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
                    IsDepot = false,
                    Latitude = 40.831038,
                    Longitude = -74.283425,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
                    Alias = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
                    IsDepot = false,
                    Latitude = 40.249947,
                    Longitude = -74.366984,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
                    Alias = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
                    IsDepot = false,
                    Latitude = 40.473348,
                    Longitude = -74.461946,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
                    Alias = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
                    IsDepot = false,
                    Latitude = 40.339464,
                    Longitude = -74.042152,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
                    Alias = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
                    IsDepot = false,
                    Latitude = 40.728082,
                    Longitude = -74.22216,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
                    Alias = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.739888,
                    Longitude = -74.213131,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
                    Alias = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
                    IsDepot = false,
                    Latitude = 40.429843,
                    Longitude = -74.188241,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
                    Alias = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
                    IsDepot = false,
                    Latitude = 40.39891,
                    Longitude = -74.111004,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
                    Alias = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
                    IsDepot = false,
                    Latitude = 40.459219,
                    Longitude = -74.404777,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
                    Alias = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.26011,
                    Longitude = -74.270311,
                    Time = 0
                },

                new Address
                {
                    AddressString = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
                    Alias = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.450615,
                    Longitude = -74.314199,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
                    Alias = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.366317,
                    Longitude = -74.437075,
                    Time = 0
                },

                new Address
                {
                    AddressString = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
                    Alias = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.090073,
                    Longitude = -74.209407,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
                    Alias = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
                    IsDepot = false,
                    Latitude = 40.20265,
                    Longitude = -74.196459,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
                    Alias = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.356058,
                    Longitude = -74.452228,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
                    Alias = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
                    IsDepot = false,
                    Latitude = 40.097688,
                    Longitude = -74.10064,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
                    Alias = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
                    IsDepot = false,
                    Latitude = 40.391643,
                    Longitude = -74.39227,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
                    Alias = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
                    IsDepot = false,
                    Latitude = 40.414913,
                    Longitude = -74.058999,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
                    Alias = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
                    IsDepot = false,
                    Latitude = 39.666109,
                    Longitude = -74.280843,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
                    Alias = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
                    IsDepot = false,
                    Latitude = 40.289643,
                    Longitude = -74.078431,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
                    Alias = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
                    IsDepot = false,
                    Latitude = 40.740142,
                    Longitude = -74.206301,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
                    Alias = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
                    IsDepot = false,
                    Latitude = 39.47564,
                    Longitude = -74.541319,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
                    Alias = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.867849,
                    Longitude = -74.13855,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
                    Alias = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.366221,
                    Longitude = -74.435404,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
                    Alias = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
                    IsDepot = false,
                    Latitude = 40.743912,
                    Longitude = -74.196031,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
                    Alias = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
                    IsDepot = false,
                    Latitude = 40.751429,
                    Longitude = -74.227549,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
                    Alias = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.712976,
                    Longitude = -74.210091,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
                    Alias = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
                    IsDepot = false,
                    Latitude = 40.486612,
                    Longitude = -74.448257,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
                    Alias = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.437284,
                    Longitude = -74.203406,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
                    Alias = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
                    IsDepot = false,
                    Latitude = 40.25995,
                    Longitude = -74.000243,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
                    Alias = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
                    IsDepot = false,
                    Latitude = 40.752345,
                    Longitude = -74.187634,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
                    Alias = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
                    IsDepot = false,
                    Latitude = 40.734446,
                    Longitude = -74.174573,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
                    Alias = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
                    IsDepot = false,
                    Latitude = 40.485749,
                    Longitude = -74.283706,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
                    Alias = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
                    IsDepot = false,
                    Latitude = 40.429671,
                    Longitude = -74.132774,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
                    Alias = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.468223,
                    Longitude = -74.306587,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
                    Alias = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.450583,
                    Longitude = -74.382182,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
                    Alias = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.249606,
                    Longitude = -74.271902,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
                    Alias = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.450583,
                    Longitude = -74.382182,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
                    Alias = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.470359,
                    Longitude = -74.359202,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
                    Alias = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
                    IsDepot = false,
                    Latitude = 40.723126,
                    Longitude = -74.141612,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
                    Alias = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
                    IsDepot = false,
                    Latitude = 40.330025,
                    Longitude = -74.074258,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
                    Alias = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
                    IsDepot = false,
                    Latitude = 40.495696,
                    Longitude = -74.443867,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
                    Alias = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.346942,
                    Longitude = -74.076597,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
                    Alias = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.722866,
                    Longitude = -74.183561,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
                    Alias = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
                    IsDepot = false,
                    Latitude = 40.756185,
                    Longitude = -74.17351,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
                    Alias = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
                    IsDepot = false,
                    Latitude = 40.124934,
                    Longitude = -74.046072,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
                    Alias = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
                    IsDepot = false,
                    Latitude = 40.919728,
                    Longitude = -74.187345,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
                    Alias = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.259859,
                    Longitude = -74.278887,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
                    Alias = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.872648,
                    Longitude = -74.137303,
                    Time = 0
                },

                new Address
                {
                    AddressString = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
                    Alias = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.357862,
                    Longitude = -74.442969,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
                    Alias = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
                    IsDepot = false,
                    Latitude = 40.858621,
                    Longitude = -74.130554,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
                    Alias = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
                    IsDepot = false,
                    Latitude = 40.490362,
                    Longitude = -74.452509,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Latino Groc, 752 River St, Paterson, NJ, 07524",
                    Alias = "Latino Groc, 752 River St, Paterson, NJ, 07524",
                    IsDepot = false,
                    Latitude = 40.93793,
                    Longitude = -74.151234,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
                    Alias = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.349033,
                    Longitude = -74.073362,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
                    Alias = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
                    IsDepot = false,
                    Latitude = 40.33218,
                    Longitude = -74.124581,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
                    Alias = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
                    IsDepot = false,
                    Latitude = 40.31097,
                    Longitude = -73.984022,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
                    Alias = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.709927,
                    Longitude = -74.206793,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
                    Alias = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.717116,
                    Longitude = -74.190947,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
                    Alias = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.438336,
                    Longitude = -74.163793,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
                    Alias = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
                    IsDepot = false,
                    Latitude = 40.220269,
                    Longitude = -74.012208,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
                    Alias = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.718769,
                    Longitude = -74.190058,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
                    Alias = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.094515,
                    Longitude = -74.206256,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
                    Alias = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
                    IsDepot = false,
                    Latitude = 40.432301,
                    Longitude = -74.297294,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
                    Alias = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
                    IsDepot = false,
                    Latitude = 40.331405,
                    Longitude = -74.123225,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
                    Alias = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.476242,
                    Longitude = -74.31866,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
                    Alias = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
                    IsDepot = false,
                    Latitude = 40.904762,
                    Longitude = -74.063701,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
                    Alias = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.907133,
                    Longitude = -74.195232,
                    Time = 0
                },

                new Address
                {
                    AddressString = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
                    Alias = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
                    IsDepot = false,
                    Latitude = 39.69202,
                    Longitude = -74.268679,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
                    Alias = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.864086,
                    Longitude = -74.124176,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                    Alias = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.348865,
                    Longitude = -74.437009,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
                    Alias = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.906114,
                    Longitude = -74.154414,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
                    Alias = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.256763,
                    Longitude = -74.273335,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
                    Alias = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
                    IsDepot = false,
                    Latitude = 39.967992,
                    Longitude = -74.131572,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
                    Alias = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
                    IsDepot = false,
                    Latitude = 40.261439,
                    Longitude = -74.435975,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
                    Alias = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.447007,
                    Longitude = -74.373784,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
                    Alias = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.724698,
                    Longitude = -74.241679,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
                    Alias = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.369259,
                    Longitude = -74.421825,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
                    Alias = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.907441,
                    Longitude = -74.16033,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
                    Alias = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
                    IsDepot = false,
                    Latitude = 40.368815,
                    Longitude = -74.264081,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
                    Alias = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
                    IsDepot = false,
                    Latitude = 40.205113,
                    Longitude = -74.045022,
                    Time = 0
                },

                new Address
                {
                    AddressString = "My Placita, 204 Dayron, Paterson, NJ, 07504",
                    Alias = "My Placita, 204 Dayron, Paterson, NJ, 07504",
                    IsDepot = false,
                    Latitude = 40.91279,
                    Longitude = -74.138676,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
                    Alias = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
                    IsDepot = false,
                    Latitude = 39.344312,
                    Longitude = -74.469856,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
                    Alias = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
                    IsDepot = false,
                    Latitude = 40.076345,
                    Longitude = -74.085964,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
                    Alias = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
                    IsDepot = false,
                    Latitude = 40.214545,
                    Longitude = -74.030095,
                    Time = 0
                },

                new Address
                {
                    AddressString = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
                    Alias = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
                    IsDepot = false,
                    Latitude = 40.653461,
                    Longitude = -74.197261,
                    Time = 0
                },

                new Address
                {
                    AddressString = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
                    Alias = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.354239,
                    Longitude = -74.442334,
                    Time = 0
                },

                new Address
                {
                    AddressString = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.354931,
                    Longitude = -74.445481,
                    Time = 0
                },

                new Address
                {
                    AddressString = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
                    Alias = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.35306,
                    Longitude = -74.455537,
                    Time = 0
                },

                new Address
                {
                    AddressString = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
                    Alias = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.369198,
                    Longitude = -74.434158,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
                    Alias = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
                    IsDepot = false,
                    Latitude = 40.443224,
                    Longitude = -74.129878,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
                    Alias = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
                    IsDepot = false,
                    Latitude = 40.760559,
                    Longitude = -74.18531,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
                    Alias = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
                    IsDepot = false,
                    Latitude = 40.894001,
                    Longitude = -74.158519,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
                    Alias = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
                    IsDepot = false,
                    Latitude = 40.919132,
                    Longitude = -74.186458,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
                    Alias = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.2566,
                    Longitude = -74.273895,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
                    Alias = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.464455,
                    Longitude = -74.267104,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
                    Alias = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
                    IsDepot = false,
                    Latitude = 39.953836,
                    Longitude = -74.194426,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
                    Alias = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
                    IsDepot = false,
                    Latitude = 39.928493,
                    Longitude = -74.140786,
                    Time = 0
                },

                new Address
                {
                    AddressString = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
                    Alias = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.465032,
                    Longitude = -74.267977,
                    Time = 0
                },

                new Address
                {
                    AddressString = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
                    Alias = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
                    IsDepot = false,
                    Latitude = 40.219447,
                    Longitude = -74.032187,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
                    Alias = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
                    IsDepot = false,
                    Latitude = 40.921862,
                    Longitude = -74.150214,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
                    Alias = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.35422,
                    Longitude = -74.442381,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
                    Alias = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
                    IsDepot = false,
                    Latitude = 40.455067,
                    Longitude = -74.295686,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
                    Alias = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
                    IsDepot = false,
                    Latitude = 40.212786,
                    Longitude = -74.00703,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
                    Alias = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
                    IsDepot = false,
                    Latitude = 40.330632,
                    Longitude = -74.119752,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
                    Alias = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
                    IsDepot = false,
                    Latitude = 40.956753,
                    Longitude = -74.188582,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
                    Alias = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
                    IsDepot = false,
                    Latitude = 40.175265,
                    Longitude = -74.026946,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
                    Alias = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
                    IsDepot = false,
                    Latitude = 40.28636,
                    Longitude = -74.095283,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
                    Alias = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
                    IsDepot = false,
                    Latitude = 39.381348,
                    Longitude = -74.532306,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
                    Alias = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
                    IsDepot = false,
                    Latitude = 40.724029,
                    Longitude = -74.244734,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
                    Alias = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
                    IsDepot = false,
                    Latitude = 40.402437,
                    Longitude = -74.298044,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
                    Alias = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
                    IsDepot = false,
                    Latitude = 40.222173,
                    Longitude = -74.01957,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
                    Alias = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
                    IsDepot = false,
                    Latitude = 40.102325,
                    Longitude = -74.104611,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
                    Alias = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
                    IsDepot = false,
                    Latitude = 40.478299,
                    Longitude = -74.297118,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
                    Alias = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.915172,
                    Longitude = -74.171049,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
                    Alias = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.874758,
                    Longitude = -74.122109,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
                    Alias = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
                    IsDepot = false,
                    Latitude = 39.753011,
                    Longitude = -74.222953,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
                    Alias = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
                    IsDepot = false,
                    Latitude = 40.40657,
                    Longitude = -74.386443,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
                    Alias = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
                    IsDepot = false,
                    Latitude = 40.527608,
                    Longitude = -74.275038,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
                    Alias = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
                    IsDepot = false,
                    Latitude = 40.198761,
                    Longitude = -74.03182,
                    Time = 0
                },

                new Address
                {
                    AddressString = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
                    Alias = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
                    IsDepot = false,
                    Latitude = 40.76638,
                    Longitude = -74.185647,
                    Time = 0
                },

                new Address
                {
                    AddressString = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
                    Alias = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.73986,
                    Longitude = -74.204978,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
                    Alias = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
                    IsDepot = false,
                    Latitude = 40.885017,
                    Longitude = -74.138497,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
                    Alias = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.867337,
                    Longitude = -74.122781,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
                    Alias = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
                    IsDepot = false,
                    Latitude = 40.73986,
                    Longitude = -74.204978,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
                    Alias = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.368863,
                    Longitude = -74.416528,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
                    Alias = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
                    IsDepot = false,
                    Latitude = 40.881156,
                    Longitude = -74.141612,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
                    Alias = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
                    IsDepot = false,
                    Latitude = 40.720399,
                    Longitude = -74.210768,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
                    Alias = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.738746,
                    Longitude = -74.192629,
                    Time = 0
                },

                new Address
                {
                    AddressString = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
                    Alias = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.726324,
                    Longitude = -74.228643,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
                    Alias = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
                    IsDepot = false,
                    Latitude = 40.438034,
                    Longitude = -74.143822,
                    Time = 0
                },

                new Address
                {
                    AddressString = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
                    Alias = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
                    IsDepot = false,
                    Latitude = 40.859614,
                    Longitude = -74.124275,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
                    Alias = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
                    IsDepot = false,
                    Latitude = 39.389619,
                    Longitude = -74.52014,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Save More, 506 21st Ave, Paterson, NJ, 07513",
                    Alias = "Save More, 506 21st Ave, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.905816,
                    Longitude = -74.151835,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
                    Alias = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
                    IsDepot = false,
                    Latitude = 40.927511,
                    Longitude = -74.176373,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
                    Alias = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
                    IsDepot = false,
                    Latitude = 40.409412,
                    Longitude = -73.996244,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
                    Alias = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.361097,
                    Longitude = -74.430216,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
                    Alias = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
                    IsDepot = false,
                    Latitude = 40.488921,
                    Longitude = -74.448212,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
                    Alias = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
                    IsDepot = false,
                    Latitude = 40.414517,
                    Longitude = -74.142318,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
                    Alias = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.337487,
                    Longitude = -74.075389,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
                    Alias = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
                    IsDepot = false,
                    Latitude = 40.728866,
                    Longitude = -74.217217,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
                    Alias = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
                    IsDepot = false,
                    Latitude = 40.011365,
                    Longitude = -74.147465,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
                    Alias = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
                    IsDepot = false,
                    Latitude = 40.511425,
                    Longitude = -74.278813,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
                    Alias = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
                    IsDepot = false,
                    Latitude = 40.152529,
                    Longitude = -74.073501,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
                    Alias = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
                    IsDepot = false,
                    Latitude = 40.917592,
                    Longitude = -74.144103,
                    Time = 0
                },

                new Address
                {
                    AddressString = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
                    Alias = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
                    IsDepot = false,
                    Latitude = 40.40416,
                    Longitude = -74.203567,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
                    Alias = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.757528,
                    Longitude = -74.218494,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
                    Alias = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
                    IsDepot = false,
                    Latitude = 40.40805,
                    Longitude = -74.506502,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
                    Alias = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
                    IsDepot = false,
                    Latitude = 40.409682,
                    Longitude = -74.348256,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
                    Alias = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.427168,
                    Longitude = -74.197201,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
                    Alias = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
                    IsDepot = false,
                    Latitude = 40.174388,
                    Longitude = -74.026944,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
                    Alias = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
                    IsDepot = false,
                    Latitude = 40.445177,
                    Longitude = -74.371003,
                    Time = 0
                },

                new Address
                {
                    AddressString = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
                    Alias = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
                    IsDepot = false,
                    Latitude = 40.484253,
                    Longitude = -74.280944,
                    Time = 0
                },

                new Address
                {
                    AddressString = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
                    Alias = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
                    IsDepot = false,
                    Latitude = 40.176369,
                    Longitude = -74.062386,
                    Time = 0
                },

                new Address
                {
                    AddressString = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
                    Alias = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.923346,
                    Longitude = -74.145269,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
                    Alias = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
                    IsDepot = false,
                    Latitude = 40.305772,
                    Longitude = -74.09978,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
                    Alias = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
                    IsDepot = false,
                    Latitude = 40.924336,
                    Longitude = -74.17162,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
                    Alias = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
                    IsDepot = false,
                    Latitude = 40.413674,
                    Longitude = -74.037695,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
                    Alias = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.261613,
                    Longitude = -74.272369,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
                    Alias = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
                    IsDepot = false,
                    Latitude = 40.914833,
                    Longitude = -74.152895,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
                    Alias = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
                    IsDepot = false,
                    Latitude = 40.863545,
                    Longitude = -74.128925,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
                    Alias = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
                    IsDepot = false,
                    Latitude = 40.528262,
                    Longitude = -74.271842,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
                    Alias = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
                    IsDepot = false,
                    Latitude = 40.749295,
                    Longitude = -74.207298,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
                    Alias = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
                    IsDepot = false,
                    Latitude = 40.413983,
                    Longitude = -74.038003,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
                    Alias = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.38752,
                    Longitude = -74.097893,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
                    Alias = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
                    IsDepot = false,
                    Latitude = 40.437838,
                    Longitude = -74.202413,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
                    Alias = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.090165,
                    Longitude = -74.205323,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
                    Alias = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
                    IsDepot = false,
                    Latitude = 40.192329,
                    Longitude = -74.25131,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
                    Alias = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
                    IsDepot = false,
                    Latitude = 40.881156,
                    Longitude = -74.141612,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
                    Alias = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
                    IsDepot = false,
                    Latitude = 40.079432,
                    Longitude = -74.216707,
                    Time = 0
                },

                new Address
                {
                    AddressString = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
                    Alias = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
                    IsDepot = false,
                    Latitude = 40.286583,
                    Longitude = -74.295474,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
                    Alias = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
                    IsDepot = false,
                    Latitude = 39.365196,
                    Longitude = -74.410547,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
                    Alias = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
                    IsDepot = false,
                    Latitude = 40.41911,
                    Longitude = -74.084993,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
                    Alias = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
                    IsDepot = false,
                    Latitude = 40.199729,
                    Longitude = -74.174155,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
                    Alias = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
                    IsDepot = false,
                    Latitude = 40.328604,
                    Longitude = -74.040089,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
                    Alias = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
                    IsDepot = false,
                    Latitude = 40.426408,
                    Longitude = -74.103064,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
                    Alias = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
                    IsDepot = false,
                    Latitude = 40.342252,
                    Longitude = -74.067954,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
                    Alias = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.919401,
                    Longitude = -74.142398,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
                    Alias = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
                    IsDepot = false,
                    Latitude = 40.427893,
                    Longitude = -74.194583,
                    Time = 0
                },

                new Address
                {
                    AddressString = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
                    Alias = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
                    IsDepot = false,
                    Latitude = 40.913417,
                    Longitude = -74.170402,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "C# TEST 3",
                RT = true,
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Today.Date),
                RouteTime = 25200,
                RouteMaxDuration = 36000,
                VehicleMaxDistanceMI = 10000,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Parts = 5
            };

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunAsyncOptimization(optimizationParameters, out var errorString);

            if (dataObject != null)
                Console.WriteLine("Optimization finished with the state: " + dataObject.State);
            else
                Console.WriteLine("Optimization failed.");

            Assert.IsNotNull(dataObject, "Optimization failed. " + errorString);
            // Output the result
        }

        [ClassCleanup]
        public static void RouteTypesGroupCleanup()
        {
            var result = tdr.RemoveOptimization(new[] {dataObjectMDMD24.OptimizationProblemId});

            Assert.IsTrue(result, "Removing of the optimization with 24 stops failed.");
        }
    }

    [TestClass]
    public class AddressbookContactsGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static AddressBookContact contact1, contact2;

        private static AddressBookContact scheduledContact1, scheduledContact1Response;
        private static AddressBookContact scheduledContact2, scheduledContact2Response;
        private static AddressBookContact scheduledContact3, scheduledContact3Response;
        private static AddressBookContact scheduledContact4, scheduledContact4Response;
        private static AddressBookContact scheduledContact5, scheduledContact5Response;

        private static readonly List<int> lsRemoveContacts = new List<int>();

        private static AddressBookContact contactToRemove;

        [ClassInitialize]
        public static void AddAddressBookContactsTest(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class AddressbookContactsGroup failed.");

            var route4Me = new Route4MeManager(c_ApiKey);

            var contact = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814
            };

            // Run the query
            contact1 = route4Me.AddAddressBookContact(contact, out var errorString);

            Assert.IsNotNull(contact1, "AddAddressBookContactsTest failed. " + errorString);

            var location1 = contact1.AddressId != null ? Convert.ToInt32(contact1.AddressId) : -1;

            if (location1 > 0) lsRemoveContacts.Add(location1);

            var dCustom = new Dictionary<string, string>
            {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            };
            contact = new AddressBookContact
            {
                FirstName = "Test FirstName " + new Random().Next(),
                Address1 = "Test Address1 " + new Random().Next(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = dCustom
            };

            contact2 = route4Me.AddAddressBookContact(contact, out errorString);

            Assert.IsNotNull(contact2, "AddAddressBookContactsTest failed. " + errorString);

            var location2 = contact2.AddressId != null ? Convert.ToInt32(contact2.AddressId) : -1;

            if (location2 > 0) lsRemoveContacts.Add(location2);

            var contactParams = new AddressBookContact
            {
                FirstName = "Test FirstName Rem" + new Random().Next(),
                Address1 = "Test Address1 Rem " + new Random().Next(),
                CachedLat = 38.02466,
                CachedLng = -77.33882
            };

            contactToRemove = route4Me.AddAddressBookContact(contactParams, out errorString);
        }

        [TestMethod]
        public void AddCustomDataToContactTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            contact1.AddressCustomData = new Dictionary<string, string>
            {
                {"Service type", "publishing"},
                {"Facilities", "storage"},
                {"Parking", "temporarry"}
            };

            // Run the query
            var updatableProperties = new List<string>
            {
                "AddressId", "AddressCustomData"
            };

            var updatedContact = route4Me.UpdateAddressBookContact(
                contact1,
                updatableProperties,
                out var errorString);

            Assert.IsNotNull(
                updatedContact.AddressCustomData,
                "AddCustomDataToContactTest failed. " + errorString);
        }

        [TestMethod]
        public void AddScheduledAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            #region // Add a location, scheduled daily.

            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            scheduledContact1 = new AddressBookContact
            {
                Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                AddressAlias = "1604 PARKRIDGE PKWY 40214",
                AddressGroup = "Scheduled daily",
                FirstName = "Peter",
                LastName = "Newman",
                AddressEmail = "pnewman6564@yahoo.com",
                AddressPhoneNumber = "65432178",
                CachedLat = 38.141598,
                CachedLng = -85.793846,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "publishing"}
                },
                Schedule = new List<Schedule> {sched1}
            };

            scheduledContact1Response = route4Me.AddAddressBookContact(
                scheduledContact1,
                out var errorString);

            Assert.IsNotNull(
                scheduledContact1Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location1 = scheduledContact1Response.AddressId != null
                ? Convert.ToInt32(scheduledContact1Response.AddressId)
                : -1;

            if (location1 > 0) lsRemoveContacts.Add(location1);

            #endregion

            #region // Add a location, scheduled weekly.

            var sched2 = new Schedule("weekly", false)
            {
                Enabled = true,
                Weekly = new ScheduleWeekly(1, new[] {1, 2, 3, 4, 5})
            };

            scheduledContact2 = new AddressBookContact
            {
                Address1 = "1407 MCCOY, Louisville, KY, 40215",
                AddressAlias = "1407 MCCOY 40215",
                AddressGroup = "Scheduled weekly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.202496,
                CachedLng = -85.786514,
                AddressCity = "Louisville",
                ServiceTime = 600,
                Schedule = new List<Schedule> {sched2}
            };

            scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, out errorString);

            Assert.IsNotNull(
                scheduledContact2Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location2 = scheduledContact2Response.AddressId != null
                ? Convert.ToInt32(scheduledContact2Response.AddressId)
                : -1;

            if (location2 > 0) lsRemoveContacts.Add(location2);

            #endregion

            #region // Add a location, scheduled monthly (dates mode).

            var sched3 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(
                    1,
                    "dates",
                    new[] {20, 22, 23, 24, 25})
            };

            scheduledContact3 = new AddressBookContact
            {
                Address1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                Address2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
                AddressAlias = "4805 BELLEVUE AVE 40215",
                AddressGroup = "Scheduled monthly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.178844,
                CachedLng = -85.774864,
                AddressCountryId = "US",
                AddressStateId = "KY",
                AddressZip = "40215",
                AddressCity = "Louisville",
                ServiceTime = 750,
                Schedule = new List<Schedule> {sched3},
                Color = "red"
            };

            scheduledContact3Response = route4Me.AddAddressBookContact(
                scheduledContact3,
                out errorString);

            Assert.IsNotNull(
                scheduledContact3Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location3 = scheduledContact3Response.AddressId != null
                ? Convert.ToInt32(scheduledContact3Response.AddressId)
                : -1;

            if (location3 > 0) lsRemoveContacts.Add(location3);

            #endregion

            #region // Add a location, scheduled monthly (nth mode).

            var sched4 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(
                    1,
                    "nth",
                    _nth: new Dictionary<int, int> {{1, 4}})
            };

            scheduledContact4 = new AddressBookContact
            {
                Address1 = "730 CECIL AVENUE, Louisville, KY, 40211",
                AddressAlias = "730 CECIL AVENUE 40211",
                AddressGroup = "Scheduled monthly",
                FirstName = "David",
                LastName = "Silvester",
                AddressEmail = "dsilvester5874@yahoo.com",
                AddressPhoneNumber = "36985214",
                CachedLat = 38.248684,
                CachedLng = -85.821121,
                AddressCity = "Louisville",
                ServiceTime = 450,
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "library"}
                },
                Schedule = new List<Schedule> {sched4},
                AddressIcon = "emoji/emoji-bus"
            };

            scheduledContact4Response = route4Me.AddAddressBookContact(
                scheduledContact4,
                out errorString);

            Assert.IsNotNull(
                scheduledContact4Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location4 = scheduledContact4Response.AddressId != null
                ? Convert.ToInt32(scheduledContact4Response.AddressId)
                : -1;

            if (location4 > 0) lsRemoveContacts.Add(location4);

            #endregion

            #region // Add a location with the daily scheduling and blacklist.

            var sched5 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            scheduledContact5 = new AddressBookContact
            {
                Address1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                AddressAlias = "4629 HILLSIDE DRIVE 40216",
                AddressGroup = "Scheduled daily",
                FirstName = "Kim",
                LastName = "Shandor",
                AddressEmail = "kshand8524@yahoo.com",
                AddressPhoneNumber = "9874152",
                CachedLat = 38.176067,
                CachedLng = -85.824638,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>
                {
                    {"scheduled", "yes"},
                    {"service type", "appliance"}
                },
                Schedule = new List<Schedule> {sched5},
                ScheduleBlacklist = new[] {"2017-12-22", "2017-12-23"},
                ServiceTime = 300
            };

            scheduledContact5Response = route4Me.AddAddressBookContact(
                scheduledContact5,
                out errorString);

            Assert.IsNotNull(
                scheduledContact5Response,
                "AddAddressBookContactsTest failed. " + errorString);

            var location5 = scheduledContact5Response.AddressId != null
                ? Convert.ToInt32(scheduledContact5Response.AddressId)
                : -1;

            if (location5 > 0) lsRemoveContacts.Add(location5);

            #endregion
        }

        [TestMethod]
        public void UpdateAddressBookContactTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            Assert.IsNotNull(contact1, "contact1 is null.");

            contact1.AddressGroup = "Updated";
            contact1.ScheduleBlacklist = new[] {"2020-03-14", "2020-03-15"};
            contact1.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1"}, {"key2", "value2"}
            };
            contact1.LocalTimeWindowStart = 25400;
            contact1.LocalTimeWindowEnd = 26000;
            contact1.AddressCube = 5;
            contact1.AddressPieces = 6;
            contact1.AddressRevenue = 700;
            contact1.AddressWeight = 80;
            contact1.AddressPriority = 9;

            var updatableProperties = new List<string>
            {
                "AddressId", "AddressGroup", "ScheduleBlacklist",
                "AddressCustomData", "LocalTimeWindowStart", "LocalTimeWindowEnd",
                "AddressCube", "AddressPieces", "AddressRevenue", "AddressWeight", "AddressPriority"
            };

            // Run the query
            var updatedContact = route4Me.UpdateAddressBookContact(
                contact1,
                updatableProperties,
                out var errorString);

            Assert.IsNotNull(
                updatedContact,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.ScheduleBlacklist,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.LocalTimeWindowStart,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                updatedContact.LocalTimeWindowEnd,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressCube == 5,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressPieces == 6,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressRevenue == 700,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressWeight == 80,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                updatedContact.AddressPriority == 9,
                "UpdateAddressBookContactTest failed. " + errorString);

            contact1.ScheduleBlacklist = null;
            contact1.AddressCustomData = null;
            contact1.LocalTimeWindowStart = null;
            contact1.LocalTimeWindowEnd = null;
            contact1.AddressCube = null;
            contact1.AddressPieces = null;
            contact1.AddressRevenue = null;
            contact1.AddressWeight = null;
            contact1.AddressPriority = null;

            var updatedContact1 = route4Me.UpdateAddressBookContact(
                contact1,
                updatableProperties,
                out var errorString1);

            Assert.IsNotNull(
                updatedContact1,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.ScheduleBlacklist,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.LocalTimeWindowStart,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.LocalTimeWindowEnd,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressCube,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressPieces,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressRevenue,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressWeight,
                "UpdateAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                updatedContact1.AddressPriority,
                "UpdateAddressBookContactTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateWholeAddressBookContactTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            Assert.IsNotNull(contact1, "contact1 is null..");

            // Create contact clone in the memory
            var contactClone = R4MeUtils.ObjectDeepClone(contact1);

            // Modify the parameters of the contactClone
            contactClone.AddressGroup = "Updated";
            contactClone.ScheduleBlacklist = new[] {"2020-03-14", "2020-03-15"};
            contactClone.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1"}, {"key2", "value2"}
            };
            contactClone.LocalTimeWindowStart = 25400;
            contactClone.LocalTimeWindowEnd = 26000;
            contactClone.AddressCube = 5;
            contactClone.AddressPieces = 6;
            contactClone.AddressRevenue = 700;
            contactClone.AddressWeight = 80;
            contactClone.AddressPriority = 9;

            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            contactClone.Schedule = new List<Schedule> {sched1};

            contact1 = route4Me.UpdateAddressBookContact(contactClone, contact1, out var errorString);

            Assert.IsNotNull(
                contact1,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                contact1.ScheduleBlacklist,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                contact1.LocalTimeWindowStart,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNotNull(
                contact1.LocalTimeWindowEnd,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                contact1.AddressCube == 5,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                contact1.AddressPieces == 6,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                contact1.AddressRevenue == 700,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                contact1.AddressWeight == 80,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsTrue(
                contact1.AddressPriority == 9,
                "UpdateWholeAddressBookContactTest failed. " + errorString);

            contactClone = R4MeUtils.ObjectDeepClone(contact1);

            contactClone.ScheduleBlacklist = null;
            contactClone.AddressCustomData = null;
            contactClone.LocalTimeWindowStart = null;
            contactClone.LocalTimeWindowEnd = null;
            contactClone.AddressCube = null;
            contactClone.AddressPieces = null;
            contactClone.AddressRevenue = null;
            contactClone.AddressWeight = null;
            contactClone.AddressPriority = null;

            contact1 = route4Me.UpdateAddressBookContact(contactClone, contact1, out errorString);

            Assert.IsNotNull(
                contact1,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.ScheduleBlacklist,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.LocalTimeWindowStart,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.LocalTimeWindowEnd,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.AddressCube,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.AddressPieces,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.AddressRevenue,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.AddressWeight,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
            Assert.IsNull(
                contact1.AddressPriority,
                "UpdateWholeAddressBookContactTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchLocationsByTextTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Query = "Test Address1",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.IsInstanceOfType(
                contacts,
                typeof(AddressBookContact[]),
                "SearchLocationsByTextTest failed. " + errorString);

            Assert.IsNotNull(total, "SearchLocationsByTextTest failed.");
        }

        [TestMethod]
        public void SearchLocationsByIDsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            Assert.IsNotNull(contact1, "contact1 is null.");
            Assert.IsNotNull(contact2, "contact2 is null.");

            var addresses = contact1.AddressId + "," + contact2.AddressId;

            var addressBookParameters = new AddressBookParameters
            {
                AddressId = addresses
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.IsInstanceOfType(
                contacts,
                typeof(AddressBookContact[]),
                "SearchLocationsByIDsTest failed.. " + errorString);

            Assert.IsNotNull(total, "SearchLocationsByIDsTest failed.");
        }

        [TestMethod]
        public void GetSpecifiedFieldsSearchTextTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Query = "FirstFieldValue1",
                Fields = "first_name,address_email,schedule_blacklist,schedule,address_custom_data,address_1",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            if (addressBookParameters.Fields != null)
            {
                var response = route4Me.SearchAddressBookLocation(
                    addressBookParameters,
                    out var contactsFromObjects,
                    out var errorString);

                Assert.IsInstanceOfType(
                    response.Total,
                    typeof(uint),
                    "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
                Assert.IsInstanceOfType(
                    contactsFromObjects,
                    typeof(List<AddressBookContact>),
                    "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
            }
            else
            {
                var response = route4Me.GetAddressBookContacts(
                    addressBookParameters,
                    out var total,
                    out var errorString);
                Assert.IsInstanceOfType(
                    response,
                    typeof(AddressBookContact[]),
                    "GetSpecifiedFieldsSearchTextTest failed. " + errorString);
            }
        }

        [TestMethod]
        public void GetAddressBookContactsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.IsInstanceOfType(
                contacts,
                typeof(AddressBookContact[]),
                "GetAddressBookContactsTest failed. " + errorString);

            Assert.IsNotNull(total, "GetAddressBookContactsTest failed.");
        }

        [TestMethod]
        public void RemoveAddressbookContactsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var removed = route4Me.RemoveAddressBookContacts(
                new[] {contactToRemove.AddressId.ToString()},
                out var errorString);

            Assert.IsTrue(removed,
                "Cannot remove the address book contact." + Environment.NewLine + errorString);
        }

        [TestMethod]
        public void SearchRoutedLocationsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Display = "routed",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            var contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out var total,
                out var errorString);

            Assert.IsInstanceOfType(
                contacts,
                typeof(AddressBookContact[]),
                "SearchRoutedLocationsTest failed. " + errorString);

            Assert.IsNotNull(total, "SearchRoutedLocationsTest failed.");
        }

        [ClassCleanup]
        public static void AddressbookContactsGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var lsRemLocations = new List<string>();

            if (lsRemoveContacts.Count > 0)
            {
                foreach (var loc1 in lsRemoveContacts) lsRemLocations.Add(loc1.ToString());

                var removed = route4Me.RemoveAddressBookContacts(
                    lsRemLocations.ToArray(),
                    out var errorString);

                Assert.IsTrue(removed, "RemoveAddressBookContactsTest failed. " + errorString);
            }
        }
    }

    [TestClass]
    public class AddressbookGroupsGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static AddressBookGroup group1, group2;

        private static readonly List<string> lsGroups = new List<string>();

        [ClassInitialize]
        public static void AddressBookGroupsInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class AddressbookGroupsGroup failed.");

            group1 = CreateAddreessBookGroup(out var errorString);

            Assert.IsNotNull(group1, "AddressBookGroupsInitialize failed. " + errorString);

            group2 = CreateAddreessBookGroup(out errorString);

            Assert.IsNotNull(group2, "AddressBookGroupsInitialize failed. " + errorString);

            lsGroups.Add(group2.GroupId);
        }

        [TestMethod]
        public void GetAddressBookGroupsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var groups = route4Me.GetAddressBookGroups(
                addressBookGroupParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                groups,
                typeof(AddressBookGroup[]),
                "GetAddressBookGroupsTest failed. " + errorString);
        }

        [TestMethod]
        public void GetAddressBookGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                GroupId = group2.GroupId
            };

            // Run the query
            var addressBookGroup = route4Me.GetAddressBookGroup(
                addressBookGroupParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                addressBookGroup,
                typeof(AddressBookGroup),
                "GetAddressBookGroupTest failed. " + errorString);
        }

        [TestMethod]
        public void GetAddressBookContactsByGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                groupID = group2.GroupId
            };

            // Run the query
            var addressBookGroup = route4Me.GetAddressBookContactsByGroup(addressBookGroupParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                addressBookGroup,
                typeof(AddressBookSearchResponse),
                "GetAddressBookContactsByGroupTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchAddressBookContactsByFilterTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var filterParam = new AddressBookGroupFilterParameter
            {
                Query = "623CEE8BCD50B75A66268AAC11E552FD",
                Display = "all"
            };

            var addressBookGroupParameters = new AddressBookGroupParameters
            {
                Fields = new[] {"address_id", "address_1", "address_group"},
                offset = 0,
                limit = 10,
                filter = filterParam
            };

            // Run the query
            var results = route4Me.SearchAddressBookContactsByFilter(
                addressBookGroupParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                results,
                typeof(AddressBookContactsResponse),
                "GetAddressBookContactsByGroupTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateAddressBookGroupTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupRule = new AddressBookGroupRule
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty1234567"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "AND",
                Rules = new[] {addressBookGroupRule}
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupId = group2.GroupId,
                GroupColor = "cd74e6",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me.UpdateAddressBookGroup(
                addressBookGroupParameters,
                out var errorString);

            Assert.IsNotNull(
                addressBookGroup,
                "UpdateAddressBookGroupTest failed. " + errorString);
        }

        [TestMethod]
        public void AddAddressBookGroupTest()
        {
            var addressBookGroup = CreateAddreessBookGroup(out var errorString);

            Assert.IsNotNull(addressBookGroup, "AddAddreessBookGroupTest failed. " + errorString);

            lsGroups.Add(addressBookGroup.GroupId);
        }

        private static AddressBookGroup CreateAddreessBookGroup(out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressBookGroupRule = new AddressBookGroupRule
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty123456"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "AND",
                Rules = new[] {addressBookGroupRule}
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupName = "All Group",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me.AddAddressBookGroup(
                addressBookGroupParameters,
                out errorString);

            return addressBookGroup;
        }

        private static StatusResponse DeleteAddreessBookGroup(string remeoveGroupID, out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressGroupParams = new AddressBookGroupParameters
            {
                groupID = remeoveGroupID
            };

            var status = route4Me.RemoveAddressBookGroup(
                addressGroupParams,
                out errorString);
            return status;
        }

        [TestMethod]
        public void RemoveAddressBookGroupTest()
        {
            var response = DeleteAddreessBookGroup(group1.GroupId, out var errorString);

            Assert.IsTrue(response.Status, "RemoveAddressBookGroupTest failed. " + errorString);
        }

        [ClassCleanup]
        public static void AddressBookGroupsGroupCleanup()
        {
            foreach (var curGroupID in lsGroups)
            {
                var resposne = DeleteAddreessBookGroup(curGroupID, out var errorString);

                Assert.IsTrue(
                    resposne.Status,
                    "Removing of the address book group with group ID = " + curGroupID + " failed.");
            }
        }
    }

    [TestClass]
    public class AvoidanseZonesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static readonly List<string> lsAvoidanceZones = new List<string>();

        [ClassInitialize]
        public static void AvoidanseZonesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class AvoidanseZonesGroup failed.");

            var route4Me = new Route4MeManager(c_ApiKey);

            var circleAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleAvoidanceZone = route4Me.AddAvoidanceZone(
                circleAvoidanceZoneParameters,
                out var errorString);

            if (circleAvoidanceZone != null) lsAvoidanceZones.Add(circleAvoidanceZone.TerritoryId);

            Assert.IsNotNull(
                circleAvoidanceZone,
                "Add Circle Avoidance Zone test failed. " + errorString);

            var polyAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Poly Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Poly.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "37.75886716305343,-77.68974800109863",
                        "37.74763966054455,-77.6917221069336",
                        "37.74655084306813,-77.68863220214844",
                        "37.7502255383101,-77.68125076293945",
                        "37.74797991274437,-77.67498512268066",
                        "37.73327960206065,-77.6411678314209",
                        "37.74430510679532,-77.63172645568848",
                        "37.76641925847049,-77.66846199035645"
                    }
                }
            };

            var polyAvoidanceZone = route4Me.AddAvoidanceZone(
                polyAvoidanceZoneParameters,
                out errorString);

            Assert.IsNotNull(
                polyAvoidanceZone,
                "Add Polygon Avoidance Zone test failed. " + errorString);

            if (polyAvoidanceZone != null) lsAvoidanceZones.Add(polyAvoidanceZone.TerritoryId);

            var rectAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Rect Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Rect.Description(),
                    Data = new[]
                    {
                        "43.51668853502909,-109.3798828125",
                        "46.98025235521883,-101.865234375"
                    }
                }
            };

            var rectAvoidanceZone = route4Me.AddAvoidanceZone(
                rectAvoidanceZoneParameters,
                out errorString);

            Assert.IsNotNull(
                rectAvoidanceZone,
                "Add Rectangular Avoidance Zone test failed. " + errorString);

            if (lsAvoidanceZones != null) lsAvoidanceZones.Add(rectAvoidanceZone.TerritoryId);
        }

        [TestMethod]
        public void AddAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var circleAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleAvoidanceZone = route4Me.AddAvoidanceZone(
                circleAvoidanceZoneParameters,
                out var errorString);

            if (circleAvoidanceZone != null) lsAvoidanceZones.Add(circleAvoidanceZone.TerritoryId);

            Assert.IsNotNull(
                circleAvoidanceZone,
                "Add Circle Avoidance Zone test failed. " + errorString);
        }

        [TestMethod]
        public void GetAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var avoidanceZoneQuery = new AvoidanceZoneQuery();

            // Run the query
            var avoidanceZones = route4Me.GetAvoidanceZones(
                avoidanceZoneQuery,
                out var errorString);

            Assert.IsInstanceOfType(
                avoidanceZones,
                typeof(AvoidanceZone[]),
                "GetAvoidanceZonesTest failed. " + errorString);
        }

        [TestMethod]
        public void GetAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsAvoidanceZones.Count > 1 ? lsAvoidanceZones[1] : "";

            var avoidanceZoneQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var avoidanceZone = route4Me.GetAvoidanceZone(
                avoidanceZoneQuery,
                out var errorString);

            Assert.IsNotNull(
                avoidanceZone,
                "GetAvoidanceZonesTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsAvoidanceZones.Count > 1 ? lsAvoidanceZones[1] : "";

            var avoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff00ff",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "38.41322259056806,-78.501953234",
                        "3000"
                    }
                }
            };

            // Run the query
            var avoidanceZone = route4Me.UpdateAvoidanceZone(
                avoidanceZoneParameters,
                out var errorString);

            Assert.IsNotNull(
                avoidanceZone,
                "UpdateAvoidanceZoneTest failed. " + errorString);
        }

        [TestMethod]
        public void RemoveAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsAvoidanceZones.Count > 0 ? lsAvoidanceZones[0] : "";

            var avoidanceZoneQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var result = route4Me.DeleteAvoidanceZone(
                avoidanceZoneQuery,
                out var errorString);

            Assert.IsTrue(result, "RemoveAvoidanceZoneTest failed. " + errorString);

            if (result) lsAvoidanceZones.RemoveAt(0);
        }

        [ClassCleanup]
        public static void AvoidanseZonesGroupCleanup()
        {
            foreach (var territoryId in lsAvoidanceZones)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                var avoidanceZoneQuery = new AvoidanceZoneQuery
                {
                    TerritoryId = territoryId
                };

                // Run the query
                var result = route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, out var errorString);

                Assert.IsTrue(result, "RemoveAvoidanceZoneTest failed. " + errorString);
            }
        }
    }

    [TestClass]
    public class TerritoriesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static readonly List<string> lsTerritories = new List<string>();

        [ClassInitialize]
        public static void TerritoriesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class TerritoriesGroup failed.");

            var route4Me = new Route4MeManager(c_ApiKey);

            var circleTerritoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleTerritory = route4Me.CreateTerritory(
                circleTerritoryParameters,
                out var errorString);

            if (circleTerritory != null) lsTerritories.Add(circleTerritory.TerritoryId);

            Assert.IsNotNull(circleTerritory, "Add Circle Territory test failed. " + errorString);

            var polyTerritoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Poly Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Poly.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "37.75886716305343,-77.68974800109863",
                        "37.74763966054455,-77.6917221069336",
                        "37.74655084306813,-77.68863220214844",
                        "37.7502255383101,-77.68125076293945",
                        "37.74797991274437,-77.67498512268066",
                        "37.73327960206065,-77.6411678314209",
                        "37.74430510679532,-77.63172645568848",
                        "37.76641925847049,-77.66846199035645"
                    }
                }
            };

            var polyTerritory = route4Me.CreateTerritory(polyTerritoryParameters, out errorString);

            Assert.IsNotNull(polyTerritory, "Add Polygon Territory test failed. " + errorString);

            if (polyTerritory != null) lsTerritories.Add(polyTerritory.TerritoryId);

            var rectTerritoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Rect Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Rect.Description(),
                    Data = new[]
                    {
                        "43.51668853502909,-109.3798828125",
                        "46.98025235521883,-101.865234375"
                    }
                }
            };

            var rectTerritory = route4Me.CreateTerritory(
                rectTerritoryParameters,
                out errorString);

            Assert.IsNotNull(
                rectTerritory,
                "Add Rectangular Avoidance Zone test failed. " + errorString);

            if (lsTerritories != null) lsTerritories.Add(rectTerritory.TerritoryId);
        }

        [TestMethod]
        public void AddTerritoriesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var circleTerritoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleTerritory = route4Me.CreateTerritory(
                circleTerritoryParameters,
                out var errorString);

            if (circleTerritory != null) lsTerritories.Add(circleTerritory.TerritoryId);

            Assert.IsNotNull(circleTerritory, "Add Circle Territory test failed. " + errorString);
        }

        [TestMethod]
        public void GetTerritoriesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryQuery = new AvoidanceZoneQuery();

            // Run the query
            var territories = route4Me.GetTerritories(
                territoryQuery,
                out var errorString);

            Assert.IsInstanceOfType(
                territories,
                typeof(AvoidanceZone[]),
                "GetTerritoriesTest failed. " + errorString);
        }

        [TestMethod]
        public void GetTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsTerritories.Count > 1 ? lsTerritories[1] : "";

            var territoryQuery = new TerritoryQuery
            {
                TerritoryId = territoryId,
                Addresses = 1,
                Orders = 1
            };

            // Run the query
            var territory = route4Me.GetTerritory(territoryQuery, out var errorString);

            Assert.IsNotNull(territory, "GetTerritoryTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsTerritories.Count > 1 ? lsTerritories[1] : "";

            var territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff00ff",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "38.41322259056806,-78.501953234",
                        "3000"
                    }
                }
            };

            // Run the query
            var territory = route4Me.UpdateTerritory(territoryParameters, out var errorString);

            Assert.IsNotNull(territory, "UpdateTerritoryTest failed. " + errorString);
        }

        [TestMethod]
        public void RemoveTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = lsTerritories.Count > 0 ? lsTerritories[0] : "";

            var territoryQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var result = route4Me.RemoveTerritory(territoryQuery, out var errorString);

            Assert.IsTrue(result, "RemoveTerritoriesTest failed. " + errorString);

            if (result) lsTerritories.RemoveAt(0);
        }

        [ClassCleanup]
        public static void TerritoriesGroupCleanup()
        {
            foreach (var territoryId in lsTerritories)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                var territoryQuery = new AvoidanceZoneQuery
                {
                    TerritoryId = territoryId
                };

                // Run the query
                var result = route4Me.RemoveTerritory(territoryQuery, out var errorString);

                Assert.IsTrue(result, "RemoveTerritoriesTest failed. " + errorString);
            }
        }
    }

    [TestClass]
    public class OrdersGroup
    {
        private static string skip;

        private static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey; // This group allowed only for business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey; //
        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;
        private static readonly List<string> lsOrderIds = new List<string>();
        private static readonly List<Order> lsOrders = new List<Order>();

        [ClassInitialize]
        public static void CreateOrderTest(TestContext context)
        {
            if (c_ApiKey == c_ApiKey_1) skip = "yes";
            else skip = "no";

            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            lsOptimizationIDs = new List<string>();
            context.Properties.Add("Categ", "Ignorable");
            tdr = new TestDataRepository();

            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id);

            var dtTomorrow = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            var order = new Order
            {
                Address1 = "Test Address1 " + new Random().Next(),
                AddressAlias = "Test AddressAlias " + new Random().Next(),
                CachedLat = 37.773972,
                CachedLng = -122.431297,
                DayScheduledFor_YYYYMMDD = dtTomorrow.ToString("yyyy-MM-dd"),
                CustomUserFields = new[]
                {
                    new OrderCustomField
                    {
                        OrderCustomFieldId = 93,
                        OrderCustomFieldValue = "false"
                    }
                }
            };

            if (c_ApiKey != c_ApiKey_1)
            {
                // Run the query
                var resultOrder = route4Me.AddOrder(order, out var errorString);

                Assert.IsNotNull(resultOrder, "CreateOrderTest failed. " + errorString);

                lsOrderIds.Add(resultOrder.OrderId.ToString());
                lsOrders.Add(resultOrder);
            }
            else
            {
                Assert.AreEqual(c_ApiKey_1, c_ApiKey);
            }
        }

        [TestMethod]
        public void GetOrdersTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParameters = new OrderParameters
            {
                Offset = 0,
                Limit = 10
            };

            var orders = route4Me.GetOrders(
                orderParameters,
                out var total,
                out var errorString);

            Assert.IsInstanceOfType(
                orders, typeof(Order[]),
                "GetOrdersTest failed. " + errorString
            );
        }

        [TestMethod]
        public void GetOrderByIDTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParameters = new OrderParameters
            {
                order_id = lsOrderIds[0]
            };

            var order = route4Me.GetOrderByID(orderParameters, out var errorString);

            Assert.IsInstanceOfType(
                order,
                typeof(Order),
                "GetOrderByIDTest failed. " + errorString);
        }

        [TestMethod]
        public void GetOrderByInsertedDateTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var InsertedDate = DateTime.Now.ToString("yyyy-MM-dd");

            var oParams = new OrderParameters {DayAddedYYMMDD = InsertedDate};

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(GetOrdersResponse),
                "GetOrderByInsertedDateTest failed. " + errorString
            );
        }

        [TestMethod]
        public void GetOrderByScheduledDateTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var dtTomorrow = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            var oParams = new OrderParameters
            {
                ScheduledForYYMMDD = dtTomorrow.ToString("yyyy-MM-dd")
            };

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(GetOrdersResponse),
                "GetOrderByScheduledDateTest failed. " + errorString
            );
        }

        [TestMethod]
        public void GetOrdersByScheduleFilter()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var startDate = (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd");
            var endDate = (DateTime.Now + new TimeSpan(31, 0, 0, 0)).ToString("yyyy-MM-dd");

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new[] {startDate, endDate}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(Order[]),
                "GetOrdersByScheduleFilter failed. " + errorString);
        }

        [TestMethod]
        public void FilterOrdersByTrackingNumbers()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    TrackingNumbers = new[] {"TN1"}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(Order[]),
                "FilterOrdersByTrackingNumbers failed. " + errorString);
        }

        [TestMethod]
        public void GetOrdersBySpecifiedTextTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var query = "Test Address1";

            var oParams = new OrderParameters
            {
                Query = query,
                Offset = 0,
                Limit = 20
            };

            var orders = route4Me.SearchOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(GetOrdersResponse),
                "GetOrdersBySpecifiedTextTest failed. " + errorString
            );
        }

        [TestMethod]
        public void GetOrdersByCustomFieldsTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var startDate = (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd");
            var endDate = (DateTime.Now + new TimeSpan(31, 0, 0, 0)).ToString("yyyy-MM-dd");

            var oParams = new OrderFilterParameters
            {
                Limit = 10,
                Filter = new FilterDetails
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new[] {startDate, endDate}
                }
            };

            var orders = route4Me.FilterOrders(oParams, out var errorString);

            Assert.IsInstanceOfType(
                orders,
                typeof(Order[]),
                "GetOrdersByScheduleFilter failed. " + errorString
            );
        }

        [TestMethod]
        public void UpdateOrderTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            //Order order = null;
            var orderId = lsOrderIds.Count > 0 ? lsOrderIds[0] : "";

            Assert.IsFalse(orderId == "", "There is no order for updating.");

            var orderParameters = new OrderParameters
            {
                order_id = orderId
            };

            var order = route4Me.GetOrderByID(orderParameters, out var errorString);

            Assert.IsTrue(
                order != null,
                "There is no order for updating. " + errorString);

            order.ExtFieldLastName = "Updated " + new Random().Next();

            // Run the query
            var updatedOrder = route4Me.UpdateOrder(order, out errorString);

            Assert.IsNotNull(
                updatedOrder,
                "UpdateOrderTest failed. " + errorString);
        }

        [TestMethod]
        public void AddScheduledOrderTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "318 S 39th St, Louisville, KY 40212, USA",
                CachedLat = 38.259326,
                CachedLng = -85.814979,
                CurbsideLat = 38.259326,
                CurbsideLng = -85.814979,
                AddressAlias = "318 S 39th St 40212",
                AddressCity = "Louisville",
                ExtFieldFirstName = "Lui",
                ExtFieldLastName = "Carol",
                ExtFieldEmail = "lcarol654@yahoo.com",
                ExtFieldPhone = "897946541",
                ExtFieldCustomData = new Dictionary<string, string> {{"order_type", "scheduled order"}},
                DayScheduledFor_YYYYMMDD = "2020-12-20",
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out var errorString);

            Assert.IsNotNull(newOrder, "AddScheduledOrdersTest failed. " + errorString);
            Assert.IsInstanceOfType(
                newOrder,
                typeof(Order),
                $"Cannot create the order in the test AddScheduledOrderTest. {errorString}");

            lsOrders.Add(newOrder);
        }

        [TestMethod]
        public void AddOrdersToOptimizationTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var rQueryParams = new OptimizationParameters
            {
                OptimizationProblemID = tdr.SDRT_optimization_problem_id,
                Redirect = false
            };

            var lsTimeWindowStart = new List<int>();

            var dtCurDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            dtCurDate = new DateTime(dtCurDate.Year, dtCurDate.Month, dtCurDate.Day, 8, 0, 0);

            var tsp1000sec = new TimeSpan(0, 0, 1000);

            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));
            dtCurDate += tsp1000sec;
            lsTimeWindowStart.Add((int) R4MeUtils.ConvertToUnixTimestamp(dtCurDate));

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "273 Canal St, New York, NY 10013, USA",
                    Latitude = 40.7191558,
                    Longitude = -74.0011966,
                    Alias = "",
                    CurbsideLatitude = 40.7191558,
                    CurbsideLongitude = -74.0011966,
                    IsDepot = true
                },
                new Address
                {
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
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[0],
                    TimeWindowEnd = lsTimeWindowStart[0] + 300,
                    OrderId = 7205705
                },
                new Address
                {
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
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[1],
                    TimeWindowEnd = lsTimeWindowStart[1] + 300,
                    OrderId = 7205704
                },
                new Address
                {
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
                    CustomFields = new Dictionary<string, string> {{"icon", null}},
                    Time = 0,
                    TimeWindowStart = lsTimeWindowStart[2],
                    TimeWindowEnd = lsTimeWindowStart[2] + 300,
                    OrderId = 7205703
                }
            };

            #endregion

            var rParams = new RouteParameters
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

            var dataObject = route4Me.AddOrdersToOptimization(
                rQueryParams,
                addresses,
                rParams,
                out var errorString);

            Assert.IsNotNull(dataObject, "AddOrdersToOptimizationTest failed. " + errorString);
        }

        [TestMethod]
        public void CreateOrderWithCustomFieldTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderParams = new Order
            {
                Address1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
                CachedLat = 48.335991,
                CachedLng = 31.18287,
                DayScheduledFor_YYYYMMDD = "2019-10-11",
                AddressAlias = "Auto test address",
                CustomUserFields = new[]
                {
                    new OrderCustomField
                    {
                        OrderCustomFieldId = 93,
                        OrderCustomFieldValue = "false"
                    }
                }
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            Assert.IsNotNull(result, "AddOrdersToRouteTest failed. " + errorString);

            lsOrderIds.Add(result.OrderId.ToString());

            lsOrders.Add(result);
        }

        [TestMethod]
        public void CreateOrderWithOrderTypeTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Using of an existing tracking number raises error
            var randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            var orderParams = new Order
            {
                Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = randomTrackingNumber,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            var order = result;

            Assert.IsNotNull(order, "CreateOrderWithTrackingNumberTest failed. " + errorString);

            route4Me.RemoveOrders(new[] {order.OrderId.ToString()}, out var errorString2);
        }

        [TestMethod]
        public void CreateWrongOrderTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            var orderParams = new Order
            {
                //Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = randomTrackingNumber,
                AddressStopType = AddressStopType.Delivery.Description()
            };

            var result = route4Me.AddOrder(orderParams, out var errorString);

            if ((result?.OrderId ?? null) != null)
                route4Me.RemoveOrders(new[] {result.OrderId.ToString()}, out var errorString2);

            Assert.IsNull(result, "CreateWrongOrderTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateOrderWithCustomFieldTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var order = lsOrders[lsOrders.Count - 1];

            order.CustomUserFields = new[]
            {
                new OrderCustomField
                {
                    OrderCustomFieldId = 93,
                    OrderCustomFieldValue = "true"
                }
            };

            var result = route4Me.UpdateOrder(order, out var errorString);

            Assert.IsNotNull(result, "UpdateOrderWithCustomFieldTest failed. " + errorString);
        }

        [TestMethod]
        public void AddOrdersToRouteTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var rQueryParams = new RouteParametersQuery
            {
                RouteId = tdr.SDRT_route_id,
                Redirect = false
            };

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "273 Canal St, New York, NY 10013, USA",
                    Latitude = 40.7191558,
                    Longitude = -74.0011966,
                    Alias = "",
                    CurbsideLatitude = 40.7191558,
                    CurbsideLongitude = -74.0011966
                },
                new Address
                {
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
                    CustomFields = new Dictionary<string, string>
                    {
                        {
                            "icon",
                            null
                        }
                    },
                    Time = 0,
                    OrderId = 7205705
                },
                new Address
                {
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
                    CustomFields = new Dictionary<string, string>
                    {
                        {
                            "icon",
                            null
                        }
                    },
                    Time = 0,
                    OrderId = 7205703
                }
            };

            #endregion

            var rParams = new RouteParameters
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
                rParams,
                out var errorString);

            Assert.IsNotNull(
                result,
                "AddOrdersToRouteTest failed. " + errorString);
        }

        [ClassCleanup]
        public static void RemoveOrdersTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var removed = route4Me.RemoveOrders(lsOrderIds.ToArray(), out var errorString);

            lsOrders.Clear();
            lsOrderIds.Clear();

            Assert.IsTrue(removed, "RemoveOrdersTest failed. " + errorString);

            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");

            lsOptimizationIDs.Clear();
        }
    }

    [TestClass]
    public class OrderCustomUserFieldsGroup
    {
        private static string skip;

        private static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey; // This group allowed only for business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey; //
        private static List<int> lsOrderCustomUserFieldIDs = new List<int>();

        [ClassInitialize]
        public static void OrderCustomUserFieldsInitialize(TestContext context)
        {
            if (c_ApiKey == c_ApiKey_1) skip = "yes";
            else skip = "no";

            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderCustomUserFields = route4Me.GetOrderCustomUserFields(out var errorString);

            int customFieldId;

            if (orderCustomUserFields
                .Where(x => x.OrderCustomFieldName == "CustomField33")
                .Count() > 0)
            {
                customFieldId = orderCustomUserFields
                    .Where(x => x.OrderCustomFieldName == "CustomField33")
                    .FirstOrDefault().OrderCustomFieldId;
            }
            else
            {
                var orderCustomFieldParams = new OrderCustomFieldParameters
                {
                    OrderCustomFieldName = "CustomField33",
                    OrderCustomFieldLabel = "Custom Field 33",
                    OrderCustomFieldType = "checkbox",
                    OrderCustomFieldTypeInfo = new Dictionary<string, object>
                    {
                        {"short_label", "cFl33"},
                        {"description", "This is test order custom field"},
                        {"custom field no", 10}
                    }
                };

                var createdCustomField = route4Me.CreateOrderCustomUserField(
                    orderCustomFieldParams,
                    out errorString);

                Assert.IsInstanceOfType(
                    createdCustomField,
                    typeof(OrderCustomFieldCreateResponse),
                    "Cannot initialize the class OrderCustomUserFieldsGroup. " + errorString);

                customFieldId = createdCustomField.Data.OrderCustomFieldId;
            }

            lsOrderCustomUserFieldIDs = new List<int> {customFieldId};
        }


        [TestMethod]
        public void GetOrderCustomUserFieldsTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderCustomUserFields = route4Me.GetOrderCustomUserFields(out var errorString);

            Assert.IsInstanceOfType(
                orderCustomUserFields,
                typeof(OrderCustomField[]),
                "GetOrderCustomUserFieldsTest failed. " + errorString);
        }

        [TestMethod]
        public void CreateOrderCustomUserFieldTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldName = "CustomField77",
                OrderCustomFieldLabel = "Custom Field 77",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>
                {
                    {"short_label", "cFl77"},
                    {"description", "This is test order custom field"},
                    {"custom field no", 11}
                }
            };

            var orderCustomUserField = route4Me.CreateOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.IsInstanceOfType(
                orderCustomUserField,
                typeof(OrderCustomFieldCreateResponse),
                "CreateOrderCustomUserFieldTest failed. " + errorString);

            lsOrderCustomUserFieldIDs.Add(orderCustomUserField.Data.OrderCustomFieldId);
        }

        [TestMethod]
        public void UpdateOrderCustomUserFieldTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldId = lsOrderCustomUserFieldIDs[lsOrderCustomUserFieldIDs.Count - 1],
                OrderCustomFieldLabel = "Custom Field 55",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>
                {
                    {"short_label", "cFl55"},
                    {"description", "This is updated test order custom field"},
                    {"custom field no", 12}
                }
            };

            var orderCustomUserField = route4Me.UpdateOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.IsInstanceOfType(
                orderCustomUserField,
                typeof(OrderCustomFieldCreateResponse),
                "UpdateOrderCustomUserFieldTest failed. " + errorString);

            Assert.AreEqual(
                "Custom Field 55",
                orderCustomUserField.Data.OrderCustomFieldLabel,
                "UpdateOrderCustomUserFieldTest failed. " + errorString);
        }

        [TestMethod]
        public void RemoveOrderCustomUserFieldTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var orderCustomFieldId = lsOrderCustomUserFieldIDs[lsOrderCustomUserFieldIDs.Count - 1];

            var orderCustomFieldParams = new OrderCustomFieldParameters
            {
                OrderCustomFieldId = orderCustomFieldId
            };

            var response = route4Me.RemoveOrderCustomUserField(
                orderCustomFieldParams,
                out var errorString);

            Assert.IsTrue(
                response.Affected == 1,
                "RemoveOrderCustomUserFieldTest failed. " + errorString);

            lsOrderCustomUserFieldIDs.Remove(orderCustomFieldId);
        }

        [ClassCleanup]
        public static void RemoveOrderCustomUserFields()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var customFieldId in lsOrderCustomUserFieldIDs)
            {
                var customFieldParam = new OrderCustomFieldParameters
                {
                    OrderCustomFieldId = customFieldId
                };

                var removeResult = route4Me.RemoveOrderCustomUserField(
                    customFieldParam,
                    out var errorString);

                Assert.IsTrue(
                    removeResult.Affected == 1,
                    "Cannot remove order customuser field with id=" + customFieldId + ". " + errorString);
            }

            lsOrderCustomUserFieldIDs.Clear();
        }
    }

    [TestClass]
    public class ActivitiesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;

        [ClassInitialize]
        public static void ActivitiesGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(context, "Initialization of the class ActivitiesGroup failed.");

            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();

            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");

            Assert.IsTrue(
                tdr.SD10Stops_route.Addresses.Length > 0,
                "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id);
        }

        [TestMethod]
        public void LogCustomActivityTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var message = "Test User Activity " + DateTime.Now;

            var activity = new Activity
            {
                ActivityType = "user_message",
                ActivityMessage = message,
                RouteId = routeId
            };

            // Run the query
            var added = route4Me.LogCustomActivity(activity, out var errorString);

            Assert.IsTrue(added, "LogCustomActivityTest failed. " + errorString);
        }

        [TestMethod]
        public void GetRouteTeamActivitiesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var activityParameters = new ActivityParameters
            {
                RouteId = routeId,
                Team = "true",
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "GetActivitiesTest failed. " + errorString);
        }

        [TestMethod]
        public void GetActivitiesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "GetActivitiesTest failed. " + errorString);
        }

        [TestMethod]
        public void GetActivitiesByMemberTest()
        {
            if (c_ApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var parameters = new GenericParameters();

            var response = route4Me.GetUsers(parameters, out var userErrorString);

            Assert.IsInstanceOfType(
                response.Results,
                typeof(MemberResponseV4[]),
                "GetActivitiesByMemberTest failed - cannot get users");
            Assert.IsTrue(
                response.Results.Length > 0,
                "Cannot retrieve more than 0 users");

            var activityParameters = new ActivityParameters
            {
                MemberId = response.Results[0].MemberId != null
                    ? Convert.ToInt32(response.Results[0].MemberId)
                    : -1,
                Offset = 0,
                Limit = 10
            };

            // Run the query
            var activities = route4Me.GetActivities(activityParameters, out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "GetActivitiesByMemberTest failed. " + errorString);
        }

        [TestMethod]
        public void GetLastActivities()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activitiesAfterTime = DateTime.Now - new TimeSpan(7, 0, 0, 0);

            activitiesAfterTime = new DateTime(
                activitiesAfterTime.Year,
                activitiesAfterTime.Month,
                activitiesAfterTime.Day,
                0, 0, 0);

            var uiActivitiesAfterTime = (uint) R4MeUtils
                .ConvertToUnixTimestamp(activitiesAfterTime);

            var activityParameters = new ActivityParameters
            {
                Limit = 10,
                Offset = 0,
                Start = uiActivitiesAfterTime
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            foreach (var activity in activities)
            {
                var activityTime = activity.ActivityTimestamp != null
                    ? (uint) activity.ActivityTimestamp
                    : 0;
                Assert.IsTrue(
                    activityTime >= uiActivitiesAfterTime,
                    "GetLastActivities failed. " + errorString);
            }
        }

        [TestMethod]
        public void SearchAreaUpdatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-updated"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchAreaUpdatedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchAreaAddedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-added"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchAreaAddedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchAreaRemovedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-removed"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchAreaRemovedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDestinationDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "delete-destination"
                //RouteId = "5C15E83A4BE005BCD1537955D28D51D7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDestinationDeletedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDestinationInsertedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination"
                //RouteId = "87B8873BAEA4E09942C68E2C92A9C4B7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDestinationInsertedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDestinationMarkedAsDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed"
                //RouteId = "03CEF546324F727239ABA69EFF3766E1"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDestinationMarkedAsDepartedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDestinationOutSequenceTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "destination-out-sequence"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDestinationOutSequenceTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDestinationUpdatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "update-destinations"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDestinationUpdatedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDriverArrivedEarlyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-early"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDriverArrivedEarlyTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDriverArrivedLateTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-late"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDriverArrivedLateTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchDriverArrivedOnTimeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-on-time"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchDriverArrivedOnTimeTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchGeofenceEnteredTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "geofence-entered"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchGeofenceEnteredTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchGeofenceLeftTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "geofence-left"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchGeofenceLeftTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchInsertDestinationAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchInsertDestinationAllTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMarkDestinationDepartedAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMarkDestinationDepartedAllTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMarkDestinationVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-visited"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMarkDestinationVisitedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMemberCreatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-created"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMemberCreatedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMemberDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-deleted"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMemberDeletedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMemberModifiedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-modified"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMemberModifiedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchMoveDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "move-destination"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchMoveDestinationTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchNoteInsertedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "note-insert"
                //RouteId = "C3E7FD2F8775526674AE5FD83E25B88A"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchNoteInsertedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchNoteInsertedAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "note-insert"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchNoteInsertedAllTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchRouteDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-delete"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchRouteDeletedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchRouteOptimizedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-optimized"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchRouteOptimizedTest failed. " + errorString);
        }

        [TestMethod]
        public void SearchRouteOwnerChanged()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-owner-changed"
                //RouteId = "5C15E83A4BE005BCD1537955D28D51D7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.IsInstanceOfType(
                activities,
                typeof(Activity[]),
                "SearchRouteOwnerChanged failed. " + errorString);
        }

        [ClassCleanup]
        public static void ActivitiesGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");
        }
    }

    [TestClass]
    public class AddressesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;
        private static int removedAddressId;

        [ClassInitialize]
        public static void AddressGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class AddressesGroup failed.");

            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(
                result,
                "Single Driver Round Trip generation failed.");

            Assert.IsTrue(
                tdr.SDRT_route.Addresses.Length > 0,
                "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id);

            removedAddressId = -1;
        }

        [TestMethod]
        public void GetAddressTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeIdToMoveTo = tdr.SDRT_route_id;
            Assert.IsNotNull(
                routeIdToMoveTo,
                "routeId_SingleDriverRoundTrip is null.");

            var addressId = tdr.DataObjectSDRT != null &&
                            tdr.DataObjectSDRT.Routes != null &&
                            tdr.DataObjectSDRT.Routes.Length > 0 &&
                            tdr.DataObjectSDRT.Routes[0].Addresses.Length > 1 &&
                            tdr.DataObjectSDRT.Routes[0].Addresses[1].RouteDestinationId != null
                ? tdr.DataObjectSDRT.Routes[0].Addresses[1].RouteDestinationId.Value
                : 0;

            var addressParameters = new AddressParameters
            {
                RouteId = routeIdToMoveTo,
                RouteDestinationId = addressId,
                Notes = true
            };

            // Run the query
            var dataObject = route4Me.GetAddress(
                addressParameters,
                out var errorString);

            Assert.IsNotNull(dataObject, "GetAddressTest failed. " + errorString);
        }

        [TestMethod]
        public void AddDestinationToOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the address that we are going to add to an existing route optimization
            var addresses = new[]
            {
                new Address
                {
                    AddressString = "717 5th Ave New York, NY 10021",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                }
            };

            // Optionally change any route parameters, such as maximum route duration, 
            // maximum cubic constraints, etc.
            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = tdr.SDRT_optimization_problem_id,
                Addresses = addresses,
                ReOptimize = true
            };

            // Execute the optimization to re-optimize and rebalance 
            // all the routes in this optimization
            var dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out var errorString);

            tdr.SDRT_route_id = dataObject.Routes.Length > 0
                ? dataObject.Routes[0].RouteId
                : "";

            Assert.IsNotNull(
                tdr.DataObjectSDRT,
                "AddDestinationToOptimization and reoptimized Test  failed. " + errorString);

            optimizationParameters.ReOptimize = false;
            dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out errorString);

            tdr.SDRT_route_id = dataObject.Routes.Length > 0
                ? dataObject.Routes[0].RouteId
                : "";

            Assert.IsNotNull(
                tdr.DataObjectSDRT,
                "AddDestinationToOptimization and not reoptimized Test  failed. " + errorString);
        }

        [TestMethod]
        public void RemoveDestinationFromOptimizationTest()
        {
            var delta = removedAddressId ==
                        tdr.SDRT_route.Addresses[tdr.SDRT_route.Addresses.Length - 1].RouteDestinationId
                ? 2
                : 1;

            var destinationToRemove = tdr.SDRT_route != null && tdr.SDRT_route.Addresses.Length > 0
                ? tdr.SDRT_route.Addresses[tdr.SDRT_route.Addresses.Length - delta]
                : null;

            var route4Me = new Route4MeManager(c_ApiKey);

            var OptimizationProblemId = tdr.SDRT_optimization_problem_id;
            Assert.IsNotNull(OptimizationProblemId, "OptimizationProblemId is null.");

            var destinationId = destinationToRemove.RouteDestinationId != null
                ? Convert.ToInt32(destinationToRemove.RouteDestinationId)
                : -1;
            Assert.AreNotEqual(-1, "destinationId is null.");

            // Run the query
            var removed = route4Me.RemoveDestinationFromOptimization(
                OptimizationProblemId,
                destinationId,
                out var errorString);

            Assert.IsTrue(
                removed,
                "RemoveDestinationFromOptimizationTest failed. " + errorString);

            removedAddressId = destinationId;
        }

        [TestMethod]
        public void AddRouteDestinationsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.143526,
                    Longitude = -83.240354,
                    Time = 0
                },

                new Address
                {
                    AddressString = "222 Blake Cir Milledgeville GA 31061",
                    Latitude = 33.177852,
                    Longitude = -83.263535,
                    Time = 0
                }
            };

            #endregion

            // Run the query
            var optimalPosition = true;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.IsInstanceOfType(
                destinationIds,
                typeof(int[]),
                "AddRouteDestinationsTest failed. " + errorString);
        }

        [TestMethod]
        public void AddRouteDestinationInSpecificPositionTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.143526,
                    Longitude = -83.240354,
                    SequenceNo = 3,
                    Time = 0
                }
            };

            #endregion

            // Run the query
            var optimalPosition = false;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.IsInstanceOfType(
                destinationIds,
                typeof(int[]),
                "AddRouteDestinationsTest failed. " + errorString);
        }

        [TestMethod]
        public void RemoveRouteDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = tdr.SDRT_route_id;
            ;
            Assert.IsNotNull(route_id, "rote_id is null.");

            var delta = removedAddressId == tdr.SDRT_route
                .Addresses[tdr.SDRT_route.Addresses.Length - 1]
                .RouteDestinationId
                ? 2
                : 1;

            object oDestinationId = tdr.SDRT_route
                .Addresses[tdr.SDRT_route.Addresses.Length - delta]
                .RouteDestinationId;

            var destination_id = oDestinationId != null
                ? Convert.ToInt32(oDestinationId)
                : -1;
            Assert.IsNotNull(oDestinationId, "destination_id is null.");

            // Run the query
            var deleted = route4Me.RemoveRouteDestination(
                route_id,
                destination_id,
                out var errorString);

            Assert.IsTrue(deleted, "RemoveRouteDestinationTest failed. " + errorString);

            removedAddressId = destination_id;
        }

        [TestMethod]
        public void MarkAddressAsMarkedAsDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = tdr.SDRT_route_id,
                RouteDestinationId = tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsDeparted = true
            };

            // Run the query
            var resultAddress = route4Me.MarkAddressAsMarkedAsDeparted(
                aParams,
                out var errorString);

            Assert.IsNotNull(
                resultAddress,
                "MarkAddressAsMarkedAsDepartedTest. " + errorString);
        }

        [TestMethod]
        public void MarkAddressAsMarkedAsVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = tdr.SDRT_route_id,
                RouteDestinationId = tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsVisited = true
            };

            // Run the query
            var resultAddress = route4Me.MarkAddressAsMarkedAsVisited(
                aParams,
                out var errorString);

            Assert.IsNotNull(
                resultAddress,
                "MarkAddressAsMarkedAsVisitedTest. " + errorString);
        }

        [TestMethod]
        public void MarkAddressDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = tdr.SDRT_route_id,
                AddressId = tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsDeparted = true
            };

            // Run the query
            var result = route4Me.MarkAddressVisited(aParams, out var errorString);

            Assert.IsNotNull(result, "MarkAddressDepartedTest. " + errorString);
        }

        [TestMethod]
        public void MarkAddressVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = tdr.SDRT_route_id,
                AddressId = tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsVisited = true
            };

            // Run the query
            object oResult = route4Me.MarkAddressVisited(aParams, out var errorString);

            Assert.IsNotNull(oResult, "MarkAddressVisitedTest. " + errorString);
        }

        [ClassCleanup]
        public static void AddressGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");
        }
    }

    [TestClass]
    public class TrackingGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;

        [ClassInitialize]
        public static void TrackingGroupInitialize(TestContext context)
        {
            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue
            (result,
                "Single Driver Round Trip generation failed.");

            Assert.IsTrue(
                tdr.SDRT_route.Addresses.Length > 0,
                "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id);

            var recorded = SetAddressGPSPosition(tdr.SDRT_route.Addresses[1]);

            Assert.IsTrue(recorded, "Cannot record GPS position of the address");

            recorded = SetAddressGPSPosition(tdr.SDRT_route.Addresses[2]);

            Assert.IsTrue(recorded, "Cannot record GPS position of the address");
        }

        [TestMethod]
        public void FindAssetTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var tracking = tdr.SDRT_route != null
                ? tdr.SDRT_route.Addresses.Length > 1
                    ? tdr.SDRT_route.Addresses[1].TrackingNumber != null
                        ? tdr.SDRT_route.Addresses[1].TrackingNumber
                        : ""
                    : ""
                : "";

            Assert.IsTrue(
                tracking != "",
                "Can not find valid tracking number in the newly generated route's second destination."
            );

            // Run the query
            var result = route4Me.FindAsset(tracking, out var errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(FindAssetResponse),
                "FindAssetTest failed. " + errorString
            );
        }

        [TestMethod]
        public void SetGPSPositionTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.14384;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.22466;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = (int) tdr.SDRT_route.Addresses[1].MemberId,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = route4Me.SetGPS(gpsParameters, out var errorString);

            Assert.IsNotNull(
                response,
                "SetGPSPositionTest failed. " + errorString
            );
            Assert.IsTrue(
                response.Status,
                "SetGPSPositionTest failed. " + errorString
            );
        }

        private SetGpsResponse SetGpsPosition(out string errorString)
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Latitude
                : 33.14384;
            var lng = tdr.SDRT_route.Addresses.Length > 1
                ? tdr.SDRT_route.Addresses[1].Longitude
                : -83.22466;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = 725205,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return route4Me.SetGPS(gpsParameters, out errorString);
        }

        /// <summary>
        ///     Set GPS postion record by route address
        /// </summary>
        /// <param name="address">Route address</param>
        /// <returns>True if the GPS position recorded successfully.</returns>
        private static bool SetAddressGPSPosition(Address address)
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = address.Latitude;
            var lng = address.Longitude;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = (int) address.MemberId,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = route4Me.SetGPS(gpsParameters, out var _);

            return response != null && response.GetType() == typeof(SetGpsResponse)
                ? true
                : false;
        }

        [TestMethod]
        public void GetDeviceHistoryTimeRangeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var tsp2days = new TimeSpan(2, 0, 0, 0);
            var dtNow = DateTime.Now;

            var gpsParameters = new GPSParameters
            {
                Format = "json",
                RouteId = tdr.SDRT_route.RouteId,
                TimePeriod = "custom",
                StartDate = R4MeUtils.ConvertToUnixTimestamp(dtNow - tsp2days),
                EndDate = R4MeUtils.ConvertToUnixTimestamp(dtNow + tsp2days)
            };

            var response = route4Me.GetDeviceLocationHistory(gpsParameters, out var errorString);

            Assert.IsInstanceOfType(
                response,
                typeof(DeviceLocationHistoryResponse),
                "GetDeviceHistoryTimeRangeTest failed. " + errorString
            );
        }

        [TestMethod]
        public void TrackDeviceLastLocationHistoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var trParameters = new RouteParametersQuery
            {
                RouteId = tdr.SDRT_route_id,
                DeviceTrackingHistory = true
            };

            var dataObject = route4Me.GetLastLocation(trParameters, out var errorString);

            Assert.IsNotNull(
                dataObject,
                "TrackDeviceLastLocationHistoryTest failed. " + errorString);
        }

        [TestMethod]
        public void GetAllUserLocationsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(
                genericParameters,
                out var errorString);

            Assert.IsNotNull(
                userLocations,
                "GetAllUserLocationsTest failed. " + errorString);
        }

        [TestMethod]
        public void QueryUserLocationsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(genericParameters, out var errorString);

            Assert.IsNotNull(
                userLocations,
                "GetAllUserLocationsTest failed. " + errorString
            );

            var userLocation = userLocations.Where(x => x.UserTracking != null).FirstOrDefault();

            var email = userLocation.MemberData.MemberEmail;

            genericParameters.ParametersCollection.Add("query", email);

            var queriedUserLocations = route4Me.GetUserLocations(genericParameters, out errorString);

            Assert.IsNotNull(
                queriedUserLocations,
                "QueryUserLocationsTest failed. " + errorString
            );

            Assert.IsTrue(
                queriedUserLocations.Count() == 1,
                "QueryUserLocationsTest failed. " + errorString
            );
        }

        [ClassCleanup]
        public static void TrackingGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");
        }
    }

    [TestClass]
    public class UsersGroup
    {
        private static string skip;

        private static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey; // Creating of a user better to do with the business and higher account types --- put in the parameter an appropriate API key

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        private static List<string> lsMembers;

        private readonly int? createdMemberID;

        [ClassInitialize]
        public static void UserGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class UsersGroup failed.");

            skip = c_ApiKey == c_ApiKey_1 ? "yes" : "no";

            lsMembers = new List<string>();

            var dispetcher = new UsersGroup().CreateUser("SUB_ACCOUNT_DISPATCHER", out var errorString);
            Assert.IsInstanceOfType(
                dispetcher,
                typeof(MemberResponseV4),
                "Cannot create dispetcher. " + errorString);

            lsMembers.Add(dispetcher.MemberId);

            var driver = new UsersGroup().CreateUser("SUB_ACCOUNT_DRIVER", out errorString);
            Assert.IsInstanceOfType(
                driver,
                typeof(MemberResponseV4),
                "Cannot create driver. " + errorString);

            lsMembers.Add(driver.MemberId);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            if (skip == "yes") return;

            var dispetcher = CreateUser("SUB_ACCOUNT_DISPATCHER", out var errorString);

            //For successful testing of an user creating, you shuld provide valid email address, otherwise you'll get error "Email is used in system"
            var rightResponse = dispetcher != null
                ? "ok"
                : errorString == "Email is used in system" ||
                  errorString == "Registration: The e-mail address is missing or invalid."
                    ? "ok"
                    : "";

            Assert.IsTrue(rightResponse == "ok", "CreateUserTest failed. " + errorString);

            lsMembers.Add(dispetcher.MemberId);
        }

        public MemberResponseV4 CreateUser(string memberType, out string errorString)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var userFirstName = "";
            var userLastName = "";
            var userPhone = "";

            switch (memberType)
            {
                case "SUB_ACCOUNT_DISPATCHER":
                    userFirstName = "Clay";
                    userLastName = "Abraham";
                    userPhone = "571-259-5939";
                    break;
                case "SUB_ACCOUNT_DRIVER":
                    userFirstName = "Driver";
                    userLastName = "Driverson";
                    userPhone = "577-222-5555";
                    break;
            }

            var @params = new MemberParametersV4
            {
                HIDE_ROUTED_ADDRESSES = "FALSE",
                member_phone = userPhone,
                member_zipcode = "22102",
                member_email = "regression.autotests+" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com",
                HIDE_VISITED_ADDRESSES = "FALSE",
                READONLY_USER = "FALSE",
                member_type = memberType,
                date_of_birth = "2010",
                member_first_name = userFirstName,
                member_password = "123456",
                HIDE_NONFUTURE_ROUTES = "FALSE",
                member_last_name = userLastName,
                SHOW_ALL_VEHICLES = "FALSE",
                SHOW_ALL_DRIVERS = "FALSE"
            };

            var result = route4Me.CreateUser(@params, out errorString);

            return result;
        }

        [TestMethod]
        public void AddEditCustomDataToUserTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var memberId = Convert.ToInt32(lsMembers[lsMembers.Count - 1]);

            var customParams = new MemberParametersV4
            {
                member_id = memberId,
                custom_data = new Dictionary<string, string> {{"Custom Key 2", "Custom Value 2"}}
            };

            var result2 = route4Me.UserUpdate(customParams, out var errorString);

            Assert.IsTrue(result2 != null, "UpdateUserTest failed. " + errorString);

            var customData = result2.CustomData;

            Assert.IsTrue(
                customData.Keys.ElementAt(0) == "Custom Key 2",
                "Custom Key is not 'Custom Key 2'");

            Assert.IsTrue(
                customData["Custom Key 2"] == "Custom Value 2",
                "Custom Value is not 'Custom Value 2'");
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var memberID = Convert.ToInt32(lsMembers[0]);
            var @params = new MemberParametersV4 {member_id = memberID};

            // Run the query
            var result = route4Me.GetUserById(@params, out var errorString);

            Assert.IsNotNull(result, "GetUserByIdTest. " + errorString);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var parameters = new GenericParameters();

            // Run the query
            var dataObjects = route4Me.GetUsers(parameters, out var errorString);

            Assert.IsInstanceOfType(
                dataObjects,
                typeof(GetUsersResponse),
                "GetUsersTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            Console.WriteLine("createdMemberID -> " + createdMemberID);

            var @params = new MemberParametersV4
            {
                member_id = createdMemberID != null
                    ? createdMemberID
                    : Convert.ToInt32(lsMembers[lsMembers.Count - 1]),
                member_phone = "571-259-5939"
            };

            // Run the query
            var result = route4Me.UserUpdate(@params, out var errorString);

            Assert.IsNotNull(result, "UpdateUserTest failed. " + errorString);
        }

        [TestMethod]
        public void UserAuthenticationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberParameters
            {
                StrEmail = "aaaaaaaa@gmail.com",
                StrPassword = "11111111111",
                Format = "json"
            };

            // Run the query
            var result = route4Me.UserAuthentication(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object properties have non nul values
            Assert.IsNotNull(result, "UserAuthenticationTest failed. " + errorString);
        }

        [TestMethod]
        public void UserRegistrationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberParameters
            {
                StrEmail = "thewelco@gmail.com",
                StrPassword_1 = "11111111",
                StrPassword_2 = "11111111",
                StrFirstName = "Olman",
                StrLastName = "Progman",
                StrIndustry = "Transportation",
                Format = "json",
                ChkTerms = 1,
                DeviceType = "web",
                Plan = "free",
                MemberType = 5
            };

            // Run the query
            var result = route4Me.UserRegistration(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object property Status=true
            Assert.IsNotNull(result, "UserRegistrationTest failed. " + errorString);
        }

        [TestMethod]
        public void ValidateSessionTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberParameters
            {
                SessionGuid = "ad9001f33ed6875b5f0e75bce52cbc34",
                MemberId = 1,
                Format = "json"
            };

            // Run the query
            var result = route4Me.ValidateSession(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object properties have non nul values
            Assert.IsNotNull(result, "ValidateSessionTest failed. " + errorString);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberParametersV4
            {
                member_id = Convert.ToInt32(lsMembers[lsMembers.Count - 1])
            };

            // Run the query
            var result = route4Me.UserDelete(@params, out var errorString);

            Assert.IsNotNull(result, "DeleteUserTest failed. " + errorString);

            lsMembers.RemoveAt(lsMembers.Count - 1);
        }

        [ClassCleanup]
        public static void UsersGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            var parameters = new MemberParametersV4();

            bool result;

            foreach (var memberId in lsMembers)
            {
                parameters.member_id = Convert.ToInt32(memberId);
                result = route4Me.UserDelete(parameters, out var errorString);
            }
        }
    }

    [TestClass]
    public class MemberConfigurationGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private static List<string> lsConfigurationKeys;

        [ClassInitialize]
        public static void MemberConfigurationGroupInitialize(TestContext context)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            lsConfigurationKeys = new List<string>();

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My height",
                ConfigValue = "180"
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(@params, out var errorString);
            Assert.IsNotNull(
                result,
                "AddNewConfigurationKeyTest failed. " + errorString);

            lsConfigurationKeys.Add("Test My height");

            var keyrParams = new MemberConfigurationParameters
            {
                ConfigKey = "Test Remove Key",
                ConfigValue = "remove"
            };

            var result2 = route4Me.CreateNewConfigurationKey(keyrParams, out errorString);
            Assert.IsNotNull(
                result2,
                "AddNewConfigurationKeyTest failed... " + errorString);

            lsConfigurationKeys.Add("Test Remove Key");
        }

        [TestMethod]
        public void AddNewConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My weight",
                ConfigValue = "100"
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(@params, out var errorString);

            Assert.IsNotNull(result, "AddNewConfigurationKeyTest failed. " + errorString);

            lsConfigurationKeys.Add("Test My weight");
        }

        [TestMethod]
        public void AddConfigurationKeyArrayTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var parametersArray = new[]
            {
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test My Height",
                    ConfigValue = "185"
                },
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test My Weight",
                    ConfigValue = "110"
                }
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(
                parametersArray,
                out var errorString);

            Assert.IsNotNull(
                result,
                "AddNewConfigurationKeyTest failed. " + errorString);

            lsConfigurationKeys.Add("Test My Height");
            lsConfigurationKeys.Add("Test My Weight");
        }

        [TestMethod]
        public void GetAllConfigurationDataTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters();

            // Run the query
            var result = route4Me.GetConfigurationData(@params, out var errorString);

            Assert.IsNotNull(
                result,
                "GetAllConfigurationDataTest failed. " + errorString);
        }

        [TestMethod]
        public void GetSpecificConfigurationKeyDataTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters {ConfigKey = "Test My height"};

            // Run the query
            var result = route4Me.GetConfigurationData(@params, out var errorString);

            Assert.IsNotNull(
                result,
                "GetSpecificConfigurationKeyDataTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My height",
                ConfigValue = "190"
            };

            // Run the query
            var result = route4Me.UpdateConfigurationKey(
                @params,
                out var errorString);

            Assert.IsNotNull(
                result,
                "UpdateConfigurationKeyTest failed. " + errorString);
        }

        [TestMethod]
        public void RemoveConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test Remove Key"
            };

            // Run the query
            var result = route4Me.RemoveConfigurationKey(@params, out var errorString);

            Assert.IsNotNull(result, "RemoveConfigurationKeyTest failed. " + errorString);

            lsConfigurationKeys.Remove("Test Remove Key");
        }

        [ClassCleanup]
        public static void MemberConfigurationGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var testKey in lsConfigurationKeys)
            {
                var @params = new MemberConfigurationParameters {ConfigKey = testKey};

                var result = route4Me.RemoveConfigurationKey(@params, out var errorString);

                Assert.IsNotNull(result, "MemberConfigurationGroupCleanup failed.");
            }
        }
    }

    [TestClass]
    public class VehiclesGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static List<string> lsVehicleIDs;

        [ClassInitialize]
        public static void VehiclesGroupInitialize(TestContext context)
        {
            lsVehicleIDs = new List<string>();

            var vehicleGroup = new VehiclesGroup();

            var vehicles = vehicleGroup.GetVehiclesList();

            if ((vehicles?.Length ?? 0) < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleName = "Ford Transit Test 6",
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = vehicleGroup.CreateVehicle(newVehicle);
                lsVehicleIDs.Add(vehicle.VehicleGuid);
            }
            else
            {
                foreach (var veh1 in vehicles) lsVehicleIDs.Add(veh1.VehicleId);
            }
        }

        [TestMethod]
        public void GetVehiclesListTest()
        {
            GetVehiclesList();
        }

        public Vehicle[] GetVehiclesList()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParameters = new VehicleParameters
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            var vehicles = route4Me.GetVehicles(vehicleParameters, out var errorString);

            return vehicles;
        }

        [TestMethod]
        public void CreateVehicleTest()
        {
            // Create common vehicle
            var commonVehicleParams = new VehicleV4Parameters
            {
                VehicleName = "Ford Transit Test 6",
                VehicleAlias = "Ford Transit Test 6"
            };

            var commonVehicle = CreateVehicle(commonVehicleParams);

            if (commonVehicle != null && commonVehicle.GetType() == typeof(VehicleV4CreateResponse))
                lsVehicleIDs.Add(commonVehicle.VehicleGuid);

            // Create a truck belonging to the class 6
            var class6TruckParams = new VehicleV4Parameters
            {
                VehicleName = "GMC TopKick C5500",
                VehicleAlias = "GMC TopKick C5500",
                VehicleVin = "SAJXA01A06FN08012",
                VehicleLicensePlate = "CVH4561",
                VehicleModel = "TopKick C5500",
                VehicleModelYear = 1995,
                VehicleYearAcquired = 2008,
                VehicleRegCountryId = 223,
                VehicleMake = "GMC",
                VehicleTypeID = "pickup_truck",
                VehicleAxleCount = 2,
                MpgCity = 7,
                MpgHighway = 14,
                FuelType = "diesel",
                HeightInches = 97,
                HeightMetric = 243,
                WeightLb = 19000,
                MaxWeightPerAxleGroupInPounds = 9500,
                MaxWeightPerAxleGroupMetric = 4300,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 244,
                LengthMetric = 610,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "NO",
                DividedHighwayAvoidPreference = "NEUTRAL",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "FULLSIZEVAN"
            };

            var class6Truck = CreateVehicle(class6TruckParams);

            if (class6Truck != null && class6Truck.GetType() == typeof(VehicleV4CreateResponse))
                lsVehicleIDs.Add(class6Truck.VehicleGuid);

            // Create a truck belonging to the class 7
            var class7TruckParams = new VehicleV4Parameters
            {
                VehicleName = "FORD F750",
                VehicleAlias = "FORD F750",
                VehicleVin = "1NPAX6EX2YD550743",
                VehicleLicensePlate = "FFV9547",
                VehicleModel = "F-750",
                VehicleModelYear = 2010,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleTypeID = "livestock_carrier",
                VehicleAxleCount = 2,
                MpgCity = 8,
                MpgHighway = 15,
                FuelType = "diesel",
                HeightInches = 96,
                HeightMetric = 244,
                WeightLb = 26000,
                MaxWeightPerAxleGroupInPounds = 15000,
                MaxWeightPerAxleGroupMetric = 6800,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 312,
                LengthMetric = 793,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "YES",
                DividedHighwayAvoidPreference = "FAVOR",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "26_STRAIGHT_TRUCK",
                TollRoadUsage = "ALWAYS_AVOID",
                InternationalBordersOpen = "NO",
                PurchasedNew = true
            };

            var class7Truck = CreateVehicle(class7TruckParams);

            if (class7Truck != null && class7Truck.GetType() == typeof(VehicleV4CreateResponse))
                lsVehicleIDs.Add(class7Truck.VehicleGuid);

            // Create a truck belonging to the class 8
            var class8TruckParams = new VehicleV4Parameters
            {
                VehicleName = "Peterbilt 579",
                VehicleAlias = "Peterbilt 579",
                VehicleVin = "1NP5DB9X93N507873",
                VehicleLicensePlate = "PPV7516",
                VehicleModel = "579",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Peterbilt",
                VehicleTypeID = "tractor_trailer",
                VehicleAxleCount = 4,
                MpgCity = 6,
                MpgHighway = 12,
                FuelType = "diesel",
                HeightInches = 114,
                HeightMetric = 290,
                WeightLb = 50350,
                MaxWeightPerAxleGroupInPounds = 40000,
                MaxWeightPerAxleGroupMetric = 18000,
                WidthInInches = 102,
                WidthMetric = 258,
                LengthInInches = 640,
                LengthMetric = 1625,
                Use53FootTrailerRouting = "YES",
                UseTruckRestrictions = "YES",
                DividedHighwayAvoidPreference = "STRONG_FAVOR",
                FreewayAvoidPreference = "STRONG_AVOID",
                TruckConfig = "53_SEMI_TRAILER",
                TollRoadUsage = "ALWAYS_AVOID",
                InternationalBordersOpen = "YES",
                PurchasedNew = true
            };

            var class8Truck = CreateVehicle(class8TruckParams);

            if (class8Truck != null && class8Truck.GetType() == typeof(VehicleV4CreateResponse))
                lsVehicleIDs.Add(class8Truck.VehicleGuid);
        }

        public VehicleV4CreateResponse CreateVehicle(VehicleV4Parameters vehicleParams)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var errorString = "";
            var result = route4Me.CreateVehicle(vehicleParams, out errorString);

            Assert.IsNotNull(result, "CreatetVehiclTest failed. " + errorString);

            return result;
        }

        [TestMethod]
        public void GetVehicleTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParameters = new VehicleParameters
            {
                VehicleId = lsVehicleIDs[lsVehicleIDs.Count - 1]
            };

            // Run the query
            var vehicles = route4Me.GetVehicle(vehicleParameters, out var errorString);

            Assert.IsInstanceOfType(
                vehicles,
                typeof(VehicleV4Response),
                "getVehicleTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateVehicleTest()
        {
            if (c_ApiKey == ApiKeys.DemoApiKey) return;

            if (lsVehicleIDs.Count < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = CreateVehicle(newVehicle);
                lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var route4Me = new Route4MeManager(c_ApiKey);

            // TO DO: on this stage specifying of the parameter vehicle_alias is mandatory. 
            // Will be checked later
            var vehicleParams = new VehicleV4Parameters
            {
                VehicleModelYear = 1995,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleAxleCount = 2,
                FuelType = "unleaded 93",
                HeightInches = 72,
                WeightLb = 2000
            };

            // Run the query
            var vehicles = route4Me.UpdateVehicle(
                vehicleParams,
                lsVehicleIDs[lsVehicleIDs.Count - 1],
                out var errorString);

            Assert.IsInstanceOfType(
                vehicles,
                typeof(VehicleV4Response),
                "updateVehicleTest failed. " + errorString);
        }

        [TestMethod]
        public void DeleteVehicleTest()
        {
            if (lsVehicleIDs.Count < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = CreateVehicle(newVehicle);
                lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParams = new VehicleV4Parameters
            {
                VehicleId = lsVehicleIDs[lsVehicleIDs.Count - 1]
            };

            // Run the query
            var vehicles = route4Me.DeleteVehicle(vehicleParams, out var errorString);

            Assert.IsInstanceOfType(
                vehicles,
                typeof(VehicleV4Response),
                "updateVehicleTest failed. " + errorString);

            lsVehicleIDs.RemoveAt(lsVehicleIDs.Count - 1);
        }

        [ClassCleanup]
        public static void VehiclesGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var vehicleId in lsVehicleIDs)
            {
                var vehicleParams = new VehicleV4Parameters
                {
                    VehicleId = vehicleId
                };

                // Run the query
                var vehicles = route4Me.DeleteVehicle(vehicleParams, out var errorString);
            }
        }
    }

    [TestClass]
    public class GeocodingGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        [TestInitialize]
        public void GeocodingGroupInitialize()
        {
            //Console.SetOut(new StreamWriter(new FileStream("Console_Output.txt", FileMode.Append)) { AutoFlush = true });
        }

        [TestMethod]
        public void GeocodingForwardTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA||3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.Geocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "GeocodingForwardTest failed. " + errorString);
        }

        [TestMethod]
        public void BatchGeocodingForwardTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.BatchGeocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "GeocodingForwardTest failed. " + errorString);
        }

        [TestMethod]
        public void BatchGeocodingForwardAsyncTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.BatchGeocodingAsync(geoParams, out var errorString);

            Assert.IsNotNull(result, "GeocodingForwardTest failed. " + errorString);
        }


        [TestMethod]
        public void UploadAndGeocodeLargeJsonFile()
        {
            var fastProcessing = new FastBulkGeocoding(c_ApiKey, false);
            var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            var addressesInFile = 13;

            fastProcessing.GeocodingIsFinished += (sender, e) =>
            {
                Assert.IsNotNull(lsAddresses, "Geocoding process failed");

                Assert.AreEqual(
                    addressesInFile,
                    lsAddresses.Count,
                    "Not all the addresses were geocoded");

                Console.WriteLine("Large addresses file geocoding is finished");
            };

            fastProcessing.AddressesChunkGeocoded += (sender, e) =>
            {
                if (e.lsAddressesChunkGeocoded != null)
                    foreach (var addr1 in e.lsAddressesChunkGeocoded)
                        lsAddresses.Add(addr1.GeocodedAddress.AddressString);

                Console.WriteLine("Total Geocoded Addresses -> " + lsAddresses.Count);
            };

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.uploadAndGeocodeLargeJsonFile(
                stPath + @"\Data\JSON\batch_socket_upload_error_addresses_data_5.json");
        }

        private void FastProcessing_AddressesChunkGeocoded(object sender,
            FastBulkGeocoding.AddressesChunkGeocodedArgs e)
        {
            if (e.lsAddressesChunkGeocoded != null)
                Console.WriteLine("Geocoded addresses " + e.lsAddressesChunkGeocoded.Count);
        }

        [TestMethod]
        public void RapidStreetDataAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters();

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataAllTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetDataLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Offset = 10,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataLimitedTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetDataSingleTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Pk = 4
            };

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataSingleTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetServiceAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Housenumber = "17"
            };

            // Run the query
            var result = route4Me.RapidStreetService(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetServiceAllTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetServiceLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Housenumber = "17",
                Offset = 1,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetService(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetServiceLimitedTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetZipcodeAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601"
            };

            // Run the query
            var result = route4Me.RapidStreetZipcode(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetZipcodeAllTest failed. " + errorString);
        }

        [TestMethod]
        public void RapidStreetZipcodeLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Offset = 1,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetZipcode(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetZipcodeLimitedTest failed. " + errorString);
        }

        [TestMethod]
        public void ReverseGeocodingTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "41.00367151,-81.59846105"
            };

            geoParams.ExportFormat = "json";

            // Run the query
            var result = route4Me.Geocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "ReverseGeocodingTest failed. " + errorString);
        }
    }

    [TestClass]
    public class OptimizationsGroup
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;
        private static List<string> lsAddressbookContacts;
        private static List<string> lsOrders;

        [ClassInitialize]
        public static void OptimizationsGroupInitialize(TestContext context)
        {
            Assert.IsNotNull(
                context,
                "Initialization of the class OptimizationsGroup failed.");

            lsOptimizationIDs = new List<string>();
            lsAddressbookContacts = new List<string>();
            lsOrders = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();

            Assert.IsTrue(result, "Single Driver 10 stops generation failed.");

            Assert.IsTrue(
                tdr.SD10Stops_route.Addresses.Length > 0,
                "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.DataObjectSD10Stops.OptimizationProblemId);
        }

        [TestMethod]
        public void GetOptimizationsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var queryParameters = new OptimizationParameters
            {
                Limit = 5,
                Offset = 2
            };

            // Run the query
            var dataObjects = route4Me.GetOptimizations(queryParameters, out var errorString);

            Assert.IsInstanceOfType(
                dataObjects,
                typeof(DataObject[]),
                "GetOptimizationsTest failed. " + errorString);
        }

        [TestMethod]
        public void GetOptimizationsFromDateRangeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var queryParameters = new OptimizationParameters
            {
                StartDate = "2019-09-15",
                EndDate = "2019-09-20"
            };

            // Run the query
            string errorString;
            var dataObjects = route4Me.GetOptimizations(queryParameters, out errorString);

            Assert.IsInstanceOfType(
                dataObjects,
                typeof(DataObject[]),
                "GetOptimizationsFromDateRangeTest failed. " + errorString);
        }

        [TestMethod]
        public void GetOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = tdr.SD10Stops_optimization_problem_id
            };

            // Run the query
            var dataObject = route4Me.GetOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObject,
                "GetOptimizationTest failed. " + errorString);
        }

        [TestMethod]
        public void ReOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = tdr.SD10Stops_optimization_problem_id,
                ReOptimize = true
            };

            // Run the query
            var dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out var errorString);

            lsOptimizationIDs.Add(dataObject.OptimizationProblemId);

            Assert.IsNotNull(
                dataObject,
                "ReOptimizationTest failed. " + errorString);
        }

        [TestMethod]
        public void UpdateOptimizationDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var address = tdr.SD10Stops_route.Addresses[3];

            address.FirstName = "UpdatedFirstName";
            address.LastName = "UpdatedLastName";

            var errorString = "";
            var updatedAddress = route4Me.UpdateOptimizationDestination(address, out errorString);

            Assert.IsNotNull(
                updatedAddress,
                "UpdateOptimizationDestinationTest failed.");
        }

        [TestMethod]
        public void RemoveOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var result = tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(
                result,
                "Generation of the route Single Driver Round Trip failed.");

            var opt_id = tdr.SDRT_optimization_problem_id;
            Assert.IsNotNull(opt_id, "optimizationProblemID is null.");

            string[] OptIDs = {opt_id};

            // Run the query
            var removed = route4Me.RemoveOptimization(OptIDs, out var errorString);

            Assert.IsTrue(removed, "RemoveOptimizationTest failed. " + errorString);
        }

        [TestMethod]
        public void HybridOptimizationFrom1000AddressesTest()
        {
            var ApiKey =
                ApiKeys.ActualApiKey; // The addresses in this test not allowed for this API key, you shuld put your valid API key.

            // Comment 2 lines bellow if you have put in the above line your valid key.
            Assert.IsTrue(1 > 0, "");
            if (ApiKey.Length > 10) return;

            var route4Me = new Route4MeManager(ApiKey);

            #region ======= Add scheduled address book locations to an user account ================================

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/addresses_1000.csv";
            var sched0 = new Schedule("daily", false);

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    //int iCount = 0;
                    while (csv.Read())
                    {
                        var lng = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var alias = csv.GetField(2);
                        var address1 = csv.GetField(3);
                        var city = csv.GetField(4);
                        var state = csv.GetField(5);
                        var zip = csv.GetField(6);
                        var phone = csv.GetField(7);
                        //var sched_date = csv.GetField(8);
                        var sched_mode = csv.GetField(8);
                        var sched_enabled = csv.GetField(9);
                        var sched_every = csv.GetField(10);
                        var sched_weekdays = csv.GetField(11);
                        var sched_monthly_mode = csv.GetField(12);
                        var sched_monthly_dates = csv.GetField(13);
                        var sched_annually_usenth = csv.GetField(14);
                        var sched_annually_months = csv.GetField(15);
                        var sched_nth_n = csv.GetField(16);
                        var sched_nth_what = csv.GetField(17);

                        var sAddress = "";

                        if (address1 != null) sAddress += address1.Trim();
                        if (city != null) sAddress += ", " + city.Trim();
                        if (state != null) sAddress += ", " + state.Trim();
                        if (zip != null) sAddress += ", " + zip.Trim();

                        if (sAddress == "") continue;

                        var newLocation = new AddressBookContact();

                        if (lng != null) newLocation.CachedLng = Convert.ToDouble(lng);
                        if (lat != null) newLocation.CachedLat = Convert.ToDouble(lat);
                        if (alias != null) newLocation.AddressAlias = alias.Trim();
                        newLocation.Address1 = sAddress;
                        if (phone != null) newLocation.AddressPhoneNumber = phone.Trim();

                        //newLocation.schedule = new Schedule[]{};
                        if (!sched0.ValidateScheduleMode(sched_mode)) continue;

                        sched0.From = DateTime.Now.ToString("yyyy-MM-dd");

                        var blNth = false;

                        if (sched0.ValidateScheduleMonthlyMode(sched_monthly_mode))
                            if (sched_monthly_mode == "nth")
                                blNth = true;
                        if (sched0.ValidateScheduleUseNth(sched_annually_usenth))
                            if (sched_annually_usenth.ToLower() == "true")
                                blNth = true;

                        var schedule = new Schedule(sched_mode, blNth);

                        var dt = DateTime.Now;
                        //if (schedule.ValidateScheduleMode(sched_mode))
                        //{
                        schedule.Mode = sched_mode;
                        if (schedule.ValidateScheduleEnabled(sched_enabled))
                        {
                            schedule.Enabled = Convert.ToBoolean(sched_enabled);
                            if (schedule.ValidateScheduleEvery(sched_every))
                            {
                                var iEvery = Convert.ToInt32(sched_every);

                                switch (schedule.Mode)
                                {
                                    case "daily":
                                        schedule.Daily.Every = iEvery;
                                        break;
                                    case "weekly":
                                        if (schedule.ValidateScheduleWeekdays(sched_weekdays))
                                        {
                                            schedule.Weekly.Every = iEvery;

                                            var arWeekdays = sched_weekdays.Split(',');
                                            var lsWeekdays = new List<int>();

                                            for (var i = 0; i < arWeekdays.Length; i++)
                                                lsWeekdays.Add(Convert.ToInt32(arWeekdays[i]));
                                            schedule.Weekly.Weekdays = lsWeekdays.ToArray();
                                        }

                                        break;
                                    case "monthly":
                                        if (schedule.ValidateScheduleMonthlyMode(sched_monthly_mode))
                                        {
                                            schedule.Monthly.Every = iEvery;
                                            schedule.Monthly.Mode = sched_monthly_mode;
                                            switch (schedule.Monthly.Mode)
                                            {
                                                case "dates":
                                                    if (schedule.ValidateScheduleMonthDays(sched_monthly_dates))
                                                    {
                                                        var arMonthdays = sched_monthly_dates.Split(',');
                                                        var lsMonthdays = new List<int>();

                                                        for (var i = 0; i < arMonthdays.Length; i++)
                                                            lsMonthdays.Add(Convert.ToInt32(arMonthdays[i]));
                                                        schedule.Monthly.Dates = lsMonthdays.ToArray();
                                                    }

                                                    break;
                                                case "nth":
                                                    if (schedule.ValidateScheduleNthN(sched_nth_n))
                                                        schedule.Monthly.Nth.N = Convert.ToInt32(sched_nth_n);
                                                    if (schedule.ValidateScheduleNthWhat(sched_nth_what))
                                                        schedule.Monthly.Nth.What = Convert.ToInt32(sched_nth_what);
                                                    break;
                                            }
                                        }

                                        break;
                                    case "annually":
                                        if (schedule.ValidateScheduleUseNth(sched_annually_usenth))
                                        {
                                            schedule.Annually.Every = iEvery;
                                            schedule.Annually.UseNth = Convert.ToBoolean(sched_annually_usenth);
                                            if (schedule.Annually.UseNth)
                                            {
                                                if (schedule.ValidateScheduleNthN(sched_nth_n))
                                                    schedule.Annually.Nth.N = Convert.ToInt32(sched_nth_n);
                                                if (schedule.ValidateScheduleNthWhat(sched_nth_what))
                                                    schedule.Annually.Nth.What = Convert.ToInt32(sched_nth_what);
                                            }
                                            else
                                            {
                                                if (schedule.ValidateScheduleYearMonths(sched_annually_months))
                                                {
                                                    var arYearmonths = sched_annually_months.Split(',');
                                                    var lsMonths = new List<int>();

                                                    for (var i = 0; i < arYearmonths.Length; i++)
                                                        lsMonths.Add(Convert.ToInt32(arYearmonths[i]));
                                                    schedule.Annually.Months = lsMonths.ToArray();
                                                }
                                            }
                                        }

                                        break;
                                }
                            }
                        }

                        newLocation.Schedule = new List<Schedule> {schedule}.ToArray();
                        //}

                        //string errorString;
                        var resultContact = route4Me.AddAddressBookContact(newLocation, out var errorString);

                        Assert.IsNotNull(resultContact, "Creation of an addressbook contact failed... " + errorString);

                        if (resultContact != null)
                        {
                            var AddressId = resultContact.AddressId != null ? resultContact.AddressId.ToString() : "";

                            if (AddressId != "") lsAddressbookContacts.Add(AddressId);
                        }

                        Thread.Sleep(1000);
                    }
                }
            }

            ;

            #endregion

            Thread.Sleep(2000);

            #region ======= Get Hybrid Optimization ================================

            var tsp1day = new TimeSpan(1, 0, 0, 0);
            var lsScheduledDays = new List<string>();
            var curDate = DateTime.Now;

            for (var i = 0; i < 5; i++)
            {
                curDate += tsp1day;
                lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"));
            }

            #region Addresses

            Address[] Depots =
            {
                new Address
                {
                    AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239",
                    IsDepot = true,
                    Latitude = 32.474395,
                    Longitude = -99.7447021,
                    CurbsideLatitude = 32.474395,
                    CurbsideLongitude = -99.7447021
                },
                new Address
                {
                    AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596",
                    IsDepot = true,
                    Latitude = 26.170834,
                    Longitude = -98.116201,
                    CurbsideLatitude = 26.170834,
                    CurbsideLongitude = -98.116201
                },
                new Address
                {
                    AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505",
                    IsDepot = true,
                    Latitude = 35.221969,
                    Longitude = -101.835288,
                    CurbsideLatitude = 35.221969,
                    CurbsideLongitude = -101.835288
                },
                new Address
                {
                    AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242",
                    IsDepot = true,
                    Latitude = 35.236626,
                    Longitude = -101.795117,
                    CurbsideLatitude = 35.236626,
                    CurbsideLongitude = -101.795117
                },
                new Address
                {
                    AddressString = "1525 New York Ave, Arlington, TX, 76010-4723",
                    IsDepot = true,
                    Latitude = 32.720524,
                    Longitude = -97.080195,
                    CurbsideLatitude = 32.720524,
                    CurbsideLongitude = -97.080195
                }
            };

            #endregion

            foreach (var ScheduledDay in lsScheduledDays)
            {
                var hparams = new HybridOptimizationParameters
                {
                    TargetDateString = ScheduledDay,
                    TimezoneOffsetMinutes = -240
                };

                var resultOptimization = route4Me.GetHybridOptimization(hparams, out var errorString1);

                Assert.IsNotNull(resultOptimization, "Get Hybrid Optimization failed... " + errorString1);

                var HybridOptimizationId = "";

                if (resultOptimization != null)
                    HybridOptimizationId = resultOptimization.OptimizationProblemId;
                else
                    continue;

                //============== Add Depot To Hybrid Optimization ===============
                var hDepotParams = new HybridDepotParameters
                {
                    OptimizationProblemId = HybridOptimizationId,
                    DeleteOldDepots = true,
                    NewDepots = new[] {Depots[lsScheduledDays.IndexOf(ScheduledDay)]}
                };

                var addDepotResult = route4Me.AddDepotsToHybridOptimization(hDepotParams, out var errorString3);

                Assert.IsTrue(addDepotResult, "Adding a depot to the Hybrid Optimization failed... " + errorString3);

                Thread.Sleep(5000);

                //============== Reoptimization =================================
                var optimizationParameters = new OptimizationParameters
                {
                    OptimizationProblemID = HybridOptimizationId,
                    ReOptimize = true
                };

                var finalOptimization = route4Me.UpdateOptimization(optimizationParameters, out var errorString2);

                Assert.IsNotNull(finalOptimization, "Update optimization failed... " + errorString1);

                if (finalOptimization != null) lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId);

                Thread.Sleep(5000);
                //=================================================================
            }

            #endregion

            var removeLocations = tdr.RemoveAddressBookContacts(lsAddressbookContacts, ApiKey);

            Assert.IsTrue(removeLocations, "Removing of the addressbook contacts failed...");
        }

        [TestMethod]
        public void HybridOptimizationFrom1000OrdersTest()
        {
            var ApiKey =
                ApiKeys.ActualApiKey; // The addresses in this test not allowed for this API key, you shuld put your valid API key.

            // Comment 2 lines bellow if you have put in the above line your valid key.
            Assert.IsTrue(1 > 0, "");
            if (ApiKey.Length > 10) return;

            var route4Me = new Route4MeManager(ApiKey);

            #region ======= Add scheduled address book locations to an user account ================================

            var sAddressFile = @"Data/CSV/orders_1000.csv";

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var lng = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var alias = csv.GetField(2);
                        var address1 = csv.GetField(3);
                        var city = csv.GetField(4);
                        var state = csv.GetField(5);
                        var zip = csv.GetField(6);
                        var phone = csv.GetField(7);
                        var sched_date = csv.GetField(8);

                        var sAddress = "";

                        if (address1 != null) sAddress += address1.Trim();
                        if (city != null) sAddress += ", " + city.Trim();
                        if (state != null) sAddress += ", " + state.Trim();
                        if (zip != null) sAddress += ", " + zip.Trim();

                        if (sAddress == "") continue;

                        var newOrder = new Order();

                        if (lng != null) newOrder.CachedLat = Convert.ToDouble(lng);
                        if (lat != null) newOrder.CachedLng = Convert.ToDouble(lat);
                        if (alias != null) newOrder.AddressAlias = alias.Trim();
                        newOrder.Address1 = sAddress;
                        if (phone != null) newOrder.ExtFieldPhone = phone.Trim();

                        var dt = DateTime.Now;

                        if (sched_date != null)
                            if (DateTime.TryParse(sched_date, out dt))
                            {
                                dt = Convert.ToDateTime(sched_date);
                                newOrder.DayScheduledFor_YYYYMMDD = dt.ToString("yyyy-MM-dd");
                            }

                        var resultOrder = route4Me.AddOrder(newOrder, out var errorString);
                        Assert.IsNotNull(resultOrder, "Creating of an order failed... " + errorString);

                        if (resultOrder != null)
                        {
                            var OrderId = resultOrder.OrderId != null ? resultOrder.OrderId.ToString() : "";

                            if (OrderId != "") lsOrders.Add(OrderId);
                        }

                        Thread.Sleep(1000);
                    }
                }

                ;
            }

            ;

            #endregion

            Thread.Sleep(2000);

            #region ======= Get Hybrid Optimization ================================

            var tsp1day = new TimeSpan(1, 0, 0, 0);
            var lsScheduledDays = new List<string>();
            var curDate = DateTime.Now;

            for (var i = 0; i < 5; i++)
            {
                curDate += tsp1day;
                lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"));
            }

            Address[] Depots =
            {
                new Address
                {
                    AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239",
                    IsDepot = true,
                    Latitude = 32.474395,
                    Longitude = -99.7447021,
                    CurbsideLatitude = 32.474395,
                    CurbsideLongitude = -99.7447021
                },
                new Address
                {
                    AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596",
                    IsDepot = true,
                    Latitude = 26.170834,
                    Longitude = -98.116201,
                    CurbsideLatitude = 26.170834,
                    CurbsideLongitude = -98.116201
                },
                new Address
                {
                    AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505",
                    IsDepot = true,
                    Latitude = 35.221969,
                    Longitude = -101.835288,
                    CurbsideLatitude = 35.221969,
                    CurbsideLongitude = -101.835288
                },
                new Address
                {
                    AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242",
                    IsDepot = true,
                    Latitude = 35.236626,
                    Longitude = -101.795117,
                    CurbsideLatitude = 35.236626,
                    CurbsideLongitude = -101.795117
                },
                new Address
                {
                    AddressString = "1525 New York Ave, Arlington, TX, 76010-4723",
                    IsDepot = true,
                    Latitude = 32.720524,
                    Longitude = -97.080195,
                    CurbsideLatitude = 32.720524,
                    CurbsideLongitude = -97.080195
                }
            };

            foreach (var ScheduledDay in lsScheduledDays)
            {
                var hparams = new HybridOptimizationParameters
                {
                    TargetDateString = ScheduledDay,
                    TimezoneOffsetMinutes = 480
                };

                var resultOptimization = route4Me.GetHybridOptimization(hparams, out var errorString1);

                var HybridOptimizationId = "";

                if (resultOptimization != null)
                {
                    HybridOptimizationId = resultOptimization.OptimizationProblemId;
                    Console.WriteLine("Hybrid optimization generating executed successfully");

                    Console.WriteLine("Generated hybrid optimization ID: {0}", HybridOptimizationId);
                }
                else
                {
                    Console.WriteLine("Hybrid optimization generating error: {0}", errorString1);
                    continue;
                }

                //============== Add Depot To Hybrid Optimization ===============
                var hDepotParams = new HybridDepotParameters
                {
                    OptimizationProblemId = HybridOptimizationId,
                    DeleteOldDepots = true,
                    NewDepots = new[] {Depots[lsScheduledDays.IndexOf(ScheduledDay)]}
                };

                var addDepotResult = route4Me.AddDepotsToHybridOptimization(hDepotParams, out var errorString3);

                Thread.Sleep(5000);

                //============== Reoptimization =================================
                var optimizationParameters = new OptimizationParameters
                {
                    OptimizationProblemID = HybridOptimizationId,
                    ReOptimize = true
                };

                var finalOptimization = route4Me.UpdateOptimization(optimizationParameters, out var errorString2);

                Assert.IsNotNull(finalOptimization, "Update optimization failed... " + errorString1);

                if (finalOptimization != null) lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId);

                Thread.Sleep(5000);
                //=================================================================
            }

            var removeOrders = tdr.RemoveOrders(lsOrders, ApiKey);

            Assert.IsTrue(removeOrders, "Removing of the orders failed...");

            #endregion
        }

        [ClassCleanup]
        public static void OptimizationsGroupCleanup()
        {
            var result = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");
        }
    }

    [TestClass]
    public class TelematicsGateWayAPI
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        //static List<string> lsMembers;
        private static string firstMemberId;

        private static string apiToken;

        private static List<TelematicsConnection> lsCreatedConnections;

        private static TelematicsVendors tomtomVendor;

        [ClassInitialize]
        public static void TelematicsGateWayAPIInitialize(TestContext context)
        {
            if (ApiKeys.ActualApiKey == ApiKeys.DemoApiKey)
                Assert.Inconclusive("The test cannot done with demo API key");

            var route4Me = new Route4MeManager(c_ApiKey);

            //lsMembers = new List<string>();
            lsCreatedConnections = new List<TelematicsConnection>();

            var members = route4Me.GetUsers(new GenericParameters(), out var errString);

            Assert.IsNotNull((members?.Results?.Length ?? 0) > 0,
                "Cannot retrieve the account members." + Environment.NewLine + errString);

            firstMemberId = members.Results[0].MemberId;

            var memberParameters = new TelematicsVendorParameters
            {
                MemberID = Convert.ToUInt32(firstMemberId),
                ApiKey = c_ApiKey
            };

            var result = route4Me.RegisterTelematicsMember(memberParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test registerMemberTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsRegisterMemberResponse));

            apiToken = result.ApiToken;

            var vendParams = new TelematicsVendorParameters {Search = "tomtom"};

            var vendors = route4Me.SearchTelematicsVendors(vendParams, out var errorString2);

            Assert.IsNotNull(
                vendors?.Vendors ?? null,
                "Cannot retrieve tomtom vendor. " + errorString);

            Assert.IsInstanceOfType(
                vendors.Vendors,
                typeof(TelematicsVendors[]));

            Assert.IsTrue(vendors.Vendors.Length > 0);

            tomtomVendor = vendors.Vendors[0];

            #region Test Connection

            var conParams = new TelematicsConnectionParameters
            {
                Vendor = TelematicsVendorType.Geotab.Description(),
                AccountId = "54321",
                UserName = "John Doe 0",
                Password = "password0",
                VehiclePositionRefreshRate = 60,
                Name = "Test Geotab Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result0 = route4Me.CreateTelematicsConnection(apiToken, conParams, out var errorString0);

            Assert.IsNotNull(result0,
                "The test createTelematicsConnectionTest failed. " + errorString);

            Assert.IsInstanceOfType(result0,
                typeof(TelematicsConnection));

            lsCreatedConnections.Add(result0);

            #endregion
        }

        [TestMethod]
        public void getAllVendorsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters();

            var vendors = route4Me.GetAllTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(vendors, "The test getAllVendorsTest failed. " + errorString);

            Assert.IsInstanceOfType(
                vendors,
                typeof(TelematicsVendorsResponse),
                "The test getAllVendorsTest failed. " + errorString);
        }

        [TestMethod]
        public void getVendorTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendors = route4Me.GetAllTelematicsVendors(
                new TelematicsVendorParameters(),
                out var errorString);

            var randomNumber = new Random().Next(0, vendors.Vendors.Count() - 1);
            var randomVendorID = vendors.Vendors[randomNumber].ID;

            var vendorParameters = new TelematicsVendorParameters
            {
                VendorID = Convert.ToUInt32(randomVendorID)
            };

            var vendor = route4Me.GetTelematicsVendor(vendorParameters, out errorString);

            Assert.IsNotNull(vendors, "The test getVendorTest failed. " + errorString);

            Assert.IsInstanceOfType(
                vendors,
                typeof(TelematicsVendorsResponse),
                "The test getVendorTest failed. " + errorString);
        }

        [TestMethod]
        public void searchVendorsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                //Country = "GB",  // uncomment this line for searching by Country
                IsIntegrated = 1,
                //Feature = "Satelite",  // uncomment this line for searching by Feature
                Search = "Fleet",
                Page = 1,
                PerPage = 15
            };

            var vendors = route4Me.SearchTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(vendors, "The test searchVendorsTest failed. " + errorString);

            Assert.IsInstanceOfType(
                vendors,
                typeof(TelematicsVendorsResponse),
                "The test searchVendorsTest failed. " + errorString);
        }

        [TestMethod]
        public void vendorsComparisonTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.DemoApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                Vendors = "55,56,57"
            };

            var vendors = route4Me.SearchTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(
                vendors,
                "The test vendorsComparisonTest failed. " + errorString);

            Assert.IsInstanceOfType(
                vendors, typeof(TelematicsVendorsResponse),
                "The test vendorsComparisonTest failed. " + errorString
            );
        }

        [TestMethod]
        public void registerMemberTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                MemberID = Convert.ToUInt32(firstMemberId),
                ApiKey = c_ApiKey
            };

            var result = route4Me.RegisterTelematicsMember(vendorParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test registerMemberTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsRegisterMemberResponse));
        }

        [TestMethod]
        public void getTelematicsConnectionsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                ApiToken = apiToken
            };

            var result = route4Me.GetTelematicsConnections(vendorParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test getTelematicsConnectionsTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsConnection[]));
        }

        [TestMethod]
        public void createTelematicsConnectionTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(tomtomVendor.ID),
                Vendor = tomtomVendor.Slug,
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result = route4Me.CreateTelematicsConnection(apiToken, conParams, out var errorString);

            Assert.IsNotNull(
                result,
                "The test createTelematicsConnectionTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsConnection));

            lsCreatedConnections.Add(result);
        }

        [TestMethod]
        public void deleteTelematicsConnectionTest()
        {
            if (lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.DeleteTelematicsConnection(
                apiToken,
                lsCreatedConnections[lsCreatedConnections.Count - 1].ConnectionToken,
                out var errorString);

            Assert.IsNotNull(
                result,
                "The test deleteTelematicsConnectionTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsConnection));

            lsCreatedConnections.RemoveAt(lsCreatedConnections.Count - 1);
        }

        [TestMethod]
        public void updateTelematicsConnectionTest()
        {
            if (lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(tomtomVendor.ID),
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };
        }

        [TestMethod]
        public void getTelematicsConnectionTest()
        {
            if (lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.GetTelematicsConnection(
                apiToken,
                lsCreatedConnections[0].ConnectionToken,
                out var errorString);

            Assert.IsNotNull(
                result,
                "The test getTelematicsConnectionTest failed. " + errorString);

            Assert.IsInstanceOfType(
                result,
                typeof(TelematicsConnection));
        }

        [ClassCleanup]
        public static void TelematicsGateWayAPICleanup()
        {
            if (lsCreatedConnections.Count > 0)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                foreach (var conn in lsCreatedConnections)
                {
                    var result =
                        route4Me.DeleteTelematicsConnection(apiToken, conn.ConnectionToken, out var errorString);
                }
            }
        }
    }

    // **** Data repository for the tests ********
    public class TestDataRepository
    {
        private readonly string c_ApiKey = ApiKeys.ActualApiKey;

        public TestDataRepository()
        {
            c_ApiKey = ApiKeys.ActualApiKey;
        }

        public DataObject DataObjectSD10Stops { get; set; }

        public string SD10Stops_optimization_problem_id { get; set; }

        public DataObjectRoute SD10Stops_route { get; set; }

        public string SD10Stops_route_id { get; set; }

        public DataObject DataObjectSDRT { get; set; }

        public string SDRT_optimization_problem_id { get; set; }

        public DataObjectRoute SDRT_route { get; set; }

        public string SDRT_route_id { get; set; }

        public DataObject DataObjectMDMD24 { get; set; }

        public string MDMD24_optimization_problem_id { get; set; }

        public DataObjectRoute MDMD24_route { get; set; }

        public string MDMD24_route_id { get; set; }

        public bool RunOptimizationSingleDriverRoute10Stops()
        {
            var r4mm = new Route4MeManager(c_ApiKey);

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
            try
            {
                DataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, out var errorString);

                SD10Stops_optimization_problem_id = DataObjectSD10Stops.OptimizationProblemId;
                SD10Stops_route = DataObjectSD10Stops != null &&
                                  DataObjectSD10Stops.Routes != null &&
                                  DataObjectSD10Stops.Routes.Length > 0
                    ? DataObjectSD10Stops.Routes[0]
                    : null;
                SD10Stops_route_id = SD10Stops_route?.RouteId ?? null;

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
            var route4Me = new Route4MeManager(c_ApiKey);

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
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

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
            try
            {
                DataObjectSDRT = route4Me.RunOptimization(optimizationParameters, out var errorString);
                SDRT_optimization_problem_id = DataObjectSDRT.OptimizationProblemId;
                SDRT_route = DataObjectSDRT != null && DataObjectSDRT.Routes != null && DataObjectSDRT.Routes.Length > 0
                    ? DataObjectSDRT.Routes[0]
                    : null;
                SDRT_route_id = SDRT_route?.RouteId ?? null;

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
            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            try
            {
                var removed = route4Me.RemoveOptimization(optimizationProblemIDs, out var errorString);
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
            var route4Me = new Route4MeManager(c_ApiKey);

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
                    AddressString = "512 Florida Pl, Barberton, OH 44203",
                    IsDepot = true,
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
            //string errorString;

            try
            {
                DataObjectMDMD24 = route4Me.RunOptimization(optimizationParameters, out var errorString);

                MDMD24_route_id =
                    DataObjectMDMD24 != null && DataObjectMDMD24.Routes != null && DataObjectMDMD24.Routes.Length > 0
                        ? DataObjectMDMD24.Routes[0].RouteId
                        : null;
                MDMD24_optimization_problem_id = DataObjectMDMD24.OptimizationProblemId;
                MDMD24_route =
                    DataObjectMDMD24 != null && DataObjectMDMD24.Routes != null && DataObjectMDMD24.Routes.Length > 0
                        ? DataObjectMDMD24.Routes[0]
                        : null;
                MDMD24_route_id = MDMD24_route?.RouteId ?? null;
                //MDMD24_route_id = (MDMD24_route != null) ? MDMD24_route.RouteID : null;

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

        public bool RemoveAddressBookContacts(List<string> lsRemLocations, string ApiKey)
        {
            var route4Me = new Route4MeManager(ApiKey);

            if (lsRemLocations.Count > 0)
            {
                var removed = route4Me.RemoveAddressBookContacts(lsRemLocations.ToArray(), out var errorString);

                if (errorString != "") Console.WriteLine(errorString);

                return removed;
            }

            return false;
        }

        public bool RemoveOrders(List<string> lsOrders, string ApiKey)
        {
            var route4Me = new Route4MeManager(ApiKey);

            // Run the query
            var removed = route4Me.RemoveOrders(lsOrders.ToArray(), out var errorString);

            if (errorString != "") Console.WriteLine(errorString);

            return removed;
        }
    }

    #region Types

    [DataContract]
    internal class AddressInfo : GenericParameters
    {
        [DataMember(Name = "route_destination_id")]
        public int DestinationId { get; set; }

        [DataMember(Name = "sequence_no")] public int SequenceNo { get; set; }

        [DataMember(Name = "is_depot")] public bool IsDepot { get; set; }
    }

    [DataContract]
    internal class AddressesOrderInfo : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        [DataMember(Name = "addresses")] public AddressInfo[] Addresses { get; set; }
    }

    [DataContract]
    internal class MyAddressAndParametersHolder : GenericParameters
    {
        [DataMember] public Address[] Addresses { get; set; }

        [DataMember] public RouteParameters Parameters { get; set; }
    }

    [DataContract]
    internal class MyDataObjectGeneric
    {
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

        [DataMember(Name = "state")] public int MyState { get; set; }

        [DataMember(Name = "addresses")] public Address[] Addresses { get; set; }

        [DataMember(Name = "parameters")] public RouteParameters Parameters { get; set; }
    }

    #endregion
}