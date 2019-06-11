using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    [DataContract]
    public sealed class TelematicsVendorsResponse
    {
        [DataMember(Name = "vendors", EmitDefaultValue = false)]
        public TelematicsVendors[] Vendors { get; set; }
    }
}
