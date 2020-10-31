using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void RemoveRouteDestination(string routeId, int destinationId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      // Run the query
      string errorString;
      bool deleted = route4Me.RemoveRouteDestination(routeId, destinationId, out errorString);

      Console.WriteLine("");

      if (deleted)
      {
        Console.WriteLine("RemoveRouteDestination executed successfully");

        Console.WriteLine("Destination ID: {0}", destinationId);
      }
      else
      {
        Console.WriteLine("RemoveRouteDestination error: {0}", errorString);
      }

    }
  }
}
