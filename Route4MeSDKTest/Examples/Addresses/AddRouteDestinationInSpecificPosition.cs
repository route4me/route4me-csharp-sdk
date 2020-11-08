using System.Collections.Generic;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add route destination in specific position
        /// </summary>
        public void AddRouteDestinationInSpecificPosition()
        {
            var route4Me = new Route4MeManager(this.ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            string route_id = SD10Stops_route_id;

            // Prepare the addresses
            #region Addresses
            Address[] addresses = new Address[]
            {
                new Address() { AddressString =  "146 Bill Johnson Rd NE Milledgeville GA 31061",
                                Latitude =  33.143526,
                                Longitude = -83.240354,
                                SequenceNo = 3,
                                Time = 0 }
            };
            #endregion

            // Run the query
            bool optimalPosition = false;
            int[] destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out string errorString);

            PrintExampleDestination(destinationIds, errorString);

            RemoveTestOptimizations();
        }
    }
}
