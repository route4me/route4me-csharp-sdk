using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{


  [DataContract]
  public sealed class OptimizationParameters : GenericParameters
  {
    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
    public string OptimizationProblemID { get; set; }

    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "reoptimize", EmitDefaultValue = false)]
    public bool? ReOptimize { get; set; }

    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "show_directions", EmitDefaultValue = false)]
    public bool? ShowDirections { get; set; }

    [DataMember(Name = "parameters", EmitDefaultValue = false)]
    public RouteParameters Parameters { get; set; }
    
    [DataMember(Name = "addresses", EmitDefaultValue = false)]
    public Address[] Addresses { get; set; }
  }
}
