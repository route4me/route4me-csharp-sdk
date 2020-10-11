using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationTracePoint
    {
        [DataMember(Name = "alternatives_count")]
        public int? AlternativesCount { get; set; }

        [DataMember(Name = "waypoint_index")]
        public int? WaypointIndex { get; set; }

        [DataMember(Name = "location")]
        public int[] Location { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "distance")]
        public double? Distance { get; set; }

        [DataMember(Name = "matchings_index")]
        public int? MatchingsIndex { get; set; }
    }
}
