using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationLeg
    {
        [DataMember(Name = "geometry")] public string Geometry { get; set; }

        [DataMember(Name = "steps")] public object[] Steps { get; set; }

        [DataMember(Name = "distance")] public double? Distance { get; set; }

        [DataMember(Name = "duration")] public double? Duration { get; set; }

        [DataMember(Name = "summary")] public string Summary { get; set; }

        [DataMember(Name = "weight")] public double? Weight { get; set; }
    }
}