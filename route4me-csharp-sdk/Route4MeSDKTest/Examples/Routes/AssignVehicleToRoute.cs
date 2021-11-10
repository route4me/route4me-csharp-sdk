using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Assign a vehicle to a route.
        /// </summary>
        public void AssignVehicleToRoute()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Get Random Vehicle

            var vehicleParameters = new VehicleParameters
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            var vehicles = route4Me.GetVehicles(
                vehicleParameters,
                out string errorString
             );

            int randomNumber = (new Random()).Next(0, vehicles.Length - 1);
            var vehicleId = vehicles[randomNumber].VehicleId;

            #endregion

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            string routeId = SD10Stops_route_id;

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = new RouteParameters()
                {
                    VehicleId = vehicleId
                }
            };

            var route = route4Me.UpdateRoute(routeParameters, out errorString);

            PrintExampleRouteResult(route, errorString);

            RemoveTestOptimizations();
        }
    }
}
