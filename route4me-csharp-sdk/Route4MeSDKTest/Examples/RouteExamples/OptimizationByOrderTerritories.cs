using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OptimizationByOrderTerritories()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = "Optimization by order territories, " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                is_dynamic_start_time = false,
                Optimize = "Time",
                IgnoreTw = false,
                Parts = 10,
                RT = false,
                LockLast = false,
                DisableOptimization = false,
                VehicleId = ""
            };

            var depots = new Address[1]
            {
                new Address()
                {
                    Alias = "HQ1",
                    AddressString = "1010 N Florida ave, Tampa, FL",
                    IsDepot = true,
                    Latitude = 27.952941,
                    Longitude = -82.459493,
                    Time = 0
                }
            };

            var orderTerritories = new OrderTerritories()
            {
                SplitTerritories = true,

                // The territory IDs are taken from the test account- to run the test on your PC, put your territory IDs.
                TerritoriesId = new string[] { "5E66A5AFAB087B08E690DFAE4F8B151B", "6160CFC4CC3CD508409D238E04D6F6C4" },
                filters = new FilterDetails()
                {
                    // Specified as 'all' for test purpose - after the first optimization, the orders become routed and the test is failing.
                    // For real tasks should be specified as 'unrouted'
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new string[] { "2021-09-21" }
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Redirect = false,
                OrderTerritories = orderTerritories,
                Parameters = parameters,
                Depots = depots
            };

            // Run the query
            var dataObjects = route4Me.RunOptimizationByOrderTerritories(optimizationParameters, out string errorString);

            if ((dataObjects?.Length ?? 0)>0)
            {
                OptimizationsToRemove = new List<string>();

                foreach (var dataObject in dataObjects)
                {
                    if (dataObject.OptimizationProblemId != null) OptimizationsToRemove.Add(dataObject.OptimizationProblemId);
                    Console.WriteLine($"Optimization Problem ID: {dataObject.OptimizationProblemId}");
                }
            }
            else
            {
                Console.WriteLine($"Optimization failed. {errorString}");
            }

            RemoveTestOptimizations();
        }
    }

}
