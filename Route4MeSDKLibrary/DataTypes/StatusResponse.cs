using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class StatusResponse : GenericParameters
    {
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool status { get; set; }
    }
}
