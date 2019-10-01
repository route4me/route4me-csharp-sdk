using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationLeg
    {
        [DataMember(Name = "geometry")]
        public string Geometry { get; set; }

        [DataMember(Name = "steps")]
        public object[] Steps { get; set; }

        [DataMember(Name = "distance")]
        public decimal? Distance { get; set; }

        [DataMember(Name = "duration")]
        public decimal? Duration { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "weight")]
        public decimal? Weight { get; set; }
    }
}
