using Route4MeSDK.DataTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create an order with custom fields.
        /// </summary>
        public void CreateOrderWithCustomField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderParams = new Order()
            {
                Address1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
                CachedLat = 48.335991,
                CachedLng = 31.18287,
                DayScheduledFor_YYYYMMDD = "2019-10-11",
                AddressAlias = "Auto test address",
                CustomUserFields = new OrderCustomField[]
                {
                    new OrderCustomField()
                    {
                        OrderCustomFieldId = 93,
                        OrderCustomFieldValue = "false"
                    }
                }
            };

            var result = route4Me.AddOrder(orderParams, out string errorString);

            PrintExampleOrder(result, errorString);

            if (result != null && result.GetType() == typeof(Order))
                OrdersToRemove = new List<string>() { result.OrderId.ToString() };

            RemoveTestOrders();
        }
    }
}
