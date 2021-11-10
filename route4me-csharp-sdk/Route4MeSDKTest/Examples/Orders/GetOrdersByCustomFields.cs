using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Show Orders using selected field values
        /// </summary>
        public void GetOrdersByCustomFields(string CustomFields = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var oParams = new OrderParameters()
            {
                Fields = CustomFields == null
                    ? "order_id,member_id"
                    : CustomFields,
                Offset = 0,
                Limit = 20
            };

            var result = route4Me.SearchOrders(oParams, out string errorString);

            PrintExampleOrder(result, errorString);
        }
    }
}
