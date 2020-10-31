using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add address book contact
        /// </summary>
        public void AddAddressBookContact()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

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
            var resultContact = route4Me.AddAddressBookContact(contact, out string errorString);

            if (resultContact==null || resultContact.GetType()!=typeof(AddressBookContact))
            {
                Console.WriteLine(
                    "Cannot create an address book contact." + 
                    Environment.NewLine + 
                    errorString);

                return;
            }

            ContactsToRemove = new List<string>();
            ContactsToRemove.Add(resultContact.address_id.ToString());

            PrintExampleContact(resultContact,0, errorString);

            RemoveTestContacts();
        }
    }
}
