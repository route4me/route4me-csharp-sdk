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
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = new Dictionary<string, string>()
                {
                    {"Service type", "publishing"},
                    {"Facilities", "storage" },
                    {"Parking", "temporarry" }
                }
            };

            // Run the query
            var resultContact = route4Me.AddAddressBookContact(contact, out string errorString);

            if (resultContact == null || resultContact.GetType() != typeof(AddressBookContact))
            {
                Console.WriteLine(
                    "Cannot create an address book contact." +
                    Environment.NewLine +
                    errorString);

                return;
            }

            ContactsToRemove = new List<string>();
            ContactsToRemove.Add(resultContact.AddressId.ToString());

            PrintExampleContact(resultContact, 0, errorString);

            RemoveTestContacts();
        }
    }
}
