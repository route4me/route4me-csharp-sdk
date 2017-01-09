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

            // ======== Remove Territory ===================================
             examples.UpdateTerritory();
            // ======================================================================

            // ======== Remove Territory ===================================
            // examples.RemoveTerritory();
            // ======================================================================

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
            //examples.MergeRoutes(new string[2] { "56E8F6BF949670F0C0BBAC00590FD116", "A6DAA07A7D4737723A9C85E7C3BA2351" });
            //===================================================================

            System.Console.WriteLine("");
            System.Console.WriteLine("Press any key");
            System.Console.ReadKey();
        }
    }
}
