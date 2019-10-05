using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class UserLocation
    {
        /// <summary>
        /// Member data.
        /// </summary>
        [DataMember(Name = "member_data")]
        public User MemberData { get; set; }

        /// <summary>
        /// User tracking.
        /// </summary>
        [DataMember(Name = "tracking")]
        public UserTracking UserTracking { get; set; }

        /// <summary>
        /// If true, getting a location from the cache enabled.
        /// </summary>
        [DataMember(Name = "from_cache", EmitDefaultValue = false)]
        public bool? FromCache { get; set; }
    }

    public class UserTracking
    {
        /// <summary>
        /// Position longitude
        /// </summary>
        [DataMember(Name = "position_lng")]
        public double PositionLongitude { get; set; }

        /// <summary>
        /// Position latitude
        /// </summary>
        [DataMember(Name = "position_lat")]
        public double PositionLatitude { get; set; }

        /// <summary>
        /// Movement direction in the degrees (north = 0, clockwise).
        /// </summary>
        [DataMember(Name = "direction")]
        public int? Direction { get; set; }

        /// <summary>
        /// Data source name.
        /// </summary>
        [DataMember(Name = "data_source_name")]
        public string DataSourceName { get; set; }

        /// <summary>
        /// Activity timestamp (EPOCH).
        /// </summary>
        [DataMember(Name = "activity_timestamp")]
        public long? ActivityTimestamp { get; set; }

        /// <summary>
        /// Device timestamp (EPOCH).
        /// </summary>
        [DataMember(Name = "device_timestamp")]
        public long? DeviceTimestamp { get; set; }

        /// <summary>
        /// Route ID.
        /// </summary>
        [DataMember(Name = "route_id")]
        public string RouteId { get; set; }

        /// <summary>
        /// Device ID.
        /// </summary>
        [DataMember(Name = "device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Vehicle movement speed.
        /// </summary>
        [DataMember(Name = "speed")]
        public decimal? Speed { get; set; }

        /// <summary>
        /// Vehicle movement speed.
        /// </summary>
        [DataMember(Name = "altitude")]
        public int? Altitude { get; set; }

        /// <summary>
        /// Footsteps.
        /// </summary>
        [DataMember(Name = "footsteps")]
        public int? Footsteps { get; set; }

        /// <summary>
        ///User email.
        /// </summary>
        [DataMember(Name = "custom_data")]
        public Dictionary<string,string> CustomData { get; set; }

        /// <summary>
        /// Device timezone (e.g. 'America/New_York').
        /// </summary>
        [DataMember(Name = "device_timezone", EmitDefaultValue = false)]
        public string DeviceTimezone { get; set; }

        /// <summary>
        /// Device timezone offset.
        /// </summary>
        [DataMember(Name = "device_timezone_offset", EmitDefaultValue = false)]
        public int? DeviceTimezoneOffset { get; set; }

        /// <summary>
        /// Vehicle ID.
        /// </summary>
        [DataMember(Name = "vehicle_id_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// Day ID.
        /// </summary>
        [DataMember(Name = "day_id", EmitDefaultValue = false)]
        public int? DayId { get; set; }

        /// <summary>
        /// Device type.
        /// </summary>
        [DataMember(Name = "device_type")]
        public string DeviceType { get; set; }

        /// <summary>
        /// Unique ID of the member inside the Route4Me system.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        /// Activity timestamp friendly.
        /// </summary>
        [DataMember(Name = "activity_timestamp_friendly")]
        public string ActivityTimestampFriendly { get; set; }

        /// <summary>
        /// Timestamp of a last known location.
        /// </summary>
        [DataMember(Name = "LAST_KNOWN", EmitDefaultValue = false)]
        public int? LastKnown { get; set; }
    }
}
