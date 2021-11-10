using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class for the response from the order custom field query request.
    /// </summary>
    [DataContract]
    public sealed class OrderCustomField
    {
        /// <summary>
        ///     Custom order field ID.
        /// </summary>
        [DataMember(Name = "order_custom_field_id", EmitDefaultValue = false)]
        public int OrderCustomFieldId { get; set; }

        /// <summary>
        ///     Custom order field name.
        /// </summary>
        [DataMember(Name = "order_custom_field_name", EmitDefaultValue = false, IsRequired = false)]
        public string OrderCustomFieldName { get; set; }

        /// <summary>
        ///     Custom order field label.
        /// </summary>
        [DataMember(Name = "order_custom_field_label", EmitDefaultValue = false)]
        public string OrderCustomFieldLabel { get; set; }

        /// <summary>
        ///     Custom order field type.
        /// </summary>
        [DataMember(Name = "order_custom_field_type", EmitDefaultValue = false)]
        public string OrderCustomFieldType { get; set; }

        /// <summary>
        ///     Custom order field value.
        /// </summary>
        [DataMember(Name = "order_custom_field_value", EmitDefaultValue = false)]
        public string OrderCustomFieldValue { get; set; }

        /// <summary>
        ///     Account owner member ID.
        /// </summary>
        [DataMember(Name = "root_owner_member_id", EmitDefaultValue = false)]
        public int RootOwnerMemberId { get; set; }

        /// <summary>
        ///     Information about an order's custom field.
        ///     You can specify the propertiesof the different types in this property,
        ///     but the property "short_label" is reserved - it specifies custom field column header
        ///     in the orders table in the page: https://route4me.com/orders
        /// </summary>
        [DataMember(Name = "order_custom_field_type_info", EmitDefaultValue = false)]
        public Dictionary<string, object> OrderCustomFieldTypeInfo { get; set; }
    }
}