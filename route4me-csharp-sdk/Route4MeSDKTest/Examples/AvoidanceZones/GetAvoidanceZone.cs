using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Avoidance Zone
        /// </summary>
        /// <param name="territoryId"> Avoidance Zone Id </param>
        public void GetAvoidanceZone(string territoryId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = territoryId == null ? true : false;

            if (isInnerExample)
            {
                CreateAvoidanceZone();
                territoryId = this.avoidanceZone.TerritoryId;
            }

            var avoidanceZoneQuery = new AvoidanceZoneQuery()
            {
                TerritoryId = territoryId
            };

            // Run the query
            AvoidanceZone avoidanceZone = route4Me.GetAvoidanceZone(
                avoidanceZoneQuery,
                out string errorString);

            PrintExampleAvoidanceZone(avoidanceZone, errorString);

            if (isInnerExample) RemoveAvidanceZone(territoryId);
        }
    }
}
