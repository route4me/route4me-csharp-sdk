using Route4MeSDK.DataTypes;
using System;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the optimization problem(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class OptimizationParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of an optimization problem.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemID { get; set; }

        /// <summary>
        /// If true, the optimization will be re-optimized.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "reoptimize", EmitDefaultValue = false)]
        public bool? ReOptimize { get; set; }

        /// <summary>
        /// If true, a HTTP request will be redirected.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public Nullable<bool> Redirect { get; set; }

        /// <summary>
        /// If true, the route directions will be visible.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "show_directions", EmitDefaultValue = false)]
        public bool? ShowDirections { get; set; }

        /// <summary>
        /// A URL that gets called when the optimization is solved.
        /// <remarks><para>
        /// The callback is called with a POST request. 
        /// The POST data sent is: timestamp (Seconds): 
        /// Server timestamp of request sent optimization_problem_id (Hash String): 
        /// ID of the optimization state (Small Int). The state can be one of the values: 
        /// 4 = OPTIMIZATION_STATE_OPTIMIZED, which means the optimization was successful; or 
        /// 5 = OPTIMIZATION_STATE_ERROR, which means there was an error solving the optimization. Query string (GET fields).
        /// </para></remarks>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "optimized_callback_url", EmitDefaultValue = false)]
        public string OptimizedCallbackURL { get; set; }

        /// <summary>
        /// Limit the number of records in response.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        /// <summary>
        /// The optimization state.
        /// <para>Available values: </para>
        /// <value>Initial = 1, MatrixProcessing = 2, Optimizing = 3, Optimized = 4, Error = 5, ComputingDirections = 6</value>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "state", EmitDefaultValue = false)]
        public uint? State { get; set; }

        /// <summary>
        /// Route Parameters. See <see cref="RouteParameters"/>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters { get; set; }

        /// <summary>
        /// Valid array of the Address type objects. See <see cref="Address"/>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses { get; set; }
    }
}
