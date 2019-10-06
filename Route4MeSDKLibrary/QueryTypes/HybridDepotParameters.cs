using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the hybrid depot(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class HybridDepotParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of an optimization problem.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        /// If true, old depots will be removed from a hybrid optimization.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "delete_old_depots", EmitDefaultValue = false)]
        public bool DeleteOldDepots { get; set; }

        /// <summary>
        /// An array of the new depots. See <see cref="Address"/>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "new_depots", EmitDefaultValue = false)]
        public Address[] NewDepots { get; set; }
    }
}
