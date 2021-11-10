using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add a destination to an optimization.
        /// </summary>
        public void AddDestinationToOptimization()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            // Prepare the address that we are going to add to an existing route optimization
            Address[] addresses = new Address[]
            {
                new Address() { AddressString = "717 5th Ave New York, NY 10021",
                                Alias         = "Giorgio Armani",
                                Latitude      = 40.7669692,
                                Longitude     = -73.9693864,
                                Time          = 0
                }
            };

            //Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = SD10Stops_optimization_problem_id,
                Addresses = addresses,
                ReOptimize = true
            };

            // Execute the optimization to re-optimize and rebalance all the routes in this optimization
            DataObject dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out string errorString);

            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
