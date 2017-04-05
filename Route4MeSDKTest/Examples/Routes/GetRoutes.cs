using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetRoutes()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      RouteParametersQuery routeParameters = new RouteParametersQuery()
      {
        Limit = 10,
        Offset = 5
      };

      // Run the query
      string errorString;
      DataObjectRoute[] dataObjects = route4Me.GetRoutes(routeParameters, out errorString);

      Console.WriteLine("");

      if (dataObjects != null)
      {
        Console.WriteLine("GetRoutes executed successfully, {0} routes returned", dataObjects.Length);
        Console.WriteLine("");

        dataObjects.ForEach(dataObject =>
        {
          Console.WriteLine("RouteID: {0}", dataObject.RouteID);
          Console.WriteLine("");
        });
      }
      else
      {
        Console.WriteLine("GetRoutes error {0}", errorString);
      }
    }
  }
}
