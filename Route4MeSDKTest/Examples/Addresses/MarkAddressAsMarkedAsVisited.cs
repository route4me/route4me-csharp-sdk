using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Marked as Visited
        /// </summary>
        /// <param name="aParams">Address parameters</param>
        public void MarkAddressAsMarkedAsVisited(AddressParameters aParams = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            if (aParams == null)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                aParams = new AddressParameters
                {
                    RouteId = SD10Stops_route_id,
                    RouteDestinationId = (int)SD10Stops_route.Addresses[2].RouteDestinationId,
                    IsVisited = true
                };
            }

            // Run the query
            Address resultAddress = route4Me
                .MarkAddressAsMarkedAsVisited(aParams, out string errorString);

            PrintExampleDestination(resultAddress, errorString);

            if (aParams == null) RemoveTestOptimizations();
        }
    }
}
