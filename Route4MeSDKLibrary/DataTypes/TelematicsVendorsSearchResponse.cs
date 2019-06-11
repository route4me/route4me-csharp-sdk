using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    [DataContract]
    public sealed class TelematicsVendorsSearchResponse
    {
        [DataMember(Name = "vendors", EmitDefaultValue = false)]
        public TelematicsVendor[] Vendors { get; set; }
    }
}
