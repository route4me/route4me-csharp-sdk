using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationSummary
    {
        [DataMember(Name = "total_distance")]
        public double? TotalDistance { get; set; }

        [DataMember(Name = "total_duration")]
        public double? TotalDuration { get; set; }

        [DataMember(Name = "matchings_distance")]
        public double? MatchingsDistance { get; set; }

        [DataMember(Name = "matchings_duration")]
        public double? MatchingsDuration { get; set; }

        [DataMember(Name = "gaps_distance")]
        public double? GapsDistance { get; set; }

        [DataMember(Name = "gaps_duration")]
        public double? GapsDuration { get; set; }
    }
}
