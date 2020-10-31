using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update Territory
        /// </summary>
        public void UpdateTerritory()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            string territoryId = "1CEFEC7568D900FB781C21603780775E";

            AvoidanceZoneParameters territoryParameters = new AvoidanceZoneParameters
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] {
			            "37.569752822786455,-77.47833251953125",
			            "6000"
		            }
                }
            };

            // Run the query
            string errorString = "";
            AvoidanceZone territory = route4Me.UpdateTerritory(territoryParameters, out errorString);

            Console.WriteLine("");

            if (territory != null)
            {
                Console.WriteLine("UpdateTerritory executed successfully");
                Console.WriteLine("Territory ID: {0}", territory.TerritoryId);
            }
            else
            {
                Console.WriteLine("UpdateTerritory error: {0}", errorString);
            }
        }
    }
}
