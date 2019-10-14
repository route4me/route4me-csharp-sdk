using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddScheduledAddressBookContact()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            List<string> lsRemoveContacts = new List<string>();

            #region // Add a location, scheduled daily.
            Schedule sched1 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            AddressBookContact scheduledContact1 = new AddressBookContact()
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
                address_custom_data = new Dictionary<string, string>() { { "scheduled", "yes" }, { "service type", "publishing" } },
                schedule = new List<Schedule>() { sched1 }
            };

            string errorString;
            var scheduledContact1Response = route4Me.AddAddressBookContact(scheduledContact1, out errorString);

            int location1 = scheduledContact1Response.address_id != null ? Convert.ToInt32(scheduledContact1Response.address_id) : -1;
            if (location1 > 0)
            {
                lsRemoveContacts.Add(location1.ToString());
                Console.WriteLine("A location with the daily scheduling was created. AddressId: {0}", location1);
            }
            else Console.WriteLine("Creating if a location with daily scheduling failed");
            #endregion

            #region // Add a location, scheduled weekly.
            Schedule sched2 = new Schedule("weekly", false)
            {
                Enabled = true,
                Weekly = new ScheduleWeekly(1, new int[] { 1, 2, 3, 4, 5 })
            };

            AddressBookContact scheduledContact2 = new AddressBookContact()
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

            var scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, out errorString);

            int location2 = scheduledContact2Response.address_id != null ? Convert.ToInt32(scheduledContact2Response.address_id) : -1;

            if (location2 > 0)
            {
                lsRemoveContacts.Add(location2.ToString());
                Console.WriteLine("A location with the weekly scheduling was created. AddressId: {0}", location2);
            }
            else Console.WriteLine("Creating if a location with weekly scheduling failed");

            #endregion

            #region // Add a location, scheduled monthly (dates mode).
            Schedule sched3 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(_every: 1, _mode: "dates", _dates: new int[] { 20, 22, 23, 24, 25 })
            };

            AddressBookContact scheduledContact3 = new AddressBookContact()
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

            var scheduledContact3Response = route4Me.AddAddressBookContact(scheduledContact3, out errorString);

            int location3 = scheduledContact3Response.address_id != null ? Convert.ToInt32(scheduledContact3Response.address_id) : -1;

            if (location3 > 0)
            {
                lsRemoveContacts.Add(location3.ToString());
                Console.WriteLine("A location with the monthly scheduling (mode 'dates') was created. AddressId: {0}", location3);
            }
            else Console.WriteLine("Creating if a location with monthly scheduling (mode 'dates') failed");
            #endregion

            #region // Add a location, scheduled monthly (nth mode).
            Schedule sched4 = new Schedule("monthly", false)
            {
                Enabled = true,
                Monthly = new ScheduleMonthly(_every: 1, _mode: "nth", _nth: new Dictionary<int, int>() { { 1, 4 } })
            };

            AddressBookContact scheduledContact4 = new AddressBookContact()
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

            var scheduledContact4Response = route4Me.AddAddressBookContact(scheduledContact4, out errorString);

            int location4 = scheduledContact4Response.address_id != null ? Convert.ToInt32(scheduledContact4Response.address_id) : -1;

            if (location4 > 0)
            {
                lsRemoveContacts.Add(location4.ToString());
                Console.WriteLine("A location with the monthly scheduling (mode 'nth') was created. AddressId: {0}", location4);
            }
            else Console.WriteLine("Creating if a location with monthly scheduling (mode 'nth') failed");
            #endregion

            #region // Add a location with the daily scheduling and blacklist.
            Schedule sched5 = new Schedule("daily", false)
            {
                Enabled = true,
                Mode = "daily",
                Daily = new ScheduleDaily(1)
            };

            AddressBookContact scheduledContact5 = new AddressBookContact()
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

            var scheduledContact5Response = route4Me.AddAddressBookContact(scheduledContact5, out errorString);

            int location5 = scheduledContact5Response.address_id != null ? Convert.ToInt32(scheduledContact5Response.address_id) : -1;

            if (location5 > 0)
            {
                lsRemoveContacts.Add(location5.ToString());
                Console.WriteLine("A location with the blacklist was created. AddressId: {0}", location5);
            }
            else Console.WriteLine("Creating of a location with the blacklist failed");
            #endregion

            var removed = route4Me.RemoveAddressBookContacts(lsRemoveContacts.ToArray(), out errorString);

            if ((bool)removed) Console.WriteLine("The added testing address book locations were removed successfuly"); else Console.WriteLine("Remvoving of the added testing address book locations failed");
        }
    }
}
