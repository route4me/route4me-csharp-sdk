using Route4MeSDK.DataTypes;
using Route4MeSDK.Examples;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Route4MeSDKTest
{
    [TestFixture]
    public class DumbTest
    {
        [Test]
        public void TestNothing()
        {
            Assert.That(true, Is.True);
        }
    }

    internal sealed class Program
    {
        static void Main(string[] args)
        {
            Route4MeExamples examples = new Route4MeExamples();

            DataObject dataObject = null;

            examples.SingleDriverRoute10Stops();

            examples.ResequenceRouteDestinations();
            examples.ResequenceReoptimizeRoute();

            examples.AddRouteDestinations();
            examples.RemoveRouteDestination();

            examples.SingleDriverRoundTrip();
            examples.MoveDestinationToRoute();

            examples.MultipleDepotMultipleDriver();
            examples.MultipleDepotMultipleDriverTimeWindow();
            examples.SingleDepotMultipleDriverNoTimeWindow();
            examples.MultipleDepotMultipleDriverWith24StopsTimeWindow();
            examples.SingleDriverMultipleTimeWindows();

            examples.GetOptimization();

            examples.GetOptimizations();

            examples.AddDestinationToOptimization();

            examples.RemoveDestinationFromOptimization();

            examples.ReOptimization();

            examples.UpdateRoute();
            examples.ReoptimizeRoute();
            examples.GetRoute();
            examples.GetRoutes();

            examples.GetUsers();

            examples.LogCustomActivity();
            examples.GetActivities();

            examples.GetAddress();

            examples.AddAddressNote();
            examples.AddAddressNoteWithFile();
            examples.GetAddressNotes();

            examples.DuplicateRoute();

            examples.RemoveOptimization();

            //disabled by default, not necessary for optimization tests
            //not all accounts are capable of storing gps data
            /*if (routeId_SingleDriverRoute10Stops != null)
            {
              examples.SetGPSPosition(routeId_SingleDriverRoute10Stops);
              examples.TrackDeviceLastLocationHistory(routeId_SingleDriverRoute10Stops);
            }
            else
            {
              System.Console.WriteLine("SetGPSPosition, TrackDeviceLastLocationHistory not called. routeId_SingleDriverRoute10Stops == null.");
            }*/

            List<string> routeIdsToDelete = new List<string>();

            if (routeIdsToDelete.Count > 0)
                examples.DeleteRoutes(routeIdsToDelete.ToArray());
            else
                Console.WriteLine("routeIdsToDelete.Count == 0. DeleteRoutes not called.");

            // Remove optimization
            // Address Book
            examples.CreateTestContacts();

            AddressBookContact contact1 = examples.contact1;
            AddressBookContact contact2 = examples.contact2;

            examples.GetAddressBookContacts();
            if (contact1 != null)
            {
                contact1.last_name = "Updated " + (new Random()).Next().ToString();
                examples.UpdateAddressBookContact(contact1);
            }
            else
            {
                Console.WriteLine("contact1 == null. UpdateAddressBookContact not called.");
            }

            List<string> addressIdsToRemove = new List<string>();

            if (contact1 != null)
                addressIdsToRemove.Add(contact1.address_id.ToString());
            if (contact2 != null)
                addressIdsToRemove.Add(contact2.address_id.ToString());

            examples.RemoveAddressBookContacts(addressIdsToRemove.ToArray());


            // Avoidance Zones
            string territoryId = examples.AddAvoidanceZone();

            examples.GetAvoidanceZones();

            if (territoryId != null)
                examples.GetAvoidanceZone(territoryId);
            else
                Console.WriteLine("GetAvoidanceZone not called. territoryId == null.");

            if (territoryId != null)
                examples.UpdateAvoidanceZone(territoryId);
            else
                Console.WriteLine("UpdateAvoidanceZone not called. territoryId == null.");

            if (territoryId != null)
                examples.DeleteAvoidanceZone(territoryId);
            else
                Console.WriteLine("DeleteAvoidanceZone not called. territoryId == null.");


            // Orders
            examples.AddOrder();
            examples.GetOrders();
            examples.UpdateOrder();
            examples.RemoveOrders();

            examples.GenericExample();
            examples.GenericExampleShortcut();

            Console.WriteLine("");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
