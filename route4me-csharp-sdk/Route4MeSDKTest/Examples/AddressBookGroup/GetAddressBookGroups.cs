using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the address book groups
        /// </summary>
        public void GetAddressBookGroups()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var addressBookGroupParameters = new AddressBookGroupParameters()
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            AddressBookGroup[] groups = route4Me
                .GetAddressBookGroups(addressBookGroupParameters, out string errorString);

            Console.WriteLine(
                    groups == null && groups.GetType() != typeof(AddressBookGroup[])
                    ? "Cannot retrieve the addres groups." + Environment.NewLine + errorString
                    : "Retrieved the address book groups: " + groups.Length
                );

            RemoveAddressBookGroups();
        }
    }
}
