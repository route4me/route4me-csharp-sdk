using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create an order custom user field.
        /// </summary>
        public void CreateOrderCustomUserField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderCustomFieldParams = new OrderCustomFieldParameters()
            {
                OrderCustomFieldName = "CustomField77",
                OrderCustomFieldLabel = "Custom Field 77",
                OrderCustomFieldType = "checkbox",
                OrderCustomFieldTypeInfo = new Dictionary<string, object>()
                {
                    {"short_label", "cFl77" },
                    {"description", "This is test order custom field" },
                    {"custom field no", 11 }
                }
            };

            var orderCustomUserField = route4Me.CreateOrderCustomUserField(
                    orderCustomFieldParams,
                    out string errorString
                );

            PrintOrderCustomField(orderCustomUserField, errorString);

            if (orderCustomUserField != null &&
                orderCustomUserField.GetType() == typeof(OrderCustomFieldCreateResponse) &&
                orderCustomUserField.Data != null)
            {
                OrderCustomFieldsToRemove.Add(orderCustomUserField.Data.OrderCustomFieldId);
            }

            RemoveTestOrderCustomField();
        }
    }
}
