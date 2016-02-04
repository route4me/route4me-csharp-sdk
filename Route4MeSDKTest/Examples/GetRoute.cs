using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetRoute(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      RouteParametersQuery routeParameters = new RouteParametersQuery()
      {
        RouteId = routeId
      };

      // Run the query
      string errorString;
      DataObjectRoute dataObject = route4Me.GetRoute(routeParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("GetRoute executed successfully");

        Console.WriteLine("Route ID: {0}", dataObject.RouteID);
        Console.WriteLine("State: {0}", dataObject.State);
        /*foreach (Address a in dataObject.Addresses)
        {
          Console.WriteLine("addr: {0}, {1}, {2}, {3}, {4}", a.RouteDestinationId, a.Latitude, a.Longitude, a.Alias, a.AddressString);
        }*/
      }
      else
      {
        Console.WriteLine("GetRoute error: {0}", errorString);
      }
    }
  }
}

