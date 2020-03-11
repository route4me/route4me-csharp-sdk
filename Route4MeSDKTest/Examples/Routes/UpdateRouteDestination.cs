using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void UpdateRouteDestination()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager("11111111111111111111111111111111");

            // The example refers to the process of updating many parameters simultaneously of the route destination by sending the HTTP parameters.

            string routeId = "824CA521E3A8DE9F1C684C8BAE90CF07";
            int routeDestionationId = 217393034;

            Dictionary<string, string> CustomData = new Dictionary<string, string>();
            CustomData.Add("BatchId", "e7c672b1-a356-4a97-803e-97db88fdcf99");
            CustomData.Add("CustomerNumber", "2718500");
            CustomData.Add("DeliveryId", "2c71f6d9-c1aa-4672-a682-3e9f12badac9");
            CustomData.Add("DeliveryInvoices", "<?xml version=\"1.0\" encoding=\"utf-16\"?>\u000d\u000a<!DOCTYPE EXAMPLEDelivery SYSTEM \"EXAMPLEDelivery.dtd\">\u000d\u000a<ArrayOfRouteDeliveryInvoice>\u000d\u000a  <RouteDeliveryInvoice>\u000d\u000a    <InvoiceNumber>999999</InvoiceNumber>\u000d\u000a    <InventoryIds>\u000d\u000a      <string>1790908</string>\u000d\u000a    </InventoryIds>\u000d\u000a    <IsRA>false</IsRA>\u000d\u000a    <IsDT>false</IsDT>\u000d\u000a    <IsINC>true</IsINC>\u000d\u000a    <IsPO>false</IsPO>\u000d\u000a    <IsPOPickup>false</IsPOPickup>\u000d\u000a  </RouteDeliveryInvoice>\u000d\u000a</ArrayOfRouteDeliveryInvoice>");
            CustomData.Add("DeliveryNotes", "");
            CustomData.Add("RouteId", "20191");

            // Run the query
            RouteParametersQuery routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId
            };

            string errorString;
            DataObjectRoute dataObject = route4Me.GetRoute(routeParameters, out errorString);

            foreach (Address oAddress in dataObject.Addresses)
            {
                if (oAddress.RouteDestinationId == routeDestionationId)
                {
                    RouteParametersQuery routeParams = new RouteParametersQuery()
                    {
                        RouteId = routeId,
                        RouteDestinationId = oAddress.ContactId
                    };
                    oAddress.Alias = "Steele's - MONTICELLO";
                    oAddress.Cost = 5;
                    oAddress.InvoiceNo = "945825";
                    // etc fill the necessary address parameters
                    oAddress.CustomFields = new Dictionary<string,string>{{"Test Custom Fields","Test custom Data"}};

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
                }
            }
        }
    }
}
