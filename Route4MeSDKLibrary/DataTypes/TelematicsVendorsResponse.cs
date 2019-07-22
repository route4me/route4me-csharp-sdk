using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the telematics vendors request.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendorsResponse
    {
        /// <summary>
        /// An array of the telematics vendors. See <see cref="TelematicsVendors"/>
        /// </summary>
        [DataMember(Name = "vendors", EmitDefaultValue = false)]
        public TelematicsVendors[] Vendors { get; set; }
    }
}
