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
}
