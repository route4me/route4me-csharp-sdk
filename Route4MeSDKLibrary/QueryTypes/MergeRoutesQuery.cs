using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;
namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Route parameters accepted by endpoints
    /// </summary>
    [DataContract]
    public sealed class MergeRoutesQuery : GenericParameters
    {
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "route_ids", EmitDefaultValue = false)]
        public string RouteIds { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_address", EmitDefaultValue = false)]
        public string DepotAddress { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "remove_origin", EmitDefaultValue = false)]
        public bool RemoveOrigin { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_lat", EmitDefaultValue = false)]
        public double DepotLat { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_lng", EmitDefaultValue = false)]
        public double DepotLng { get; set; }
    }
}