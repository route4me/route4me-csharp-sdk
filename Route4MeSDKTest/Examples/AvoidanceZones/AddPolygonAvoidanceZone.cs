using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Polygon Avoidance Zone
        /// </summary>
        /// <returns> Id of added territory </returns>
        public string AddPolygonAvoidanceZone()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            AvoidanceZoneParameters avoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Poly.Description(),
                    Data = new string[] {
			            "37.569752822786455,-77.47833251953125",
			            "37.75886716305343,-77.68974800109863",
			            "37.74763966054455,-77.6917221069336",
			            "37.74655084306813,-77.68863220214844",
			            "37.7502255383101,-77.68125076293945",
			            "37.74797991274437,-77.67498512268066",
			            "37.73327960206065,-77.6411678314209",
			            "37.74430510679532,-77.63172645568848",
			            "37.76641925847049,-77.66846199035645"
		            }
                }
            };

            // Run the query
            string errorString;
            AvoidanceZone avoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, out errorString);

            Console.WriteLine("");

            if (avoidanceZone != null)
            {
                Console.WriteLine("AddPolygonAvoidanceZone executed successfully");

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId);

                return avoidanceZone.TerritoryId;
            }
            else
            {
                Console.WriteLine("AddPolygonAvoidanceZone error: {0}", errorString);

                return null;
            }
        }
    }
}
