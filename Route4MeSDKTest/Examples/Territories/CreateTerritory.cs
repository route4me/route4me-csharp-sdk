using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create Territory with Circular Shape
        /// </summary>
        public void CreateTerritory()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            AvoidanceZoneParameters territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] {
			            "37.569752822786455,-77.47833251953125",
			            "5000"
		            }
                }
            };

            // Run the query
            string errorString = "";
            AvoidanceZone territory = route4Me.CreateTerritory(territoryParameters, out errorString);

            Console.WriteLine("");

            if (territory != null)
            {
                Console.WriteLine("CreateTerritory executed successfully");

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId);
            }
            else
            {
                Console.WriteLine("CreateTerritory error: {0}", errorString);
            }
        }
    }
}
