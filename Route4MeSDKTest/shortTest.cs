using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Examples;
using System;
using System.Collections.Generic;

namespace Route4MeSDKTest
{
    public class shortTest
    {
        public static void TestRun()
        {
            Route4MeExamples examples = new Route4MeExamples();

            // Note run an example by uncommenting code lines between appropriate //=======.... lines

            // ======== COnverting XML file to JSON string ================
                //Dictionary<string, string> customData = new Dictionary<string,string>();
                //customData.Add("BatchId","e7c672b1-a356-4a97-803e-97db88fdcf99");
                //customData.Add("CustomerNumber", "2718500");
                //customData.Add("DeliveryId", "2c71f6d9-c1aa-4672-a682-3e9f12badac9");
                //customData.Add("DeliveryInvoices", "<?xml version=\"1.0\" encoding=\"utf-16\"?>\u000d\u000a<!DOCTYPE PAMSDelivery SYSTEM \"PAMSDelivery.dtd\">\u000d\u000a<ArrayOfRouteDeliveryInvoice>\u000d\u000a  <RouteDeliveryInvoice>\u000d\u000a    <InvoiceNumber>945822</InvoiceNumber>\u000d\u000a    <InventoryIds>\u000d\u000a      <string>1790908</string>\u000d\u000a    </InventoryIds>\u000d\u000a    <IsRA>false</IsRA>\u000d\u000a    <IsDT>false</IsDT>\u000d\u000a    <IsINC>true</IsINC>\u000d\u000a    <IsPO>false</IsPO>\u000d\u000a    <IsPOPickup>false</IsPOPickup>\u000d\u000a  </RouteDeliveryInvoice>\u000d\u000a</ArrayOfRouteDeliveryInvoice>");
                //customData.Add("DeliveryNotes", "");
                //customData.Add("RouteId", "20191");
                examples.UpdateRouteDestination();
            //

            // ======== Converting XML file to JSON string ================
                //examples.GetActivities("824CA521E3A8DE9F1C684C8BAE90CF07");
                //examples.GetRouteTeamActivities();
                //examples.LogCustomActivity("Custom message", "824CA521E3A8DE9F1C684C8BAE90CF07");
                //examples.SearchAreaUpdated();
                //examples.SearchAreaAdded();
                //examples.SearchAreaRemoved();
                //examples.SearchDestinationDeleted();
                //examples.SearchDestinationInserted();
                //examples.SearchDestinationMarkedAsDeparted();
                //examples.SearchDestinationOutSequence();
                //examples.SearchDestinationUpdated();
                //examples.SearchDriverArrivedEarly();
                //examples.SearchDriverArrivedLate();
                //examples.SearchDriverArrivedOnTime();
                //examples.SearchGeofenceEntered();
                //examples.SearchGeofenceLeft();
                //examples.SearchInsertDestinationAll();
                //examples.SearchMarkDestinationDepartedAll();
                //examples.SearchMarkDestinationVisited();
                //examples.SearchMemberCreated();
                //examples.SearchMemberDeleted();
                //examples.SearchMemberModified();
                //examples.SearchMoveDestination();
                //examples.SearchNoteInserted();
                //examples.SearchNoteInsertedAll();
                //examples.SearchRouteDeleted();
                //examples.SearchRouteOptimized();
                //examples.SearchRouteOwnerChanged();
            //

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

            // ======== Add address book contact to an acoount ============================
            //  examples.AddAddressBookContact();
            //

            // ======== Upload addressbook loactions JSON file to the SQL server ==========
                //examples.UploadAddressbookJSONtoSQL(DB_Type.MySQL);
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

            // ======== Get Hybrid Optimization ===================================
            //  examples.HybridOptimizationFrom1000Orders();
            // 

            // ======== Get Hybrid Optimization ===================================
            // examples.HybridOptimizationFrom1000Addresses();
            // ==============================================================

            // ======== Update Configuration Key ===================================
            //  examples.UpdateConfigurationKey();
            // ==============================================================

            // ======== Remove Configuration Key ===================================
            // examples.RemoveConfigurationKey();
            // ==============================================================

            // ======== Get Specific Configuration Key Data =================
            // examples.GetSpecificConfigurationKeyData();
            // ==============================================================

             // ======== Get All Configuration Data =========================
            // examples.GetAllConfigurationData();
            // ==============================================================

             // ======== Create New Configuration Key =======================
            // examples.AddNewConfigurationKey();
            // ==============================================================

            // ======== Update Territory ===================================
            // examples.UpdateTerritory();
            // ==============================================================

            // ======== Remove Territory ===================================
            // examples.RemoveTerritory();
            // =============================================================

            // ======== Get Territory ===================================
            // examples.GetTerritory();
            // ======================================================================

            // ======== Get Territories ===================================
            // examples.GetTerritories();
            // ======================================================================

            // ======== Create a Territory with Rectangular Shape ===================
            // examples.CreateRectTerritory();
            // ======================================================================

            // ======== Create a Territory with Polygon Shape ===========================
            // examples.CreatePolygonTerritory();
            // ======================================================================

            // ======== Create a Territory with Circular Shape ===========================
            // examples.CreateTerritory();
            // ======================================================================

            // ======== Add Rectangular Avoidance Zone  ===========================
            // examples.AddRectAvoidanceZone();
            // ======================================================================

            // ======== Add Polygon Avoidance Zone  ===========================
            // examples.AddPolygonAvoidanceZone();
            // ======================================================================

            // ======== Search Routed Locations  ===========================
            //  examples.SearchRoutedLocations();
            // ======================================================================

            // ======== Search Locations By IDs  ===========================
            // examples.SearchLocationsByIDs();
            // ======================================================================

            // ======== Get Addressbook Specified Fields Filtered by Text in Any Field  ===========================
            // examples.GetSpecifiedFieldsSearchText();
            // ======================================================================

            // ======== Get Addressbook Locations By Text In Any Field ===========================
            // examples.GetAddressbookLocation();
            // ======================================================================

            // ======== Get Team Activities on a Route  ===========================
            // examples.GetRouteTeamActivities();
            // ======================================================================

            // ======== Search Area Added Activiities ===========================
            // examples.SearchAreaAdded();
            // ======================================================================

            // ======== Get Vehicles ===========================
            // examples.GetVehicles();
            // ======================================================================

            // ======== Validate Session ===========================
            // examples.ValidateSession();
            // ======================================================================

            // ======== User Registratin ===========================
            //examples.UserRegistration();
            // ======================================================================

            // ======== User Authentication ===========================
            //examples.UserAuthentication();
            // ======================================================================

            // ======== Update an User  ===========================
            //examples.UpdateUser();
            // ======================================================================

            // ======== Get USer By ID  ===========================
            //examples.GetUserById();
            // ======================================================================

            // ======== Delete an User  ===========================
            //examples.DeleteUser();
            // ======================================================================

            // ======== Create an User  ===========================
            //examples.CreateUser();
            // ======================================================================

            // ======== Get Device History from Time Range  ============
            //examples.GetDeviceHistoryTimeRange("814FB49CEA8188D134E9D4D4B8B0DAF7");
            // ======================================================================

            // ======== Find Asset (Asset Tracking) ===========================
             //examples.FindAsset();
            // ======================================================================

            // ======== Rapid Stret Service Limited ===========================
            //examples.RapidStreetServiceLimited();
            // =====================================================================

            // ======== Rapid Stret Service All ===========================
            //examples.RapidStreetServiceAll();
            // ======================================================================

            // ======== Rapid Stret Zipcode Limited ===========================
            //examples.RapidStreetZipcodeLimited();
            // ======================================================================

            // ======== Rapid Stret Zipcode All ===========================
            //examples.RapidStreetZipcodeAll();
            // ======================================================================

            // ======== Rapid Stret Data Single ===========================
            //examples.RapidStreetDataSingle();
            // ======================================================================

            // ======== Rapid Stret Data Limited ===========================
            //examples.RapidStreetDataLimited();
            // ======================================================================

            // ======== Rapid Stret Data All ===========================
            //examples.RapidStreetDataAll();
            //======================================================================

            // ======== Reverse Geocoding ===========================
            //xexamples.ReverseGeocoding();
            //=======================================================

            // ======== Forward Geocoding ===========================
            //GeocodingParameters geoParams = new GeocodingParameters
            //{
            //    Addresses = "Los20%Angeles20%International20%Airport,20%CA",
            //    Format = "xml"
            //};
            //examples.GeocodingForward(geoParams);
            //======================================================================

            // ======== Mark Address As Marked As Departed ===========================
            //AddressParameters aParams = new AddressParameters
            //{
            //    RouteId = "241466F15515D67D3F951E2DA38DE76D",
            //    RouteDestinationId = 167899269,
            //    IsDeparted = true
            //};
            //examples.MarkAddressAsMarkedAsDeparted(aParams);
            //======================================================================

            // ======== Mark Address As Marked As Visited ===========================
            //AddressParameters aParams = new AddressParameters
            //{
            //    RouteId = "241466F15515D67D3F951E2DA38DE76D",
            //    RouteDestinationId = 167899269,
            //    IsVisited = true
            //};
            //examples.MarkAddressAsMarkedAsVisited(aParams);
            //======================================================================

            // ======== Mark Address As Departed ===========================
            //AddressParameters aParams = new AddressParameters
            //{
            //    RouteId = "DD376C7148E7FEE36CFABE2BD9978BDD",
            //    AddressId = 183045808,
            //    IsDeparted = true
            //};
            //examples.MarkAddressDeparted(aParams);
            //======================================================================

            // ======== Mark Address As Visited ===========================
            //AddressParameters aParams = new AddressParameters
            //{
            //    RouteId = "DD376C7148E7FEE36CFABE2BD9978BDD",
            //    AddressId = 183045808,
            //    IsVisited = true
            //};
            //examples.MarkAddressVisited(aParams);
            //======================================================================

            //======== Get the Orders by containing specified text in any field =======
            //string query = "Luzerne";
            //examples.GetOrdersBySpecifiedText(query);
            //======================================================================

            //======== Show the Orders using values of the specified fields  =======
            //string CustomFields = "order_id,member_id";
            //examples.GetOrdersByCustomFields(CustomFields);
            //======================================================================

            //======== Get the Orders by Scheduled Date ===========================
            //string scheduledDate = "2016-12-20";
            //examples.GetOrderByScheduledDate(scheduledDate);
            //======================================================================

            //======== Get the Orders by Inserted Date ===========================
            //string InsertedDate = "2016-12-18";
            //examples.GetOrderByInsertedDate(InsertedDate);
            //======================================================================

            //======== Get an Order Details by order_id ===========================
            //string OrderIds = "437,438,439";
            //examples.GetOrderByID(OrderIds);
            //======================================================================

            //======== Add Orders To a Route ===========================
            //examples.AddOrdersToOptimization();
            //======================================================================

            //======== Add Orders To a Route ===========================
            //examples.AddOrdersToRoute();
            //======================================================================

            //======== Search Routes For Text  ============
            //examples.SearchRoutesForText("Tbilisi");
            //===================================================================

            //======== Update Route Custom Data  ============
            //string RouteId = "CA902292134DBC134EAF8363426BD247";
            //int RouteDestinationId = 174405640;

            //Dictionary<string, string> CustomData = new Dictionary<string, string>();
            //CustomData.Add("animal", "tiger");
            //CustomData.Add("bird", "canary");
            //examples.UpdateRouteCustomData(RouteId, RouteDestinationId, CustomData);
            //===================================================================

            //======== Route Sharing  ============
            //examples.RouteSharing("56E8F6BF949670F0C0BBAC00590FD116", "ooooooo@yahoo.com");
            //===================================================================

            //======== Routes Merging  ============
            // examples.MergeRoutes(new string[2] { "56E8F6BF949670F0C0BBAC00590FD116", "A6DAA07A7D4737723A9C85E7C3BA2351" });
            //===================================================================

            System.Console.WriteLine("");
            System.Console.WriteLine("Press any key");
            System.Console.ReadKey();
        }
    }
}
