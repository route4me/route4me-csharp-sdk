using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Delete Avoidance Zone
    /// </summary>
    /// <param name="territoryId"> Avoidance Zone Id </param>
    public void DeleteAvoidanceZone(string territoryId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      AvoidanceZoneQuery avoidanceZoneQuery = new AvoidanceZoneQuery()
      {
        TerritoryId = territoryId
      };

      // Run the query
      string errorString;
      route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, out errorString);

      Console.WriteLine("");

      if (errorString == "")
      {
        Console.WriteLine("DeleteAvoidanceZone executed successfully");

        Console.WriteLine("Territory ID: {0}", territoryId);
      }
      else
      {
        Console.WriteLine("DeleteAvoidanceZone error: {0}", errorString);
      }
    }
  }
}
