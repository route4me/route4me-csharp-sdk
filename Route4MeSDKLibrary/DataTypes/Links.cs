using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{

  [DataContract]
  public sealed class Links
  {
    [DataMember(Name = "route")]
    public string Route { get; set; }

    [DataMember(Name = "view")]
    public string View { get; set; }

    [DataMember(Name = "optimization_problem_id")]
    public string OptimizationProblemId { get; set; }
  }
}
