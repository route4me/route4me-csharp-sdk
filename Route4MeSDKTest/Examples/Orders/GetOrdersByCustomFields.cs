using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Show Orders using chosen fields' values
        /// </summary>
        public void GetOrdersByCustomFields(string CustomFields)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            OrderParameters oParams = new OrderParameters()
            { 
                Fields = CustomFields,
                Offset = 0,
                Limit = 20
            };

            string errorString = "";
            Order[] orders = route4Me.SearchOrders(oParams, out errorString);

            Console.WriteLine("");

            if (orders != null)
            {
                Console.WriteLine("GetOrdersByCustomFields executed successfully, orders searched total = {0}", orders.Length);
            }
            else
            {
                Console.WriteLine("GetOrdersByCustomFields error: {0}", errorString);
            }
        }
    }
}
