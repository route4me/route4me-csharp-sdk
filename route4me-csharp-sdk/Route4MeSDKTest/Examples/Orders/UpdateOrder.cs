using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="order1"> Order with updated attributes </param>
        public void UpdateOrder(Order order1 = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = (order1 == null ? true : false);

            if (isInnerExample) CreateExampleOrder();

            string orderId = isInnerExample
                ? OrdersToRemove[OrdersToRemove.Count - 1]
                : order1.OrderId.ToString();

            var orderParameters = new OrderParameters()
            {
                order_id = orderId
            };

            Order order = route4Me.GetOrderByID(
                orderParameters,
                out string errorString);

            order.ExtFieldLastName = "Updated " + (new Random()).Next().ToString();

            // Run the query
            var updatedOrder = route4Me.UpdateOrder(order, out errorString);

            PrintExampleOrder(updatedOrder, errorString);

            if (isInnerExample) RemoveTestOrders();
        }
    }
}
