using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public AddressBookContact AddAddressBookContact()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(c_ApiKey);

            var contact = new AddressBookContact()
            {
                first_name = "Test FirstName " + (new Random()).Next().ToString(),
                address_1 = "Test Address1 " + (new Random()).Next().ToString(),
                cached_lat = 38.024654,
                cached_lng = -77.338814,
                address_custom_data = new Dictionary<string, string>()
                {
                    {"Service type", "publishing"},
                    {"Facilities", "storage" },
                    {"Parking", "temporarry" }
                }
            };

            // Run the query
            string errorString;
            var resultContact = route4Me.AddAddressBookContact(contact, out errorString);

            Console.WriteLine("");

            if (resultContact != null)
            {
                Console.WriteLine("AddAddressBookContact executed successfully");

                Console.WriteLine("AddressId: {0}", resultContact.address_id);

                Console.WriteLine("Custom data:");

                foreach (var cdata in resultContact.address_custom_data)
                {
                    Console.WriteLine(cdata.Key + ": " + cdata.Value);
                }

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
