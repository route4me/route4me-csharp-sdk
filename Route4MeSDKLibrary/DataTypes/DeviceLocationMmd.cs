using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationMmd
    {
        [DataMember(Name = "matchings")]
        public DeviceLocationMatching[] Matchings { get; set; }


        [DataMember(Name = "tracepoints")]
        public DeviceLocationTracePoint[] Tracepoints { get; set; }


        [DataMember(Name = "gaps")]
        public DeviceLocationGap[] Gaps { get; set; }


        [DataMember(Name = "summary")]
        public DeviceLocationSummary Summary { get; set; }
    }
}
