using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update Avoidance Zone
        /// </summary>
        /// <param name="territoryId"> Avoidance Zone Id </param>
        public void UpdateAvoidanceZone(string territoryId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = territoryId == null ? true : false;

            if (isInnerExample)
            {
                CreateAvoidanceZone();
                territoryId = this.avoidanceZone.TerritoryId;
            }

            var avoidanceZoneParameters = new AvoidanceZoneParameters()
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff00ff",
                Territory = new Territory()
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] { "38.41322259056806,-78.501953234",
                                "3000"}
                }
            };

            // Run the query
            AvoidanceZone avoidanceZone = route4Me.UpdateAvoidanceZone(
                avoidanceZoneParameters,
                out string errorString);

            PrintExampleAvoidanceZone(avoidanceZone, errorString);

            if (isInnerExample) RemoveAvidanceZone(territoryId);
        }
    }
}
