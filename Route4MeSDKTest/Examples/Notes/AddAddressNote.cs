using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void AddAddressNote(string routeId, int addressId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      NoteParameters noteParameters = new NoteParameters()
      {
        RouteId = routeId,
        AddressId = addressId,
        Latitude = 33.132675170898,
        Longitude = -83.244743347168,
        DeviceType = DeviceType.Web.Description(),
        ActivityType = StatusUpdateType.DropOff.Description()
      };

      // Run the query
      string errorString;
      string contents = "Test Note Contents " + DateTime.Now.ToString();
      AddressNote note = route4Me.AddAddressNote(noteParameters, contents, out errorString);

      Console.WriteLine("");

      if (note != null)
      {
        Console.WriteLine("AddAddressNote executed successfully");

        Console.WriteLine("Note ID: {0}", note.NoteId);
      }
      else
      {
        Console.WriteLine("AddAddressNote error: {0}", errorString);
      }
    }
  }
}
