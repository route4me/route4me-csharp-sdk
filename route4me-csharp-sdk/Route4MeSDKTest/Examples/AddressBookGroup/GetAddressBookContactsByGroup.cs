using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the address book contacts by specified address book group.
        /// </summary>
        public void GetAddressBookContactsByGroup()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateAddressBookGroup();

            string groupId = addressBookGroupsToRemove[addressBookGroupsToRemove.Count - 1];

            var addressBookGroupParameters = new AddressBookGroupParameters()
            {
                groupID = groupId,
                Fields = new string[] { "address_id" }
            };

            // Run the query
            var response = route4Me
                .GetAddressBookContactsByGroup(
                addressBookGroupParameters,
                out string errorString);

            Console.WriteLine((response?.Results?.Length ?? 0) < 1
                ? "Cannot retrieve contacts by group " + groupId + Environment.NewLine + errorString
                : "Retrieved the contacts by group " + groupId + ": " + response.Results.Length
                );

            RemoveAddressBookGroups();
        }
    }
}
