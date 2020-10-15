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
    public void UpdateRoute(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      var parametersNew = new RouteParameters()
      {
        RouteName = "New name of the route"
      };

      var routeParameters = new RouteParametersQuery()
      {
        RouteId = routeId,
        Parameters = parametersNew
      };

      // Run the query
      DataObjectRoute dataObject = route4Me.UpdateRoute(routeParameters, out string errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("UpdateRoute executed successfully");

        Console.WriteLine("Route ID: {0}", dataObject.RouteID);
      }
      else
      {
        Console.WriteLine("UpdateRoute error: {0}", errorString);
      }
    }
  }
}

