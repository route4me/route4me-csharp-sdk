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

      string errorString;

      // Run the query
      DataObject dataObject = route4Me.UpdateOptimization(optimizatonParameters, out errorString);

      if (dataObject != null)
      {
        Console.WriteLine("ReOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);
      }
      else
      {
        // TODO error handling
        Console.WriteLine("ReOptimization error: {0}", errorString);
      }
    }
  }
}
