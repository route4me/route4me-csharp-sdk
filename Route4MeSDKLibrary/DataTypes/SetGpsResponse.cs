using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response for the GPS setting request.
    /// </summary>
    [DataContract]
    public sealed class SetGpsResponse
    {
        /// <summary>
        /// Status of the GPS setting request process.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the GPS setting request finished successfully; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <summary>
        /// Unique ID of the GPS points group.
        /// <remarks><para>
        /// On the first request you do not need to provide a tx_id. 
        /// On the second request you provide the tx_id which you got back 
        /// from the first request. The tx_id is the unique group of points 
        /// related to this specific route transaction.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "tx_id", EmitDefaultValue = false)]
        public string TX_ID { get; set; }
    }
}
