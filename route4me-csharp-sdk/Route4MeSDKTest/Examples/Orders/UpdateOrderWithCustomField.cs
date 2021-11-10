using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an order with custom field.
        /// </summary>
        public void UpdateOrderWithCustomField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            lastCreatedOrder.CustomUserFields = new OrderCustomField[]
            {
                new OrderCustomField()
                {
                    OrderCustomFieldId = 93,
                    OrderCustomFieldValue = "true"
                }
            };

            var result = route4Me.UpdateOrder(
                lastCreatedOrder,
                out string errorString);

            PrintExampleOrder(result, errorString);

            RemoveTestOrders();
        }
    }
}
