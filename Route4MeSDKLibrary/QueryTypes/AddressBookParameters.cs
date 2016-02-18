
namespace Route4MeSDK.QueryTypes
{
  public sealed class AddressBookParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
    public string AddressId { get; set; }

    [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
    public uint? Limit { get; set; }

    [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
    public uint? Offset { get; set; }

    [HttpQueryMemberAttribute(Name = "start", EmitDefaultValue = false)]
    public uint? Start { get; set; }

    [HttpQueryMemberAttribute(Name = "querry", EmitDefaultValue = false)]
    public string Querry { get; set; }

    [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
    public string Fields { get; set; }

    [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
    public string Display { get; set; }
  }
}
