using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Get Avoidance Zone
    /// </summary>
    /// <param name="territoryId"> Avoidance Zone Id </param>
    public void GetAvoidanceZone(string territoryId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      AvoidanceZoneQuerry avoidanceZoneQuerry = new AvoidanceZoneQuerry()
      {
        TerritoryId = territoryId
      };

      // Run the query
      string errorString;
      AvoidanceZone avoidanceZone = route4Me.GetAvoidanceZone(avoidanceZoneQuerry, out errorString);

      Console.WriteLine("");

      if (avoidanceZone != null)
      {
        Console.WriteLine("GetAvoidanceZone executed successfully");

        Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId);
      }
      else
      {
        Console.WriteLine("GetAvoidanceZone error: {0}", errorString);
      }
    }
  }
}
