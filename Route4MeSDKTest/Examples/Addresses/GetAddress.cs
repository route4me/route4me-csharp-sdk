using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetAddress(string routeId, int routeDestinationId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      AddressParameters addressParameters = new AddressParameters()
      {
        RouteId = routeId,
        RouteDestinationId = routeDestinationId,
        Notes = true
      };

      // Run the query
      string errorString;
      Address dataObject = route4Me.GetAddress(addressParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("GetAddress executed successfully");
        Console.WriteLine("RouteId: {0}; RouteDestinationId: {1}", dataObject.RouteId, dataObject.RouteDestinationId);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine("GetAddress error: {0}", errorString);
        Console.WriteLine("");
      }
    }
  }
}
