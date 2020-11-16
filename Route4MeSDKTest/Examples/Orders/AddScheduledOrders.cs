using Route4MeSDK.DataTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Scheduled Order
        /// </summary>
        public void AddScheduledOrder()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderParams = new Order()
            {
                address_1 = "318 S 39th St, Louisville, KY 40212, USA",
                cached_lat = 38.259326,
                cached_lng = -85.814979,
                curbside_lat = 38.259326,
                curbside_lng = -85.814979,
                address_alias = "318 S 39th St 40212",
                address_city = "Louisville",
                EXT_FIELD_first_name = "Lui",
                EXT_FIELD_last_name = "Carol",
                EXT_FIELD_email = "lcarol654@yahoo.com",
                EXT_FIELD_phone = "897946541",
                EXT_FIELD_custom_data = new Dictionary<string, string>() { { "order_type", "scheduled order" } },
                day_scheduled_for_YYMMDD = "2017-12-20",
                local_time_window_end = 39000,
                local_time_window_end_2 = 46200,
                local_time_window_start = 37800,
                local_time_window_start_2 = 45000,
                local_timezone_string = "America/New_York",
                order_icon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out string errorString);

            PrintExampleOrder(newOrder, errorString);

            if (newOrder != null && newOrder.GetType() == typeof(Order)) 
                OrdersToRemove = new List<string>() { newOrder.order_id.ToString() };

            RemoveTestOrders();
        }
    }
}
