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
                address_1 = "Test Address1 " + (new Random()).Next().ToString(),
                address_alias = "Test AddressAlias " + (new Random()).Next().ToString(),
                cached_lat = 37.773972,
                cached_lng = -122.431297
            };

            // Run the query
            Order resultOrder = route4Me.AddOrder(order, out string errorString);

            if (resultOrder != null && resultOrder.GetType() == typeof(Order))
                OrdersToRemove.Add(resultOrder.order_id.ToString());

            PrintExampleOrder(resultOrder, errorString);

            RemoveTestOrders();
        }
    }
}

