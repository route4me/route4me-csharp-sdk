
namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Helper class, for setting Route data
  /// Used to create the suitable query string
  /// See example in Route4MeExamples.GenericExampleShortcut()
  /// </summary>
  public sealed class RouteParametersQuery : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "directions", EmitDefaultValue = false)]
    public bool? Directions { get; set; }

    [HttpQueryMemberAttribute(Name = "route_path_output", EmitDefaultValue = false)]
    public string RoutePathOutput { get; set; }

    [HttpQueryMemberAttribute(Name = "device_tracking_history", EmitDefaultValue = false)]
    public bool? DeviceTrackingHistory { get; set; }

    [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
    public uint? Limit { get; set; }

    [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
    public uint? Offset { get; set; }

    [HttpQueryMemberAttribute(Name = "original", EmitDefaultValue = false)]
    public bool? Original { get; set; }

    [HttpQueryMemberAttribute(Name = "notes", EmitDefaultValue = false)]
    public bool? Notes { get; set; }
  }
}
