﻿using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
  /// <summary>
  /// Helper functions used by some of the examples.
  /// </summary>
  public sealed partial class Route4MeExamples
  {
    
    //your api key
    public const string c_ApiKey = "11111111111111111111111111111111";

    private void PrintExampleOptimizationResult(string exampleName, DataObject dataObject, string errorString)
    {
      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("{0} executed successfully", exampleName);
        Console.WriteLine("");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);

        dataObject.UserErrors.ForEach(error => Console.WriteLine("UserError : '{0}'", error));

        Console.WriteLine("");

        // Sort addresses by sequence order
        Address[] addressesSorted = dataObject.Addresses.Clone() as Address[];
        Array.Sort(addressesSorted, delegate(Address address1, Address address2)
        {
          int result = (address1.SequenceNo != null ? address1.SequenceNo.Value : -1) - (address2.SequenceNo != null ? address2.SequenceNo.Value : -1);
          if (result == 0)
            result = (address1.IsDepot != null && address1.IsDepot.Value ? 0 : 1) - (address2.IsDepot != null && address2.IsDepot.Value ? 0 : 1);
          return result;
        });

        addressesSorted.ForEach(address =>
        {
          Console.WriteLine("Address: {0}", address.AddressString);
          Console.WriteLine("Route ID: {0}", address.RouteId);
        });
      }
      else
      {
        Console.WriteLine("{0} error {1}", exampleName, errorString);
      }
    }
  }
}
