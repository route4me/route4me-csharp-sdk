using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public string DuplicateRoute()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      RouteParametersQuery routeParameters = new RouteParametersQuery()
      {
        RouteId = "7C0119495FBB74108F269DFA0E7FDED1"
      };

      // Run the query
      string errorString;
      string duplicatedRouteId = route4Me.DuplicateRoute(routeParameters, out errorString);

      Console.WriteLine("");

      if (duplicatedRouteId != null)
      {
        Console.WriteLine("DuplicateRoute executed successfully, duplicated route ID: {0}", duplicatedRouteId);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine("DuplicateRoute error {0}", errorString);
      }

      return duplicatedRouteId;
    }
  }
}
