using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddOrderWithTrackingNumber()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order()
            {
                address_1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                TrackingNumber = "AA11ZZCC",
                AddressStopType = AddressStopType.PickUp.Description()
            };

            // Run the query
            var resultOrder = route4Me.AddOrder(order, out string errorString);

            if (resultOrder != null && resultOrder.GetType() == typeof(Order))
                OrdersToRemove.Add(resultOrder.order_id.ToString());

            PrintExampleOrder(resultOrder, errorString);

            RemoveTestOrders();
        }
    }
}
