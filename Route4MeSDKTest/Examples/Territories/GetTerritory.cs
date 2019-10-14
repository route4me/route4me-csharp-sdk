using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Territory
        /// </summary>
        public void GetTerritory()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            string territoryId = "596A2A44FE9FB19EEB9C3C072BF2D0BE";

            TerritoryQuery territoryQuery1 = new TerritoryQuery
            {
                TerritoryId = territoryId,
                Addresses = 1
            };

            // Run the query
            string errorString = "";
            TerritoryZone territory = route4Me.GetTerritory(territoryQuery1, out errorString);

            Console.WriteLine("");

            if (territory != null)
            {
                Console.WriteLine("GetTerritory executed successfully");

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId);
            }
            else
            {
                Console.WriteLine("GetTerritory error: {0}", errorString);
            }
        }
    }
}
