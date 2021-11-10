using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating many parameters 
        /// simultaneously of the route destination by sending the HTTP parameters.
        /// </summary>
        public void UpdateRouteDestination()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            int routeDestionationId = (int)SD10Stops_route.Addresses[1].RouteDestinationId;

            var CustomData = new Dictionary<string, string>();

            CustomData.Add("BatchId", "e7c672b1-a356-4a97-803e-97db88fdcf99");
            CustomData.Add("CustomerNumber", "2718500");
            CustomData.Add("DeliveryId", "2c71f6d9-c1aa-4672-a682-3e9f12badac9");
            CustomData.Add("DeliveryInvoices", "<?xml version=\"1.0\" encoding=\"utf-16\"?>\u000d\u000a<!DOCTYPE EXAMPLEDelivery SYSTEM \"EXAMPLEDelivery.dtd\">\u000d\u000a<ArrayOfRouteDeliveryInvoice>\u000d\u000a  <RouteDeliveryInvoice>\u000d\u000a    <InvoiceNumber>999999</InvoiceNumber>\u000d\u000a    <InventoryIds>\u000d\u000a      <string>1790908</string>\u000d\u000a    </InventoryIds>\u000d\u000a    <IsRA>false</IsRA>\u000d\u000a    <IsDT>false</IsDT>\u000d\u000a    <IsINC>true</IsINC>\u000d\u000a    <IsPO>false</IsPO>\u000d\u000a    <IsPOPickup>false</IsPOPickup>\u000d\u000a  </RouteDeliveryInvoice>\u000d\u000a</ArrayOfRouteDeliveryInvoice>");
            CustomData.Add("DeliveryNotes", "");
            CustomData.Add("RouteId", "20191");

            // Run the query
            var routeParameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id
            };

            DataObjectRoute dataObject = route4Me
                .GetRoute(routeParameters, out string errorString);

            Address oAddress = dataObject.Addresses
                .Where(x => x.RouteDestinationId == routeDestionationId)
                .FirstOrDefault();

            var routeParams = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                RouteDestinationId = oAddress.ContactId
            };

            oAddress.Alias = "Steele's - MONTICELLO";
            oAddress.Cost = 5;
            oAddress.InvoiceNo = "945825";
            // etc fill the necessary address parameters
            oAddress.CustomFields = new Dictionary<string, string> 
            { 
                { "Test Custom Fields", "Test custom Data" } 
            };

            errorString = "";
            Address address = route4Me.UpdateRouteDestination(oAddress, out errorString);

            Console.WriteLine("");

            if (address != null)
            {
                Console.WriteLine("UpdateRouteDestination executed successfully");
                Console.WriteLine("Alias {0}", address.Alias);
                Console.WriteLine("Cost {0}", address.Cost);
                Console.WriteLine("InvoiceNo {0}", address.InvoiceNo);

                foreach (KeyValuePair<string, string> kvpair in address.CustomFields)
                {
                    Console.WriteLine(kvpair.Key + ": " + kvpair.Value);
                }
            }
            else
            {
                Console.WriteLine("UpdateRouteDestination error {0}", errorString);
            }

            RemoveTestOptimizations();
        }
    }
}
