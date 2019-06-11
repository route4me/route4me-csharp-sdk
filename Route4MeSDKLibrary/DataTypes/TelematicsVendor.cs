using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class TelematicsVendor
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "logo_url", EmitDefaultValue = false)]
        public string logoURL { get; set; }

        [DataMember(Name = "website_url", EmitDefaultValue = false)]
        public string websiteURL { get; set; }

        [DataMember(Name = "api_docs_url", EmitDefaultValue = false)]
        public string apiDocsURL { get; set; }

        [DataMember(Name = "is_integrated", EmitDefaultValue = false)]
        public string isIntegrated { get; set; }

        [DataMember(Name = "size", EmitDefaultValue = false)]
        public string Size { get; set; }

        [DataMember(Name = "features", EmitDefaultValue = false)]
        public TelematicsVendorFeature[] Features { get; set; }

        [DataMember(Name = "countries", EmitDefaultValue = false)]
        public Country[] Countries { get; set; }

    }
}
