using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class TelematicsVendors
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        [DataMember(Name = "logo_url", EmitDefaultValue = false)]
        public string logoURL { get; set; }

        [DataMember(Name = "is_integrated", EmitDefaultValue = false)]
        public string isIntegrated { get; set; }
    }
}
