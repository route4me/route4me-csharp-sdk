using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void UpdateAddressBookContact(AddressBookContact contact)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      AddressBookContact updatedContact = route4Me.UpdateAddressBookContact(contact, out errorString);

      Console.WriteLine("");

      if (updatedContact != null)
      {
        Console.WriteLine("UpdateAddressBookContact executed successfully");
      }
      else
      {
        Console.WriteLine("UpdateAddressBookContact error: {0}", errorString);
      }
    }
  }
}
