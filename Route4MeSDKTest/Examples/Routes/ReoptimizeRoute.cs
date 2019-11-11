using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  /// <summary>
  /// Update Route Parameters
  /// </summary>
  public sealed partial class Route4MeExamples
  {
    public void ReoptimizeRoute(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      RouteParametersQuery routeParameters = new RouteParametersQuery()
      {
        RouteId = routeId,
        ReOptimize = true
      };

      // Run the query
      string errorString;
      DataObjectRoute dataObject = route4Me.UpdateRoute(routeParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("ReoptimizeRoute executed successfully");

        Console.WriteLine("Route ID: {0}", dataObject.RouteID);
      }
      else
      {
        Console.WriteLine("ReoptimizeRoute error: {0}", errorString);
      }
    }
  }
}

