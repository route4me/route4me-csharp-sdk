using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Main data object data-structure
    /// See <see cref="https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4"/>  
    /// </summary>
    [DataContract]
    [KnownType(typeof(DataObjectRoute))]
    public class DataObject
    {
        /// <summary>
        /// Optimization problem ID
        /// </summary>
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

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
        /// If true it means the solution was not returned (it is being computed in the background)
        /// </summary>
        [DataMember(Name = "sent_to_background")]
        public bool IsSentToBackground { get; set; }

        /// <summary>
        /// When the optimization problem was created
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long? CreatedTimestamp { get; set; }

        /// <summary>
        /// An Unix Timestamp the Optimization Problem was scheduled for
        /// </summary>
        [DataMember(Name = "scheduled_for", EmitDefaultValue = false)]
        public long? ScheduledFor { get; set; }

        /// <summary>
        /// Route Parameters. See <see cref="RouteParameters"/>
        /// </summary>
        [DataMember(Name = "parameters")]
        public RouteParameters Parameters { get; set; }

        /// <summary>
        /// An array ot the Address type objects. See  <see cref="Address"/>
        /// </summary>
        [DataMember(Name = "addresses")]
        public Address[] Addresses { get; set; }

        /// <summary>
        /// An array ot the DataObjectRoute type objects. See <see cref="DataObjectRoute"/>
        /// <para>The routes included in the optimization problem</para>
        /// </summary>
        [DataMember(Name = "routes")]
        public DataObjectRoute[] Routes { get; set; }

        /// <summary>
        /// The links to the GET operations for the optimization problem. See <see cref="Links"/>
        /// </summary>
        [DataMember(Name = "links")]
        public Links Links { get; set; }

        /// <summary>
        /// A collection of device tracking data with coordinates, speed, and timestamps.
        /// <para>See <see cref="TrackingHistory"/></para>
        /// </summary>
        [DataMember(Name = "tracking_history")]
        public TrackingHistory[] TrackingHistory { get; set; }

        /// <summary>
        /// Edge by edge turn-by-turn directions. See <see cref="Direction"/>
        /// </summary>
        [DataMember(Name = "directions")]
        public Direction[] Directions { get; set; }

        /// <summary>
        /// Edge-wise path to be drawn on the map See <see cref="DirectionPathPoint"/>
        /// </summary>
        [DataMember(Name = "path")]
        public DirectionPathPoint[] Path { get; set; }

        /// <summary>
        /// Total number of the addresses included in the optimization
        /// </summary>
        [DataMember(Name = "total_addresses", EmitDefaultValue = false)]
        public int? TotalAddresses { get; set; }
    }
}
