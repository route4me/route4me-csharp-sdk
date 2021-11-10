using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     URL query parameters for retrieving the schedule calendar.
    /// </summary>
    [DataContract]
    public sealed class ScheduleCalendarQuery : GenericParameters
    {
        private int? timezoneOffsetMinutes;

        /// <summary>
        ///     Start date to filter the schedules in the string format (e.g. 2020-10-27).
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "date_from_string", EmitDefaultValue = false)]
        public string DateFromString { get; set; }

        // <summary>
        /// End date to filter the schedules in the string format (e.g. 2020-10-30).
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "date_to_string", EmitDefaultValue = false)]
        public string DateToString { get; set; }

        /// <summary>
        ///     Member ID
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Timezone offset (in minutes) (e.g. NYT: -4*60 = -480, Kiev: 3*60 = 180).
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "timezone_offset_minutes", EmitDefaultValue = false)]
        public int? TimezoneOffsetMinutes
        {
            get => -timezoneOffsetMinutes;
            set => timezoneOffsetMinutes = value != null ? -value : default;
        }

        /// <summary>
        ///     If true, the scheduled orders are included in the calendar.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "orders", EmitDefaultValue = false)]
        public bool? ShowOrders { get; set; }

        /// <summary>
        ///     If true, the scheduled address book contacts are included in the calendar.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "ab", EmitDefaultValue = false)]
        public bool? ShowContacts { get; set; }

        /// <summary>
        ///     If true, the scheduled routes are included in the calendar.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "routes_count", EmitDefaultValue = false)]
        public bool? RoutesCount { get; set; }
    }
}