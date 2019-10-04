using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class TerritoryQuery : GenericParameters
    {
        /// <summary>
        /// Device Id
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Territory Id
        /// </summary>
        [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
        public string TerritoryId { get; set; }

        /// <summary>
        /// If equal = 1, the enclosed addresses will be included in the response.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public int? Addresses { get; set; }

        /// <summary>
        /// If equal = 1, the enclosed orders will be included in the response.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "orders", EmitDefaultValue = false)]
        public int? Orders { get; set; }
    }
}
