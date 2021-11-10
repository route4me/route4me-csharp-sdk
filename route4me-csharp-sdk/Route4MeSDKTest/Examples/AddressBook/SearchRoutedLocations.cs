using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search Routed Locations
        /// </summary>
        public void SearchRoutedLocations()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Display = "routed",
                Offset = 0,
                Limit = 20
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
