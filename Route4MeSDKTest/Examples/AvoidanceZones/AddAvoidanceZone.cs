using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Add Avoidance Zone
    /// </summary>
    /// <returns> Id of added territory </returns>
    public string AddAvoidanceZone()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      AvoidanceZoneParameters avoidanceZoneParameters = new AvoidanceZoneParameters()
      {
        TerritoryName = "Test Territory",
        TerritoryColor = "ff0000",
        Territory = new Territory()
        {
          Type = TerritoryType.Circle.Description(),
          Data = new string[] { "37.569752822786455,-77.47833251953125",
                                "5000"}
        }
      };

      // Run the query
      string errorString;
      AvoidanceZone avoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, out errorString);

      Console.WriteLine("");

      if (avoidanceZone != null)
      {
        Console.WriteLine("AddAvoidanceZone executed successfully");

        Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId);

        return avoidanceZone.TerritoryId;
      }
      else
      {
        Console.WriteLine("AddAvoidanceZone error: {0}", errorString);

        return null;
      }
    }
  }
}
