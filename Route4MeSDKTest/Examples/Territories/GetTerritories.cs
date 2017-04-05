using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Territories
        /// </summary>
        public void GetTerritories()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            AvoidanceZoneQuery territoryQuery = new AvoidanceZoneQuery();

            // Run the query
            string errorString = "";
            AvoidanceZone[] territories = route4Me.GetTerritories(territoryQuery, out errorString);

            Console.WriteLine("");

            if (territories != null)
            {
                Console.WriteLine("GetTerritories executed successfully");

                Console.WriteLine("GetAvoidanceZones executed successfully, {0} territories returned", territories.Length);
            }
            else
            {
                Console.WriteLine("GetTerritories error: {0}", errorString);
            }
        }
    }
}
