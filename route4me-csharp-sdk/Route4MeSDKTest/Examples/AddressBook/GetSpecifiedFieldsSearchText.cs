using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search for Specified Text, Show Specified Fields
        /// </summary>
        public void GetSpecifiedFieldsSearchText()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestContacts();

            var addressBookParameters = new AddressBookParameters
            {
                Query = "Test FirstName",
                Fields = "address_id,first_name,address_email,address_group,first_name,cached_lat,schedule",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            var response = route4Me.SearchAddressBookLocation(
                addressBookParameters,
                out List<AddressBookContact> contactsFromObjects,
                out string errorString);

            PrintExampleContact(
                contactsFromObjects.ToArray(),
                (contactsFromObjects != null ? (uint)contactsFromObjects.Count : 0),
                errorString);

            RemoveTestContacts();
        }
    }
}
