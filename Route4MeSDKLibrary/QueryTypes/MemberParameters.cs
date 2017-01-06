using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
        public string SessionGuid
        {
            get { return m_SessionGuid; }
            set { m_SessionGuid = value; }
        }
        private string m_SessionGuid;

        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string hFormat
        {
            get { return m_hFormat; }
            set { m_hFormat = value; }
        }
        private string m_hFormat;

        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public System.Nullable<int> MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private System.Nullable<int> m_MemberId;

        [HttpQueryMemberAttribute(Name = "plan", EmitDefaultValue = false)]
        public string Plan
        {
            get { return m_Plan; }
            set { m_Plan = value; }
        }
        private string m_Plan;

        [DataMember(Name = "strEmail", EmitDefaultValue = false)]
        public string StrEmail
        {
            get { return m_StrEmail; }
            set { m_StrEmail = value; }
        }
        private string m_StrEmail;

        [DataMember(Name = "strPassword", EmitDefaultValue = false)]
        public string StrPassword
        {
            get { return m_StrPassword; }
            set { m_StrPassword = value; }
        }
        private string m_StrPassword;

        [DataMember(Name = "format", EmitDefaultValue = false)]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }
        private string m_Format;

        [DataMember(Name = "strIndustry", EmitDefaultValue = false)]
        public string StrIndustry
        {
            get { return m_StrIndustry; }
            set { m_StrIndustry = value; }
        }
        private string m_StrIndustry;

        [DataMember(Name = "strFirstName", EmitDefaultValue = false)]
        public string StrFirstName
        {
            get { return m_StrFirstName; }
            set { m_StrFirstName = value; }
        }
        private string m_StrFirstName;

        [DataMember(Name = "strLastName", EmitDefaultValue = false)]
        public string StrLastName
        {
            get { return m_StrLastName; }
            set { m_StrLastName = value; }
        }
        private string m_StrLastName;

        [DataMember(Name = "chkTerms", EmitDefaultValue = false)]
        public System.Nullable<int> ChkTerms
        {
            get { return m_ChkTerms; }
            set { m_ChkTerms = value; }
        }
        private System.Nullable<int> m_ChkTerms;

        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
        private string m_DeviceType;

        [DataMember(Name = "strPassword_1", EmitDefaultValue = false)]
        public string StrPassword_1
        {
            get { return m_StrPassword1; }
            set { m_StrPassword1 = value; }
        }
        private string m_StrPassword1;

        [DataMember(Name = "strPassword_2", EmitDefaultValue = false)]
        public string StrPassword_2
        {
            get { return m_StrPassword2; }
            set { m_StrPassword2 = value; }
        }
        private string m_StrPassword2;
    }
}