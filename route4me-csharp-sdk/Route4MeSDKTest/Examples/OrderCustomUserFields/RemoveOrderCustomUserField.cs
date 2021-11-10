using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove an order custom user field.
        /// </summary>
        public void RemoveOrderCustomUserField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestOrderCustomUserField();

            int orderCustomFieldId = OrderCustomFieldsToRemove[OrderCustomFieldsToRemove.Count - 1];

            var orderCustomFieldParams = new OrderCustomFieldParameters()
            {
                OrderCustomFieldId = orderCustomFieldId
            };

            var result = route4Me.RemoveOrderCustomUserField(
                orderCustomFieldParams,
                out string errorString
            );

            PrintOrderCustomField(result, errorString);
        }
    }
}
