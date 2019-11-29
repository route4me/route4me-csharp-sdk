using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
        public string SessionGuid { get; set; }

        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string hFormat { get; set; }

        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        [HttpQueryMemberAttribute(Name = "plan", EmitDefaultValue = false)]
        public string Plan { get; set; }

        [HttpQueryMemberAttribute(Name = "member_type", EmitDefaultValue = false)]
        public int? MemberType { get; set; }

        [DataMember(Name = "strEmail", EmitDefaultValue = false)]
        public string StrEmail { get; set; }

        [DataMember(Name = "strPassword", EmitDefaultValue = false)]
        public string StrPassword { get; set; }

        [DataMember(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        [DataMember(Name = "strIndustry", EmitDefaultValue = false)]
        public string StrIndustry { get; set; }

        [DataMember(Name = "strFirstName", EmitDefaultValue = false)]
        public string StrFirstName { get; set; }

        [DataMember(Name = "strLastName", EmitDefaultValue = false)]
        public string StrLastName { get; set; }

        [DataMember(Name = "chkTerms", EmitDefaultValue = false)]
        public int? ChkTerms { get; set; }

        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        [DataMember(Name = "strPassword_1", EmitDefaultValue = false)]
        public string StrPassword_1 { get; set; }

        [DataMember(Name = "strPassword_2", EmitDefaultValue = false)]
        public string StrPassword_2 { get; set; }

        [DataMember(Name = "strSubAccountType", EmitDefaultValue = true)]
        public string strSubAccountType { get; set; }

        [DataMember(Name = "blDisableMarketing", EmitDefaultValue = true)]
        public bool blDisableMarketing { get; set; }

        [DataMember(Name = "blDisableAccountActivationEmail", EmitDefaultValue = true)]
        public bool blDisableAccountActivationEmail { get; set; }
    }
}
