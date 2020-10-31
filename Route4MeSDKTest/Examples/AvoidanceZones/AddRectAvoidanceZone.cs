using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Rectangular Avoidance Zone
        /// </summary>
        /// <returns> Id of added territory </returns>
        public string AddRectAvoidanceZone()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            AvoidanceZoneParameters avoidanceZoneParameters = new AvoidanceZoneParameters
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
            string errorString;
            AvoidanceZone avoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, out errorString);

            Console.WriteLine("");

            if (avoidanceZone != null)
            {
                Console.WriteLine("AddRectAvoidanceZone executed successfully");

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId);

                return avoidanceZone.TerritoryId;
            }
            else
            {
                Console.WriteLine("AddRectAvoidanceZone error: {0}", errorString);

                return null;
            }
        }
    }
}
