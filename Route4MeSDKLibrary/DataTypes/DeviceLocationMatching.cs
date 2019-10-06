using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationMatching
    {
        [DataMember(Name = "confidence")]
        public double? Confidence { get; set; }

        [DataMember(Name = "geometry")]
        public string Geometry { get; set; }

        [DataMember(Name = "legs")]
        public DeviceLocationLeg[] Legs { get; set; }

        [DataMember(Name = "distance")]
        public decimal? Distance { get; set; }

        [DataMember(Name = "duration")]
        public decimal? Duration { get; set; }

        [DataMember(Name = "weight_name")]
        public string WeightName { get; set; }

        [DataMember(Name = "weight")]
        public decimal? Weight { get; set; }
    }
}
