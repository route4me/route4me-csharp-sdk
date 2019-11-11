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

      RouteParameters parametersNew = new RouteParameters()
      {
        RouteName = "New name of the route"
      };

      RouteParametersQuery routeParameters = new RouteParametersQuery()
      {
        RouteId = routeId,
        Parameters = parametersNew
      };

      // Run the query
      string errorString;
      DataObjectRoute dataObject = route4Me.UpdateRoute(routeParameters, out errorString);

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

