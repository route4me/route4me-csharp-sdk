using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void SetGPSPosition()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Create the gps parametes
      GPSParameters gpsParameters = new GPSParameters()
      {
        Format = Format.Csv.Description(),
        RouteId = "742A9E5051AA84B9E6365C92369B030C",
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
      string response = route4Me.SetGPS(gpsParameters, out errorString);

      Console.WriteLine("SetGps response: {0}", response);
    }
  }
}
