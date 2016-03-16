using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void DeleteRoutes(string[] routeIds)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      //routeIds = new string[] { "1" };

      // Run the query
      string errorString;
      string[] deletedRouteIds = route4Me.DeleteRoutes(routeIds, out errorString);

      Console.WriteLine("");

      if (deletedRouteIds != null)
      {
        Console.WriteLine("DeleteRoutes executed successfully, {0} routes deleted", deletedRouteIds.Length);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine("DeleteRoutes error {0}", errorString);
      }
    }
  }
}
