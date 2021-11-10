using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Visited
        /// </summary>
        /// <param name="aParams">Address parameters</param>
        public void MarkAddressVisited(AddressParameters aParams = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = aParams == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                aParams = new AddressParameters
                {
                    RouteId = SD10Stops_route_id,
                    AddressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId,
                    IsVisited = true
                };
            }

            // Run the query
            object oResult = route4Me.MarkAddressVisited(aParams, out string errorString);

            bool marked = int.TryParse(oResult.ToString(), out _)
                ? (Convert.ToInt32(oResult) > 0 ? true : false)
                : false;

            PrintExampleDestination(marked, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
