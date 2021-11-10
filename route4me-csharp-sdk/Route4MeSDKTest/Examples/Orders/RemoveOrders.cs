using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove Orders
        /// </summary>
        /// <param name="orderIds"> Order Ids </param>
        public void RemoveOrders(string[] orderIds = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = orderIds == null ? true : false;

            if (isInnerExample) CreateExampleOrder();

            orderIds = orderIds == null
                ? new string[] { lastCreatedOrder.OrderId.ToString() }
                : orderIds;

            // Run the query
            bool removed = route4Me.RemoveOrders(orderIds, out string errorString);

            Console.WriteLine("");

            Console.WriteLine(
                removed
                ? String.Format("RemoveOrders executed successfully, {0} orders removed", orderIds.Length)
                : String.Format("RemoveOrders error: {0}", errorString)
            );
        }
    }
}
