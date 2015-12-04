
namespace Route4MeSDK.QueryTypes
{
  public sealed class AddressParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
    public int RouteDestinationId { get; set; }
  }
}
