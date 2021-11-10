using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates the process of creating an order with the specified tracking number and address stop type.
        /// </summary>
        public void AddOrderWithOrderType()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Using of an existing tracking number raises error
            var randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            var order = new Order()
            {
                Address1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = randomTrackingNumber,
                AddressStopType = AddressStopType.PickUp.Description()
            };

            // Run the query
            var resultOrder = route4Me.AddOrder(order, out string errorString);

            if (resultOrder != null && resultOrder.GetType() == typeof(Order))
                OrdersToRemove.Add(resultOrder.OrderId.ToString());

            PrintExampleOrder(resultOrder, errorString);

            RemoveTestOrders();
        }
    }
}
