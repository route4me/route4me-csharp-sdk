using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void RemoveDestinationFromOptimization(string optimizationId, int destinationId, bool andReOptimize)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      // Run the query
      string errorString;
      bool removed = route4Me.RemoveDestinationFromOptimization(optimizationId, destinationId, out errorString);

      Console.WriteLine("");

      if (removed)
      {
        Console.WriteLine("RemoveAddressFromOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}, Destination ID: {1}", optimizationId, destinationId);
      }
      else
      {
        Console.WriteLine("RemoveAddressFromOptimization error: {0}", errorString);
      }
    }
  }
}
