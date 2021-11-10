using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an address book group
        /// </summary>
        public void UpdateAddressBookGroup()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateAddressBookGroup();

            string groupId = addressBookGroupsToRemove[addressBookGroupsToRemove.Count - 1];

            var addressBookGroupRule = new AddressBookGroupRule()
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty1234567"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter()
            {
                Condition = "AND",
                Rules = new AddressBookGroupRule[] { addressBookGroupRule }
            };

            var addressBookGroupParameters = new AddressBookGroup()
            {
                GroupId = groupId,
                GroupColor = "cd74e6",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me
                .UpdateAddressBookGroup(addressBookGroupParameters, out string errorString);

            if (addressBookGroup == null && addressBookGroup.GetType() != typeof(AddressBookGroup))
            {
                Console.WriteLine("Cannot update the address book group " + groupId);
            }
            else
            {
                Console.WriteLine(
                    addressBookGroup.GroupColor == "cd74e6"
                    ? "Updated the color of the address book group " + groupId
                    : "Cannot update the color of the address book group " + groupId
                    );
            }

            RemoveAddressBookGroups();
        }
    }
}
