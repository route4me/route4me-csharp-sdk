using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The class for the response from the creating/updating an order custom field.
    /// </summary>
    [DataContract]
    public sealed class OrderCustomFieldCreateResponse
    {
        /// <summary>
        /// If the order custom field created/update succsessfully, equals to 'OK'.
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public string Result { get; set; }

        /// <summary>
        /// How many custom order fields were affected..
        /// </summary>
        [DataMember(Name = "affected", EmitDefaultValue = false)]
        public int Affected { get; set; }

        /// <summary>
        /// Created/updated custom order field.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public OrderCustomField Data { get; set; }
    }
}
