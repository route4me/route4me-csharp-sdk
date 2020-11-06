using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Delete Avoidance Zone
        /// </summary>
        /// <param name="territoryId"> Avoidance Zone Id </param>
        public void DeleteAvoidanceZone(string territoryId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = territoryId == null ? true : false;

            if (isInnerExample)
            {
                CreateAvoidanceZone();
                territoryId = avoidanceZone.TerritoryId;
            }

            var avoidanceZoneQuery = new AvoidanceZoneQuery()
            {
                TerritoryId = territoryId
            };

            // Run the query
            route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, out string errorString);

            PrintExampleAvoidanceZone(territoryId, errorString);
        }
    }
}
