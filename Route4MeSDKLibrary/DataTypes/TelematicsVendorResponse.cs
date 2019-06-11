using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    [DataContract]
    public sealed class TelematicsVendorResponse
    {
        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public TelematicsVendor Vendor { get; set; }
    }
}
