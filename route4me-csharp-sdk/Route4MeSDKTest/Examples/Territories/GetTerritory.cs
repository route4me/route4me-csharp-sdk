using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Territory
        /// </summary>
        public void GetTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTerritoryZone();

            string territoryId = TerritoryZonesToRemove[TerritoryZonesToRemove.Count - 1];

            var territoryQuery = new TerritoryQuery
            {
                TerritoryId = territoryId,
                Addresses = 1
            };

            // Run the query
            TerritoryZone territory = route4Me.GetTerritory(territoryQuery,
                                                            out string errorString);

            PrintExampleTerritory(territory, errorString);

            RemoveTestTerritoryZones();
        }
    }
}
