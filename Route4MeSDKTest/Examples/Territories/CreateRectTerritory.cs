using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            AvoidanceZoneParameters territoryParameters = new AvoidanceZoneParameters
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
            string errorString = "";
            AvoidanceZone territory = route4Me.CreateTerritory(territoryParameters, out errorString);

            Console.WriteLine("");

            if (territory != null)
            {
                Console.WriteLine("CreateRectTerritory executed successfully");
                Console.WriteLine("Territory ID: {0}", territory.TerritoryId);
            }
            else
            {
                Console.WriteLine("CreateRectTerritory error: {0}", errorString);
            }
        }
    }
}
