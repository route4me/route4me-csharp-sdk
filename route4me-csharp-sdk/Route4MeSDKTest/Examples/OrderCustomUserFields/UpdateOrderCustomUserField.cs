using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an order custom user field. 
        /// </summary>
        public void updateOrderCustomUserField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestOrderCustomUserField();

            var orderCustomFieldId = OrderCustomFieldsToRemove[OrderCustomFieldsToRemove.Count - 1];

            var orderCustomFieldParams = new OrderCustomFieldParameters()
            {
                OrderCustomFieldId = orderCustomFieldId,
                OrderCustomFieldLabel = "Custom Field 55",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>()
                {
                    {"short_label", "cFl55" },
                    {"description", "This is updated test order custom field" },
                    {"custom field no", 12 }
                }
            };

            var orderCustomUserField = route4Me.UpdateOrderCustomUserField(
                orderCustomFieldParams,
                out string errorString
             );

            PrintOrderCustomField(orderCustomUserField, errorString);

            RemoveTestOrderCustomField();
        }
    }
}
