using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders be containing specified text in any text field
        /// </summary>
        public void GetOrdersBySpecifiedText(string query = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = query == null ? true : false;

            if (isInnerExample)
            {
                CreateExampleOrder();
                query = "Carol";
            }

            var oParams = new OrderParameters()
            {
                Query = query,
                Offset = 0,
                Limit = 20
            };

            var result = route4Me.SearchOrders(oParams, out string errorString);

            PrintExampleOrder(result, errorString);

            if (isInnerExample) RemoveTestOrders();
        }
    }
}
