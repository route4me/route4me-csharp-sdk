using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an optimization destination
        /// </summary>
        public void UpdateOptimizationDestination()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>();
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id);

            var address = SD10Stops_route.Addresses[3];

            address.FirstName = "UpdatedFirstName";
            address.LastName = "UpdatedLastName";

            var updatedAddress = route4Me.UpdateOptimizationDestination(
                address,
                out string errorString);

            PrintExampleDestination(updatedAddress, errorString);

            RemoveTestOptimizations();
        }
    }
}
