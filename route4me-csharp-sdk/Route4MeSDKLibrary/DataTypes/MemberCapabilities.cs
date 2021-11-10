using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Member capabilities data structure
    /// </summary>
    [DataContract]
    public class MemberCapabilities : GenericParameters
    {
        /// <summary>
        ///     Array of the avoidance zone IDs
        /// </summary>
        [DataMember(Name = "avoid", EmitDefaultValue = false)]
        public string[] Avoid { get; set; }

        /// <summary>
        ///     Road avoid options: "Highways", "Tolls", "highways,tolls".
        /// </summary>
        [DataMember(Name = "avoid_roads", EmitDefaultValue = false)]
        public string[] AvoidRoads { get; set; }

        /// <summary>
        ///     Restriction options
        /// </summary>
        [DataMember(Name = "features", EmitDefaultValue = false)]
        public Dictionary<string, bool> Features { get; set; }

        /// <summary>
        ///     Travel modes: "Highways", "Tolls", "highways,tolls"
        /// </summary>
        [DataMember(Name = "travelModes", EmitDefaultValue = false)]
        public Dictionary<string, string> TravelModes { get; set; }

        /// <summary>
        ///     Navigate options
        /// </summary>
        [DataMember(Name = "navigateBy", EmitDefaultValue = false)]
        public string[] NavigateBy { get; set; }

        /// <summary>
        ///     Array of the license modules
        /// </summary>
        [DataMember(Name = "LicensedModules", EmitDefaultValue = false)]
        public string[] LicensedModules { get; set; }

        /// <summary>
        ///     If true, the member subscription is commercial.
        /// </summary>
        [DataMember(Name = "commercial", EmitDefaultValue = false)]
        public bool Commercial { get; set; }
    }
}