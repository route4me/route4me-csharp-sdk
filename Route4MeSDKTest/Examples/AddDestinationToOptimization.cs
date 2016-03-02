using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void AddDestinationToOptimization(string optimizationProblemID, bool andReOptimize)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Prepare the address
      Address[] addresses = new Address[]
      {
        new Address() { AddressString = "717 5th Ave New York, NY 10021",
                        Alias         = "Giorgio Armani",
                        Latitude      = 40.7669692,
                        Longitude     = -73.9693864,
                        Time          = 0
        }
      };

      OptimizationParameters optimizationParameters = new OptimizationParameters()
      {
        OptimizationProblemID = optimizationProblemID,
        Addresses = addresses,
        ReOptimize = andReOptimize
      };

      // Run the query
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
    }
  }
}
