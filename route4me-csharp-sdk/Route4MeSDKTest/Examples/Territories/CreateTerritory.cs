using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create Territory with Circular Shape
        /// </summary>
        public void CreateTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            // Run the query
            TerritoryZone territory = route4Me.CreateTerritory(territoryParameters,
                                                               out string errorString);

            if ((territory?.TerritoryId ?? null) != null)
                TerritoryZonesToRemove.Add(territory.TerritoryId);

            PrintExampleTerritory(territory, errorString);

            RemoveTestTerritoryZones();
        }
    }
}
