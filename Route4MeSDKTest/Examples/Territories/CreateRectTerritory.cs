using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create Territory with Rectangular Shape
        /// </summary>
        public void CreateRectTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Rect.Description(),
                    Data = new string[] {
                        "43.51668853502909,-109.3798828125",
                        "46.98025235521883,-101.865234375"
                    }
                }
            };

            // Run the query
            TerritoryZone territory = route4Me.CreateTerritory(territoryParameters,
                                                               out string errorString);

            if ((territory?.TerritoryId ?? null) != null) TerritoryZonesToRemove.Add(territory.TerritoryId);

            PrintExampleTerritory(territory, errorString);

            RemoveTestTerritoryZones();
        }
    }
}
