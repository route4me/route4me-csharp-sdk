using System;
using System.Collections.Generic;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSdkV5UnitTest.V5;
using Xunit;
using Xunit.Abstractions;

namespace Route4MeSdkV5UnitTest.AddOnRoutesApi
{
    public class AddOnRoutesApiTests : IDisposable
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository tdr;
        private static TestDataRepository tdr2;
        private static List<string> lsOptimizationIDs;
        private static List<string> lsRoutenIDs;

        private readonly ITestOutputHelper _output;

        public AddOnRoutesApiTests(ITestOutputHelper output)
        {
            _output = output;

            lsOptimizationIDs = new List<string>();
            lsRoutenIDs = new List<string>();

            tdr = new TestDataRepository();
            tdr2 = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();
            var result2 = tdr2.RunOptimizationSingleDriverRoute10Stops();
            var result3 = tdr2.RunSingleDriverRoundTrip();
            var result4 = tdr2.MultipleDepotMultipleDriverWith24StopsTimeWindowTest();

            Assert.True(result, "Single Driver 10 Stops generation failed.");
            Assert.True(result2, "Single Driver 10 Stops generation failed.");
            Assert.True(result4, "Multi-Depot Multi-Driver 24 Stops generation failed.");

            Assert.True(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.True(tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id);
            lsOptimizationIDs.Add(tdr2.SD10Stops_optimization_problem_id);
            lsOptimizationIDs.Add(tdr2.SDRT_optimization_problem_id);
            lsOptimizationIDs.Add(tdr2.MDMD24_optimization_problem_id);
        }

        public void Dispose()
        {
            var optimizationResult = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsRoutenIDs.Count > 0)
            {
                var routesDeleteResponse = route4Me.DeleteRoutes(
                    lsRoutenIDs.ToArray(),
                    out var resultResponse);
            }

            Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Fact]
        public void GetAllRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetAllRoutesWithPaginationTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Page = 1,
                PerPage = 20
            };

            var dataObjects = route4Me.GetAllRoutesWithPagination(routeParameters, out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetPaginatedRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Page = 1,
                PerPage = 20
            };

            var dataObjects =
                route4Me.GetPaginatedRouteListWithoutElasticSearch(routeParameters, out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteDataTableWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] {"2021-02-01", "2021-02-01"}
                },
                OrderBy = new List<string[]>
                {
                    new[] {"route_created_unix", "desc"}
                },
                Timezone = "UTC"
            };

            var dataObjects = route4Me.GetRouteDataTableWithElasticSearch(
                routeParameters,
                out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteDatatableWithElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] {"2021-02-01", "2021-02-01"}
                },
                OrderBy = new List<string[]>
                {
                    new[] {"route_created_unix", "desc"}
                },
                Timezone = "UTC"
            };

            var dataObjects = route4Me.GetRouteDataTableWithElasticSearch(
                routeParameters,
                out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Offset = 0,
                Limit = 10
            };

            var dataObjects = route4Me.GetRouteListWithoutElasticSearch(routeParameters, out var resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void DuplicateRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeIDs = new[] {tdr.SD10Stops_route.RouteID};

            var result = route4Me.DuplicateRoute(routeIDs, out var resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDuplicateResponse>(result);
            Assert.True(result.Status);

            if (result.RouteIDs.Length > 0)
                foreach (var routeId in result.RouteIDs)
                    lsRoutenIDs.Add(routeId);
        }

        [Fact]
        public void DeleteRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeIDs = new[] {tdr2.MDMD24_route_id};

            var result = route4Me.DeleteRoutes(routeIDs, out var resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RoutesDeleteResponse>(result);
            Assert.True(result.Deleted);
        }

        [Fact]
        public void GetRouteDataTableConfig()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.GetRouteDataTableConfig(out var resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDataTableConfigResponse>(result);
        }

        [Fact]
        public void GetRouteDataTableFallbackConfig()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.GetRouteDataTableFallbackConfig(out var resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDataTableConfigResponse>(result);
        }

        [Fact(Skip = "Will be finished after implementing Route Destinations API")]
        public void UpdateRouteTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            tdr.SD10Stops_route.Parameters.DistanceUnit = DistanceUnit.KM.Description();
            tdr.SD10Stops_route.Parameters.Parts = 2;
            tdr.SD10Stops_route.Parameters = null;
            var addresses = new List<Address>();

            tdr.SD10Stops_route.Addresses[2].SequenceNo = 4;
            tdr.SD10Stops_route.Addresses[2].Alias = "Address 2";
            tdr.SD10Stops_route.Addresses[2].AddressStopType = AddressStopType.Delivery.Description();
            addresses.Add(tdr.SD10Stops_route.Addresses[2]);

            tdr.SD10Stops_route.Addresses[3].SequenceNo = 3;
            tdr.SD10Stops_route.Addresses[3].Alias = "Address 3";
            tdr.SD10Stops_route.Addresses[3].AddressStopType = AddressStopType.PickUp.Description();
            addresses.Add(tdr.SD10Stops_route.Addresses[3]);

            tdr.SD10Stops_route.Addresses = addresses.ToArray();

            var updatedRoute = route4Me.UpdateRoute(tdr.SD10Stops_route, out var resultResponse);

            Assert.NotNull(updatedRoute);
            Assert.IsType<DataObjectRoute>(updatedRoute);
        }
    }
}