using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  
  [DataContract]
  public sealed class Address
  {
    [DataMember(Name = "RouteDestinationId", EmitDefaultValue = false)]
    public int? RouteDestinationId { get; set; }

    [DataMember(Name = "alias", EmitDefaultValue = false)]
    public string Alias { get; set; }
  
    //the id of the member inside the route4me system
    [DataMember(Name = "MemberId", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    [DataMember(Name = "address")]
    public string AddressString { get; set; }

    //designate this stop as a depot
    //a route may have multiple depots/points of origin
    [DataMember(Name = "is_depot", EmitDefaultValue = false)]
    public bool? IsDepot { get; set; }
  
    //the latitude of this address
    [DataMember(Name = "lat")]
    public double Latitude { get; set; }
  
    //the longitude of this address
    [DataMember(Name = "lng")]
    public double Longitude { get; set; }

    //the id of the route being viewed, modified, erased
    [DataMember(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    //if this route was duplicated from an existing route, this value would have the original route's id
    [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
    public string OriginalRouteId { get; set; }

    //the id of the optimization request that was used to initially instantiate this route
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

    //when planning a route from the address book or using existing address book ids
    //pass the address book id (contact_id) for an address so that route4me can run
    //analytics on the address book addresses that were used to plan routes, and to find previous visits to 
    //favorite addresses
    [DataMember(Name = "contact_id", EmitDefaultValue = false)]
    public int? ContactId { get; set; }


    //status flag to mark an address as visited (aka check in)
    [DataMember(Name = "is_visited", EmitDefaultValue = false)]
    public bool? IsVisited { get; set; }

    //status flag to mark an address as departed (aka check out)
    [DataMember(Name = "is_departed", EmitDefaultValue = false)]
    public bool? IsDeparted { get; set; }

    //the last known visited timestamp of this address
    [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
    public uint? TimestampLastVisited { get; set; }
    
    //the last known departed timestamp of this address
    [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
    public uint? TimestampLastDeparted { get; set; }

    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
    [DataMember(Name = "customer_po", EmitDefaultValue = false)]
    public object CustomerPo { get; set; }
    
    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
    [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
    public object InvoiceNo { get; set; }

    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
    [DataMember(Name = "reference_no", EmitDefaultValue = false)]
    public object ReferenceNo { get; set; }

    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
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

    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
    //also used to email clients when vehicles are approaching (future capability)
    [DataMember(Name = "email", EmitDefaultValue = false)]
    public object Email { get; set; }

    //pass-through data about this route destination
    //the data will be visible on the manifest, website, and mobile apps
    //also used to sms message clients when vehicles are approaching (future capability)
    [DataMember(Name = "phone", EmitDefaultValue = false)]
    public object Phone { get; set; }

    //the number of notes that are already associated with this address on the route
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

    [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
    public Dictionary<string, string> CustomFields { get; set; }
  }
}
