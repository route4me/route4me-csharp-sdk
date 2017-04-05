using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void RemoveOptimization(string optimizationProblemID)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      bool removed = route4Me.RemoveOptimization(optimizationProblemID, out errorString);

      Console.WriteLine("");

      if (removed)
      {
        Console.WriteLine("RemoveOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}", optimizationProblemID);
      }
      else
      {
        Console.WriteLine("RemoveOptimization error: {0}", errorString);
      }
    }
  }
}
