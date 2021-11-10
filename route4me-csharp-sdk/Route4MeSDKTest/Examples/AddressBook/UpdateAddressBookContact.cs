using Route4MeSDK.DataTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update a contact by modifying the specified parameters.
        /// </summary>
        /// <param name="contact">Initial address book contact</param>
        public void UpdateAddressBookContact(AddressBookContact contact = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestContacts();

            if (contact != null) contact1 = contact;

            contact1.AddressGroup = "Updated";
            contact1.ScheduleBlacklist = new string[] { "2020-03-14", "2020-03-15" };
            contact1.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1" }, {"key2", "value2" }
            };
            contact1.LocalTimeWindowStart = R4MeUtils.DDHHMM2Seconds("7:03", out _);
            contact1.LocalTimeWindowEnd = R4MeUtils.DDHHMM2Seconds("7:37", out _);
            contact1.AddressCube = 5;
            contact1.AddressPieces = 6;
            contact1.AddressRevenue = 700;
            contact1.AddressWeight = 80;
            contact1.AddressPriority = 9;
            
            var updatableProperties = new List<string>()
            {
                "AddressId", "AddressGroup", "ScheduleBlacklist",
                "AddressCustomData", "LocalTimeWindowStart", "LocalTimeWindowEnd",
                "AddressCube","AddressPieces","AddressRevenue","AddressWeight","AddressPriority","ConvertBooleansToInteger"
            };

            // Run the query
            var updatedContact = route4Me.UpdateAddressBookContact(
                contact1,
                updatableProperties,
                out string errorString);

            PrintExampleContact(updatedContact, 0, errorString);

            RemoveTestContacts();
        }
    }
}
