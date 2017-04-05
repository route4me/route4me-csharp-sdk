using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class RouteResponse
    {
        [DataMember(Name = "route_id")]
        public string RouteID
        {
            get { return m_RouteID; }
            set { m_RouteID = value; }
        }
        private string m_RouteID;

        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId
        {
            get { return m_OptimizationProblemId; }
            set { m_OptimizationProblemId = value; }
        }
        private string m_OptimizationProblemId;

        /// <summary>
        /// Route rating by user [0 - 5]
        /// </summary>
        [DataMember(Name = "user_route_rating")]
        public System.Nullable<int> UserRouteRating
        {
            get { return m_UserRouteRating; }
            set { m_UserRouteRating = value; }
        }
        private System.Nullable<int> m_UserRouteRating;

        [DataMember(Name = "member_id")]
        public System.Nullable<int> MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private System.Nullable<int> m_MemberId;

        [DataMember(Name = "member_email")]
        public string MemberEmail
        {
            get { return m_MemberEmail; }
            set { m_MemberEmail = value; }
        }
        private string m_MemberEmail;

        [DataMember(Name = "member_first_name")]
        public string MemberFirstName
        {
            get { return m_MemberFirstName; }
            set { m_MemberFirstName = value; }
        }
        private string m_MemberFirstName;

        [DataMember(Name = "member_last_name")]
        public string MemberLastName
        {
            get { return m_MemberLastName; }
            set { m_MemberLastName = value; }
        }
        private string m_MemberLastName;

        [DataMember(Name = "channel_name")]
        public string ChannelName
        {
            get { return m_ChannelName; }
            set { m_ChannelName = value; }
        }
        private string m_ChannelName;

        [DataMember(Name = "vehicle_alias")]
        public string VehicleAlias
        {
            get { return m_VehicleAlias; }
            set { m_VehicleAlias = value; }
        }
        private string m_VehicleAlias;

        [DataMember(Name = "driver_alias")]
        public string DriverAlias
        {
            get { return m_DriverAlias; }
            set { m_DriverAlias = value; }
        }
        private string m_DriverAlias;

        [DataMember(Name = "trip_distance")]
        public System.Nullable<double> TripDistance
        {
            get { return m_TripDistance; }
            set { m_TripDistance = value; }
        }
        private System.Nullable<double> m_TripDistance;

        [DataMember(Name = "is_unrouted")]
        public bool IsUnrouted
        {
            get { return m_IsUnrouted; }
            set { m_IsUnrouted = value; }
        }
        private bool m_IsUnrouted;

        [DataMember(Name = "route_cost")]
        public System.Nullable<double> RouteCost
        {
            get { return m_RouteCost; }
            set { m_RouteCost = value; }
        }
        private System.Nullable<double> m_RouteCost;

        [DataMember(Name = "route_revenue")]
        public System.Nullable<double> RouteRevenue
        {
            get { return m_RouteRevenue; }
            set { m_RouteRevenue = value; }
        }
        private System.Nullable<double> m_RouteRevenue;

        [DataMember(Name = "net_revenue_per_distance_unit")]
        public System.Nullable<double> NetRevenuePerDistanceUnit
        {
            get { return m_NetRevenuePerDistanceUnit; }
            set { m_NetRevenuePerDistanceUnit = value; }
        }
        private System.Nullable<double> m_NetRevenuePerDistanceUnit;

        [DataMember(Name = "created_timestamp")]
        public System.Nullable<int> CreatedTimestamp
        {
            get { return m_CreatedTimestamp; }
            set { m_CreatedTimestamp = value; }
        }
        private System.Nullable<int> m_CreatedTimestamp;

        [DataMember(Name = "mpg")]
        public System.Nullable<double> mpg
        {
            get { return m_mpg; }
            set { m_mpg = value; }
        }
        private System.Nullable<double> m_mpg;

        [DataMember(Name = "gas_price")]
        public System.Nullable<double> GasPrice
        {
            get { return m_GasPrice; }
            set { m_GasPrice = value; }
        }
        private System.Nullable<double> m_GasPrice;

        [DataMember(Name = "route_duration_sec")]
        public System.Nullable<int> RouteDurationSec
        {
            get { return m_RouteDurationSec; }
            set { m_RouteDurationSec = value; }
        }
        private System.Nullable<int> m_RouteDurationSec;

        [DataMember(Name = "planned_total_route_duration")]
        public System.Nullable<int> PlannedTotalRouteDuration
        {
            get { return m_PlannedTotalRouteDuration; }
            set { m_PlannedTotalRouteDuration = value; }
        }
        private System.Nullable<int> m_PlannedTotalRouteDuration;

        [DataMember(Name = "actual_travel_distance")]
        public System.Nullable<double> ActualTravelDistance
        {
            get { return m_ActualTravelDistance; }
            set { m_ActualTravelDistance = value; }
        }
        private System.Nullable<double> m_ActualTravelDistance;

        [DataMember(Name = "actual_travel_time")]
        public System.Nullable<int> ActualTravelTime
        {
            get { return m_ActualTravelTime; }
            set { m_ActualTravelTime = value; }
        }
        private System.Nullable<int> m_ActualTravelTime;

        [DataMember(Name = "actual_footsteps")]
        public System.Nullable<int> ActualFootSteps
        {
            get { return m_ActualFootSteps; }
            set { m_ActualFootSteps = value; }
        }
        private System.Nullable<int> m_ActualFootSteps;

        [DataMember(Name = "working_time")]
        public System.Nullable<int> WorkingTime
        {
            get { return m_WorkingTime; }
            set { m_WorkingTime = value; }
        }
        private System.Nullable<int> m_WorkingTime;

        [DataMember(Name = "driving_time")]
        public System.Nullable<int> DrivingTime
        {
            get { return m_DrivingTime; }
            set { m_DrivingTime = value; }
        }
        private System.Nullable<int> m_DrivingTime;

        [DataMember(Name = "idling_time")]
        public System.Nullable<int> IdlingTime
        {
            get { return m_IdlingTime; }
            set { m_IdlingTime = value; }
        }
        private System.Nullable<int> m_IdlingTime;

        [DataMember(Name = "paying_miles")]
        public System.Nullable<double> PayingMiles
        {
            get { return m_PayingMiles; }
            set { m_PayingMiles = value; }
        }
        private System.Nullable<double> m_PayingMiles;

        [DataMember(Name = "geofence_polygon_type")]
        public string GeofencePolygonType
        {
            get { return m_GeofencePolygonType; }
            set { m_GeofencePolygonType = value; }
        }
        private string m_GeofencePolygonType;

        [DataMember(Name = "geofence_polygon_size")]
        public System.Nullable<int> GeofencePolygonSize
        {
            get { return m_GeofencePolygonSize; }
            set { m_GeofencePolygonSize = value; }
        }
        private System.Nullable<int> m_GeofencePolygonSize;

        [DataMember(Name = "parameters")]
        public RouteParameters Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }
        private RouteParameters m_Parameters;

        [DataMember(Name = "addresses")]
        public Address[] Addresses
        {
            get { return m_Addresses; }
            set { m_Addresses = value; }
        }
        private Address[] m_Addresses;

        [DataMember(Name = "links")]
        public Links Links
        {
            get { return m_Links; }
            set { m_Links = value; }
        }
        private Links m_Links;

        [DataMember(Name = "notes")]
        public AddressNote[] Notes
        {
            get { return m_Notes; }
            set { m_Notes = value; }
        }
        private AddressNote[] m_Notes;

        [DataMember(Name = "path")]
        public GeoPoint[] Path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }
        private GeoPoint[] m_Path;

        [DataMember(Name = "directions")]
        public Direction[] Directions
        {
            get { return m_Directions; }
            set { m_Directions = value; }
        }
        private Direction[] m_Directions;
    }
}
