using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Order details by order_id
        /// </summary>
        public void GetOrderByID(string orderIds)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            OrderParameters orderParameters = new OrderParameters()
            {
                order_id = orderIds
            };

            string errorString;
            Order[] orders = route4Me.GetOrderByID(orderParameters, out errorString);

            Console.WriteLine("");

            if (orders != null)
            {
                Console.WriteLine("GetOrderByID executed successfully, orders total = {0}", orders.Length);
            }
            else
            {
                Console.WriteLine("GetOrderByID error: {0}", errorString);
            }
        }
    }
}
