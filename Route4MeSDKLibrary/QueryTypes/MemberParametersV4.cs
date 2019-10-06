using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberParametersV4 : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public System.Nullable<int> member_id
        {
            get { return m_member_id; }
            set { m_member_id = value; }
        }
        private System.Nullable<int> m_member_id;

        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string device_id { get; set; }

        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_ROUTED_ADDRESSES { get; set; }

        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_VISITED_ADDRESSES { get; set; }

        [DataMember(Name = "READONLY_USER", EmitDefaultValue = false)]
        public string READONLY_USER { get; set; }

        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public string HIDE_NONFUTURE_ROUTES { get; set; }

        [DataMember(Name = "SHOW_ALL_VEHICLES", EmitDefaultValue = false)]
        public string SHOW_ALL_VEHICLES { get; set; }

        [DataMember(Name = "SHOW_ALL_DRIVERS", EmitDefaultValue = false)]
        public string SHOW_ALL_DRIVERS { get; set; }

        [DataMember(Name = "member_phone", EmitDefaultValue = false)]
        public string member_phone
        {
            get { return m_member_phone; }
            set { m_member_phone = value; }
        }
        private string m_member_phone;

        [DataMember(Name = "member_zipcode", EmitDefaultValue = false)]
        public string member_zipcode
        {
            get { return m_member_zipcode; }
            set { m_member_zipcode = value; }
        }
        private string m_member_zipcode;

        [DataMember(Name = "user_reg_country_id", EmitDefaultValue = false)]
        public int? user_reg_country_id { get; set; }

        [DataMember(Name = "user_reg_state_id", EmitDefaultValue = false)]
        public int? user_reg_state_id { get; set; }

        [DataMember(Name = "preferred_language", EmitDefaultValue = false)]
        public string preferred_language { get; set; }

        [DataMember(Name = "preferred_units", EmitDefaultValue = false)]
        public string preferred_units { get; set; }

        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string timezone { get; set; }

        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> custom_data
        {
            get
            {
                if (_custom_data == null)
                {
                    return null;
                }
                else
                {
                    var v1 = (Dictionary<string, string>)_custom_data;

                    Dictionary<string, string> v2 = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> kv1 in v1)
                    {
                        if (kv1.Key != null)
                        {
                            if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value.ToString()); else v2.Add(kv1.Key, "");
                        }
                        else continue;
                    }

                    return v2;
                }
            }
            set
            {
                if (value == null)
                {
                    _custom_data = null;
                }
                else
                {
                    var v1 = (Dictionary<string, string>)value;
                    Dictionary<string, string> v2 = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> kv1 in v1)
                    {
                        if (kv1.Key != null)
                        {
                            if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value.ToString()); else v2.Add(kv1.Key, "");
                        }
                        else continue;
                    }
                    _custom_data = v2;
                }
            }
        }
        private Dictionary<string, string> _custom_data;

        [HttpQueryMemberAttribute(Name = "route_count", EmitDefaultValue = false)]
        public System.Nullable<int> route_count
        {
            get { return m_route_count; }
            set { m_route_count = value; }
        }
        private System.Nullable<int> m_route_count;

        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string member_email
        {
            get { return m_member_email; }
            set { m_member_email = value; }
        }
        private string m_member_email;

        [DataMember(Name = "member_type", EmitDefaultValue = false)]
        public string member_type
        {
            get { return m_member_type; }
            set { m_member_type = value; }
        }
        private string m_member_type;

        [DataMember(Name = "date_of_birth", EmitDefaultValue = false)]
        public string date_of_birth
        {
            get { return m_date_of_birth; }
            set { m_date_of_birth = value; }
        }
        private string m_date_of_birth;

        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string member_first_name
        {
            get { return m_member_first_name; }
            set { m_member_first_name = value; }
        }
        private string m_member_first_name;

        [DataMember(Name = "member_password", EmitDefaultValue = false)]
        public string member_password
        {
            get { return m_member_password; }
            set { m_member_password = value; }
        }
        private string m_member_password;

        [DataMember(Name = "OWNER_MEMBER_ID", EmitDefaultValue = false)]
        public int? OWNER_MEMBER_ID { get; set; }

        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string member_last_name
        {
            get { return m_member_last_name; }
            set { m_member_last_name = value; }
        }
        private string m_member_last_name;

    }
}
