using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class UserLocation
    {
        /// <summary>
        ///     Member data
        /// </summary>
        [DataMember(Name = "member_data", EmitDefaultValue = false)]
        public User MemberData { get; set; }

        /// <summary>
        ///     User tracking
        /// </summary>
        [DataMember(Name = "tracking", EmitDefaultValue = false)]
        public UserTracking UserTracking { get; set; }

        /// <summary>
        ///     If true, getting a location from the cache enabled.
        /// </summary>
        [DataMember(Name = "from_cache", EmitDefaultValue = false)]
        public bool? FromCache { get; set; }
    }

    public class UserTracking
    {
        /// <summary>
        ///     Position longitude
        /// </summary>
        [DataMember(Name = "position_lng", EmitDefaultValue = false)]
        public double PositionLongitude { get; set; }

        /// <summary>
        ///     Position latitude
        /// </summary>
        [DataMember(Name = "position_lat", EmitDefaultValue = false)]
        public double PositionLatitude { get; set; }

        /// <summary>
        ///     Movement direction in the degrees (north = 0, clockwise).
        /// </summary>
        [DataMember(Name = "direction", EmitDefaultValue = false)]
        public int? Direction { get; set; }

        /// <summary>
        ///     Data source name
        /// </summary>
        [DataMember(Name = "data_source_name", EmitDefaultValue = false)]
        public string DataSourceName { get; set; }

        /// <summary>
        ///     Activity timestamp (EPOCH).
        /// </summary>
        [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
        public long? ActivityTimestamp { get; set; }

        /// <summary>
        ///     Device timestamp (EPOCH).
        /// </summary>
        [DataMember(Name = "device_timestamp", EmitDefaultValue = false)]
        public long? DeviceTimestamp { get; set; }

        /// <summary>
        ///     Route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Device ID
        /// </summary>
        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceId { get; set; }

        /// <summary>
        ///     Vehicle movement speed
        /// </summary>
        [DataMember(Name = "speed", EmitDefaultValue = false)]
        public double? Speed { get; set; }

        /// <summary>
        ///     Calculated speed
        /// </summary>
        [DataMember(Name = "calculated_speed", EmitDefaultValue = false)]
        public string CalculatedSpeed { get; set; }

        /// <summary>
        ///     Speed Accuracy
        /// </summary>
        [DataMember(Name = "speed_accuracy", EmitDefaultValue = false)]
        public string SpeedAccuracy { get; set; }

        /// <summary>
        ///     Speed Unit
        /// </summary>
        [DataMember(Name = "speed_unit", EmitDefaultValue = false)]
        public string SpeedUnit { get; set; }

        /// <summary>
        ///     Bearing
        /// </summary>
        [DataMember(Name = "bearing", EmitDefaultValue = false)]
        public int? Bearing { get; set; }

        /// <summary>
        ///     Bearing accuracy
        /// </summary>
        [DataMember(Name = "bearing_accuracy", EmitDefaultValue = false)]
        public string BearingAccuracy { get; set; }

        /// <summary>
        ///     Accuracy
        /// </summary>
        [DataMember(Name = "accuracy", EmitDefaultValue = false)]
        public string Accuracy { get; set; }

        /// <summary>
        ///     Vehicle movement speed.
        /// </summary>
        [DataMember(Name = "altitude", EmitDefaultValue = false)]
        public int? Altitude { get; set; }

        /// <summary>
        ///     Footsteps
        /// </summary>
        [DataMember(Name = "footsteps", EmitDefaultValue = false)]
        public int? Footsteps { get; set; }

        /// <summary>
        ///     User email
        /// </summary>
        [DataMember(Name = "custom_data")]
        public Dictionary<string, string> CustomData { get; set; }

        /// <summary>
        ///     Device timezone (e.g. 'America/New_York').
        /// </summary>
        [DataMember(Name = "device_timezone", EmitDefaultValue = false)]
        public string DeviceTimezone { get; set; }

        /// <summary>
        ///     Device timezone offset
        /// </summary>
        [DataMember(Name = "device_timezone_offset", EmitDefaultValue = false)]
        public int? DeviceTimezoneOffset { get; set; }

        /// <summary>
        ///     Vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Day ID
        /// </summary>
        [DataMember(Name = "day_id", EmitDefaultValue = false)]
        public int? DayId { get; set; }

        /// <summary>
        ///     Device type
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        /// <summary>
        ///     Unique ID of the member inside the Route4Me system.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>
        ///     Activity timestamp friendly.
        /// </summary>
        [DataMember(Name = "activity_timestamp_friendly", EmitDefaultValue = false)]
        public string ActivityTimestampFriendly { get; set; }

        /// <summary>
        ///     Timestamp of a last known location.
        /// </summary>
        [DataMember(Name = "LAST_KNOWN", EmitDefaultValue = false)]
        public int? LastKnown { get; set; }
    }
}