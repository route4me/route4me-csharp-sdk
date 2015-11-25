using Route4MeSDK.Examples;

namespace Route4MeSDKTest
{
  internal sealed class Program
  {
    static void Main(string[] args)
    {
      Route4MeExamples examples = new Route4MeExamples();
      examples.SingleDriverRoute10Stops();
      examples.SingleDriverRoundTrip();
      examples.SingleDriverRoundTripGeneric();
      examples.MultipleDepotMultipleDriver();
      examples.MultipleDepotMultipleDriverTimeWindow();
      examples.SingleDepotMultipleDriverNoTimeWindow();
      examples.MultipleDepotMultipleDriverWith24StopsTimeWindow();
      examples.GetOptimization();
      examples.GetOptimizations();
      examples.ReOptimization();
      examples.GetRoute();
      examples.GetRoutes();
      examples.GetUsers();

      //disabled by default
      //examples.AddAddressNote();
      
      //disabled by default, not necessary for optimization tests
      //not all accounts are capable of storing gps data
      //examples.SetGPSPosition();
      //examples.TrackDeviceLastLocationHistory();

      examples.GenericExample();
      examples.GenericExampleShortcut();

      System.Console.WriteLine("Press any key");
      System.Console.ReadKey();
    }
  }
}
