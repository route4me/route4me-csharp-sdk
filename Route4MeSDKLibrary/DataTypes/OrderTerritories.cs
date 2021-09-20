using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Order Territories for an optimization payload. 
    /// </summary>
    [DataContract]
    public sealed class OrderTerritories : GenericParameters
    {
        /// <summary>
        /// If true, split each territory to own optimization
        /// </summary>
        [DataMember(Name = "split_territories", EmitDefaultValue = false)]
        public bool? SplitTerritories { get; set; }

        /// <summary>
        /// An array of the territory IDs
        /// </summary>
        [DataMember(Name = "territories_id", EmitDefaultValue = false)]
        public string[] TerritoriesId { get; set; }

        /// <summary>
        /// Order filters. 
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public FilterDetails filters { get; set; }
    }
}
