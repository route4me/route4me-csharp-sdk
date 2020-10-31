using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Get Avoidance Zone list
    /// </summary>
    public void GetAvoidanceZones()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      AvoidanceZoneQuery avoidanceZoneQuery = new AvoidanceZoneQuery()
      {
        
      };

      // Run the query
      string errorString;
      AvoidanceZone[] avoidanceZones = route4Me.GetAvoidanceZones(avoidanceZoneQuery, out errorString);

      Console.WriteLine("");

      if (avoidanceZones != null)
      {
        Console.WriteLine("GetAvoidanceZones executed successfully, {0} zones returned", avoidanceZones.Length);
      }
      else
      {
        Console.WriteLine("GetAvoidanceZones error: {0}", errorString);
      }
    }
  }
}
