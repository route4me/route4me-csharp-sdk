using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Departed
        /// </summary>
        /// <param name="aParams">Address parameters</param>
        public void MarkAddressDeparted(AddressParameters aParams = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            if (aParams == null)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                #region Before marking an address as departed, mark it as visited.
                var visitedParams = new AddressParameters
                {
                    RouteId = SD10Stops_route_id,
                    AddressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId,
                    IsVisited = true
                };

                object oVisited = route4Me.MarkAddressVisited(visitedParams, out string errorString0);

                bool visited = int.TryParse(oVisited.ToString(), out _)
                    ? (Convert.ToInt32(oVisited) > 0 ? true : false)
                    : false;

                if (!visited)
                {
                    Console.WriteLine("Cannot mark the address as visited");
                    return;
                }

                #endregion

                aParams = new AddressParameters
                {
                    RouteId = SD10Stops_route_id,
                    AddressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId,
                    IsDeparted = true
                };
            }

            // Run the query
            int result = route4Me.MarkAddressDeparted(aParams, out string errorString);

            bool departed = result > 0 ? true : false;

            PrintExampleDestination(departed, errorString);

            if (aParams == null) RemoveTestOptimizations();
        }
    }
}
