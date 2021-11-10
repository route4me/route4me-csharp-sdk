using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class for the fast geocoded bulk addresses
    /// </summary>
    [DataContract]
    public sealed class AddressGeocoded
    {
        /// <summary>
        ///     The geocoded address name
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public Address GeocodedAddress { get; set; }

        /// <summary>
        ///     A member ID the geocoded address belongs
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int MemberId { get; set; }

        /// <summary>
        ///     The member type ID
        /// </summary>
        [DataMember(Name = "member_type_id", EmitDefaultValue = false)]
        public int MemberTypeID { get; set; }

        /// <summary>
        ///     The user's position latitude
        /// </summary>
        [DataMember(Name = "userLat")]
        public double? UserLatitude { get; set; }

        /// <summary>
        ///     The user's position longitude
        /// </summary>
        [DataMember(Name = "userLng")]
        public double? UserLongitude { get; set; }

        /// <summary>
        ///     The user's IP
        /// </summary>
        [DataMember(Name = "intUserIP", EmitDefaultValue = false)]
        public uint IntUserIP { get; set; }

        /// <summary>
        ///     The geocoding method
        /// </summary>
        [DataMember(Name = "strGeocodingMethod", EmitDefaultValue = false)]
        public string StrGeocodingMethod { get; set; }

        /// <summary>
        ///     Get the geocoded address curbside
        /// </summary>
        [DataMember(Name = "getCurbside", EmitDefaultValue = false)]
        public bool GetCurbside { get; set; }

        /// <summary>
        ///     The geocoded address priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public int Priority { get; set; }
    }
}