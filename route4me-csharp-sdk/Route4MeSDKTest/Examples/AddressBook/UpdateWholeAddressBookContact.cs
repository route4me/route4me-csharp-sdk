using Route4MeSDK.DataTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an address book contact by sending whole modified contact object.
        /// </summary>
        public void UpdateWholeAddressBookContact()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestContacts();

            // Create contact clone in the memory
            var contactClone = R4MeUtils.ObjectDeepClone<AddressBookContact>(contact1);

            // Modify the parameters of the contactClone
            contactClone.AddressGroup = "Updated";
            contactClone.ScheduleBlacklist = new string[] { "2020-03-14", "2020-03-15" };

            contactClone.AddressCustomData = new Dictionary<string, string>
            {
                {"key1", "value1" }, {"key2", "value2" }
            };

            contactClone.LocalTimeWindowStart = R4MeUtils.DDHHMM2Seconds("7:05", out _);
            contactClone.LocalTimeWindowEnd = R4MeUtils.DDHHMM2Seconds("7:22", out _);
            contactClone.AddressCube = 5;
            contactClone.AddressPieces = 6;
            contactClone.AddressRevenue = 700;
            contactClone.AddressWeight = 80;
            contactClone.AddressPriority = 9;

            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            contactClone.Schedule = new List<Schedule>() { sched1 };

            contact1 = route4Me.UpdateAddressBookContact(contactClone, contact1, out string errorString);

            PrintExampleContact(contact1, 0, errorString);

            RemoveTestContacts();
        }
    }
}
