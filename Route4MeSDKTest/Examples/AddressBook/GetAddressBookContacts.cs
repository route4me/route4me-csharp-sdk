using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetAddressBookContacts()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      AddressBookParameters addressBookParameters = new AddressBookParameters()
      {
        Limit = 10,
        Offset = 0
      };

      // Run the query
      uint total;
      string errorString;
      AddressBookContact[] contacts = route4Me.GetAddressBookLocation(addressBookParameters, out total, out errorString);

      Console.WriteLine("");

      if (contacts != null)
      {
        Console.WriteLine("GetAddressBookContacts executed successfully, {0} contacts returned, total = {1}", contacts.Length, total);
        Console.WriteLine("");

      }
      else
      {
        Console.WriteLine("GetAddressBookContacts error: {0}", errorString);
      }
    }
  }
}
