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
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            OrderParameters orderParameters = new OrderParameters()
            {
                order_id = orderIds
            };

            string errorString;
            Order order = route4Me.GetOrderByID(orderParameters, out errorString);

            Console.WriteLine("");

            if (order != null)
            {
                Console.WriteLine("GetOrderByID executed successfully");
            }
            else
            {
                Console.WriteLine("GetOrderByID error: {0}", errorString);
            }
        }
    }
}
