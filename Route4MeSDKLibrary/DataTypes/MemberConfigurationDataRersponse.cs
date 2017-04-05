using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class MemberConfigurationDataRersponse
    {
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string result
        {
            get { return m_result; }
            set { m_result = value; }
        }
        private string m_result;

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public MemberConfigurationData[] data
        {
            get { return m_data; }
            set { m_data = value; }
        }
        private MemberConfigurationData[] m_data;
    }
}