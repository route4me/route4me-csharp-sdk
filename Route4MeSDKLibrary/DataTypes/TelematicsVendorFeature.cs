using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Telematics vendor's feature
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendorFeature
    {
        /// <summary>
        /// Feature ID.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        /// Feature name.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Feature slug.
        /// </summary>
        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        /// <summary>
        /// Feature group.
        /// </summary>
        [DataMember(Name = "feature_group", EmitDefaultValue = false)]
        public string featureGroup { get; set; }
    }
}
