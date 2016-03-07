using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Update Avoidance Zone
    /// </summary>
    /// <param name="territoryId"> Avoidance Zone Id </param>
    public void UpdateAvoidanceZone(string territoryId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      AvoidanceZoneParameters avoidanceZoneParameters = new AvoidanceZoneParameters()
      {
        TerritoryId = territoryId,
        TerritoryName = "Test Territory Updated",
        TerritoryColor = "ff00ff",
        Territory = new Territory()
        {
          Type = TerritoryType.Circle.Description(),
          Data = new string[] { "38.41322259056806,-78.501953234",
                                "3000"}
        }
      };

      // Run the query
      string errorString;
      AvoidanceZone avoidanceZone = route4Me.UpdateAvoidanceZone(avoidanceZoneParameters, out errorString);

      Console.WriteLine("");

      if (avoidanceZone != null)
      {
        Console.WriteLine("UpdateAvoidanceZone executed successfully");

        Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId);
      }
      else
      {
        Console.WriteLine("UpdateAvoidanceZone error: {0}", errorString);
      }
    }
  }
}
