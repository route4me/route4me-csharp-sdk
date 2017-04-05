using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class MemberConfigurationResponse
    {
        [DataMember(Name = "result")]
        public string result
        {
            get { return m_result; }
            set { m_result = value; }
        }
        private string m_result;

        [DataMember(Name = "affected")]
        public int affected
        {
            get { return m_affected; }
            set { m_affected = value; }
        }
        private int m_affected;

    }
}
