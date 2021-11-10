using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search Locations By IDs
        /// </summary>
        public void SearchLocationsByIDs()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestContacts();

            var addressBookParameters = new AddressBookParameters
            {
                AddressId = contact1.AddressId + "," + contact2.AddressId
            };

            // Run the query

            AddressBookContact[] contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out uint total,
                out string errorString);

            PrintExampleContact(contacts, total, errorString);

            RemoveTestContacts();
        }
    }
}
