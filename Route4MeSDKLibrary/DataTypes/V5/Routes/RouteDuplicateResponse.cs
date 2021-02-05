using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// The response from the route delete process.
    /// </summary>
    [DataContract]
    public class RouteDuplicateResponse
    {
        /// <summary>
        /// If true, the route duplicated successfully.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        /// <summary>
        /// If true, the route duplication process was asynchronous.
        /// </summary>
        [DataMember(Name = "async", EmitDefaultValue = false)]
        public bool? Async { get; set; }

        /// <summary>
        /// An array of the duplicated route IDs.-
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIDs { get; set; }
    }
}
