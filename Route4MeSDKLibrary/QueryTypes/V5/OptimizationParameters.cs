using Route4MeSDK.DataTypes.V5;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{

    [DataContract]
    public sealed class OptimizationParameters : QueryTypes.GenericParameters
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
        public bool? Redirect { get; set; }

        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "show_directions", EmitDefaultValue = false)]
        public bool? ShowDirections { get; set; }

        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "optimized_callback_url", EmitDefaultValue = false)]
        public string OptimizedCallbackURL { get; set; }

        /// <summary>
        /// The number of existing routes that should be returned per response when looking at a list of all the routes.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// The page number for route listing pagination. Increment the offset by the limit number to move to the next page.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "end_date", EmitDefaultValue = false)]
        public string EndDate { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        /// The optimization state: 
        /// New = 0,
        /// Initial = 1, 
        /// MatrixProcessing = 2, 
        /// Optimizing = 3, 
        /// Optimized = 4, 
        /// Error = 5, 
        /// ComputingDirections = 6,
        /// InQueue = 7
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "state", EmitDefaultValue = false)]
        public uint? State { get; set; }

        /// <summary>
        /// Route Parameters to update.
        /// (After a PUT there is no guarantee that the route_destination_id values are preserved! It may create copies resulting in new destination IDs, especially when dealing with multiple depots.)
        /// </summary>
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters { get; set; }

        /// <summary>
        /// Array of the route addresses
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses { get; set; }

        /// <summary>
        /// Array of the depots
        /// </summary>
        [DataMember(Name = "depots", EmitDefaultValue = false)]
        public Address[] Depots { get; set; }
    }
}
