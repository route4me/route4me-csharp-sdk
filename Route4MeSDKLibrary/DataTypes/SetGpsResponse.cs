using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{

    [DataContract]
    public sealed class SetGpsResponse
    {
        [DataMember(Name = "status")]
        public bool Status { get; set; }

        [DataMember(Name = "tx_id", EmitDefaultValue = false)]
        public string TX_ID { get; set; }
    }
}