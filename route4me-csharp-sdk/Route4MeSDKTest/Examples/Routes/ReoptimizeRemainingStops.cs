using System;
using System.Collections.Generic;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void ReoptimizeRemainingStops()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region == Prepare Test Data ==

            RunSingleDriverRoundTrip();
            OptimizationsToRemove = new List<string>()
            {
                SDRT_optimization_problem_id
            };

            var route = SDRT_route;

            #endregion

            #region == Visit Second Address ==

            var visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[1].RouteDestinationId,
                IsVisited = true
            };

            int result = route4Me.MarkAddressVisited(visitedParams, out string errorString);

            if (result == 1)
            {
                Console.WriteLine("The address " + visitedParams.AddressId + " visited");
            }
            else
            {
                Console.WriteLine("Cannot visit the address " + visitedParams.AddressId);
                return;
            }

            #endregion

            #region == Visit Third Address ==

            visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[2].RouteDestinationId,
                IsVisited = true
            };

            result = route4Me.MarkAddressVisited(visitedParams, out errorString);

            if (result == 1)
            {
                Console.WriteLine("The address " + visitedParams.AddressId + " visited");
            }
            else
            {
                Console.WriteLine("Cannot visit the address " + visitedParams.AddressId);
                return;
            }

            #endregion

            #region == Reoptimize Remaining Addresses ==

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = route.RouteId,
                ReOptimize = true,
                Remaining = true
            };

            var updatedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            PrintExampleRouteResult(updatedRoute, errorString);

            #endregion

            RemoveTestOptimizations();
        }
    }
}
