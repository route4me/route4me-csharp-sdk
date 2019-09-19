using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the orders request
    /// </summary>
    [DataContract]
    public sealed class OrdersResponse : GenericParameters
    {
        /// <summary>
        /// An array of the Order type objects
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public Order[] results { get; set; }

        /// <summary>
        /// Total number of the returned orders
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? total { get; set; }
    }
}
