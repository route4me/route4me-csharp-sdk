using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Device tracking history data structure
    ///     <remarks>
    ///         <para>
    ///             Tracking data key names are shortened to reduce bandwidth usage (even with compression on
    ///         </para>
    ///     </remarks>
    /// </summary>
    [DataContract]
    public sealed class TrackingHistory
    {
        /// <summary>
        ///     Speed at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "s", EmitDefaultValue = false)]
        public string Speed { get; set; }

        /// <summary>
        ///     Speed unit ('mph', 'kph')
        /// </summary>
        [DataMember(Name = "su", EmitDefaultValue = false)]
        public string SpeedUnit { get; set; }

        /// <summary>
        ///     Latitude at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "lt", EmitDefaultValue = false)]
        public string Latitude { get; set; }

        /// <summary>
        ///     Member ID.
        /// </summary>
        [DataMember(Name = "m", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Longitude at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "lg", EmitDefaultValue = false)]
        public string Longitude { get; set; }

        /// <summary>
        ///     Direction/heading at the time of the location transaction event.
        /// </summary>
        [DataMember(Name = "d", EmitDefaultValue = false)]
        public int? D { get; set; }

        /// <summary>
        ///     The original timestamp in unix timestamp format at the moment location transaction event.
        /// </summary>
        [DataMember(Name = "ts", EmitDefaultValue = false)]
        public string TimeStamp { get; set; }

        /// <summary>
        ///     The original timestamp in a human readable timestamp format at the moment location transaction event.
        /// </summary>
        [DataMember(Name = "ts_friendly", EmitDefaultValue = false)]
        public string TimeStampFriendly { get; set; }

        /// <summary>
        ///     Package src (e.g. 'R4M').
        /// </summary>
        [DataMember(Name = "src", EmitDefaultValue = false)]
        public string Src { get; set; }
    }
}