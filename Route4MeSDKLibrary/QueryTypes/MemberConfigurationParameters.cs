using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberConfigurationParameters : GenericParameters
    {
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string config_key
        {
            get { return m_config_key; }
            set { m_config_key = value; }
        }
        private string m_config_key;

        [DataMember(Name = "config_value", EmitDefaultValue = false)]
        public string config_value
        {
            get { return m_config_value; }
            set { m_config_value = value; }
        }
        private string m_config_value;
    }
}