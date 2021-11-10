using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Avoidance Zone
        /// </summary>
        /// <returns> Id of added territory </returns>
        public string AddAvoidanceZone(bool removeAvoidanceZone = true)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var avoidanceZoneParameters = new AvoidanceZoneParameters()
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory()
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] { "37.569752822786455,-77.47833251953125",
                                "5000"}
                }
            };

            // Run the query
            AvoidanceZone avoidanceZone = route4Me.AddAvoidanceZone(
                avoidanceZoneParameters,
                out string errorString);

            PrintExampleAvoidanceZone(avoidanceZone, errorString);

            string avZoneId = avoidanceZone != null && avoidanceZone.GetType() == typeof(AvoidanceZone)
                ? avoidanceZone.TerritoryId
                : null;

            if (removeAvoidanceZone) RemoveAvidanceZone(avZoneId);

            return removeAvoidanceZone ? null : avZoneId;
        }
    }
}
