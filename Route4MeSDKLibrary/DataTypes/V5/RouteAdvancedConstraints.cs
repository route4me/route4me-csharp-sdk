using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.DataTypes.V5
{
    [DataContract]
    public sealed class RouteAdvancedConstraints
    {
        /// <summary>
        /// Maximum cargo volume per route
        /// </summary>
        [DataMember(Name = "max_cargo_volume", EmitDefaultValue = false)]
        public double? MaximumCargoVolume { get; set; }

        /// <summary>
        /// Vehicle capacity.
        /// <para>How much total cargo can be transported per route (units, e.g. cubic meters)</para>
        /// </summary>
        [DataMember(Name = "max_capacity", EmitDefaultValue = false)]
        public int? MaximumCapacity { get; set; }

        /// <summary>
        /// Legacy feature which permits a user to request an example number of optimized routes.
        /// </summary>
        [DataMember(Name = "members_count", EmitDefaultValue = false)]
        public int? MembersCount { get; set; }

        /// <summary>
        /// An array of the available time windows (e.g. [ [25200, 75000 ] )
        /// </summary>
        [DataMember(Name = "available_time_windows", EmitDefaultValue = false)]
        public List<int[]> AvailableTimeWindows { get; set; }

        /// <summary>
        /// The driver tags specified in a team member's custom data.
        /// (e.g. "driver skills": 
        /// ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        /// </summary>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public string[] Tags { get; set; }

        /// <summary>
        /// An array of the skilled driver IDs.
        /// </summary>
        [DataMember(Name = "route4me_members_id", EmitDefaultValue = false)]
        public int[] Route4meMembersId { get; set; }
    }
}
