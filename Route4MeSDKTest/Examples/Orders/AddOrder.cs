using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Add Order
    /// </summary>
    /// <returns> Added Order </returns>
    public Order AddOrder()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      Order order = new Order()
      {
        Address1 = "Test Address1 " + (new Random()).Next().ToString(),
        AddressAlias = "Test AddressAlias " + (new Random()).Next().ToString(),
        CachedLatitude = 37.773972,
        CachedLongitude = -122.431297
      };

      // Run the query
      string errorString;
      Order resultOrder = route4Me.AddOrder(order, out errorString);

      Console.WriteLine("");

      if (resultOrder != null)
      {
        Console.WriteLine("AddOrder executed successfully");

        Console.WriteLine("Order ID: {0}", resultOrder.OrderId);

        return resultOrder;
      }
      else
      {
        Console.WriteLine("AddOrder error: {0}", errorString);

        return null;
      }
    }
  }
}

