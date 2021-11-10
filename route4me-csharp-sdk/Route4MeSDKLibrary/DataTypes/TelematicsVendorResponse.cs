using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the telematics vendor request.
    /// </summary>
    [DataContract]
    public sealed class TelematicsVendorResponse
    {
        /// <summary>
        ///     Telematics vendor.
        /// </summary>
        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public TelematicsVendor Vendor { get; set; }
    }
}