using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public AddressBookContact AddAddressBookContact()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      AddressBookContact contact = new AddressBookContact()
      {
        FirstName = "Test FirstName " + (new Random()).Next().ToString(),
        Address1 = "Test Address1 " + (new Random()).Next().ToString(),
        CachedLat = 38.024654,
        CachedLng = -77.338814
      };

      // Run the query
      string errorString;
      AddressBookContact resultContact = route4Me.AddAddressBookContact(contact, out errorString);

      Console.WriteLine("");

      if (resultContact != null)
      {
        Console.WriteLine("AddAddressBookContact executed successfully");

        Console.WriteLine("AddressId: {0}", resultContact.AddressId);

        return resultContact;
      }
      else
      {
        Console.WriteLine("AddAddressBookContact error: {0}", errorString);

        return null;
      }
    }
  }
}
