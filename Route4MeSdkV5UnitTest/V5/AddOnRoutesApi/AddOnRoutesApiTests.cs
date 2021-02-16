using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Route4MeSDK;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDK.DataTypes.V5;
using Route4MeSdkV5UnitTest.V5;
using Xunit.Abstractions;

namespace Route4MeSdkV5UnitTest.AddOnRoutesApi
{
    public class AddOnRoutesApiTests : IDisposable
    {
        static string c_ApiKey = ApiKeys.actualApiKey;

        private readonly ITestOutputHelper _output;

        static TestDataRepository tdr;
        static TestDataRepository tdr2;
        static List<string> lsOptimizationIDs;
        static List<string> lsRoutenIDs;

        public AddOnRoutesApiTests(ITestOutputHelper output)
        {
            _output = output;

            lsOptimizationIDs = new List<string>();
            lsRoutenIDs = new List<string>();

            tdr = new TestDataRepository();
            tdr2 = new TestDataRepository();

            bool result = tdr.RunOptimizationSingleDriverRoute10Stops();
            bool result2 = tdr2.RunOptimizationSingleDriverRoute10Stops();
            bool result3 = tdr2.RunSingleDriverRoundTrip();
            bool result4 = tdr2.MultipleDepotMultipleDriverWith24StopsTimeWindowTest();

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
            bool optimizationResult = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsRoutenIDs.Count>0)
            {
                RoutesDeleteResponse routesDeleteResponse = route4Me.DeleteRoutes(
                    lsRoutenIDs.ToArray(), 
                    out ResultResponse resultResponse);
            }

            Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Fact]
        public void GetAllRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery()
            {
                Limit = 1,
                Offset = 15
            };

            // Run the query
            DataObjectRoute[] dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetAllRoutesWithPaginationTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery()
            {
                Page = 1,
                PerPage = 20
            };

            DataObjectRoute[] dataObjects = route4Me.GetAllRoutesWithPagination(routeParameters, out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetPaginatedRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery()
            {
                Page = 1,
                PerPage = 20
            };

            DataObjectRoute[] dataObjects = route4Me.GetPaginatedRouteListWithoutElasticSearch(routeParameters, out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteDataTableWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteFilterParameters()
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters()
                {
                    ScheduleDate = new string[] { "2021-02-01", "2021-02-02" }
                },
                OrderBy = new List<string[]>()
                {
                    new string[] { "route_created_unix", "desc" }
                },
                Timezone = "UTC"
            };

            DataObjectRoute[] dataObjects = route4Me.GetRouteDataTableWithoutElasticSearch(
                routeParameters,
                out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteDatatableWithElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteFilterParameters()
            {
                Page = 1,
                PerPage = 10,
                Filters = new RouteFilterParametersFilters()
                {
                    ScheduleDate = new string[] { "2021-02-11", "2021-02-12" }
                },
                OrderBy = new List<string[]>()
                {
                    new string[] { "route_created_unix", "asc" }
                },
                Timezone = "UTC"
            };

            DataObjectRoute[] dataObjects = route4Me.GetRouteDataTableWithElasticSearch(
                routeParameters,
                out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void GetRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeParameters = new RouteParametersQuery()
            {
                Offset = 0,
                Limit = 10
            };

            DataObjectRoute[] dataObjects = route4Me.GetRouteListWithoutElasticSearch(routeParameters, out ResultResponse resultResponse);

            Assert.IsType<DataObjectRoute[]>(dataObjects);
        }

        [Fact]
        public void DuplicateRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeIDs = new string[] { tdr.SD10Stops_route.RouteID };

            var result = route4Me.DuplicateRoute(routeIDs, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDuplicateResponse>(result);
            Assert.True(result.Status);

            if (result.RouteIDs.Length>0)
            {
                foreach (string routeId in result.RouteIDs) lsRoutenIDs.Add(routeId);
            }
        }

        [Fact]
        public void DeleteRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var routeIDs =  new string[] { tdr2.MDMD24_route_id };

            var result = route4Me.DeleteRoutes(routeIDs, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RoutesDeleteResponse>(result);
            Assert.True(result.Deleted);
        }

        [Fact]
        public void GetRouteDataTableConfig()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.GetRouteDataTableConfig(out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDataTableConfigResponse>(result);
        }

        [Fact]
        public void GetRouteDataTableFallbackConfig()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.GetRouteDataTableFallbackConfig(out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<RouteDataTableConfigResponse>(result);
        }

        [Fact (Skip = "Will be finished after implementing Route Destinations API")]
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

            var updatedRoute = route4Me.UpdateRoute(tdr.SD10Stops_route, out ResultResponse resultResponse);

            Assert.NotNull(updatedRoute);
            Assert.IsType<DataObjectRoute>(updatedRoute);
        }
    }
}
