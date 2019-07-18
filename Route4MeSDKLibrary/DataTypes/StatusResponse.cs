using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the requests returning only boolean parameter 'status'
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class StatusResponse : GenericParameters
    {
        /// <summary>
        /// Status of the request process.
        /// </summary>
        /// <value>
        ///   <c>true</c> if request finished successfully; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool status { get; set; }
    }
}
