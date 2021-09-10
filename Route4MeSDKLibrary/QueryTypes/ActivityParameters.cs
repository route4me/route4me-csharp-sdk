﻿namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the activity feed request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class ActivityParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of a route.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Unique ID of a device attached to a vehicle.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Unique ID of a member.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        /// Limit the number of records in response.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        /// <summary>
        /// If equal to 'true' the response will include team activities.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "team", EmitDefaultValue = false)]
        public string Team { get; set; }

        /// <summary>
        /// Start of the time filter.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "start", EmitDefaultValue = false)]
        public long? Start { get; set; }

        /// <summary>
        /// End of the time filter.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "end", EmitDefaultValue = false)]
        public long? End { get; set; }

        /// <summary>
        /// If specified, the response will include only activities of the specified type.
        /// <remarks><para>Query parameter.</para></remarks>
        /// <para>Available values:</para>
        /// <para>'', 'area-removed', 'area-added', 'area-updated', 'delete-destination', </para>
        /// <para>'insert-destination', 'destination-out-sequence', 'driver-arrived-early', 'driver-arrived-late', </para>
        /// <para>'driver-arrived-on-time', 'geofence-left', 'geofence-entered', 'mark-destination-departed', </para>
        /// <para>'mark-destination-visited', 'member-created', 'member-deleted', 'member-modified', </para>
        /// <para>'move-destination', 'note-insert', 'route-delete', 'route-optimized', </para>
        /// <para>'route-owner-changed', 'route-duplicate', 'update-destinations', 'user_message'</para>
        /// <para>'order-created', 'order-updated', 'order-deleted', 'unapproved-to-execute', 'route-update'</para>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "activity_type", EmitDefaultValue = false)]
        public string ActivityType { get; set; }

        /// <summary>
        /// Activity message.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "activity_message", EmitDefaultValue = false)]
        public string ActivityMessage { get; set; }
    }
}
