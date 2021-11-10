using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Telematics vendors list item data structure.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendors
    {
        /// <summary>
        ///     Vendor ID.
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
        ///     Description of the telematics vendor
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        ///     URL to the telematics vendor's logo.
        /// </summary>
        [DataMember(Name = "logo_url", EmitDefaultValue = false)]
        public string LogoURL { get; set; }

        /// <summary>
        ///     URL to the telematics vendor's website.
        /// </summary>
        [DataMember(Name = "website_url", EmitDefaultValue = false)]
        public string WebsiteURL { get; set; }

        /// <summary>
        ///     URL to the telematics vendor's website.
        /// </summary>
        [DataMember(Name = "api_docs_url", EmitDefaultValue = false)]
        public string ApiDocsURL { get; set; }

        /// <summary>
        ///     Whether, the vendor is or not integrated into the Route4Me system.
        /// </summary>
        [DataMember(Name = "is_integrated", EmitDefaultValue = false)]
        public string IsIntegrated { get; set; }

        /// <summary>
        ///     Telematics vendor size (e.g. 'global', regional', 'local')
        /// </summary>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public string Size { get; set; }
    }
}