using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get limited number of the Orders
        /// </summary>
        public void GetOrders()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderParameters = new OrderParameters()
            {
                Limit = 10
            };

            Order[] orders = route4Me.GetOrders(
                orderParameters,
                out uint total,
                out string errorString);

            PrintExampleOrder(orders, errorString);
        }
    }
}
