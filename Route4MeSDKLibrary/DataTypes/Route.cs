using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The route data structure
    /// </summary>
    [DataContract]
    public sealed class DataObjectRoute : DataObject
    {
        /// <summary>
        /// The route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteID { get; set; }

        /// <summary>
        /// The member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        /// The member's email
        /// </summary>
        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string MemberEmail { get; set; }

        /// <summary>
        /// The member's first name
        /// </summary>
        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string MemberFirstName { get; set; }

        /// <summary>
        /// The member's last name
        /// </summary>
        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string MemberLastName { get; set; }

        /// <summary>
        /// URL to a member picture
        /// </summary>
        [DataMember(Name = "member_picture", EmitDefaultValue = false)]
        public string MemberPicture { get; set; }

        /// <summary>
        /// Member tracking subheadline
        /// </summary>
        [DataMember(Name = "member_tracking_subheadline", EmitDefaultValue = false)]
        public string MemberTrackingSubheadline { get; set; }

        /// <summary>
        /// If true, the order is approved for execution
        /// </summary>
        [DataMember(Name = "approved_for_execution")]
        public bool ApprovedForExecution { get; set; }

        /// <summary>
        /// Counter of the approved revisions
        /// </summary>
        [DataMember(Name = "approved_revisions_counter", EmitDefaultValue = false)]
        public int? ApprovedRevisionsCounter { get; set; }

        /// <summary>
        /// Vehicle alias
        /// </summary>
        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Driver alias
        /// </summary>
        [DataMember(Name = "driver_alias", EmitDefaultValue = false)]
        public string DriverAlias { get; set; }

        /// <summary>
        /// Cost of the route
        /// </summary>
        [DataMember(Name = "route_cost", EmitDefaultValue = false)]
        public double? RouteCost { get; set; }

        /// <summary>
        /// Total route revenue
        /// </summary>
        [DataMember(Name = "route_revenue", EmitDefaultValue = false)]
        public double? RouteRevenue { get; set; }

        /// <summary>
        /// Net revenue per distance unit
        /// </summary>
        [DataMember(Name = "net_revenue_per_distance_unit", EmitDefaultValue = false)]
        public double? NetRevenuePerDistanceUnit { get; set; }

        /// <summary>
        /// Miles per gallon
        /// </summary>
        [DataMember(Name = "mpg", EmitDefaultValue = false)]
        public string mpg { get; set; }

        /// <summary>
        /// Total route's trip distance
        /// </summary>
        [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
        public double? TripDistance { get; set; }

        [DataMember(Name = "gas_price", EmitDefaultValue = false)]
        public double? GasPrice { get; set; }

        /// <summary>
        /// Total route's duration (seconds)
        /// </summary>
        [DataMember(Name = "route_duration_sec", EmitDefaultValue = false)]
        public int? RouteDurationSec { get; set; }

        /// <summary>
        /// Route notes
        /// </summary>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public AddressNote[] Notes { get; set; }
    }
}
