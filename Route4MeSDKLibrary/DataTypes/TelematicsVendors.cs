using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Telematics vendor's data structure.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendors
    {
        /// <summary>
        /// Vendor ID.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        /// Vendor name.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Vendor slug.
        /// </summary>
        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        /// <summary>
        /// URL to the telematics vendor's logo.
        /// </summary>
        [DataMember(Name = "logo_url", EmitDefaultValue = false)]
        public string logoURL { get; set; }

        /// <summary>
        /// Whether, the vendor is or not integrated into the Route4Me system.
        /// </summary>
        [DataMember(Name = "is_integrated", EmitDefaultValue = false)]
        public string isIntegrated { get; set; }
    }
}
