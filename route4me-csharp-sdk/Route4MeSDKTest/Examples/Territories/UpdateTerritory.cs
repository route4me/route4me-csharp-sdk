using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update Territory
        /// </summary>
        public void UpdateTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTerritoryZone();

            string territoryId = TerritoryZonesToRemove[TerritoryZonesToRemove.Count - 1];

            var territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] {
                        "37.569752822786455,-77.47833251953125",
                        "6000"
                    }
                }
            };

            // Run the query
            AvoidanceZone territory = route4Me.UpdateTerritory(territoryParameters,
                                                               out string errorString);

            PrintExampleTerritory(territory, errorString);

            RemoveTestTerritoryZones();
        }
    }
}
