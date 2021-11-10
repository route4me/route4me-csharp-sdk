using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove specified address book group
        /// </summary>
        public void RemoveAddressBookGroup()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateAddressBookGroup();

            string groupId = addressBookGroupsToRemove[addressBookGroupsToRemove.Count - 1];

            var addressGroupParams = new AddressBookGroupParameters()
            {
                groupID = groupId
            };

            var response = route4Me
                .RemoveAddressBookGroup(addressGroupParams, out string errorString);

            Console.WriteLine(
                (response?.Status ?? false)
                ? "Removed the address book group " + groupId
                : "Cannot remove the address book group " + groupId
                );
        }
    }
}
