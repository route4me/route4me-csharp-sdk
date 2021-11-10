using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Link to a generated route
    /// </summary>
    [DataContract]
    public sealed class Links
    {
        /// <summary>
        ///     API URL Route GET call for the current route
        /// </summary>
        [DataMember(Name = "route")]
        public string Route { get; set; }

        /// <summary>
        ///     A Link to the GET operation for the optimization problem
        /// </summary>
        [DataMember(Name = "view")]
        public string View { get; set; }

        /// <summary>
        ///     The optimization problem ID
        /// </summary>
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }
    }
}