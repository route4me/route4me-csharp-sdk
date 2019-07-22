using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Device tracking history data structure
    /// <remarks><para>
    /// Tracking data key names are shortened to reduce bandwidth usage (even with compression on
    /// </para></remarks>
    /// </summary>
    [DataContract]
    public sealed class TrackingHistory
    {
        /// <summary>
        /// Speed at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "s")]
        public double? Speed { get; set; }

        /// <summary>
        /// Latitude at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "lt")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "lg")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Direction/heading at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "d")]
        public string D { get; set; }

        /// <summary>
        /// The original timestamp in unix timestamp format at the moment location transaction event.
        /// </summary>
        [DataMember(Name = "ts")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// The original timestamp in a human readable timestamp format at the moment location transaction event.
        /// </summary>
        [DataMember(Name = "ts_friendly")]
        public string TimeStampFriendly { get; set; }
    }
}
