using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Avoidance Zone list
        /// </summary>
        public void GetAvoidanceZones()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var avoidanceZoneQuery = new AvoidanceZoneQuery()
            { };

            // Run the query
            AvoidanceZone[] avoidanceZones = route4Me.GetAvoidanceZones(
                avoidanceZoneQuery,
                out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                avoidanceZones != null
                ? String.Format("GetAvoidanceZones executed successfully, {0} zones returned", avoidanceZones.Length)
                : String.Format("GetAvoidanceZones error: {0}", errorString)
                );
        }
    }
}
