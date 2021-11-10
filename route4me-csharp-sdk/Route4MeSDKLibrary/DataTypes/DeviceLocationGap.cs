using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationGap
    {
        [DataMember(Name = "distance")] public double? Distance { get; set; }

        [DataMember(Name = "duration")] public double? Duration { get; set; }

        [DataMember(Name = "geometry")] public string Geometry { get; set; }
    }
}