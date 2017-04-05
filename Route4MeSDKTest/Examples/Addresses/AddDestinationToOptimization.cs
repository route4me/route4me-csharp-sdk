using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public DataObject AddDestinationToOptimization(string optimizationProblemID, bool andReOptimize)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Prepare the address that we are going to add to an existing route optimization
      Address[] addresses = new Address[]
      {
        new Address() { AddressString = "717 5th Ave New York, NY 10021",
                        Alias         = "Giorgio Armani",
                        Latitude      = 40.7669692,
                        Longitude     = -73.9693864,
                        Time          = 0
        }
      };

      //Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
      OptimizationParameters optimizationParameters = new OptimizationParameters()
      {
        OptimizationProblemID = optimizationProblemID,
        Addresses = addresses,
        ReOptimize = andReOptimize
      };

      // Execute the optimization to re-optimize and rebalance all the routes in this optimization
      string errorString;
      DataObject dataObject = route4Me.UpdateOptimization(optimizationParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("AddDestinationToOptimization executed successfully");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);
      }
      else
      {
        Console.WriteLine("AddDestinationToOptimization error: {0}", errorString);
      }

      return dataObject;
    }
  }
}
