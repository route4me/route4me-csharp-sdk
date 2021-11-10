using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response for the user's authentication, registration, session validation process
    /// </summary>
    [DataContract]
    public sealed class MemberResponse
    {
        /// <summary>
        ///     Process status
        /// </summary>
        [DataMember(Name = "status")]
        public bool? Status { get; set; }

        /// <summary>
        ///     Geocoding service
        /// </summary>
        [DataMember(Name = "geocoding_service")]
        public string GeocodingService { get; set; }

        /// <summary>
        ///     Session ID
        /// </summary>
        [DataMember(Name = "session_id")]
        public int? SessionId { get; set; }

        /// <summary>
        ///     Session GUID
        /// </summary>
        [DataMember(Name = "session_guid")]
        public string SessionGuid { get; set; }

        /// <summary>
        ///     Member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        ///     User's API key
        /// </summary>
        [DataMember(Name = "api_key")]
        public string ApiKey { get; set; }

        /// <summary>
        ///     Tracking TTL
        /// </summary>
        [DataMember(Name = "tracking_ttl")]
        public int? TrackingTtl { get; set; }

        /// <summary>
        ///     Geofence polygon shape. Available values: circle, poly, rect.
        /// </summary>
        [DataMember(Name = "geofence_polygon_shape")]
        public string GeofencePolygonShape { get; set; }

        /// <summary>
        ///     Geofence polygon size
        /// </summary>
        [DataMember(Name = "geofence_polygon_size")]
        public int? GeofencePolygonSize { get; set; }

        /// <summary>
        ///     Geofence onsite trigger time (seconds)
        /// </summary>
        [DataMember(Name = "geofence_time_onsite_trigger_secs")]
        public long? GeofenceTimeOnsiteTriggerSecs { get; set; }

        /// <summary>
        ///     Geofence's minimum trigger speed
        /// </summary>
        [DataMember(Name = "geofence_minimum_trigger_speed")]
        public int? GeofenceMinimumTriggerSpeed { get; set; }

        /// <summary>
        ///     True if the subscription is past due
        /// </summary>
        [DataMember(Name = "is_subscription_past_due")]
        public bool? IsSubscriptionPastDue { get; set; }

        /// <summary>
        ///     If true, triggering of the visited and departed activities is enabled.
        /// </summary>
        [DataMember(Name = "visited_departed_enabled")]
        public string VisitedDepartedEnabled { get; set; }

        /// <summary>
        ///     If true, long press is enabled
        /// </summary>
        [DataMember(Name = "long_press_enabled")]
        public string LongPressEnabled { get; set; }

        /// <summary>
        ///     The account type ID
        /// </summary>
        [DataMember(Name = "account_type_id")]
        public int? AccountTypeId { get; set; }

        /// <summary>
        ///     Account type alias
        /// </summary>
        [DataMember(Name = "account_type_alias")]
        public string AccountTypeAlias { get; set; }

        /// <summary>
        ///     Member type. Available values:
        ///     <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ///     <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER,</para>
        ///     <para>SUB_ACCOUNT_ANALYSTSUB_ACCOUNT_VENDORSUB_ACCOUNT_CUSTOMER_SERVICE</para>
        /// </summary>
        [DataMember(Name = "member_type")]
        public string MemberType { get; set; }

        /// <summary>
        ///     Maximum allowed number of the stops per route.
        /// </summary>
        [DataMember(Name = "max_stops_per_route")]
        public int? MaxStopsPerRoute { get; set; }

        /// <summary>
        ///     Maximum allowed number of the generated routes
        /// </summary>
        [DataMember(Name = "max_routes")]
        public int? MaxRoutes { get; set; }

        /// <summary>
        ///     Number of the planned routes by the user
        /// </summary>
        [DataMember(Name = "routes_planned")]
        public int? RoutesPlanned { get; set; }

        /// <summary>
        ///     Preferred units (mi, km)
        /// </summary>
        [DataMember(Name = "preferred_units")]
        public string PreferredUnits { get; set; }

        /// <summary>
        ///     Preferred language (en, fr)
        /// </summary>
        [DataMember(Name = "preferred_language")]
        public string PreferredLanguage { get; set; }

        /// <summary>
        ///     If true, routed addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_ROUTED_ADDRESSES")]
        public string HideRoutedAddresses { get; set; }

        /// <summary>
        ///     If true, visited addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_VISITED_ADDRESSES")]
        public string HideVisitedAddresses { get; set; }

        /// <summary>
        ///     If true, nonfuture routes will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES")]
        public string HideNonfutureAddresses { get; set; }

        /// <summary>
        ///     Time in seconds. A user will be logged out after been inactive during specified by this parameter seconds.
        /// </summary>
        [DataMember(Name = "auto_logout_ts")]
        public long? AutoLogoutTs { get; set; }
    }
}