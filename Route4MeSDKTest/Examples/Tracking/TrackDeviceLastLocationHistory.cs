using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void TrackDeviceLastLocationHistory(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Create the gps parametes
      GPSParameters gpsParameters = new GPSParameters()
      {
        Format          = Format.Csv.Description(),
        RouteId         = routeId,
        Latitude        = 33.14384,
        Longitude       = -83.22466,
        Course          = 1,
        Speed           = 120,
        DeviceType      = DeviceType.IPhone.Description(),
        MemberId        = 1,
        DeviceGuid      = "TEST_GPS",
        DeviceTimestamp = "2014-06-14 17:43:35"
      };

      string errorString;
      var response = route4Me.SetGPS(gpsParameters, out errorString);

      if (!string.IsNullOrEmpty(errorString))
      {
        Console.WriteLine("SetGps error: {0}", errorString);
        return;
      }

      Console.WriteLine("SetGps response: {0}", response.ToString());

      GenericParameters genericParameters = new GenericParameters();
      genericParameters.ParametersCollection.Add("route_id", routeId);
      genericParameters.ParametersCollection.Add("device_tracking_history", "1");

      var dataObject = route4Me.GetLastLocation(genericParameters, out errorString);

      Console.WriteLine("");

      if (dataObject != null)
      {
        Console.WriteLine("TrackDeviceLastLocationHistory executed successfully");
        Console.WriteLine("");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("");

        dataObject.TrackingHistory.ForEach(th =>
        {
          Console.WriteLine("Speed: {0}",      th.Speed);
          Console.WriteLine("Longitude: {0}",  th.Longitude);
          Console.WriteLine("Latitude: {0}",   th.Latitude);
          Console.WriteLine("Time Stamp: {0}", th.TimeStampFriendly);
          Console.WriteLine("");
        });
      }
      else
      {
        Console.WriteLine("TrackDeviceLastLocationHistory error: {0}", errorString);
      }
    }
  }
}
