using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Main data object data-structure
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
  /// </summary>
  [DataContract]
  [KnownType(typeof(DataObjectRoute))]
  public class DataObject
  {
    [DataMember(Name = "optimization_problem_id")]
    public string OptimizationProblemId { get; set; }

    [DataMember(Name = "state")]
    public OptimizationState State { get; set; }

    [DataMember(Name = "user_errors")]
    public string[] UserErrors { get; set; }

    [DataMember(Name = "sent_to_background")]
    public bool IsSentToBackground { get; set; }

    [DataMember(Name = "parameters")]
    public RouteParameters Parameters { get; set; }

    [DataMember(Name = "addresses")]
    public Address[] Addresses { get; set; }

    [DataMember(Name = "routes")]
    public Address[] Routes { get; set; }

    [DataMember(Name = "links")]
    public Links Links { get; set; }

    [DataMember(Name = "tracking_history")]
    public TrackingHistory[] TrackingHistory { get; set; }
  }

  //public sealed  class Parameters2
  //{
  //  public bool is_upload { get; set; }
  //  public bool rt { get; set; }
  //  public string route_name { get; set; }
  //  public int route_date { get; set; }
  //  public object route_time { get; set; }
  //  public bool shared_publicly { get; set; }
  //  public bool disable_optimization { get; set; }
  //  public string optimize { get; set; }
  //  public bool lock_last { get; set; }
  //  public int vehicle_capacity { get; set; }
  //  public int vehicle_max_distance_mi { get; set; }
  //  public string distance_unit { get; set; }
  //  public string travel_mode { get; set; }
  //  public string avoid { get; set; }
  //  public object vehicle_id { get; set; }
  //  public object driver_id { get; set; }
  //  public object dev_lat { get; set; }
  //  public object dev_lng { get; set; }
  //  public int route_max_duration { get; set; }
  //  public object route_email { get; set; }
  //  public string route_type { get; set; }
  //  public bool store_route { get; set; }
  //  public int metric { get; set; }
  //  public string algorithm_type { get; set; }
  //  public string member_id { get; set; }
  //  public long ip { get; set; }
  //  public int dm { get; set; }
  //  public int dirm { get; set; }
  //  public int parts { get; set; }
  //  public object device_id { get; set; }
  //  public object device_type { get; set; }
  //  public bool has_trailer { get; set; }
  //  public object trailer_weight_t { get; set; }
  //  public object limited_weight_t { get; set; }
  //  public object weight_per_axle_t { get; set; }
  //  public object truck_height_meters { get; set; }
  //  public object truck_width_meters { get; set; }
  //  public object truck_length_meters { get; set; }
  //  public object max_tour_size { get; set; }
  //  public object min_tour_size { get; set; }
  //}

  //public sealed class CustomFields2
  //{
  //}

  //public sealed class Manifest
  //{
  //  public int running_service_time { get; set; }
  //  public int running_travel_time { get; set; }
  //  public double running_distance { get; set; }
  //  public double fuel_from_start { get; set; }
  //  public int fuel_cost_from_start { get; set; }
  //  public int projected_arrival_time_ts { get; set; }
  //  public int projected_departure_time_ts { get; set; }
  //  public object actual_arrival_time_ts { get; set; }
  //  public object actual_departure_time_ts { get; set; }
  //  public int estimated_arrival_time_ts { get; set; }
  //  public int estimated_departure_time_ts { get; set; }
  //  public object time_impact { get; set; }
  //}

  //public sealed class Address2
  //{
  //  public int route_destination_id { get; set; }
  //  public string alias { get; set; }
  //  public string member_id { get; set; }
  //  public string address { get; set; }
  //  public bool is_depot { get; set; }
  //  public double lat { get; set; }
  //  public double lng { get; set; }
  //  public string route_id { get; set; }
  //  public object original_route_id { get; set; }
  //  public string optimization_problem_id { get; set; }
  //  public int sequence_no { get; set; }
  //  public bool geocoded { get; set; }
  //  public object preferred_geocoding { get; set; }
  //  public bool failed_geocoding { get; set; }
  //  public List<object> geocodings { get; set; }
  //  public int contact_id { get; set; }
  //  public bool is_visited { get; set; }
  //  public object timestamp_last_visited { get; set; }
  //  public bool is_departed { get; set; }
  //  public object timestamp_last_departed { get; set; }
  //  public object customer_po { get; set; }
  //  public object invoice_no { get; set; }
  //  public object reference_no { get; set; }
  //  public object order_no { get; set; }
  //  public object weight { get; set; }
  //  public object cost { get; set; }
  //  public object revenue { get; set; }
  //  public object cube { get; set; }
  //  public object pieces { get; set; }
  //  public object email { get; set; }
  //  public object phone { get; set; }
  //  public int destination_note_count { get; set; }
  //  public int drive_time_to_next_destination { get; set; }
  //  public double distance_to_next_destination { get; set; }
  //  public string channel_name { get; set; }
  //  public int? generated_time_window_start { get; set; }
  //  public int? generated_time_window_end { get; set; }
  //  public int time_window_start { get; set; }
  //  public int time_window_end { get; set; }
  //  public int time { get; set; }
  //  public CustomFields2 custom_fields { get; set; }
  //  public List<object> notes { get; set; }
  //  public Manifest manifest { get; set; }
  //}

  

  //public sealed class Route
  //{
  //  public string route_id { get; set; }
  //  public string optimization_problem_id { get; set; }
  //  public string member_id { get; set; }
  //  public string member_email { get; set; }
  //  public string channel_name { get; set; }
  //  public string vehicle_alias { get; set; }
  //  public string driver_alias { get; set; }
  //  public double trip_distance { get; set; }
  //  public int mpg { get; set; }
  //  public object gas_price { get; set; }
  //  public int route_duration_sec { get; set; }
  //  public Parameters2 parameters { get; set; }
  //  public List<Address2> addresses { get; set; }
  //  public Links links { get; set; }
  //  public List<object> notes { get; set; }
  //}

  //public sealed  class RootObject
  //{
  //  public string optimization_problem_id { get; set; }
  //  public List<object> user_errors { get; set; }
  //  public int state { get; set; }
  //  public RouteParameters parameters { get; set; }
  //  public bool sent_to_background { get; set; }
  //  public List<Address> addresses { get; set; }
  //  public List<Route> routes { get; set; }
  //  public Links links { get; set; }
  //}
}
