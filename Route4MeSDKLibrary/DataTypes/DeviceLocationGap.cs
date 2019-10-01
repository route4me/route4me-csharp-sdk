using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationGap
    {
        [DataMember(Name = "distance")]
        public decimal? Distance { get; set; }

        [DataMember(Name = "duration")]
        public decimal? Duration { get; set; }

        [DataMember(Name = "geometry")]
        public string Geometry { get; set; }
    }
}
