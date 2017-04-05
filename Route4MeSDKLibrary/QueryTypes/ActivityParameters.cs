
namespace Route4MeSDK.QueryTypes
{
  public sealed class ActivityParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
    public string DeviceID { get; set; }

    [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
    public int? MemberId { get; set; }

    [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
    public uint? Limit { get; set; }

    [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
    public uint? Offset { get; set; }

    [HttpQueryMemberAttribute(Name = "team", EmitDefaultValue = false)]
    public string Team { get; set; }

    [HttpQueryMemberAttribute(Name = "start", EmitDefaultValue = false)]
    public uint? Start { get; set; }

    [HttpQueryMemberAttribute(Name = "end", EmitDefaultValue = false)]
    public uint? End { get; set; }

    [HttpQueryMemberAttribute(Name = "activity_type", EmitDefaultValue = false)]
    public string ActivityType { get; set; }

    [HttpQueryMemberAttribute(Name = "activity_message", EmitDefaultValue = false)]
    public string ActivityMessage { get; set; }
  }
}
