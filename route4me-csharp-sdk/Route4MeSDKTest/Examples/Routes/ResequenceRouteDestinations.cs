using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Resequence route destinations.
        /// </summary>
        public void ResequenceRouteDestinations()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var route = SD10Stops_route;

            var rParams = new RouteParametersQuery()
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
                out string errorString
            );

            PrintExampleRouteResult(route1, errorString);

            RemoveTestOptimizations();
        }
    }
}
