using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Update Order
    /// </summary>
    /// <param name="order"> Order with updated attributes </param>
    public void UpdateOrder(Order order)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      Order updatedOrder = route4Me.UpdateOrder(order, out errorString);

      Console.WriteLine("");

      if (updatedOrder != null)
      {
        Console.WriteLine("UpdateOrder executed successfully");
      }
      else
      {
        Console.WriteLine("UpdateOrder error: {0}", errorString);
      }
    }
  }
}
