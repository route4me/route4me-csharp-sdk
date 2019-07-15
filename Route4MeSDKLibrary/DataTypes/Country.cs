using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The Country class.
    /// TODO: check if it will not be used
    /// </summary>
    [DataContract]
    public sealed class Country
    {
        /// <summary>
        /// Country ID
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string countryCcode { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        [DataMember(Name = "country_name", EmitDefaultValue = false)]
        public string countryName { get; set; }
    }
}
