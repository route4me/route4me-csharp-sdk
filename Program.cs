using Route4MeSDK.Examples;

namespace Route4MeSDK
{
  internal sealed class Program
  {
    static void Main(string[] args)
    {
      Route4MeExamples examples = new Route4MeExamples();

      examples.SingleDriverRoute10Stops();
      //examples.SingleDriverRoundTrip();
      //examples.SingleDriverRoundTripGeneric();
      //examples.MultipleDepotMultipleDriver();
      //examples.GetRoute();
      //examples.ReOptimization();
      //examples.SetGPSPosition();
      //examples.TrackDeviceLastLocationHistory();

      //examples.GenericExample();
      //examples.GenericExampleShortcut();
    }
  }
}
