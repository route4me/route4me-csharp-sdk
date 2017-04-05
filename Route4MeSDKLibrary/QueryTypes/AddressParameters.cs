
namespace Route4MeSDK.QueryTypes
{
  public sealed class AddressParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
    public int RouteDestinationId { get; set; }

    [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
    public int AddressId { get; set; }

    [HttpQueryMemberAttribute(Name = "notes")]
    public bool Notes { get; set; }

    [HttpQueryMemberAttribute(Name = "is_departed")]
    public bool IsDeparted { get; set; }

    [HttpQueryMemberAttribute(Name = "is_visited")]
    public bool IsVisited { get; set; }

  }
}
