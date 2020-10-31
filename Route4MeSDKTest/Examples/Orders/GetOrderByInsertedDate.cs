using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders by Inserted Date
        /// </summary>
        public void GetOrderByInsertedDate(string InsertedDate)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            OrderParameters oParams = new OrderParameters { DayAddedYYMMDD = InsertedDate };

            string errorString = "";
            Order[] orders = route4Me.SearchOrders(oParams, out errorString);

            Console.WriteLine("");

            if (orders != null)
            {
                Console.WriteLine("GetOrderByInsertedDate executed successfully, orders searched total = {0}", orders.Length);
            }
            else
            {
                Console.WriteLine("GetOrderByInsertedDate error: {0}", errorString);
            }
        }
    }
}
