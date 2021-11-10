using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Order
        /// </summary>
        /// <returns> Added Order </returns>
        public void AddOrder()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next().ToString(),
                CachedLat = 37.773972,
                CachedLng = -122.431297
            };

            // Run the query
            Order resultOrder = route4Me.AddOrder(order, out string errorString);

            if (resultOrder != null && resultOrder.GetType() == typeof(Order))
                OrdersToRemove.Add(resultOrder.OrderId.ToString());

            PrintExampleOrder(resultOrder, errorString);

            RemoveTestOrders();
        }
    }
}
