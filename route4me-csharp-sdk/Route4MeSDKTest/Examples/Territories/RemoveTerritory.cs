using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove Territory
        /// </summary>
        public void RemoveTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTerritoryZone();

            string territoryId = TerritoryZonesToRemove[TerritoryZonesToRemove.Count - 1];

            var territoryQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            bool removed = route4Me.RemoveTerritory(territoryQuery, out string errorString);

            Console.WriteLine(
                removed
                ? String.Format("The territory {0} removed successfully", territoryId)
                : String.Format("Cannot remove the territory {0}", territoryId) + Environment.NewLine + errorString
            );
        }
    }
}
