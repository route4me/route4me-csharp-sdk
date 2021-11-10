using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add a scheduled address book contact
        /// </summary>
        public void AddScheduledAddressBookContact()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            ContactsToRemove = new List<string>();

            #region // Add a location, scheduled daily.
            var sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                From = DateTime.Today.ToString("yyyy-MM-dd"),
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            scheduledContact1 = new AddressBookContact()
            {
                Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                AddressAlias = "1604 PARKRIDGE PKWY 40214",
                AddressGroup = "Scheduled daily",
                FirstName = "Peter",
                LastName = "Newman",
                AddressEmail = "pnewman6564@yahoo.com",
                AddressPhoneNumber = "65432178",
                CachedLat = 38.141598,
                CachedLng = -85.793846,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>() {
                    { "scheduled", "yes" },
                    { "service type", "publishing" }
                },
                Schedule = new List<Schedule>() { sched1 }
            };

            scheduledContact1Response = route4Me.AddAddressBookContact(scheduledContact1, out string errorString);

            PrintExampleScheduledContact(scheduledContact1Response, "daily", errorString);
            #endregion

            #region // Add a location, scheduled weekly.
            var sched2 = new Schedule("weekly", false)
            {
                Enabled = true,
                From = DateTime.Today.ToString("yyyy-MM-dd"),
                Weekly = new ScheduleWeekly(1, new int[] { 1, 2, 3, 4, 5 })
            };

            scheduledContact2 = new AddressBookContact()
            {
                Address1 = "1407 MCCOY, Louisville, KY, 40215",
                AddressAlias = "1407 MCCOY 40215",
                AddressGroup = "Scheduled weekly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.202496,
                CachedLng = -85.786514,
                AddressCity = "Louisville",
                ServiceTime = 600,
                Schedule = new List<Schedule>() { sched2 }
            };

            scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, out errorString);

            PrintExampleScheduledContact(
                scheduledContact2Response,
                "weekly",
                errorString);
            #endregion

            #region // Add a location, scheduled monthly (dates mode).
            var sched3 = new Schedule("monthly", false)
            {
                Enabled = true,
                From = DateTime.Today.ToString("yyyy-MM-dd"),
                Monthly = new ScheduleMonthly(_every: 1, _mode: "dates", _dates: new int[] { 20, 22, 23, 24, 25 })
            };

            scheduledContact3 = new AddressBookContact()
            {
                Address1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                Address2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
                AddressAlias = "4805 BELLEVUE AVE 40215",
                AddressGroup = "Scheduled monthly",
                FirstName = "Bart",
                LastName = "Douglas",
                AddressEmail = "bdouglas9514@yahoo.com",
                AddressPhoneNumber = "95487454",
                CachedLat = 38.178844,
                CachedLng = -85.774864,
                AddressCountryId = "US",
                AddressStateId = "KY",
                AddressZip = "40215",
                AddressCity = "Louisville",
                ServiceTime = 750,
                Schedule = new List<Schedule>() { sched3 },
                Color = "red"
            };

            scheduledContact3Response = route4Me.AddAddressBookContact(scheduledContact3, out errorString);

            PrintExampleScheduledContact(
                scheduledContact3Response,
                "monthly (dates mode)",
                errorString);
            #endregion

            #region // Add a location, scheduled monthly (nth mode).
            var sched4 = new Schedule("monthly", false)
            {
                Enabled = true,
                From = DateTime.Today.ToString("yyyy-MM-dd"),
                Monthly = new ScheduleMonthly(_every: 1, _mode: "nth", _nth: new Dictionary<int, int>() { { 1, 4 } })
            };

            scheduledContact4 = new AddressBookContact()
            {
                Address1 = "730 CECIL AVENUE, Louisville, KY, 40211",
                AddressAlias = "730 CECIL AVENUE 40211",
                AddressGroup = "Scheduled monthly",
                FirstName = "David",
                LastName = "Silvester",
                AddressEmail = "dsilvester5874@yahoo.com",
                AddressPhoneNumber = "36985214",
                CachedLat = 38.248684,
                CachedLng = -85.821121,
                AddressCity = "Louisville",
                ServiceTime = 450,
                AddressCustomData = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "library" } },
                Schedule = new List<Schedule>() { sched4 },
                AddressIcon = "emoji/emoji-bus"
            };

            scheduledContact4Response = route4Me.AddAddressBookContact(scheduledContact4, out errorString);

            PrintExampleScheduledContact(
                scheduledContact4Response,
                "monthly (nth mode)",
                errorString);
            #endregion

            #region // Add a location with the daily scheduling and blacklist.
            var sched5 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                From = DateTime.Today.ToString("yyyy-MM-dd"),
                Daily = new ScheduleDaily(1)
            };

            scheduledContact5 = new AddressBookContact()
            {
                Address1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                AddressAlias = "4629 HILLSIDE DRIVE 40216",
                AddressGroup = "Scheduled daily",
                FirstName = "Kim",
                LastName = "Shandor",
                AddressEmail = "kshand8524@yahoo.com",
                AddressPhoneNumber = "9874152",
                CachedLat = 38.176067,
                CachedLng = -85.824638,
                AddressCity = "Louisville",
                AddressCustomData = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "appliance" } },
                Schedule = new List<Schedule>() { sched5 },
                ScheduleBlacklist = new string[] { "2017-12-22", "2017-12-23" },
                ServiceTime = 300
            };

            scheduledContact5Response = route4Me.AddAddressBookContact(scheduledContact5, out errorString);

            PrintExampleScheduledContact(
               scheduledContact5Response,
               "daily (with blacklist)",
               errorString);
            #endregion

            RemoveTestContacts();
        }
    }
}
