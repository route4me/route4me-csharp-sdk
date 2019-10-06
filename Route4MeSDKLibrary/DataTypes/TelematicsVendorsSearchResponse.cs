using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the telematics vendors search request.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendorsSearchResponse
    {
        /// <summary>
        /// An array of the telematics vendor. See <see cref="TelematicsVendor"/>
        /// </summary>
        [DataMember(Name = "vendors", EmitDefaultValue = false)]
        public TelematicsVendor[] Vendors { get; set; }
    }
}
