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

            // ======== Mark Address As Departed ===========================
            AddressParameters aParams = new AddressParameters
            {
                RouteId = "DD376C7148E7FEE36CFABE2BD9978BDD",
                AddressId = 183045808,
                IsDeparted = true
            };
            examples.MarkAddressDeparted(aParams);
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
