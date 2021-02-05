using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Base class for the DataObject and DataObjectRoute classes
    /// </summary>
    [DataContract]
    [KnownType(typeof(DataObject))]
    [KnownType(typeof(DataObjectRoute))]
    public class DataObjectBase
    {
        /// <summary>
        /// Optimization problem ID
        /// </summary>
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        /// Smart Optimization ID
        /// </summary>
        [DataMember(Name = "smart_optimization_id")]
        public string SmartOptimizationId { get; set; }

        /// <summary>
        /// When the optimization problem was created
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long? CreatedTimestamp { get; set; }

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
        /// The links to the GET operations for the optimization problem. See <see cref="Links"/>
        /// </summary>
        [DataMember(Name = "links")]
        public Links Links { get; set; }
    }
}
