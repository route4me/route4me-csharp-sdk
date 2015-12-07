
namespace Route4MeSDK.QueryTypes
{

  public sealed class NoteParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
    public int AddressId { get; set; }

    [HttpQueryMemberAttribute(Name = "dev_lat")]
    public double Latitude { get; set; }

    [HttpQueryMemberAttribute(Name = "dev_lng")]
    public double Longitude { get; set; }

    [HttpQueryMemberAttribute(Name = "device_type")]
    public string DeviceType { get; set; }

    [HttpQueryMemberAttribute(Name = "strUpdateType")]
    public string ActivityType { get; set; }

    //[HttpQueryMemberAttribute(Name = "strNoteContents", EmitDefaultValue = false)]
    //public string StrNoteContents { get; set; }
  }
}
