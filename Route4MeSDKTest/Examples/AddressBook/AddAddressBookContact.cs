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
        first_name = "Test FirstName " + (new Random()).Next().ToString(),
        address_1 = "Test Address1 " + (new Random()).Next().ToString(),
        cached_lat = 38.024654,
        cached_lng = -77.338814
      };

      // Run the query
      string errorString;
      AddressBookContact resultContact = route4Me.AddAddressBookContact(contact, out errorString);

      Console.WriteLine("");

      if (resultContact != null)
      {
        Console.WriteLine("AddAddressBookContact executed successfully");

        Console.WriteLine("AddressId: {0}", resultContact.address_id);

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
