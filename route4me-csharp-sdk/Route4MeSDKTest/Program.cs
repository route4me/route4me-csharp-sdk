using Route4MeSDK.Examples;
using System;

namespace Route4MeSDKTest
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            var examples = new Route4MeExamples();

            // Available values for the variable executeOption:
            // "api4" - execute all the examples related to the API 4 
            // "api5" - execute all the examples related to the API 5 
            // a method name - execute a specifed example method (e.g. "GetTeamMemberById")
            string executeOption = "UnlinkRouteFromOptimization";
            //object[] executeParams = new object[] { null }; // Uncomment if null or object array is sent as a parameter
            object[] executeParams = null; // Uncomment if nothing is sent

            if (executeOption.ToLower() == "api4")
            {
                #region API 4

                #region ==== Optimizations =====

                examples.GetOptimization();
                examples.GetOptimizationsByText();
                examples.GetOptimizationsFromDateRange();
                examples.OptimizationWithCallbackUrl();
                examples.GetOptimizations();
                examples.ReOptimization();
                examples.RemoveOptimization();
                examples.UpdateOptimizationDestination();

                examples.ExampleOptimization();
                examples.SingleDriverRoute7Stops();
                examples.SingleDriverRoundTripGeneric();

                examples.HybridOptimizationFrom1000Orders();
                examples.HybridOptimizationFrom1000Addresses();

                var dataobject = examples.AsyncMultipleDepotMultipleDriver().GetAwaiter().GetResult();

                #endregion

                #region Route Examples

                examples.BundledAddresses();
                examples.GetScheduleCalendar();
                examples.MultipleDepotMultipleDriverFineTuning();
                examples.MultipleDepotMultipleDriver();
                examples.MultipleDepotMultipleDriverTimeWindow();
                examples.MultipleDepotMultipleDriverWith24StopsTimeWindow();
                examples.MultipleSeparateDepostMultipleDriver();
                examples.Route300Stops();
                examples.SingleDriverRoute10Stops();
                examples.RouteSlowdown();
                examples.SingleDriverRoundTrip();
                examples.SingleDepotMultipleDriverNoTimeWindow();
                examples.SingleDriverMultipleTimeWindows();
                examples.SingleDriverRoundTripGeneric();

                #endregion

                #region ==== Route Addresses =====

                examples.MoveDestinationToRoute();
                examples.AddDestinationToOptimization();
                examples.UpdateRouteDestination();
                examples.AddRouteDestinations();
                examples.GetAddress();

                examples.MarkAddressAsMarkedAsDeparted();
                examples.MarkAddressAsMarkedAsVisited();
                examples.MarkAddressDeparted();
                examples.MarkAddressVisited();

                examples.RemoveDestinationFromOptimization();
                examples.RemoveRouteDestination();
                examples.ResequenceRouteDestinations();

                examples.AddRouteDestinationInSpecificPosition();

                #endregion

                #region ==== Address Notes ====

                examples.AddAddressNote();
                examples.AddAddressNoteWithFile();
                examples.AddComplexAddressNote();
                examples.AddCustomNoteToRoute();
                examples.AddCustomNoteType();
                examples.GetAddressNotes();
                examples.GetAllCustomNoteTypes();
                examples.RemoveCustomNoteType();

                #endregion

                #region ==== Routes ====

                examples.AssignMemberToRoute();
                examples.AssignVehicleToRoute();
                examples.ChangeRouteDepote();
                examples.DeleteRoutes();
                examples.DuplicateRoute();
                examples.GetRouteDirections();
                examples.GetRoutePathPoints();
                examples.GetRoute();
                examples.GetRoutesByIDs();
                examples.GetRoutesFromDateRange();
                examples.GetRoutes();
                examples.ReoptimizeRoute();
                examples.ResequenceReoptimizeRoute();
                examples.ResequenceRouteDestinations();
                examples.ReoptimizeRemainingStops();
                examples.RouteOriginParameter();
                examples.ShareRoute();
                examples.UnlinkRouteFromOptimization();
                examples.UpdateRouteAvoidanceZones();
                examples.UpdateRouteCustomData();
                examples.UpdateRoute();
                examples.UpdateRouteDestination();
                examples.UpdateWholeRoute();
                examples.SearchRoutesForText();
                examples.MergeRoutes();

                #endregion

                #region ==== Activities ====

                examples.GetActivities();
                examples.GetActivitiesByMember();
                examples.GetRouteTeamActivities();
                examples.GetLastActivities();
                examples.LogCustomActivity();
                examples.SearchAreaUpdated();
                examples.SearchAreaAdded();
                examples.SearchAreaRemoved();
                examples.SearchDestinationDeleted();
                examples.SearchDestinationInserted();
                examples.SearchDestinationMarkedAsDeparted();
                examples.SearchDestinationOutSequence();
                examples.SearchDestinationUpdated();
                examples.SearchDriverArrivedEarly();
                examples.SearchDriverArrivedLate();
                examples.SearchDriverArrivedOnTime();
                examples.SearchGeofenceEntered();
                examples.SearchGeofenceLeft();
                examples.SearchInsertDestinationAll();
                examples.SearchMarkDestinationDepartedAll();
                examples.SearchMarkDestinationVisited();
                examples.SearchMemberCreated();
                examples.SearchMemberDeleted();
                examples.SearchMemberModified();
                examples.SearchMoveDestination();
                examples.SearchNoteInserted();
                examples.SearchNoteInsertedAll();
                examples.SearchRouteDeleted();
                examples.SearchRouteOptimized();
                examples.SearchRouteOwnerChanged();

                examples.GetRouteTeamActivities();
                examples.SearchAreaAdded();

                #endregion

                #region ==== DataBase ====
                // ======== COnverting XML file to JSON string ================
                //    examples.convertXMLtoJSON();
                //

                // ======== Generating the SQL server tables ================
                //examples.GenerateSqlDatabase(DB_Type.MySQL);
                //examples.GenerateSqlDatabase(DB_Type.MSSQL);
                //examples.GenerateSqlDatabase(DB_Type.PostgreSQL);
                //

                // ======== Upload orders csv file to the SQL server ================
                //examples.UploadCsvToOrders(DB_Type.MySQL);
                //examples.UploadCsvToOrders(DB_Type.MSSQL);
                //examples.UploadCsvToOrders(DB_Type.PostgreSQL);
                //

                // ======== Upload orders JSON file to the SQL server =============================
                //examples.UploadOrdersJSONtoSQL(DB_Type.MySQL);
                //examples.UploadOrdersJSONtoSQL(DB_Type.MSSQL);
                //examples.UploadOrdersJSONtoSQL(DB_Type.PostgreSQL);
                //

                // ======== Upload addressbook loactions JSON file to the SQL server ==========
                //examples.UploadAddressbookJSONtoSQL(DB_Type.SQLCE);
                //examples.UploadAddressbookJSONtoSQL(DB_Type.MSSQL);
                //examples.UploadAddressbookJSONtoSQL(DB_Type.PostgreSQL);
                //

                // ======== Upload addressbook loactions csv file to the SQL server ================
                //examples.UploadCsvToAddressbookV4(DB_Type.MySQL);
                //examples.UploadCsvToAddressbookV4(DB_Type.MSSQL);
                //examples.UploadCsvToAddressbookV4(DB_Type.PostgreSQL);
                //

                // ======== Export SQL server addressbook_v4 table to csv file =======================
                //examples.MakeAddressbookCSVsample(DB_Type.MySQL);
                //examples.MakeAddressbookCSVsample(DB_Type.MySQL);
                //examples.MakeAddressbookCSVsample(DB_Type.MySQL);
                //


                #endregion

                #region ==== Address Book Contacts ====

                examples.AddAddressBookContact();
                examples.AddScheduledAddressBookContact();
                examples.GetAddressBookContacts();
                examples.SearchLocationsByText();

                examples.AddAddressBookContact();
                examples.UpdateAddressBookContact();
                examples.UpdateWholeAddressBookContact();

                examples.SearchRoutedLocations();
                examples.SearchLocationsByIDs();
                examples.RemoveAddressBookContacts();
                examples.GetSpecifiedFieldsSearchText();

                #endregion

                #region ==== Address Book Groups ====

                examples.AddAddressBookGroup();
                examples.GetAddressBookContactsByGroup();
                examples.GetAddressBookGroup();
                examples.GetAddressBookGroups();
                examples.RemoveAddressBookGroup();
                examples.SearchAddressBookContactsByFilter();
                examples.UpdateAddressBookGroup();

                #endregion

                #region ==== User Configuration ====

                examples.AddNewConfigurationKey();
                examples.AddConfigurationKeyArray();
                examples.GetAllConfigurationData();
                examples.GetSpecificConfigurationKeyData();
                examples.RemoveConfigurationKey();
                examples.UpdateConfigurationKey();

                #endregion

                #region ==== Territories ====

                examples.UpdateTerritory();
                examples.RemoveTerritory();
                examples.GetTerritory();
                examples.GetTerritories();
                examples.CreateRectTerritory();
                examples.CreatePolygonTerritory();
                examples.CreateTerritory();

                #endregion

                #region ==== Avoidance Zones ====

                examples.AddAvoidanceZone();
                examples.AddPolygonAvoidanceZone();
                examples.AddRectAvoidanceZone();
                examples.DeleteAvoidanceZone();
                examples.GetAvoidanceZone();
                examples.GetAvoidanceZones();
                examples.UpdateAvoidanceZone();

                #endregion

                #region ==== Vehicles ====

                examples.GetVehicles();
                examples.CreatetVehicle();
                examples.DeleteVehicle();
                examples.GetVehicle();
                examples.UpdateVehicle();

                #endregion

                #region ==== Users ====

                examples.ValidateSession();
                examples.UserRegistration();
                examples.UserAuthentication();
                examples.UpdateUser();
                examples.GetUserById();
                examples.DeleteUser();
                examples.CreateUser();

                #endregion

                #region ==== Tracking ====

                examples.AddEditCustomDataToUser();
                examples.CreateUser();
                examples.DeleteUser();
                examples.GetUserById();
                examples.GetUsers();
                examples.UpdateUser();
                examples.UserAuthentication();
                examples.UserRegistration();
                examples.ValidateSession();

                #endregion

                #region ==== Geocoding ====

                examples.GeocodingForward();
                examples.BatchGeocodingForward();
                examples.BatchGeocodingForwardAsync();
                examples.ReverseGeocoding();
                examples.uploadAndGeocodeLargeJsonFile();

                examples.RapidStreetDataAll();
                examples.RapidStreetDataLimited();
                examples.RapidStreetDataSingle();

                examples.RapidStreetServiceAll();
                examples.RapidStreetServiceLimited();

                examples.RapidStreetZipcodeAll();
                examples.RapidStreetZipcodeLimited();

                #endregion

                #region ==== Orders ====

                examples.AddOrder();
                examples.AddOrdersToOptimization();
                examples.AddOrdersToRoute();
                examples.AddScheduledOrder();
                examples.CreateOrderWithCustomField();
                examples.GetOrderByID();
                examples.GetOrdersByInsertedDate();
                examples.GetOrdersByScheduledDate();
                examples.GetOrdersByCustomFields();
                examples.GetOrdersByScheduleFilter();
                examples.GetOrdersBySpecifiedText();
                examples.RemoveOrders();
                examples.GetOrders();
                examples.UpdateOrder();
                examples.UpdateOrderWithCustomField();

                #endregion

                #region ==== Custom User Order Fields ====

                examples.CreateOrderCustomUserField();
                examples.GetOrderCustomUserFields();
                examples.RemoveOrderCustomUserField();
                examples.updateOrderCustomUserField();

                #endregion

                #region ==== Telematics Vendors ====

                examples.GetAllVendors();
                examples.GetVendor();
                examples.SearchVendors();
                examples.VendorsComparison();

                #endregion

                #endregion
            }
            else if (executeOption.ToLower() == "api5")
            {
                #region API 5

                #region Team Management

                examples.GetTeamMembers();
                examples.GetTeamMemberById();
                examples.RemoveTeamMember();
                examples.UpdateTeamMember();
                examples.CreateTeamMember();
                examples.BulkCreateTeamMembers();
                examples.AddSkillsToDriver();

                #endregion

                #region Driver Rating

                examples.GetDriverReviewList();
                examples.GetDriverReviewById();
                examples.CreateDriverReview();
                examples.UpdateDriverReview();

                #endregion

                #region Route Types

                examples.CreateOptimizationWithDriverSkills();

                #endregion

                #endregion
            }
            else // for a specifed example method
            {
                try
                {
                    typeof(Route4MeExamples).GetMethod(executeOption).Invoke(examples, executeParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(executeOption + " error: {0}", ex.Message);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
