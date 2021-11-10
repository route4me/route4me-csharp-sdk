using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Merge two routes in one.
        /// </summary>
        public void MergeRoutes()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            RunSingleDriverRoundTrip();
            OptimizationsToRemove.Add(SDRT_optimization_problem_id);

            var mergeRoutesParameters = new MergeRoutesQuery()
            {
                RouteIds = SD10Stops_route_id + "," + SDRT_route_id,
                DepotAddress = SDRT_route.Addresses[0].AddressString,
                RemoveOrigin = false,
                DepotLat = SDRT_route.Addresses[0].Latitude,
                DepotLng = SDRT_route.Addresses[0].Longitude,
                ToRouteId = SDRT_route_id,
                RouteDestinationId = SDRT_route.Addresses[1].RouteDestinationId.ToString()
            };

            // Run the query
            bool result = route4Me.MergeRoutes(
                mergeRoutesParameters,
                out string errorString);

            Console.WriteLine(
                result
                ? String.Format(
                    "MergeRoutes executed successfully, {0} routes merged",
                    mergeRoutesParameters.RouteIds
                  )
                : String.Format(
                    "MergeRoutes error {0}",
                    errorString
                  )
             );

            RemoveTestOptimizations();
        }
    }
}
