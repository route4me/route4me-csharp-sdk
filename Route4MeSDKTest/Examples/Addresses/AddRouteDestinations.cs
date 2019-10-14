using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public int[] AddRouteDestinations(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Prepare the addresses
      Address[] addresses = new Address[]
      {
        #region Addresses

        new Address() { AddressString =  "146 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 	33.143526,
                        Longitude = -83.240354,
                        Time = 0 },

        new Address() { AddressString =  "222 Blake Cir Milledgeville GA 31061",
                        Latitude = 	33.177852,
                        Longitude = -83.263535,
                        Time = 0 }

        #endregion
      };

      // Run the query
      bool optimalPosition = true;
      string errorString;
      int[] destinationIds = route4Me.AddRouteDestinations(routeId, addresses, optimalPosition, out errorString);

      Console.WriteLine("");

      if (destinationIds != null)
      {
        Console.WriteLine("AddRouteDestinations executed successfully");

        Console.WriteLine("Destination IDs: {0}", string.Join(" ", destinationIds));
      }
      else
      {
        Console.WriteLine("AddRouteDestinations error: {0}", errorString);
      }

      return destinationIds;

    }
  }
}
