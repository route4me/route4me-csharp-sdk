using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the address book contacts
        /// </summary>
        public void GetAddressBookContacts()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var addressBookParameters = new AddressBookParameters()
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            AddressBookContact[] contacts = route4Me.GetAddressBookLocation(
                addressBookParameters,
                out uint total,
                out string errorString);

            PrintExampleContact(contacts, total, errorString);
        }
    }
}
