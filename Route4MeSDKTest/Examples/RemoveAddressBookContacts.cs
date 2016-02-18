using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void RemoveAddressBookContacts(string[] addressIds)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      bool removed = route4Me.RemoveAddressBookContacts(addressIds, out errorString);

      Console.WriteLine("");

      if (removed)
      {
        Console.WriteLine("RemoveAddressBookContacts executed successfully, {0} contacts deleted", addressIds.Length);
      }
      else
      {
        Console.WriteLine("RemoveAddressBookContacts error: {0}", errorString);
      }

    }
  }
}
