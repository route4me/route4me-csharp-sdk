using System;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add an address book group to the user's account.
        /// </summary>
        public void AddAddressBookGroup()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var addressBookGroupRule = new AddressBookGroupRule()
            {
                ID = "address_1",
                Field = "address_1",
                Operator = "not_equal",
                Value = "qwerty123456"
            };

            var addressBookGroupFilter = new AddressBookGroupFilter()
            {
                Condition = "AND",
                Rules = new AddressBookGroupRule[] { addressBookGroupRule }
            };

            var addressBookGroupParameters = new AddressBookGroup()
            {
                GroupName = "All Group",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            // Run the query
            var addressBookGroup = route4Me.AddAddressBookGroup(
                addressBookGroupParameters,
                out string errorString);

            if (addressBookGroup == null || addressBookGroup.GetType() != typeof(AddressBookGroup))
            {
                Console.WriteLine(
                    "Cannot create an address book group." +
                    Environment.NewLine +
                    errorString);

                return;
            }
            else
            {
                Console.WriteLine("Created an address book group " + addressBookGroup.GroupId);
                AddAddressBookGroupToRemoveList(addressBookGroup.GroupId);
            }

            RemoveAddressBookGroups();
        }
    }
}
