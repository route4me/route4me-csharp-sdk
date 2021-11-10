using Microsoft.VisualStudio.TestTools.UnitTesting;
using Route4MeSDK.DataTypes;
using R4mActivity = Route4MeSDK.DataTypes.Activity;
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

        public List<string> AvoidanceZonesToRemove = new List<string>();
        public List<string> TerritoryZonesToRemove = new List<string>();
        public List<string> ContactsToRemove;
        public List<string> RoutesToRemove;
        public List<string> OptimizationsToRemove;
        public List<string> addressBookGroupsToRemove;
        public List<string> configKeysToRemove = new List<string>();
        public List<string> CustomNoteTypesToRemove = new List<string>();
        public List<string> OrdersToRemove = new List<string>();
        public List<int> OrderCustomFieldsToRemove = new List<int>();

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
        TerritoryZone territoryZone;

        List<string> usersToRemove = new List<string>();
        MemberResponseV4 lastCreatedUser;

        List<string> vehiclesToRemove = new List<string>();

        #region Optimizations, Routes, Destinations

        private void PrintExampleRouteResult(object dataObjectRoute, string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (dataObjectRoute == null)
            {
                Console.WriteLine("{0} error {1}", testName, errorString);
            }
            else if (dataObjectRoute.GetType() == typeof(DataObjectRoute[]))
            {
                Console.WriteLine("{0} executed successfully", testName);

                foreach (var route in (DataObjectRoute[])dataObjectRoute)
                {
                    Console.WriteLine("Route ID: {0}", route.RouteId);
                }
            }
            else
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
                    route1 != null ? route1.OptimizationProblemId : route2.OptimizationProblemId
                    );

                Console.WriteLine("");

                if (route1 != null)
                {
                    route1.Addresses.ForEach(address =>
                    {
                        Console.WriteLine("Address: {0}", address.AddressString);
                        Console.WriteLine("Route ID: {0}", address.RouteId);
                    });

                    if ((route1?.Directions?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");

                        Console.WriteLine(
                            String.Format("Directions length: {0}",
                            route1.Directions.Length)
                        );
                    }

                    if ((route1?.Path?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");

                        Console.WriteLine(
                            String.Format("Path points: {0}",
                            route1.Path.Length)
                        );
                    }
                }
                else
                {
                    route2.Addresses.ForEach(address =>
                    {
                        Console.WriteLine("Address: {0}", address.AddressString);
                        Console.WriteLine("Route ID: {0}", address.RouteId);
                    });

                    if ((route2?.Directions?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");

                        Console.WriteLine(
                            String.Format("Directions length: {0}",
                            route2.Directions.Length)
                        );
                    }

                    if ((route2?.Path?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");

                        Console.WriteLine(
                            String.Format("Path points: {0}",
                            route2.Path.Length)
                        );
                    }
                }
            }
        }

        private void PrintExampleOptimizationResult(object dataObject, string errorString)
        {
            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("{0} executed successfully", testName);
                Console.WriteLine("");

                if (dataObject.GetType() == typeof(DataObject))
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

        /// <summary>
        /// Console print of an example results.
        /// </summary>
        /// <param name="obj">An Address type object, or boolean value</param>
        /// <param name="errorString">Error string</param>
        private void PrintExampleDestination(object obj, string errorString = "")
        {
            if (obj == null && obj.GetType() != typeof(Address) && obj.GetType() != typeof(bool))
            {
                Console.WriteLine("Wrong address. Cannot print." + Environment.NewLine + errorString);
                return;
            }

            Console.WriteLine("");

            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (obj.GetType() == typeof(Address))
            {
                var address = (Address)obj;

                if (address.RouteDestinationId!=null)
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

        private bool RunOptimizationSingleDriverRoute10Stops(string routeName = null)
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
                RouteName =
                    routeName == null
                    ? "SD Route 10 Stops Test " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    : routeName,
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
                SD10Stops_route_id = (SD10Stops_route != null) ? SD10Stops_route.RouteId : null;

                if (dataObjectSD10Stops != null && dataObjectSD10Stops.Routes != null && dataObjectSD10Stops.Routes.Length > 0)
                {
                    SD10Stops_route =  dataObjectSD10Stops.Routes[0];
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
                SDRT_route_id = (SDRT_route != null) ? SDRT_route.RouteId : null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Round Trip generation failed... " + ex.Message);
                return false;
            }
        }

        #endregion

        private void PrintExampleActivities(R4mActivity[] activities, string errorString = "")
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

        #region Address Book Contacts

        public void CreateTestContacts()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var contact = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814
            };

            // Run the query
            contact1 = route4Me.AddAddressBookContact(contact, out string errorString);

            Assert.IsNotNull(contact1, "AddAddressBookContactsTest failed... " + errorString);

            int location1 = contact1.AddressId != null ? Convert.ToInt32(contact1.AddressId) : -1;

            ContactsToRemove = new List<string>();

            if (location1 > 0) ContactsToRemove.Add(location1.ToString());

            var dCustom = new Dictionary<string, string>()
            {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            };

            contact = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = dCustom
            };

            contact2 = route4Me.AddAddressBookContact(contact, out errorString);

            Assert.IsNotNull(contact2, "AddAddressBookContactsTest failed... " + errorString);

            int location2 = contact2.AddressId != null ? Convert.ToInt32(contact2.AddressId) : -1;

            if (location2 > 0) ContactsToRemove.Add(location2.ToString());

            var contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName Rem" + (new Random()).Next().ToString(),
                Address1 = "Test Address1 Rem " + (new Random()).Next().ToString(),
                CachedLat = 38.02466,
                CachedLng = -77.33882
            };

            contactToRemove = route4Me.AddAddressBookContact(contactParams, out errorString);

            if (contactToRemove!=null && contactToRemove.GetType()==typeof(AddressBookContact))
                ContactsToRemove.Add(contactToRemove.AddressId.ToString());
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
                        ContactsToRemove.Add(contactToRemove.AddressId.ToString());
                    
                    bool removed = route4Me.RemoveAddressBookContacts(ContactsToRemove.ToArray(), out string errorString);
                    ContactsToRemove = new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot remove test contacts."+Environment.NewLine+ex.Message);
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

                Console.WriteLine("AddressId: {0}", resultContact.AddressId);

                Console.WriteLine("Custom data:");

                foreach (var cdata in (Dictionary<string, string>)resultContact.AddressCustomData)
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
            int location1 = contactResponse.AddressId != null
                ? Convert.ToInt32(contactResponse.AddressId)
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

        private void RemoveAvoidanceZones()
        {
            if ((AvoidanceZonesToRemove?.Count ?? 0) < 1) return;

            foreach (string avZone in AvoidanceZonesToRemove)
            {
                RemoveAvidanceZone(avZone);
            }
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


        #region Territories

        private void PrintExampleTerritory(object territory, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;

            Console.WriteLine("");

            if (territory != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (territory.GetType() == typeof(TerritoryZone))
                {
                    string territoryZoneId = territory.GetType() == typeof(TerritoryZone)
                    ? ((TerritoryZone)territory).TerritoryId
                    : territory.ToString();

                    Console.WriteLine("Territory ID: {0}", territoryZoneId);
                }
                else if (territory.GetType() == typeof(AvoidanceZone[]))
                {
                    var territories = (AvoidanceZone[])territory;

                    foreach (var terr in territories)
                    {
                        Console.WriteLine("Territory ID: {0}", terr.TerritoryId);
                    }
                }
                else if (territory.GetType() == typeof(TerritoryZone[]))
                {
                    var territories = (TerritoryZone[])territory;

                    foreach (var terr in territories)
                    {
                        Console.WriteLine("Territory ID: {0}", terr.TerritoryId);
                    }
                }
                else
                {
                    Console.WriteLine("Unexpected territory type");
                }
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        public bool RemoveTestTerritoryZone(string territoryZoneId)
        {
            var terrZoneQuery = new AvoidanceZoneQuery()
            {
                TerritoryId = territoryZoneId
            };

            var route4Me = new Route4MeManager(ActualApiKey);

            bool deleted = route4Me.RemoveTerritory(terrZoneQuery, out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                deleted
                ? "The territory zone " + terrZoneQuery.TerritoryId + " removed successfully"
                : "Cannot remove territory zone " + terrZoneQuery.TerritoryId
                );

            return deleted;
        }

        private void RemoveTestTerritoryZones()
        {
            if ((TerritoryZonesToRemove?.Count ?? 0) < 1) return;

            foreach (string terrZone in TerritoryZonesToRemove)
            {
                RemoveTestTerritoryZone(terrZone);
            }
        }

        public void CreateTerritoryZone()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var territoryZoneParameters = new AvoidanceZoneParameters()
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
            territoryZone = route4Me.CreateTerritory(
                territoryZoneParameters,
                out string errorString);

            if (territoryZone != null)
            {
                if ((TerritoryZonesToRemove?.Count ?? 0) < 1)
                    TerritoryZonesToRemove = new List<string>();

                TerritoryZonesToRemove.Add(territoryZone.TerritoryId);
            }

            PrintExampleTerritory(territoryZone, errorString);
        }

        #endregion

        public void PrintExampleGeocodings(
            object result,
            GeocodingPrintOption printOption,
            string errorString)
        {
            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            switch (printOption)
            {
                case GeocodingPrintOption.Geocodings:
                    Console.WriteLine("");

                    if (result != null && result.ToString().Length > 0)
                    {
                        Console.WriteLine(testName + " executed successfully");
                    }
                    else
                    {
                        Console.WriteLine(testName + " error: {0}", errorString);
                    }
                    break;
                case GeocodingPrintOption.StreetData:
                case GeocodingPrintOption.StreetService:
                case GeocodingPrintOption.StreetZipCode:
                    Console.WriteLine("");

                    if (result != null && result.GetType() == typeof(ArrayList))
                    {
                        Console.WriteLine(testName + " executed successfully");
                        foreach (Dictionary<string, string> res1 in (ArrayList)result)
                        {

                            Console.WriteLine("Zipcode: " + res1["zipcode"]);
                            Console.WriteLine("Street name: " + res1["street_name"]);
                            Console.WriteLine("---------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine(testName + " error: {0}", errorString);
                    }
                    break;
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

            AddAddressBookGroupToRemoveList(addressBookGroup.GroupId);
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

                Console.WriteLine((response?.Status ?? false)
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
                    ConfigKey = "Test Config Key",
                    ConfigValue = "Test Config Value"
                },
            };

            // Run the query
            var result = route4Me.
                CreateNewConfigurationKey(parametersArray, out string errorString);

            Console.WriteLine(
                result.Result != null
                    ? "Created config key " + "Test Config Key"
                    : "Cannot create config key " + "Test Config Key." + Environment.NewLine + errorString
                );

            if ((result?.Result ?? null) != null) configKeysToRemove.Add("Test Config Key");
        }

        public void PrintConfigKey(MemberConfigurationResponse configResponse, string errorString = "")
        {
            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (configResponse != null)
            {
                Console.WriteLine(testName + " executed successfully");
                Console.WriteLine("Result: " + configResponse.Result);
                Console.WriteLine("Affected: " + configResponse.Affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        public void PrintConfigKey(MemberConfigurationDataResponse configDataResponse, string errorString = "")
        {
            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (configDataResponse != null)
            {
                Console.WriteLine(testName + " executed successfully");
                Console.WriteLine("Result: " + configDataResponse.Result);

                foreach (MemberConfigurationData mc_data in configDataResponse.Data)
                {
                    Console.WriteLine("member_id= " + mc_data.MemberId);
                    Console.WriteLine("config_key= " + mc_data.ConfigKey);
                    Console.WriteLine("config_value= " + mc_data.ConfigValue);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        public void RemoveConfigKeys()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            foreach (string configKey in configKeysToRemove)
            {
                var @params = new MemberConfigurationParameters { ConfigKey = configKey };

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

            if (note != null && note.GetType() == typeof(AddressNote))
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

            var response = route4Me.GetAllCustomNoteTypes(out string _);

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

                if (customNoteTypeId > 0)
                {
                    var removeResult = route4Me
                    .RemoveCustomNoteType((int)customNoteTypeId, out string errorString);

                    Console.WriteLine(
                        (removeResult != null && removeResult.GetType() == typeof(int))
                        ? "The custom note type " + customNoteTypeId + " removed"
                        : "Cannot remove the custom note type " + customNoteTypeId);
                }

            }
        }

        private void PrintExampleCustomNoteType(object response, string errorString = "")
        {
            string testName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
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

            var response = route4Me.GetAllCustomNoteTypes(out string _);

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
                Address1 = "318 S 39th St, Louisville, KY 40212, USA",
                CachedLat = 38.259326,
                CachedLng = -85.814979,
                CurbsideLat = 38.259326,
                CurbsideLng = -85.814979,
                AddressAlias = "318 S 39th St 40212",
                AddressCity = "Louisville",
                ExtFieldFirstName = "Lui",
                ExtFieldLastName = "Carol",
                ExtFieldEmail = "lcarol654@yahoo.com",
                ExtFieldPhone = "897946541",
                ExtFieldCustomData = new Dictionary<string, string>() { { "order_type", "scheduled order" } },
                DayScheduledFor_YYYYMMDD = DateTime.Now.ToString("yyyy-MM-dd"),
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out string errorString);

            if (newOrder != null)
            {
                OrdersToRemove.Add(newOrder.OrderId.ToString());
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

                if (result.GetType() == typeof(Order))
                {
                    Console.WriteLine("Order ID: {0}", ((Order)result).OrderId);
                }
                else if (result.GetType() == typeof(Order[]))
                {
                    foreach (Order ord in (Order[])result)
                    {
                        Console.WriteLine("Order ID: {0}", ord.OrderId);
                    }
                }
                else
                {
                    if (result.GetType() == typeof(GetOrdersResponse))
                    {
                        foreach (Order ord in (Order[])((GetOrdersResponse)result).Results)
                        {
                            Console.WriteLine("Order ID: {0}", ord.OrderId);
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

            lastCreatedOrder = null;
        }

        #endregion

        #region Order Custom Users Fields

        private void CreateTestOrderCustomUserField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters()
            {
                OrderCustomFieldName = "CustomField77",
                OrderCustomFieldLabel = "Custom Field 77",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>()
                {
                    {"short_label", "cFl77" },
                    {"description", "This is test order custom field" },
                    {"custom field no", 11 }
                }
            };

            var orderCustomUserField = route4Me.CreateOrderCustomUserField(
                    orderCustomFieldParams,
                    out string errorString
                );

            if ((orderCustomUserField?.Data?.OrderCustomFieldId ?? -1) > 0)
                OrderCustomFieldsToRemove.Add(orderCustomUserField.Data.OrderCustomFieldId);

            PrintOrderCustomField(orderCustomUserField, errorString);
        }

        private void PrintOrderCustomField(object result, string errorString = "")
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(OrderCustomFieldCreateResponse))
                {
                    var ocfResponse = (OrderCustomFieldCreateResponse)result;

                    if ((ocfResponse?.Data?.OrderCustomFieldId ?? -1) > 0)
                    {
                        Console.WriteLine(
                        "Order Custom user field ID: {0}",
                        ocfResponse.Data.OrderCustomFieldId);
                    }
                    else
                    {
                        Console.WriteLine(
                        "Order Custom user fields affected: {0}",
                        ocfResponse.Affected);
                    }
                }
                else
                {
                    foreach (var customField in (OrderCustomField[])result)
                    {
                        Console.WriteLine(
                            "Order Custom user field ID: {0}",
                            customField.OrderCustomFieldId
                        );
                    }
                }
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        private void RemoveTestOrderCustomField()
        {
            if (OrderCustomFieldsToRemove == null || OrderCustomFieldsToRemove.Count < 1) return;

            var route4Me = new Route4MeManager(ActualApiKey);

            foreach (int fieldId in OrderCustomFieldsToRemove)
            {
                var orderCustomFieldParams = new OrderCustomFieldParameters()
                {
                    OrderCustomFieldId = fieldId
                };

                var response = route4Me.RemoveOrderCustomUserField(
                    orderCustomFieldParams,
                    out string errorString);

                Console.WriteLine(
                    (response?.Affected ?? 0) > 0
                    ? String.Format("The custom field {0} removed.", fieldId)
                    : String.Format("Cannot remove the custom field {0}.", fieldId));
            }
        }

        #endregion

        #region Telematics GateWay API

        private void PrintExampleTelematicsVendor(object result, string errorString)
        {
            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(TelematicsVendorResponse))
                {
                    Console.WriteLine("Vendor :" + ((TelematicsVendorResponse)result).Vendor.Name);
                }
                else
                {
                    foreach (var vendor in ((TelematicsVendorsResponse)result).Vendors)
                    {
                        Console.WriteLine("Vendor :" + vendor.Name);
                    }
                }
            }
            else
            {
                Console.WriteLine(testName + " error: {0}", errorString);
            }
        }

        #endregion

        #region Users

        public void CreateTestUser()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            string userFirstName = "";
            string userLastName = "";
            string userPhone = "";

            string memberType = "SUB_ACCOUNT_DISPATCHER";

            switch (memberType)
            {
                case "SUB_ACCOUNT_DISPATCHER":
                    userFirstName = "Clay";
                    userLastName = "Abraham";
                    userPhone = "571-259-5939";
                    break;
                case "SUB_ACCOUNT_DRIVER":
                    userFirstName = "Driver";
                    userLastName = "Driverson";
                    userPhone = "577-222-5555";
                    break;
            }

            var @params = new MemberParametersV4()
            {
                HIDE_ROUTED_ADDRESSES = "FALSE",
                member_phone = userPhone,
                member_zipcode = "22102",
                member_email = "regression.autotests+" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com",
                HIDE_VISITED_ADDRESSES = "FALSE",
                READONLY_USER = "FALSE",
                member_type = memberType,
                date_of_birth = "2010",
                member_first_name = userFirstName,
                member_password = "123456",
                HIDE_NONFUTURE_ROUTES = "FALSE",
                member_last_name = userLastName,
                SHOW_ALL_VEHICLES = "FALSE",
                SHOW_ALL_DRIVERS = "FALSE"
            };

            var result = route4Me.CreateUser(@params, out string errorString);

            if (result != null && result.GetType() == typeof(MemberResponseV4))
            {
                usersToRemove.Add(result.MemberId);
                lastCreatedUser = result;
            }
        }

        private void PrintTestUsers(object result, string errorString)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(MemberResponseV4))
                {
                    var user = (MemberResponseV4)result;
                    Console.WriteLine("Member: {0}", user.MemberFirstName + " " + user.MemberLastName);
                }
                else if (result.GetType() == typeof(Route4MeManager.GetUsersResponse))
                {
                    var users = ((Route4MeManager.GetUsersResponse)result).Results;

                    foreach (var user in users)
                    {
                        Console.WriteLine("Member: {0}", user.MemberFirstName + " " + user.MemberLastName);
                    }
                }
                else if (result.GetType() == typeof(MemberResponse))
                {
                    var result2 = (MemberResponse)result;

                    Console.WriteLine("status: " + result2.Status);
                    Console.WriteLine("api_key: " + result2.ApiKey);
                    Console.WriteLine("member_id: " + result2.MemberId);
                    Console.WriteLine("---------------------------");
                }
                else
                {
                    Console.WriteLine(testName + ": unknown response type");
                }
            }
            else
            {
                Console.WriteLine("{0} error: {1}", testName, errorString);
            }
        }

        private void RemoveTestUsers()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            if (usersToRemove == null || usersToRemove.Count < 1) return;

            foreach (var userId in usersToRemove)
            {
                var @params = new MemberParametersV4 { member_id = Convert.ToInt32(userId) };

                bool result = route4Me.UserDelete(@params, out string errorString);

                Console.WriteLine(
                    result
                    ? String.Format("The user {0} removed successfully.", userId)
                    : String.Format("Cannot remove the user {0}.", userId)
                );
            }
        }

        #endregion

        #region Vehicles

        private void CreateTestVehcile()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var class6TruckParams = new VehicleV4Parameters()
            {
                VehicleName = "GMC TopKick C5500",
                VehicleAlias = "GMC TopKick C5500",
                VehicleVin = "SAJXA01A06FN08012",
                VehicleLicensePlate = "CVH4561",
                VehicleModel = "TopKick C5500",
                VehicleModelYear = 1995,
                VehicleYearAcquired = 2008,
                VehicleRegCountryId = 223,
                VehicleMake = "GMC",
                VehicleTypeID = "pickup_truck",
                VehicleAxleCount = 2,
                MpgCity = 7,
                MpgHighway = 14,
                FuelType = "diesel",
                HeightInches = 97,
                HeightMetric = 243,
                WeightLb = 19000,
                MaxWeightPerAxleGroupInPounds = 9500,
                MaxWeightPerAxleGroupMetric = 4300,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 244,
                LengthMetric = 610,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "NO",
                DividedHighwayAvoidPreference = "NEUTRAL",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "FULLSIZEVAN"
            };

            var result = route4Me.CreateVehicle(class6TruckParams, out string errorString);

            if (result != null && result.GetType() == typeof(VehicleV4CreateResponse))
            {
                Console.WriteLine("The test vehicle {0} created successfully.", result.VehicleGuid);

                vehiclesToRemove.Add(result.VehicleGuid);
            }
            else
            {
                Console.WriteLine("Cannot create a test vehicle");
            }
        }

        private void PrintTestVehciles(object result, string errorString)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(VehicleV4CreateResponse))
                {
                    var vehicle = (VehicleV4CreateResponse)result;

                    Console.WriteLine(
                        "Vehicle ID: {0}, Status: {1}",
                        vehicle.VehicleGuid,
                        vehicle.status
                    );
                }
                else if (result.GetType() == typeof(VehiclesPaginated))
                {
                    var vehicles = ((VehiclesPaginated)result).Data;

                    foreach (var vehicle in vehicles)
                    {
                        Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                        );
                    }
                }
                else if (result.GetType() == typeof(VehicleV4Response))
                {
                    var vehicle = (VehicleV4Response)result;

                    Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                    );
                }
                else
                {
                    Console.WriteLine(testName + ": unknown response type");
                }
            }
            else
            {
                Console.WriteLine("{0} error: {1}", testName, errorString);
            }
        }

        private void RemoveTestVehicles()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            if ((vehiclesToRemove?.Count ?? 0) < 1) return;

            foreach (var vehicleId in vehiclesToRemove)
            {
                var vehicleParams = new VehicleV4Parameters()
                {
                    VehicleId = vehicleId
                };

                var result = route4Me.DeleteVehicle(vehicleParams, out string errorString);

                Console.WriteLine(
                    (result != null && result.GetType() == typeof(VehicleV4Response))
                    ? String.Format("The vehicle {0} removed successfully.", vehicleId)
                    : String.Format("Cannot remove the vehicle {0}.", vehicleId)
                );
            }
        }

        #endregion
    }
}
