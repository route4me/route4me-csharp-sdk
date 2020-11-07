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
                address_1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                address_alias = "1604 PARKRIDGE PKWY 40214",
                address_group = "Scheduled daily",
                first_name = "Peter",
                last_name = "Newman",
                address_email = "pnewman6564@yahoo.com",
                address_phone_number = "65432178",
                cached_lat = 38.141598,
                cached_lng = -85.793846,
                address_city = "Louisville",
                address_custom_data = new Dictionary<string, string>() {
                    { "scheduled", "yes" },
                    { "service type", "publishing" }
                },
                schedule = new List<Schedule>() { sched1 }
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
                address_1 = "1407 MCCOY, Louisville, KY, 40215",
                address_alias = "1407 MCCOY 40215",
                address_group = "Scheduled weekly",
                first_name = "Bart",
                last_name = "Douglas",
                address_email = "bdouglas9514@yahoo.com",
                address_phone_number = "95487454",
                cached_lat = 38.202496,
                cached_lng = -85.786514,
                address_city = "Louisville",
                service_time = 600,
                schedule = new List<Schedule>() { sched2 }
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
                address_1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                address_2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
                address_alias = "4805 BELLEVUE AVE 40215",
                address_group = "Scheduled monthly",
                first_name = "Bart",
                last_name = "Douglas",
                address_email = "bdouglas9514@yahoo.com",
                address_phone_number = "95487454",
                cached_lat = 38.178844,
                cached_lng = -85.774864,
                address_country_id = "US",
                address_state_id = "KY",
                address_zip = "40215",
                address_city = "Louisville",
                service_time = 750,
                schedule = new List<Schedule>() { sched3 },
                color = "red"
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
                address_1 = "730 CECIL AVENUE, Louisville, KY, 40211",
                address_alias = "730 CECIL AVENUE 40211",
                address_group = "Scheduled monthly",
                first_name = "David",
                last_name = "Silvester",
                address_email = "dsilvester5874@yahoo.com",
                address_phone_number = "36985214",
                cached_lat = 38.248684,
                cached_lng = -85.821121,
                address_city = "Louisville",
                service_time = 450,
                address_custom_data = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "library" } },
                schedule = new List<Schedule>() { sched4 },
                address_icon = "emoji/emoji-bus"
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
                address_1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                address_alias = "4629 HILLSIDE DRIVE 40216",
                address_group = "Scheduled daily",
                first_name = "Kim",
                last_name = "Shandor",
                address_email = "kshand8524@yahoo.com",
                address_phone_number = "9874152",
                cached_lat = 38.176067,
                cached_lng = -85.824638,
                address_city = "Louisville",
                address_custom_data = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "appliance" } },
                schedule = new List<Schedule>() { sched5 },
                schedule_blacklist = new string[] { "2017-12-22", "2017-12-23" },
                service_time = 300
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
