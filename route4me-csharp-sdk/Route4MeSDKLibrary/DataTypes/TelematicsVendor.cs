using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Telematics vendor's data structure.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendor
    {
        /// <summary>
        ///     Unique ID of a telematics vendor.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        ///     Vendor name.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Vendor slug.
        /// </summary>
        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        /// <summary>
        ///     Vendor description.
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        ///     URL to the telematics vendor's logo.
        /// </summary>
        [DataMember(Name = "logo_url", EmitDefaultValue = false)]
        public string LogoURL { get; set; }

        /// <summary>
        ///     Website URL of a telematics vendor.
        /// </summary>
        [DataMember(Name = "website_url", EmitDefaultValue = false)]
        public string WebsiteURL { get; set; }

        /// <summary>
        ///     API URL of a telematics vendor.
        /// </summary>
        [DataMember(Name = "api_docs_url", EmitDefaultValue = false)]
        public string ApiDocsURL { get; set; }

        /// <summary>
        ///     Whether, the vendor is or not integrated into the Route4Me system.
        /// </summary>
        [DataMember(Name = "is_integrated", EmitDefaultValue = false)]
        public string IsIntegrated { get; set; }

        /// <summary>
        ///     Vendors size.
        ///     <para>Accepted values:</para>
        ///     <value>global, regional, local. </value>
        /// </summary>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public string Size { get; set; }

        /// <summary>
        ///     An array the vendor features. See <see cref="TelematicsVendorFeature" />.
        /// </summary>
        [DataMember(Name = "features", EmitDefaultValue = false)]
        public TelematicsVendorFeature[] Features { get; set; }

        /// <summary>
        ///     An array of the countries, the vendor is operating. See <see cref="Country" />.
        /// </summary>
        [DataMember(Name = "countries", EmitDefaultValue = false)]
        public Country[] Countries { get; set; }
    }
}