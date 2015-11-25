
namespace Route4MeSDK.QueryTypes
{

  public sealed class NoteParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
    public int AddressId { get; set; }

    [HttpQueryMemberAttribute(Name = "strNoteContents", EmitDefaultValue = false)]
    public string StrNoteContents { get; set; }
  }
}
