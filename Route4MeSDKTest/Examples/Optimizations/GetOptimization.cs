using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetOptimization(string optimizationProblemID)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);
    
      OptimizationParameters optimizationParameters = new OptimizationParameters()
      {
        OptimizationProblemID = optimizationProblemID
      };

      // Run the query
      string errorString;
      DataObject dataObject = route4Me.GetOptimization(optimizationParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("GetOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);
      }
      else
      {
        Console.WriteLine("GetOptimization error: {0}", errorString);
      }
    }
  }
}
