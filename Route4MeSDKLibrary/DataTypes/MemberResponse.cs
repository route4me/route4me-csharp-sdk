using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class MemberResponse
    {
        [DataMember(Name = "status")]
        public System.Nullable<bool> Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        private System.Nullable<bool> m_Status;

        [DataMember(Name = "geocoding_service")]
        public string GeocodingService
        {
            get { return m_GeocodingService; }
            set { m_GeocodingService = value; }
        }
        private string m_GeocodingService;

        [DataMember(Name = "session_id")]
        public System.Nullable<int> SessionId
        {
            get { return m_SessionId; }
            set { m_SessionId = value; }
        }
        private System.Nullable<int> m_SessionId;

        [DataMember(Name = "session_guid")]
        public string SessionGuid
        {
            get { return m_SessionGuid; }
            set { m_SessionGuid = value; }
        }
        private string m_SessionGuid;

        [DataMember(Name = "member_id")]
        public System.Nullable<int> MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private System.Nullable<int> m_MemberId;

        [DataMember(Name = "api_key")]
        public string ApiKey
        {
            get { return m_ApiKey; }
            set { m_ApiKey = value; }
        }
        private string m_ApiKey;

        [DataMember(Name = "tracking_ttl")]
        public System.Nullable<int> TrackingTtl
        {
            get { return m_TrackingTtl; }
            set { m_TrackingTtl = value; }
        }
        private System.Nullable<int> m_TrackingTtl;

        [DataMember(Name = "geofence_polygon_shape")]
        public string GeofencePolygonShape
        {
            get { return m_GeofencePolygonShape; }
            set { m_GeofencePolygonShape = value; }
        }
        private string m_GeofencePolygonShape;

        [DataMember(Name = "geofence_polygon_size")]
        public System.Nullable<int> GeofencePolygonSize
        {
            get { return m_GeofencePolygonSize; }
            set { m_GeofencePolygonSize = value; }
        }
        private System.Nullable<int> m_GeofencePolygonSize;

        [DataMember(Name = "geofence_time_onsite_trigger_secs")]
        public System.Nullable<int> GeofenceTimeOnsiteTriggerSecs
        {
            get { return m_GeofenceTimeOnsiteTriggerSecs; }
            set { m_GeofenceTimeOnsiteTriggerSecs = value; }
        }
        private System.Nullable<int> m_GeofenceTimeOnsiteTriggerSecs;

        [DataMember(Name = "geofence_minimum_trigger_speed")]
        public System.Nullable<int> GeofenceMinimumTriggerSpeed
        {
            get { return m_GeofenceMinimumTriggerSpeed; }
            set { m_GeofenceMinimumTriggerSpeed = value; }
        }
        private System.Nullable<int> m_GeofenceMinimumTriggerSpeed;

        [DataMember(Name = "is_subscription_past_due")]
        public System.Nullable<bool> IsSubscriptionPastDue
        {
            get { return m_IsSubscriptionPastDue; }
            set { m_IsSubscriptionPastDue = value; }
        }
        private System.Nullable<bool> m_IsSubscriptionPastDue;

        [DataMember(Name = "visited_departed_enabled")]
        public string VisitedDepartedEnabled
        {
            get { return m_VisitedDepartedEnabled; }
            set { m_VisitedDepartedEnabled = value; }
        }
        private string m_VisitedDepartedEnabled;

        [DataMember(Name = "long_press_enabled")]
        public string LongPressEnabled
        {
            get { return m_LongPressEnabled; }
            set { m_LongPressEnabled = value; }
        }
        private string m_LongPressEnabled;

        [DataMember(Name = "account_type_id")]
        public System.Nullable<int> AccountTypeId
        {
            get { return m_AccountTypeId; }
            set { m_AccountTypeId = value; }
        }
        private System.Nullable<int> m_AccountTypeId;

        [DataMember(Name = "account_type_alias")]
        public string AccountTypeAlias
        {
            get { return m_AccountTypeAlias; }
            set { m_AccountTypeAlias = value; }
        }
        private string m_AccountTypeAlias;

        [DataMember(Name = "member_type")]
        public string MemberType
        {
            get { return m_MemberType; }
            set { m_MemberType = value; }
        }
        private string m_MemberType;

        [DataMember(Name = "max_stops_per_route")]
        public System.Nullable<int> MaxStopsPerRoute
        {
            get { return m_MaxStopsPerRoute; }
            set { m_MaxStopsPerRoute = value; }
        }
        private System.Nullable<int> m_MaxStopsPerRoute;

        [DataMember(Name = "max_routes")]
        public System.Nullable<int> MaxRoutes
        {
            get { return m_MaxRoutes; }
            set { m_MaxRoutes = value; }
        }
        private System.Nullable<int> m_MaxRoutes;

        [DataMember(Name = "routes_planned")]
        public System.Nullable<int> RoutesPlanned
        {
            get { return m_RoutesPlanned; }
            set { m_RoutesPlanned = value; }
        }
        private System.Nullable<int> m_RoutesPlanned;

        [DataMember(Name = "preferred_units")]
        public string PreferredUnits
        {
            get { return m_PreferredUnits; }
            set { m_PreferredUnits = value; }
        }
        private string m_PreferredUnits;

        [DataMember(Name = "preferred_language")]
        public string PreferredLanguage
        {
            get { return m_PreferredLanguage; }
            set { m_PreferredLanguage = value; }
        }
        private string m_PreferredLanguage;

        [DataMember(Name = "HIDE_ROUTED_ADDRESSES")]
        public string HideRoutedAddresses
        {
            get { return m_HideRoutedAddresses; }
            set { m_HideRoutedAddresses = value; }
        }
        private string m_HideRoutedAddresses;

        [DataMember(Name = "HIDE_VISITED_ADDRESSES")]
        public string HideVisitedAddresses
        {
            get { return m_HideVisitedAddresses; }
            set { m_HideVisitedAddresses = value; }
        }
        private string m_HideVisitedAddresses;

        [DataMember(Name = "HIDE_NONFUTURE_ROUTES")]
        public string HideNonfutureAddresses
        {
            get { return m_HideNonfutureAddresses; }
            set { m_HideNonfutureAddresses = value; }
        }
        private string m_HideNonfutureAddresses;

        [DataMember(Name = "auto_logout_ts")]
        public System.Nullable<int> AutoLogoutTs
        {
            get { return m_AutoLogoutTs; }
            set { m_AutoLogoutTs = value; }
        }
        private System.Nullable<int> m_AutoLogoutTs;
    }
}