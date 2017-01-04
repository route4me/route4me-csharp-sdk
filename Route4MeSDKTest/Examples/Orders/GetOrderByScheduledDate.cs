using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders by Scheduled Date
        /// </summary>
        public void GetOrderByScheduledDate(string ScheduleddDate)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            OrderParameters oParams = new OrderParameters { ScheduledForYYMMDD = ScheduleddDate };

            string errorString = "";
            Order[] orders = route4Me.SearchOrders(oParams, out errorString);

            Console.WriteLine("");

            if (orders != null)
            {
                Console.WriteLine("GetOrderByScheduledDate executed successfully, orders searched total = {0}", orders.Length);
            }
            else
            {
                Console.WriteLine("GetOrderByScheduledDate error: {0}", errorString);
            }
        }
    }
}
