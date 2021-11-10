using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    [DataContract]
    public sealed class RouteAdvancedConstraints
    {
        /// <summary>
        ///     Maximum cargo volume per route
        /// </summary>
        [DataMember(Name = "max_cargo_volume", EmitDefaultValue = false)]
        public double? MaximumCargoVolume { get; set; }

        /// <summary>
        ///     Vehicle capacity.
        ///     <para>How much total cargo can be transported per route (units, e.g. cubic meters)</para>
        /// </summary>
        [DataMember(Name = "max_capacity", EmitDefaultValue = false)]
        public int? MaximumCapacity { get; set; }

        /// <summary>
        ///     Legacy feature which permits a user to request an example number of optimized routes.
        /// </summary>
        [DataMember(Name = "members_count", EmitDefaultValue = false)]
        public int? MembersCount { get; set; }

        /// <summary>
        ///     An array of the available time windows (e.g. [ [25200, 75000 ] )
        /// </summary>
        [DataMember(Name = "available_time_windows", EmitDefaultValue = false)]
        public List<int[]> AvailableTimeWindows { get; set; }

        /// <summary>
        ///     The driver tags specified in a team member's custom data.
        ///     (e.g. "driver skills":
        ///     ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        /// </summary>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public string[] Tags { get; set; }

        /// <summary>
        ///     An array of the skilled driver IDs.
        /// </summary>
        [DataMember(Name = "route4me_members_id", EmitDefaultValue = false)]
        public int[] Route4meMembersId { get; set; }

        /// <summary>
        ///     An array of the skilled driver IDs.
        /// </summary>
        [DataMember(Name = "location_sequence_pattern", EmitDefaultValue = false)]
        public LocationSequencePattern[] LocationSequencePattern { get; set; }
    }


    /// <summary>
    /// </summary>
    [DataContract]
    public class LocationSequencePattern
    {
        /// <summary>
        ///     Address alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        ///     Route destination address
        /// </summary>
        [DataMember(Name = "address")]
        public string AddressString { get; set; }

        /// <summary>
        ///     The latitude of this address
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     The longitude of this address
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        // <summary>
        /// The expected amount of time that will be spent at this address by the driver/user.
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public long? Time { get; set; }

        /// <summary>
        ///     Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int? RouteDestinationId { get; set; }
    }
}