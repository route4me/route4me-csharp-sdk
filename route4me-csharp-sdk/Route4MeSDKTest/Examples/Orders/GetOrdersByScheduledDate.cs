using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using static Route4MeSDK.Route4MeManager;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders by Scheduled Date
        /// </summary>
        public void GetOrdersByScheduledDate(string ScheduleddDate = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = ScheduleddDate == null ? true : false;

            if (isInnerExample)
            {
                CreateExampleOrder();
                ScheduleddDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var oParams = new OrderParameters { ScheduledForYYMMDD = ScheduleddDate };

            var result = route4Me.SearchOrders(oParams, out string errorString);

            PrintExampleOrder(result, errorString);

            if (isInnerExample && result != null && result.GetType() == typeof(GetOrdersResponse))
            {
                OrdersToRemove = new List<string>();

                foreach (Order ord in ((GetOrdersResponse)result).Results)
                    OrdersToRemove.Add(ord.OrderId.ToString());

                RemoveTestOrders();
            }
        }
    }
}
