using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get an address book group
        /// </summary>
        public void GetAddressBookGroup()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateAddressBookGroup();

            string groupId = addressBookGroupsToRemove[addressBookGroupsToRemove.Count - 1];

            var addressBookGroupParameters = new AddressBookGroupParameters()
            {
                GroupId = groupId
            };

            // Run the query
            var addressBookGroup = route4Me.GetAddressBookGroup(
                addressBookGroupParameters,
                out string errorString);

            Console.WriteLine(
                    addressBookGroup == null && addressBookGroup.GetType() != typeof(AddressBookGroup)
                    ? "Cannot retrieve the addres group " + groupId + Environment.NewLine + errorString
                    : "Retrieved the address book group " + groupId
                );

            RemoveAddressBookGroups();
        }
    }
}
