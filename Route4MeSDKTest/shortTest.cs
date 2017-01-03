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

            //======== Update Route Custom Data  ============
            string RouteId = "CA902292134DBC134EAF8363426BD247";
            int RouteDestinationId = 174405640;

            Dictionary<string, string> CustomData = new Dictionary<string, string>();
            CustomData.Add("animal", "tiger");
            CustomData.Add("bird", "canary");
            examples.UpdateRouteCustomData(RouteId, RouteDestinationId, CustomData);
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
