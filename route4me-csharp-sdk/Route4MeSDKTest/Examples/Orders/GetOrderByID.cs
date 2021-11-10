using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Order details by order_id
        /// </summary>
        public void GetOrderByID(string orderIds = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = orderIds == null ? true : false;

            if (isInnerExample) CreateExampleOrder();

            var orderId = isInnerExample
                ? OrdersToRemove[OrdersToRemove.Count - 1]
                : orderIds;

            var orderParameters = new OrderParameters()
            {
                order_id = orderId
            };

            Order order = route4Me.GetOrderByID(
                orderParameters,
                out string errorString);

            PrintExampleOrder(order, errorString);

            if (isInnerExample) RemoveTestOrders();
        }
    }
}
