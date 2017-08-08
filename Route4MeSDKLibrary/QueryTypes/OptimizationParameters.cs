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

    /// <summary>
    /// If true will be redirected
    /// </summary>
    [IgnoreDataMember()]
    [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
    public System.Nullable<bool> Redirect
    {
        get { return m_Redirect; }
        set { m_Redirect = value; }
    }
    private System.Nullable<bool> m_Redirect;

    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "show_directions", EmitDefaultValue = false)]
    public bool? ShowDirections { get; set; }

    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "optimized_callback_url", EmitDefaultValue = false)]
    public string OptimizedCallbackURL { get; set; }

    [DataMember(Name = "parameters", EmitDefaultValue = false)]
    public RouteParameters Parameters { get; set; }
    
    [DataMember(Name = "addresses", EmitDefaultValue = false)]
    public Address[] Addresses { get; set; }
  }
}
