using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update route avoidance zones.
        /// </summary>
        public void UpdateRouteAvoidanceZones()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            #region Get Avoidance Zones

            CreateAvoidanceZone();
            AvoidanceZonesToRemove = new List<string>()
            {
                avoidanceZone.TerritoryId
            };

            CreateAvoidanceZone();
            AvoidanceZonesToRemove.Add(avoidanceZone.TerritoryId);

            #endregion

            var parameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                Parameters = new RouteParameters()
                {
                    AvoidanceZones = new string[]
                    {
                        AvoidanceZonesToRemove[0],
                        AvoidanceZonesToRemove[1]
                    }
                }
            };

            var result = route4Me.UpdateRoute(parameters, out string errorString);

            PrintExampleRouteResult(result, errorString);

            RemoveTestOptimizations();
            RemoveAvoidanceZones();
        }
    }
}
