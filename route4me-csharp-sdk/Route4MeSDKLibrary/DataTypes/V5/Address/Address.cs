using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Route destination class.
    ///     <para>
    ///         See the JSON schema at
    ///         <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Address.dtd" />
    ///     </para>
    /// </summary>
    [DataContract]
    public class Address : GenericParameters
    {
        /// <summary>
        ///     Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int? RouteDestinationId { get; set; }

        /// <summary>
        ///     Address alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        ///     Member ID
        /// </summary>
        // The ID of the member inside the Route4Me system.
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        ///     Route destination address
        /// </summary>
        [DataMember(Name = "address")]
        public string AddressString { get; set; }

        /// <summary>
        ///     Route address stop type
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        ///     Designate this stop as a depot.
        ///     A route may have multiple depots/points of origin.
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }

        /// <summary>
        ///     Timeframe violation state
        /// </summary>
        [DataMember(Name = "timeframe_violation_state", EmitDefaultValue = false)]
        public int? TimeframeViolationState { get; set; }

        /// <summary>
        ///     Timeframe violation time
        /// </summary>
        [DataMember(Name = "timeframe_violation_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? TimeframeViolationTime { get; set; }

        /// <summary>
        ///     Timeframe violation rate
        /// </summary>
        [DataMember(Name = "timeframe_violation_rate", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? TimeframeViolationRate { get; set; }

        /// <summary>
        ///     The latitude of this address
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     The longitude of this address
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        ///     The ID of the route being viewed, modified, or erased.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     If this route was duplicated from an existing route, this value would have the original route's ID.
        /// </summary>
        [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
        public string OriginalRouteId { get; set; }

        /// <summary>
        ///     Route name of a depot address.
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        ///     The ID of the optimization request that was used to initially instantiate this route.
        /// </summary>
        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        ///     The destination's sequence number in the route.
        /// </summary>
        [DataMember(Name = "sequence_no", EmitDefaultValue = false)]
        public int? SequenceNo { get; set; }

        /// <summary>
        ///     True if the address is geocoded.
        /// </summary>
        [DataMember(Name = "geocoded", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool? Geocoded { get; set; }

        /// <summary>
        ///     The preferred geocoding number.
        /// </summary>
        [DataMember(Name = "preferred_geocoding", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? PreferredGeocoding { get; set; }

        /// <summary>
        ///     True if geocoding failed.
        /// </summary>
        [DataMember(Name = "failed_geocoding", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool? FailedGeocoding { get; set; }

        /// <summary>
        ///     An array containing Geocoding objects.
        /// </summary>
        [DataMember(Name = "geocodings", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public Geocoding[] Geocodings { get; set; }

        /// <summary>
        ///     When planning a route from the address book or using existing address book IDs,
        ///     pass the address book ID (contact_id) for an address so that Route4Me can run
        ///     analytics on the address book addresses that were used to plan routes, and to find previous visits to
        ///     favorite addresses.
        /// </summary>
        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public int? ContactId { get; set; }

        /// <summary>
        ///     The status flag to mark an address as visited (aka check in).
        /// </summary>
        [DataMember(Name = "is_visited", EmitDefaultValue = false)]
        public bool? IsVisited { get; set; }

        /// <summary>
        ///     The status flag to mark an address as departed (aka check out).
        /// </summary>
        [DataMember(Name = "is_departed", EmitDefaultValue = false)]
        public bool? IsDeparted { get; set; }

        /// <summary>
        ///     The last known visited timestamp of this address.
        /// </summary>
        [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
        public long? TimestampLastVisited { get; set; }

        /// <summary>
        ///     The last known departed timestamp of this address.
        /// </summary>
        [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
        public long? TimestampLastDeparted { get; set; }

        /// <summary>
        ///     Visited address latitude
        /// </summary>
        [DataMember(Name = "visited_lat")]
        public double? VisitedLatitude { get; set; }

        /// <summary>
        ///     Visited address longitude
        /// </summary>
        [DataMember(Name = "visited_lng")]
        public double? VisitedLongitude { get; set; }

        /// <summary>
        ///     Departed address latitude
        /// </summary>
        [DataMember(Name = "departed_lat")]
        public double? DepartedLatitude { get; set; }

        /// <summary>
        ///     Departed address longitude
        /// </summary>
        [DataMember(Name = "departed_lng")]
        public double? DepartedLongitude { get; set; }

        /// <summary>
        ///     The address group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [DataMember(Name = "customer_po", EmitDefaultValue = false)]
        public string CustomerPo { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
        public string InvoiceNo { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [DataMember(Name = "reference_no", EmitDefaultValue = false)]
        public string ReferenceNo { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [DataMember(Name = "order_no", EmitDefaultValue = false)]
        public string OrderNo { get; set; }

        /// <summary>
        ///     The address order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? OrderId { get; set; }

        /// <summary>
        ///     The address cargo weight
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        ///     The address cost
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        ///     The address revenue
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        ///     The cubic volume that this destination/order/line-item consumes/contains.
        ///     This is how much space it will take up on a vehicle.
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

        /// <summary>
        ///     The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        /// </summary>
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public int? Pieces { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        ///     Also used to email clients when vehicles are approaching (future capability).
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        ///     Pass-through data about this route destination.
        ///     The data will be visible on the manifest, website, and mobile apps.
        ///     Also used to send SMS messages to clients when vehicles are approaching (future capability).
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        ///     The number of notes that are already associated with this address on the route.
        /// </summary>
        [DataMember(Name = "destination_note_count", EmitDefaultValue = false)]
        public int? DestinationNoteCount { get; set; }

        /// <summary>
        ///     Server-side generated amount of km/miles that it will take to get to the next location on the route.
        /// </summary>
        [DataMember(Name = "drive_time_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? DriveTimeToNextDestination { get; set; }

        /// <summary>
        ///     Abnormal traffic time to next destination.
        /// </summary>
        [DataMember(Name = "abnormal_traffic_time_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? AbnormalTrafficTimeToNextDestination { get; set; }

        /// <summary>
        ///     Uncongested time to next destination.
        /// </summary>
        [DataMember(Name = "uncongested_time_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? UncongestedTimeToNextDestination { get; set; }

        /// <summary>
        ///     Traffic time to next destination.
        /// </summary>
        [DataMember(Name = "traffic_time_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? TrafficTimeToNextDestination { get; set; }

        /// <summary>
        ///     Server-side generated amount of seconds that it will take to get to the next location.
        /// </summary>
        [DataMember(Name = "distance_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? DistanceToNextDestination { get; set; }

        /// <summary>
        ///     UDU distance to next destination.
        /// </summary>
        [DataMember(Name = "udu_distance_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? UduDistanceToNextDestination { get; set; }

        /// <summary>
        ///     Generated time window start.
        /// </summary>
        [DataMember(Name = "generated_time_window_start", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? GeneratedTimeWindowStart { get; set; }

        /// <summary>
        ///     Estimated time window end based on the optimization engine, after all the sequencing has been completed.
        /// </summary>
        [DataMember(Name = "generated_time_window_end", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? GeneratedTimeWindowEnd { get; set; }

        /// <summary>
        ///     The unique socket channel name which should be used to get real time alerts.
        /// </summary>
        [DataMember(Name = "channel_name", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string ChannelName { get; set; }

        /// <summary>
        ///     The address time window start.
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public long? TimeWindowStart { get; set; }

        /// <summary>
        ///     The address time window end.
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public long? TimeWindowEnd { get; set; }

        /// <summary>
        ///     The address time window start 2.
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public long? TimeWindowStart2 { get; set; }

        /// <summary>
        ///     The address time window end 2.
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public long? TimeWindowEnd2 { get; set; }

        /// <summary>
        ///     Geofence detected visited timestamp
        /// </summary>
        [DataMember(Name = "geofence_detected_visited_timestamp", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? geofence_detected_visited_timestamp { get; set; }

        /// <summary>
        ///     Geofence detected departed timestamp
        /// </summary>
        [DataMember(Name = "geofence_detected_departed_timestamp", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? geofence_detected_departed_timestamp { get; set; }

        /// <summary>
        ///     Geofence detected service time
        /// </summary>
        [DataMember(Name = "geofence_detected_service_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? geofence_detected_service_time { get; set; }

        /// <summary>
        ///     Geofence detected visited latitude
        /// </summary>
        [DataMember(Name = "geofence_detected_visited_lat", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? geofence_detected_visited_lat { get; set; }

        /// <summary>
        ///     Geofence detected visited longitude
        /// </summary>
        [DataMember(Name = "geofence_detected_visited_lng", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? geofence_detected_visited_lng { get; set; }

        /// <summary>
        ///     Geofence detected departed latitude
        /// </summary>
        [DataMember(Name = "geofence_detected_departed_lat", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? geofence_detected_departed_lat { get; set; }

        /// <summary>
        ///     Geofence detected departed longitude
        /// </summary>
        [DataMember(Name = "geofence_detected_departed_lng", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? geofence_detected_departed_lng { get; set; }

        /// <summary>
        ///     The expected amount of time that will be spent at this address by the driver/user.
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public long? Time { get; set; }

        /// <summary>
        ///     The address notes
        /// </summary>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public AddressNote[] Notes { get; set; }

        /// <summary>
        ///     The route path point.
        /// </summary>
        [DataMember(Name = "path_to_next", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public GeoPoint[] PathToNext { get; set; }

        /// <summary>
        ///     If present, the priority will sequence addresses in all the optimal routes so that
        ///     higher priority addresses are general at the beginning of the route sequence.
        ///     1 is the highest priority, 100000 is the lowest.
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public int? Priority { get; set; }

        /// <summary>
        ///     Curbside latitude.
        ///     Generate optimal routes and driving directions to this curbside latitude.
        /// </summary>
        [DataMember(Name = "curbside_lat")]
        public double? CurbsideLatitude { get; set; }

        /// <summary>
        ///     Curbside longitude.
        ///     Generate optimal routes and driving directions to the curbside longitude.
        /// </summary>
        [DataMember(Name = "curbside_lng")]
        public double? CurbsideLongitude { get; set; }

        /// <summary>
        ///     The address custom fields.
        /// </summary>
        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomFields { get; set; }

        /// <summary>
        ///     The address custom fields in JSON format.
        /// </summary>
        [DataMember(Name = "custom_fields_str_json", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string CustomFieldsStrJson { get; set; }

        /// <summary>
        ///     The custom fields configuration.
        /// </summary>
        [DataMember(Name = "custom_fields_config", EmitDefaultValue = false)]
        public string[] CustomFieldsConfig { get; set; }

        /// <summary>
        ///     The custom fields configuration in JSON format.
        /// </summary>
        [DataMember(Name = "custom_fields_config_str_json", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string CustomFieldsConfigStrJson { get; set; }

        /// <summary>
        ///     System-wide unique code, which permits end-users (recipients) to track the status of their order.
        /// </summary>
        [DataMember(Name = "tracking_number", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string tracking_number { get; set; }

        /// <summary>
        ///     Wait time to next destination.
        /// </summary>
        [DataMember(Name = "wait_time_to_next_destination", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? WaitTimeToNextDestination { get; set; }

        /// <summary>
        ///     Manifest of a route address.
        /// </summary>
        [DataMember(Name = "manifest", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public AddressManifest Manifest { get; set; }

        /// <summary>
        ///     Alias of a pickup point.
        /// </summary>
        [DataMember(Name = "pickup", EmitDefaultValue = false)]
        public string Pickup { get; set; }

        /// <summary>
        ///     Alias of the paired pickup point.
        /// </summary>
        [DataMember(Name = "dropoff", EmitDefaultValue = false)]
        public string Dropoff { get; set; }

        /// <summary>
        ///     If true, the pickup and dropoff addresses are joint
        ///     (one by one despite the regular pickup-dropoff addresses
        ///     when it's possible to have multiple pickup addresses with one dropoff address).
        /// </summary>
        [DataMember(Name = "joint", EmitDefaultValue = false)]
        public bool? Joint { get; set; }

        /// <summary>
        ///     Bundle count
        /// </summary>
        [DataMember(Name = "bundle_count", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public int BundleCount { get; set; }

        /// <summary>
        ///     Bundle items
        /// </summary>
        [DataMember(Name = "bundle_items", EmitDefaultValue = true)]
        [ReadOnly(true)]
        public BundledItemResponse[] BundleItems { get; set; }

        /// <summary>
        ///     List of the order inventories
        /// </summary>
        [DataMember(Name = "order_inventory", EmitDefaultValue = true)]
        [ReadOnly(true)]
        public OrderInventory[] OrderInventory { get; set; }

        /// <summary>
        ///     The driver tags specified in a team member's custom data.
        ///     (e.g. "driver skills":
        ///     ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        /// </summary>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        [DefaultValue(false)]
        public string[] Tags { get; set; }
    }
}