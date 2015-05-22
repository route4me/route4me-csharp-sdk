using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Address data-structure
  /// See https://www.assembla.com/wiki/show/route4me_api/Route_Address_V4
  /// </summary>
  [DataContract]
  public sealed class Address
  {
    [DataMember(Name = "RouteDestinationId", EmitDefaultValue = false)]
    public int? RouteDestinationId { get; set; }

    [DataMember(Name = "alias", EmitDefaultValue = false)]
    public string Alias { get; set; }

    [DataMember(Name = "MemberId", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    [DataMember(Name = "address")]
    public string AddressString { get; set; }

    [DataMember(Name = "is_depot", EmitDefaultValue = false)]
    public bool? IsDepot { get; set; }

    [DataMember(Name = "lat")]
    public double Latitude { get; set; }

    [DataMember(Name = "lng")]
    public double Longitude { get; set; }

    [DataMember(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
    public string OriginalRouteId { get; set; }

    [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
    public string OptimizationProblemId { get; set; }

    [DataMember(Name = "sequence_no", EmitDefaultValue = false)]
    public int? SequenceNo { get; set; }

    [DataMember(Name = "geocoded", EmitDefaultValue = false)]
    public bool? Geocoded { get; set; }

    [DataMember(Name = "preferred_geocoding", EmitDefaultValue = false)]
    public int? PreferredGeocoding { get; set; }

    [DataMember(Name = "FailedGeocoding", EmitDefaultValue = false)]
    public bool? FailedGeocoding { get; set; }

    // Ignored
    //  public List<object> geocodings { get; set; }

    [DataMember(Name = "contact_id", EmitDefaultValue = false)]
    public int? ContactId { get; set; }

    [DataMember(Name = "is_visited", EmitDefaultValue = false)]
    public bool? IsVisited { get; set; }

    [DataMember(Name = "is_departed", EmitDefaultValue = false)]
    public bool? IsDeparted { get; set; }

    [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
    public uint? TimestampLastVisited { get; set; }

    [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
    public uint? TimestampLastDeparted { get; set; }

    [DataMember(Name = "customer_po", EmitDefaultValue = false)]
    public object CustomerPo { get; set; }

    [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
    public object InvoiceNo { get; set; }

    [DataMember(Name = "reference_no", EmitDefaultValue = false)]
    public object ReferenceNo { get; set; }

    [DataMember(Name = "order_no", EmitDefaultValue = false)]
    public object OrderNo { get; set; }

    [DataMember(Name = "weight", EmitDefaultValue = false)]
    public object Weight { get; set; }

    [DataMember(Name = "cost", EmitDefaultValue = false)]
    public object Cost { get; set; }

    [DataMember(Name = "revenue", EmitDefaultValue = false)]
    public object Revenue { get; set; }

    [DataMember(Name = "cube", EmitDefaultValue = false)]
    public object Cube { get; set; }

    [DataMember(Name = "pieces", EmitDefaultValue = false)]
    public object Pieces { get; set; }

    [DataMember(Name = "email", EmitDefaultValue = false)]
    public object Email { get; set; }

    [DataMember(Name = "phone", EmitDefaultValue = false)]
    public object Phone { get; set; }

    [DataMember(Name = "destination_note_count", EmitDefaultValue = false)]
    public int? DestinationNoteCount { get; set; }

    [DataMember(Name = "drive_time_to_next_destination", EmitDefaultValue = false)]
    public int? DriveTimeToNextDestination { get; set; }

    [DataMember(Name = "distance_to_next_destination", EmitDefaultValue = false)]
    public double? DistanceToNextDestination { get; set; }

    [DataMember(Name = "generated_time_window_start", EmitDefaultValue = false)]
    public int? GeneratedTimeEindowStart { get; set; }

    [DataMember(Name = "generated_time_window_end", EmitDefaultValue = false)]
    public int? GeneratedTimeWindowEnd { get; set; }

    [DataMember(Name = "channel_name", EmitDefaultValue = false)]
    public string channel_name { get; set; }

    [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
    public int? TimeWindowStart { get; set; }

    [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
    public int? TimeWindowEnd { get; set; }

    [DataMember(Name = "time", EmitDefaultValue = false)]
    public int? Time { get; set; }

    [DataMember(Name = "notes", EmitDefaultValue = false)]
    public string[] Notes { get; set; }

    [DataMember(Name = "priority", EmitDefaultValue = false)]
    public int? Priority { get; set; }

    [DataMember(Name = "curbside_lat")]
    public double? CurbsideLatitude { get; set; }

    [DataMember(Name = "curbside_lng")]
    public double? CurbsideLongitude { get; set; }

    [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
    public int? TimeWindowStart2 { get; set; }

    [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
    public int? TimeWindowEnd2 { get; set; }
  }
}
