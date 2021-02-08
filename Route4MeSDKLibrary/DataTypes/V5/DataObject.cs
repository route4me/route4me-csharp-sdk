using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Main data object data-structure
    /// See <see cref="https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4"/>  
    /// </summary>
    [DataContract]
    public class DataObject : DataObjectBase
    {
        /// <summary>
        /// An optimization problem state. See <see cref="OptimizationState"/>
        /// </summary>
        [DataMember(Name = "state")]
        public OptimizationState State { get; set; }

        /// <summary>
        /// An array of the user errors
        /// </summary>
        [DataMember(Name = "user_errors")]
        public string[] UserErrors { get; set; }

        /// <summary>
        /// An array of the optimization errors
        /// </summary>
        [DataMember(Name = "optimization_errors")]
        public string[] OptimizationErrors { get; set; }

        /// <summary>
        /// If true it means the solution was not returned (it is being computed in the background)
        /// </summary>
        [DataMember(Name = "sent_to_background")]
        public bool IsSentToBackground { get; set; }

        /// <summary>
        /// An Unix Timestamp the Optimization Problem was scheduled for
        /// </summary>
        [DataMember(Name = "scheduled_for", EmitDefaultValue = false)]
        public long? ScheduledFor { get; set; }

        /// <summary>
        /// An array ot the DataObjectRoute type objects. See <see cref="DataObjectRoute"/>
        /// <para>The routes included in the optimization problem</para>
        /// </summary>
        [DataMember(Name = "routes")]
        public DataObjectRoute[] Routes { get; set; }

        /// <summary>
        /// Total number of the addresses included in the optimization
        /// </summary>
        [DataMember(Name = "total_addresses", EmitDefaultValue = false)]
        public int? TotalAddresses { get; set; }
    }
}
