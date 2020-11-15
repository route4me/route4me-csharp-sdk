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
                address_1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
                cached_lat = 48.335991,
                cached_lng = 31.18287,
                day_scheduled_for_YYMMDD = "2019-10-11",
                address_alias = "Auto test address",
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
                OrdersToRemove = new List<string>() { result.order_id.ToString() };

            RemoveTestOrders();
        }
    }
}
