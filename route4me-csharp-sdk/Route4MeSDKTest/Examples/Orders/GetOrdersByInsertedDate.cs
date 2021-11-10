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
        /// Get orders by inserted date.
        /// </summary>
        public void GetOrdersByInsertedDate()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            string InsertedDate = DateTime.Now.ToString("yyyy-MM-dd");

            var oParams = new OrderParameters { DayAddedYYMMDD = InsertedDate };

            var result = route4Me.SearchOrders(oParams, out string errorString);

            if (result != null && result.GetType() == typeof(GetOrdersResponse))
            {
                OrdersToRemove = new List<string>();

                foreach (Order ord in ((GetOrdersResponse)result).Results)
                    OrdersToRemove.Add(ord.OrderId.ToString());
            }

            PrintExampleOrder(result, errorString);

            RemoveTestOrders();
        }
    }
}
