using NUnit.Framework;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Route4MeSDK.Route4MeManager;

namespace Route4MeSDK.Examples
{
    public enum GeocodingPrintOption
    {
        Geocodings = 1,
        StreetData = 2,
        StreetService = 3,
        StreetZipCode = 4
    }

    /// <summary>
    /// Helper functions used by some of the examples.
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        //your api key
        public string ActualApiKey = R4MeUtils.ReadSetting("actualApiKey");
        public string DemoApiKey = R4MeUtils.ReadSetting("demoApiKey");

        public List<string> ContactsToRemove;
        public List<string> RoutesToRemove;
        public List<string> OptimizationsToRemove;
        public List<string> addressBookGroupsToRemove;
        public List<string> configKeysToRemove = new List<string>();
        public List<string> CustomNoteTypesToRemove = new List<string>();
        public List<string> OrdersToRemove = new List<string>();

        Order lastCreatedOrder;

        DataObject dataObjectSD10Stops;
        string SD10Stops_optimization_problem_id;

        DataObjectRoute SD10Stops_route;
        string SD10Stops_route_id;

        public AddressBookContact contact1, contact2;

        AddressBookContact scheduledContact1, scheduledContact1Response;
        AddressBookContact scheduledContact2, scheduledContact2Response;
        AddressBookContact scheduledContact3, scheduledContact3Response;
        AddressBookContact scheduledContact4, scheduledContact4Response;
        AddressBookContact scheduledContact5, scheduledContact5Response;

        public DataObject dataObjectSDRT { get; set; }
        public string SDRT_optimization_problem_id { get; set; }
        public DataObjectRoute SDRT_route { get; set; }
        public string SDRT_route_id { get; set; }

        AddressBookContact contactToRemove;

        AvoidanceZone avoidanceZone;

        #region Optimizations, Routes, Destinations

        private void PrintExampleRouteResult(object dataObjectRoute, string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";
            
            Console.WriteLine("");

            if (dataObjectRoute != null)
            {
                var route1 = dataObjectRoute.GetType() == typeof(DataObjectRoute)
                    ? (DataObjectRoute)dataObjectRoute
                    : null;

                var route2 = dataObjectRoute.GetType() == typeof(RouteResponse)
                    ? (RouteResponse)dataObjectRoute
                    : null;

                Console.WriteLine("{0} executed successfully", testName);
                Console.WriteLine("");

                Console.WriteLine(
                    "Optimization Problem ID: {0}", 
                    route1!=null ? route1.OptimizationProblemId : route2.OptimizationProblemId
                    );

                Console.WriteLine("");

                if (route1!=null)
                {
                    route1.Addresses.ForEach(address =>
                    {
                        Console.WriteLine("Address: {0}", address.AddressString);
                        Console.WriteLine("Route ID: {0}", address.RouteId);
                    });
                }
                else
                {
                    route2.Addresses.ForEach(address =>
                    {
                        Console.WriteLine("Address: {0}", address.AddressString);
                        Console.WriteLine("Route ID: {0}", address.RouteId);
                    });
                }
            }
            else
            {
                Console.WriteLine("{0} error {1}", testName, errorString);
            }
        }

        private void PrintExampleOptimizationResult(object dataObject, string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("{0} executed successfully", testName);
                Console.WriteLine("");

                if (dataObject.GetType()==typeof(DataObject))
                {
                    var dataObject1 = (DataObject)dataObject;
                    Console.WriteLine("Optimization Problem ID: {0}", dataObject1.OptimizationProblemId);
                    Console.WriteLine("State: {0}", dataObject1.State);

                    dataObject1.UserErrors.ForEach(error => Console.WriteLine("UserError : '{0}'", error));

                    Console.WriteLine("");

                    dataObject1.Addresses.ForEach(address =>
                    {
                        Console.WriteLine("Address: {0}", address.AddressString);
                        Console.WriteLine("Route ID: {0}", address.RouteId);
                    });
                }
                else
                {
                    var optimizations = (DataObject[])dataObject;

                    Console.WriteLine(
                    testName + " executed successfully, {0} optimizations returned",
                    optimizations.Length);

                    Console.WriteLine("");

                    optimizations.ForEach(optimization =>
                    {
                        Console.WriteLine(
                            "Optimization Problem ID: {0}",
                            optimization.OptimizationProblemId);

                        Console.WriteLine("");
                    });
                }
            }
            else
            {
                Console.WriteLine("{0} error {1}", testName, errorString);
            }
        }

        private void PrintExampleDestination(object obj, string errorString = "")
        {
            if (obj == null && obj.GetType() != typeof(Address) && obj.GetType() != typeof(bool))
            {
                Console.WriteLine("Wrong address. Cannot print." + Environment.NewLine + errorString);
                return;
            }

            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (obj.GetType() == typeof(Address))
            {
                var address = (Address)obj;

                if (address.RouteDestinationId != null)
                {
                    Console.WriteLine(testName + " executed successfully");

                    Console.WriteLine("Destination ID: {0}", address.RouteDestinationId);
                }
                else
                {
                    Console.WriteLine(testName + " error: {0}", errorString);
                }
            }
            else if (obj.GetType().IsArray)
            {
                var addresses = (int[])obj;

                Console.WriteLine(testName + " executed successfully");
                Console.WriteLine("Affected destinations: " + addresses.Length);
            }
            else
            {
                Console.WriteLine((bool)obj
                    ? testName + " executed successfully"
                    : String.Format(testName + " error: {0}", errorString));
            }
        }

        private bool RunOptimizationSingleDriverRoute10Stops()
        {
            var r4mm = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
            #region Addresses

            new Address() { AddressString = "151 Arbor Way Milledgeville GA 31061",
                            //indicate that this is a departure stop
                            //single depot routes can only have one departure depot 
                            IsDepot = true, 
                        
                            //required coordinates for every departure and stop on the route
                            Latitude = 33.132675170898,
                            Longitude = -83.244743347168,
                        
                            //the expected time on site, in seconds. this value is incorporated into the optimization engine
                            //it also adjusts the estimated and dynamic eta's for a route
                            Time = 0, 
                        
                        
                            //input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest
                            CustomFields = new Dictionary<string, string>() {{"color", "red"}, {"size", "huge"}}
            },

            new Address() { AddressString = "230 Arbor Way Milledgeville GA 31061",
                            Latitude = 33.129695892334,
                            Longitude = -83.24577331543,
                            Time = 0 },

            new Address() { AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                            Latitude = 33.143497,
                            Longitude = -83.224487,
                            Time = 0 },

            new Address() { AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141784667969,
                            Longitude = -83.237518310547,
                            Time = 0 },

            new Address() { AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141086578369,
                            Longitude = -83.238258361816,
                            Time = 0 },

            new Address() { AddressString =  "131 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142036437988,
                            Longitude = -83.238845825195,
                            Time = 0 },

            new Address() { AddressString =  "138 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.14307,
                            Longitude = -83.239334,
                            Time = 0 },

            new Address() { AddressString =  "139 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142734527588,
                            Longitude = -83.237442016602,
                            Time = 0 },

            new Address() { AddressString =  "145 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.143871307373,
                            Longitude = -83.237342834473,
                            Time = 0 },

            new Address() { AddressString =  "221 Blake Cir Milledgeville GA 31061",
                            Latitude = 33.081462860107,
                            Longitude = -83.208511352539,
                            Time = 0 }

            #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                //StoreRoute = false,
                RouteName = "SD Route 10 Stops Test " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description()
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            string errorString;

            try
            {
                dataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, out errorString);

                SD10Stops_optimization_problem_id = dataObjectSD10Stops.OptimizationProblemId;
                SD10Stops_route = (dataObjectSD10Stops != null && dataObjectSD10Stops.Routes != null && dataObjectSD10Stops.Routes.Length > 0) ? dataObjectSD10Stops.Routes[0] : null;
                SD10Stops_route_id = (SD10Stops_route != null) ? SD10Stops_route.RouteID : null;

                if (dataObjectSD10Stops != null && dataObjectSD10Stops.Routes != null && dataObjectSD10Stops.Routes.Length > 0)
                {
                    SD10Stops_route = dataObjectSD10Stops.Routes[0];
                }
                else
                {
                    SD10Stops_route = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Route 10 Stops generation failed... " + ex.Message);
                return false;
            }
        }

        private void RemoveTestRoutes()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if (RoutesToRemove.Count > 0)
            {
                try
                {
                    string[] deletedRouteIds = route4Me.DeleteRoutes(RoutesToRemove.ToArray(), out string errorString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot remove test routes." + Environment.NewLine + ex.Message);
                }
            }
        }

        private void RemoveTestOptimizations()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if ((OptimizationsToRemove?.Count ?? 0) > 0)
            {
                try
                {
                    bool result = route4Me.RemoveOptimization(OptimizationsToRemove.ToArray(), out string errorString);
                    OptimizationsToRemove = new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot remove test routes." + Environment.NewLine + ex.Message);
                }
            }
        }

        public bool RunSingleDriverRoundTrip()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
              {
                #region Addresses

                new Address() { AddressString = "754 5th Ave New York, NY 10019",
                                Alias         = "Bergdorf Goodman",
                                IsDepot       = true,
                                Latitude      = 40.7636197,
                                Longitude     = -73.9744388,
                                Time          = 0 },

                new Address() { AddressString = "717 5th Ave New York, NY 10022",
                                Alias         = "Giorgio Armani",
                                Latitude      = 40.7669692,
                                Longitude     = -73.9693864,
                                Time          = 0 },

                new Address() { AddressString = "888 Madison Ave New York, NY 10014",
                                Alias         = "Ralph Lauren Women's and Home",
                                Latitude      = 40.7715154,
                                Longitude     = -73.9669241,
                                Time          = 0 },

                new Address() { AddressString = "1011 Madison Ave New York, NY 10075",
                                Alias         = "Yigal Azrou'l",
                                Latitude      = 40.7772129,
                                Longitude     = -73.9669,
                                Time          = 0 },

                 new Address() { AddressString = "440 Columbus Ave New York, NY 10024",
                                Alias         = "Frank Stella Clothier",
                                Latitude      = 40.7808364,
                                Longitude     = -73.9732729,
                                Time          = 0 },

                new Address() { AddressString = "324 Columbus Ave #1 New York, NY 10023",
                                Alias         = "Liana",
                                Latitude      = 40.7803123,
                                Longitude     = -73.9793079,
                                Time          = 0 },

                new Address() { AddressString = "110 W End Ave New York, NY 10023",
                                Alias         = "Toga Bike Shop",
                                Latitude      = 40.7753077,
                                Longitude     = -73.9861529,
                                Time          = 0 },

                new Address() { AddressString = "555 W 57th St New York, NY 10019",
                                Alias         = "BMW of Manhattan",
                                Latitude      = 40.7718005,
                                Longitude     = -73.9897716,
                                Time          = 0 },

                new Address() { AddressString = "57 W 57th St New York, NY 10019",
                                Alias         = "Verizon Wireless",
                                Latitude      = 40.7558695,
                                Longitude     = -73.9862019,
                                Time          = 0 },

                #endregion
              };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                //StoreRoute = false,
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,
                RT = true,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            string errorString;

            try
            {
                dataObjectSDRT = route4Me.RunOptimization(optimizationParameters, out errorString);
                SDRT_optimization_problem_id = dataObjectSDRT.OptimizationProblemId;
                SDRT_route = (dataObjectSDRT != null && dataObjectSDRT.Routes != null && dataObjectSDRT.Routes.Length > 0) ? dataObjectSDRT.Routes[0] : null;
                SDRT_route_id = (SDRT_route != null) ? SDRT_route.RouteID : null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Round Trip generation failed... " + ex.Message);
                return false;
            }
        }

        #endregion

        #region Address Book Contacts

        public void CreateTestContacts()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var contact = new AddressBookContact()
            {
                first_name = "Test FirstName " + (new Random()).Next().ToString(),
                address_1 = "Test Address1 " + (new Random()).Next().ToString(),
                cached_lat = 38.024654,
                cached_lng = -77.338814
            };

            // Run the query
            contact1 = route4Me.AddAddressBookContact(contact, out string errorString);

            Assert.IsNotNull(contact1, "AddAddressBookContactsTest failed... " + errorString);

            int location1 = contact1.address_id != null ? Convert.ToInt32(contact1.address_id) : -1;

            ContactsToRemove = new List<string>();

            if (location1 > 0) ContactsToRemove.Add(location1.ToString());

            var dCustom = new Dictionary<string, string>()
            {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            };

            contact = new AddressBookContact()
            {
                first_name = "Test FirstName " + (new Random()).Next().ToString(),
                address_1 = "Test Address1 " + (new Random()).Next().ToString(),
                cached_lat = 38.024654,
                cached_lng = -77.338814,
                address_custom_data = dCustom
            };

            contact2 = route4Me.AddAddressBookContact(contact, out errorString);

            Assert.IsNotNull(contact2, "AddAddressBookContactsTest failed... " + errorString);

            int location2 = contact2.address_id != null ? Convert.ToInt32(contact2.address_id) : -1;

            if (location2 > 0) ContactsToRemove.Add(location2.ToString());

            var contactParams = new AddressBookContact()
            {
                first_name = "Test FirstName Rem" + (new Random()).Next().ToString(),
                address_1 = "Test Address1 Rem " + (new Random()).Next().ToString(),
                cached_lat = 38.02466,
                cached_lng = -77.33882
            };

            contactToRemove = route4Me.AddAddressBookContact(contactParams, out errorString);

            if (contactToRemove != null && contactToRemove.GetType() == typeof(AddressBookContact))
                ContactsToRemove.Add(contactToRemove.address_id.ToString());
        }

        /// <summary>
        /// Remove the contacts created in an example.
        /// </summary>
        private void RemoveTestContacts()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if (ContactsToRemove.Count > 0)
            {
                try
                {
                    if (contactToRemove != null)
                        ContactsToRemove.Add(contactToRemove.address_id.ToString());

                    bool removed = route4Me.RemoveAddressBookContacts(ContactsToRemove.ToArray(), out string errorString);
                    ContactsToRemove = new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot remove test contacts." + Environment.NewLine + ex.Message);
                }
            }
        }

        private void PrintExampleContact(object contacts, uint total, string errorString = "")
        {
            if (contacts == null ||
                (contacts.GetType() != typeof(AddressBookContact) &&
                contacts.GetType() != typeof(AddressBookContact[])))
            {
                Console.WriteLine("Wrong contact(s). Cannot print." + Environment.NewLine + errorString);
                return;
            }

            Console.WriteLine("");

            if (contacts.GetType() == typeof(AddressBookContact))
            {
                AddressBookContact resultContact = (AddressBookContact)contacts;

                Console.WriteLine("AddAddressBookContact executed successfully");

                Console.WriteLine("AddressId: {0}", resultContact.address_id);

                Console.WriteLine("Custom data:");

                foreach (var cdata in (Dictionary<string, string>)resultContact.address_custom_data)
                {
                    Console.WriteLine(cdata.Key + ": " + cdata.Value);
                }
            }
            else
            {
                Console.WriteLine("GetAddressBookContacts executed successfully, {0} contacts returned, total = {1}", ((AddressBookContact[])contacts).Length, total);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Console print of a scheduled contact response.
        /// </summary>
        /// <param name="contactResponse">Scheduled contact</param>
        /// <param name="scheduleType">Schedule type: 'daily', 'weekly', monthly'</param>
        private void PrintExampleScheduledContact(
            AddressBookContact contactResponse,
            string scheduleType,
            string errorString = "")
        {
            int location1 = contactResponse.address_id != null
                ? Convert.ToInt32(contactResponse.address_id)
                : -1;

            if (location1 > 0)
            {
                ContactsToRemove.Add(location1.ToString());
                Console.WriteLine("A location with the " + scheduleType + " scheduling was created. AddressId: {0}", location1);
            }
            else Console.WriteLine(
                "Creating of a location with " + scheduleType + " scheduling failed." +
                Environment.NewLine +
                errorString
                );
        }

        #endregion
        
        /// <summary>
        /// Console print of an example results.
        /// </summary>
        /// <param name="obj">An Address type object, or boolean value</param>
        /// <param name="errorString">Error string</param>

        #region Avoidance Zones

        private void PrintExampleAvoidanceZone(object avoidanceZone, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;

            Console.WriteLine("");

            if (avoidanceZone != null)
            {
                Console.WriteLine(testName + " executed successfully");

                string avoidanceZoneId = avoidanceZone.GetType() == typeof(AvoidanceZone)
                    ? ((AvoidanceZone)avoidanceZone).TerritoryId
                    : avoidanceZone.ToString();

                Console.WriteLine("Territory ID: {0}", avoidanceZoneId);
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        /// <summary>
        /// Remove an avoidance zone
        /// </summary>
        /// <param name="avoidanceZoneId">Avoidance zone ID (territory ID)</param>
        /// <returns>If true, an avoidance zone removed successfully</returns>
        public bool RemoveAvidanceZone(string avoidanceZoneId)
        {
            AvoidanceZoneQuery avZoneQuery = new AvoidanceZoneQuery()
            {
                TerritoryId = avoidanceZoneId
            };

            var route4Me = new Route4MeManager(ActualApiKey);

            bool deleted = route4Me.DeleteAvoidanceZone(avZoneQuery, out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                deleted
                ? "The avoidance zone " + avZoneQuery.TerritoryId + " removed successfully"
                : "Cannot remove avoidance zone " + avZoneQuery.TerritoryId
                );

            return deleted;
        }

        public void CreateAvoidanceZone()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var avoidanceZoneParameters = new AvoidanceZoneParameters()
            {
                TerritoryName = "Test Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory()
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new string[] { "37.569752822786455,-77.47833251953125",
                                "5000"}
                }
            };

            // Run the query
            avoidanceZone = route4Me.AddAvoidanceZone(
                avoidanceZoneParameters,
                out string errorString);

            PrintExampleAvoidanceZone(avoidanceZone);
        }

        #endregion

        public void PrintExampleGeocodings(
            object result, 
            GeocodingPrintOption printOption,
            string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            switch (printOption)
            {
                case GeocodingPrintOption.Geocodings:
                    Console.WriteLine("");

                    if (result != null && result.ToString().Length>0)
                    {
                        Console.WriteLine(testName + " executed successfully");
                    }
                    else
                    {
                        Console.WriteLine(testName+" error: {0}", errorString);
                    }
                    break;
                case GeocodingPrintOption.StreetData:
                case GeocodingPrintOption.StreetService:
                case GeocodingPrintOption.StreetZipCode:
                    Console.WriteLine("");

                    if (result != null && result.GetType()==typeof(ArrayList))
                    {
                        Console.WriteLine(testName+" executed successfully");
                        foreach (Dictionary<string, string> res1 in (ArrayList)result)
                        {

                            Console.WriteLine("Zipcode: " + res1["zipcode"]);
                            Console.WriteLine("Street name: " + res1["street_name"]);
                            Console.WriteLine("---------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine(testName+" error: {0}", errorString);
                    }
                    break;
            }
        }

        private void PrintExampleActivities(Activity[] activities, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (activities != null)
            {
                Console.WriteLine(
                    "The test {0} executed successfully, " +
                    "{1} activities returned",
                    testName,
                    activities.Length);
                Console.WriteLine("");

                activities.ForEach(activity =>
                {
                    Console.WriteLine("Activity ID: {0}", activity.ActivityId);
                });

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("{0} error: {1}", testName, errorString);
            }
        }

        #region Address Book Group

        public void AddAddressBookGroupToRemoveList(string groupId)
        {
            if (addressBookGroupsToRemove == null) addressBookGroupsToRemove = new List<string>();

            addressBookGroupsToRemove.Add(groupId);
        }

        public void CreateAddressBookGroup()
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
                groupName = "All Group",
                groupColor = "92e1c0",
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

            AddAddressBookGroupToRemoveList(addressBookGroup.groupID);
        }

        public void RemoveAddressBookGroups()
        {
            if (addressBookGroupsToRemove == null || addressBookGroupsToRemove.Count < 1) return;

            var route4Me = new Route4MeManager(ActualApiKey);

            foreach (string groupId in addressBookGroupsToRemove)
            {
                var addressGroupParams =
                    new AddressBookGroupParameters() { groupID = groupId };

                var response = route4Me.RemoveAddressBookGroup(
                    addressGroupParams,
                    out string errorString);

                Console.WriteLine((response?.status ?? false)
                    ? "Removed the address book group " + groupId
                    : "Cannot removed the address book group " + groupId);

                Console.WriteLine("");
            }
        }

        #endregion

        #region Member Configuration Group

        public void CreateConfigKey()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var parametersArray = new MemberConfigurationParameters[]
            {
                new MemberConfigurationParameters
                {
                    config_key = "Test Config Key",
                    config_value = "Test Config Value"
                },
            };

            // Run the query
            var result = route4Me.
                CreateNewConfigurationKey(parametersArray, out string errorString);

            Console.WriteLine(
                result.result != null
                    ? "Created config key " + "Test Config Key"
                    : "Cannot create config key " + "Test Config Key."+Environment.NewLine+errorString 
                );

            if ((result?.result ?? null) != null) configKeysToRemove.Add("Test Config Key");
        }

        public void PrintConfigKey(MemberConfigurationResponse configResponse, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (configResponse != null)
            {
                Console.WriteLine(testName + " executed successfully");
                Console.WriteLine("Result: " + configResponse.result);
                Console.WriteLine("Affected: " + configResponse.affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        public void PrintConfigKey(MemberConfigurationDataResponse configDataResponse, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (configDataResponse != null)
            {
                Console.WriteLine(testName+" executed successfully");
                Console.WriteLine("Result: " + configDataResponse.result);

                foreach (MemberConfigurationData mc_data in configDataResponse.data)
                {
                    Console.WriteLine("member_id= " + mc_data.member_id);
                    Console.WriteLine("config_key= " + mc_data.config_key);
                    Console.WriteLine("config_value= " + mc_data.config_value);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine(testName+" error: {0}", errorString);
            }
        }

        public void RemoveConfigKeys()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            foreach (string configKey in configKeysToRemove)
            {
                var @params = new MemberConfigurationParameters { config_key = configKey };

                // Run the query
                var result = route4Me.RemoveConfigurationKey(@params, out string errorString);

                PrintConfigKey(result, errorString);
            }
        }

        #endregion

        #region Address Notes

        private void PrintExampleAddressNote(object note, string errorString = "")
        {
            Console.WriteLine("");

            if (note != null && note.GetType()==typeof(AddressNote))
            {
                Console.WriteLine("AddAddressNote executed successfully");

                Console.WriteLine("Note ID: {0}", ((AddressNote)note).NoteId);
            }
            else
            {
                Console.WriteLine("AddAddressNote error: {0}", errorString);
            }
        }

        private void CreateAddressNote(out string routeId, out int? destinationId)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunSingleDriverRoundTrip();

            OptimizationsToRemove = new List<string>() { SDRT_optimization_problem_id };

            routeId = SDRT_route_id;

            destinationId = (int)SDRT_route.Addresses[1].RouteDestinationId;

            double lat = SDRT_route.Addresses.Length > 1
                ? SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            double lng = SDRT_route.Addresses.Length > 1
                ? SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeId,
                AddressId = (int)destinationId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            // Run the query
            string contents = "Test Note Contents " + DateTime.Now.ToString();
            var note = route4Me.AddAddressNote(noteParameters, contents, out string errorString);

            PrintExampleAddressNote(note, errorString);
        }

        private void RemoveCustomNoteTypes()
        {
            if (CustomNoteTypesToRemove.Count < 1) return;

            var route4Me = new Route4MeManager(ActualApiKey);

            var response = route4Me.getAllCustomNoteTypes(out string _);

            List<CustomNoteType> allCustomNoteTypes =
                (response != null && response.GetType() == typeof(CustomNoteType[]))
                ? ((CustomNoteType[])response).ToList()
                : null;

            if (allCustomNoteTypes == null || allCustomNoteTypes.Count < 1) return;

            foreach (string customNoteType in CustomNoteTypesToRemove)
            {
                int? customNoteTypeId = allCustomNoteTypes
                    .Where(x => x.NoteCustomType == customNoteType)
                    .FirstOrDefault()
                    ?.NoteCustomTypeID ?? -1;
                
                if (customNoteTypeId>0)
                {
                    var removeResult = route4Me
                    .removeCustomNoteType((int)customNoteTypeId, out string errorString);

                    Console.WriteLine(
                        (removeResult != null && removeResult.GetType() == typeof(int))
                        ? "The custom note type " + customNoteTypeId + " removed"
                        : "Cannot remove the custom note type " + customNoteTypeId);
                }
                
            }
        }

        private void PrintExampleCustomNoteType(object response, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (response != null && response.GetType() == typeof(int))
            {
                Console.WriteLine(testName + " executed successfully");

                Console.WriteLine("Affected custom note types: {0}", (int)response);
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        private int? GetCustomNoteIdByName(string customNoteTypeName)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var response = route4Me.getAllCustomNoteTypes(out string _);

            List<CustomNoteType> allCustomNoteTypes =
                (response != null && response.GetType() == typeof(CustomNoteType[]))
                ? ((CustomNoteType[])response).ToList()
                : null;

            if (allCustomNoteTypes == null || allCustomNoteTypes.Count < 1) return null;

            int? customNoteTypeId = allCustomNoteTypes
                    .Where(x => x.NoteCustomType == customNoteTypeName)
                    .FirstOrDefault()
                    ?.NoteCustomTypeID ?? null;

            return customNoteTypeId;
        }

        private void CreateCustomNoteType()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            string customType = "To Do 5";

            string[] customValues = new string[]
            {
                "Pass a package 5", "Pickup package", "Do a service"
            };

            // Run the query
            var response = route4Me.AddCustomNoteType(
                    customType,
                    customValues,
                    out string errorString
                );

            PrintExampleCustomNoteType(response, errorString);

            CustomNoteTypesToRemove = new List<string>();

            if (response != null && response.GetType() == typeof(int))
                CustomNoteTypesToRemove.Add("To Do 5");
        }
        #endregion

        #region Orders

        private void CreateExampleOrder()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderParams = new Order()
            {
                address_1 = "318 S 39th St, Louisville, KY 40212, USA",
                cached_lat = 38.259326,
                cached_lng = -85.814979,
                curbside_lat = 38.259326,
                curbside_lng = -85.814979,
                address_alias = "318 S 39th St 40212",
                address_city = "Louisville",
                EXT_FIELD_first_name = "Lui",
                EXT_FIELD_last_name = "Carol",
                EXT_FIELD_email = "lcarol654@yahoo.com",
                EXT_FIELD_phone = "897946541",
                EXT_FIELD_custom_data = new Dictionary<string, string>() 
                { 
                    { "order_type", "scheduled order" } 
                },
                day_scheduled_for_YYMMDD = DateTime.Now.ToString("yyyy-MM-dd"),
                local_time_window_end = 39000,
                local_time_window_end_2 = 46200,
                local_time_window_start = 37800,
                local_time_window_start_2 = 45000,
                local_timezone_string = "America/New_York",
                order_icon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out string errorString);

            if (newOrder!=null)
            {
                OrdersToRemove.Add(newOrder.order_id.ToString());
                lastCreatedOrder = newOrder;
            }
        }

        private void PrintExampleOrder(object result, string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType()==typeof(Order))
                {
                    Console.WriteLine("Order ID: {0}", ((Order)result).order_id);
                }
                else if (result.GetType() == typeof(Order[]))
                {
                    foreach (Order ord in (Order[])result)
                    {
                        Console.WriteLine("Order ID: {0}", ord.order_id);
                    }
                }
                else
                {
                    if (result.GetType() == typeof(GetOrdersResponse))
                    {
                        foreach (Order ord in (Order[])((GetOrdersResponse)result).Results)
                        {
                            Console.WriteLine("Order ID: {0}", ord.order_id);
                        }
                    }
                    else if (result.GetType() == typeof(SearchOrdersResponse))
                    {
                        var fieldValueList = (object[])((SearchOrdersResponse)result).Results;

                        foreach (object fieldValues in fieldValueList)
                        {
                            string fieldValueString = "";

                            foreach (object fieldValue in (object[])fieldValues)
                            {
                                fieldValueString += fieldValue.ToString() + ",";
                            }

                            fieldValueString = fieldValueString.TrimEnd(',');

                            Console.WriteLine("Field values: {0}", fieldValueString);
                        }

                        Console.WriteLine("");

                        foreach (string fieldName in ((SearchOrdersResponse)result).Fields)
                        {
                            Console.WriteLine(fieldName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong orders search response type");
                    }
                }
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        private void RemoveTestOrders()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            if (OrdersToRemove == null || OrdersToRemove.Count < 1) return;

            bool removed = route4Me.RemoveOrders(OrdersToRemove.ToArray(), out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                removed
                ? "The test orders removed"
                : "Cannot remove the test orders"
            );

            lastCreatedOrder = null;
        }

        #endregion
    }
}
