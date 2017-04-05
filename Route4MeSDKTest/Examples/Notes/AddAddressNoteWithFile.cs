using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void AddAddressNoteWithFile(string routeId, int addressId)
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

      string tempFilePath = null;
      using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Route4MeSDKTest.Resources.test.png"))
      {
        var tempFiles = new TempFileCollection();
        {
          tempFilePath = tempFiles.AddExtension("png");
          System.Console.WriteLine(tempFilePath);
          using (Stream fileStream = File.OpenWrite(tempFilePath))
          {
            stream.CopyTo(fileStream);
          }
        }
      }

      // Run the query
      string errorString;
      string contents = "Test Note Contents with Attachment " + DateTime.Now.ToString();
      AddressNote note = route4Me.AddAddressNote(noteParameters, contents, tempFilePath, out errorString);

      Console.WriteLine("");

      if (note != null)
      {
        Console.WriteLine("AddAddressNoteWithFile executed successfully");

        Console.WriteLine("Note ID: {0}", note.NoteId);
      }
      else
      {
        Console.WriteLine("AddAddressNoteWithFile error: {0}", errorString);
      }
    }
  }
}
