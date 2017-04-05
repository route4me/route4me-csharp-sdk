using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class MemberConfigurationData
    {
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int member_id
        {
            get { return m_member_id; }
            set { m_member_id = value; }
        }
        private int m_member_id;

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