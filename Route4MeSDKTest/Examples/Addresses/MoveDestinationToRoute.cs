using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void MoveDestinationToRoute(string toRouteId, int routeDestinationId, int afterDestinationId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      bool success = route4Me.MoveDestinationToRoute(toRouteId, routeDestinationId, afterDestinationId, out errorString);

      Console.WriteLine("");

      if (success)
      {
        Console.WriteLine("MoveDestinationToRoute executed successfully");

        Console.WriteLine("Destination {0} moved to Route {1} after Destination {2}", routeDestinationId, toRouteId, afterDestinationId);
      }
      else
      {
        Console.WriteLine("MoveDestinationToRoute error: {0}", errorString);
      }

    }
  }
}
