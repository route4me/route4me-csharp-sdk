using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class Country
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string countryCcode { get; set; }

        [DataMember(Name = "country_name", EmitDefaultValue = false)]
        public string countryName { get; set; }
    }
}
