using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Helper class, for setting Optimization data
  /// Used to create the suitable query string
  /// See example in Route4MeExamples.ReOptimization() and Route4MeExamples.MultipleDepotMultipleDriver()
  /// Important: this class is used both to generate the query string and as JSON object
  /// </summary>
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
