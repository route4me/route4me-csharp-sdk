using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void SetGPSPosition(string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Create the gps parametes
      GPSParameters gpsParameters = new GPSParameters()
      {
        Format = Format.Csv.Description(),
        RouteId = routeId,
        Latitude = 33.14384,
        Longitude = -83.22466,
        Course = 1,
        Speed = 120,
        DeviceType = DeviceType.IPhone.Description(),
        MemberId = 1,
        DeviceGuid = "TEST_GPS",
        DeviceTimestamp = "2014-06-14 17:43:35"
      };

      string errorString;
      var response = route4Me.SetGPS(gpsParameters, out errorString);

      Console.WriteLine("");

      if (string.IsNullOrEmpty(errorString))
      {
        Console.WriteLine("SetGps response: {0}", response.ToString());
      }
      else
      {
        Console.WriteLine("SetGps error: {0}", errorString);
      }
    }
  }
}
