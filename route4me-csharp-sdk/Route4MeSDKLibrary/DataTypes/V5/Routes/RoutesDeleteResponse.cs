using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The response from the endpoint R4MEInfrastructureSettingsV5.Routes Delete.
    /// </summary>
    [DataContract]
    public class RoutesDeleteResponse
    {
        /// <summary>
        ///     If true, the route duplicated successfully.
        /// </summary>
        [DataMember(Name = "deleted", EmitDefaultValue = false)]
        public bool Deleted { get; set; }

        /// <summary>
        ///     If true, the route duplication process was asynchronous.
        /// </summary>
        [DataMember(Name = "async", EmitDefaultValue = false)]
        public bool? Async { get; set; }

        /// <summary>
        ///     Route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     An array of the duplicated route IDs.
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIDs { get; set; }
    }
}