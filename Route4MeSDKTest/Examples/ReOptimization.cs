using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void ReOptimization()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);
    
      OptimizatonParameters optimizatonParameters = new OptimizatonParameters()
      {
        OptimizatonProblemID = "5ACDD6065C45A34768EA97FEBB14D637",
        ReOptimize = true
      };

      // Run the query
      string errorString;
      DataObject dataObject = route4Me.UpdateOptimization(optimizatonParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("ReOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);
      }
      else
      {
        Console.WriteLine("ReOptimization error: {0}", errorString);
      }
    }
  }
}
