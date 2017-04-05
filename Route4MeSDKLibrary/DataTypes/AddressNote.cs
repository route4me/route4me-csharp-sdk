using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class AddressNote
  {
    [DataMember(Name = "note_id", EmitDefaultValue = false)]
    public int NoteId { get; set; }

    [DataMember(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
    public int RouteDestinationId { get; set; }

    [DataMember(Name = "upload_id")]
    public string UploadId { get; set; }

    [DataMember(Name = "ts_added", EmitDefaultValue = false)]
    public uint? TimestampAdded { get; set; }

    [DataMember(Name = "lat")]
    public double Latitude { get; set; }

    [DataMember(Name = "lng")]
    public double Longitude { get; set; }

    [DataMember(Name = "activity_type")]
    public string ActivityType { get; set; }

    [DataMember(Name = "contents")]
    public string Contents { get; set; }

    [DataMember(Name = "upload_type")]
    public string UploadType { get; set; }

    [DataMember(Name = "upload_url")]
    public string UploadUrl { get; set; }

    [DataMember(Name = "upload_extension")]
    public string UploadExtension { get; set; }

    [DataMember(Name = "device_type")]
    public string DeviceType { get; set; }
  }
}
