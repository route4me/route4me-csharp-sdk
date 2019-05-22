using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class MemberResponseV4
    {
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public string HIDE_NONFUTURE_ROUTES
        {
            get { return m_HIDE_NONFUTURE_ROUTES; }
            set { m_HIDE_NONFUTURE_ROUTES = value; }
        }
        private string m_HIDE_NONFUTURE_ROUTES;

        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_ROUTED_ADDRESSES
        {
            get { return m_HIDE_ROUTED_ADDRESSES; }
            set { m_HIDE_ROUTED_ADDRESSES = value; }
        }
        private string m_HIDE_ROUTED_ADDRESSES;

        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_VISITED_ADDRESSES
        {
            get { return m_HIDE_VISITED_ADDRESSES; }
            set { m_HIDE_VISITED_ADDRESSES = value; }
        }
        private string m_HIDE_VISITED_ADDRESSES;

        [DataMember(Name = "member_id")]
        public string member_id
        {
            get { return m_member_id; }
            set { m_member_id = value; }
        }
        private string m_member_id;

        [DataMember(Name = "OWNER_MEMBER_ID")]
        public string OWNER_MEMBER_ID
        {
            get { return m_OWNER_MEMBER_ID; }
            set { m_OWNER_MEMBER_ID = value; }
        }
        private string m_OWNER_MEMBER_ID;

        [DataMember(Name = "READONLY_USER")]
        public string READONLY_USER
        {
            get { return m_READONLY_USER; }
            set { m_READONLY_USER = value; }
        }
        private string m_READONLY_USER;

        [DataMember(Name = "SHOW_ALL_DRIVERS")]
        public string SHOW_ALL_DRIVERS
        {
            get { return m_SHOW_ALL_DRIVERS; }
            set { m_SHOW_ALL_DRIVERS = value; }
        }
        private string m_SHOW_ALL_DRIVERS;

        [DataMember(Name = "SHOW_ALL_VEHICLES")]
        public string SHOW_ALL_VEHICLES
        {
            get { return m_SHOW_ALL_VEHICLES; }
            set { m_SHOW_ALL_VEHICLES = value; }
        }
        private string m_SHOW_ALL_VEHICLES;

        [DataMember(Name = "date_of_birth")]
        public string date_of_birth
        {
            get { return m_date_of_birth; }
            set { m_date_of_birth = value; }
        }
        private string m_date_of_birth;

        [DataMember(Name = "member_email")]
        public string member_email
        {
            get { return m_member_email; }
            set { m_member_email = value; }
        }
        private string m_member_email;

        [DataMember(Name = "member_first_name")]
        public string member_first_name
        {
            get { return m_member_first_name; }
            set { m_member_first_name = value; }
        }
        private string m_member_first_name;

        [DataMember(Name = "member_last_name")]
        public string member_last_name
        {
            get { return m_member_last_name; }
            set { m_member_last_name = value; }
        }
        private string m_member_last_name;

        [DataMember(Name = "member_phone")]
        public string member_phone
        {
            get { return m_member_phone; }
            set { m_member_phone = value; }
        }
        private string m_member_phone;

        [DataMember(Name = "member_picture")]
        public string member_picture {
	        get { return m_member_picture; }
	        set { m_member_picture = value; }
        }
        private string m_member_picture;

        [DataMember(Name = "member_type")]
        public string member_type
        {
            get { return m_member_type; }
            set { m_member_type = value; }
        }
        private string m_member_type;

        [DataMember(Name = "member_zipcode")]
        public string member_zipcode
        {
            get { return m_member_zipcode; }
            set { m_member_zipcode = value; }
        }
        private string m_member_zipcode;

        [DataMember(Name = "preferred_language")]
        public string preferred_language
        {
            get { return m_preferred_language; }
            set { m_preferred_language = value; }
        }
        private string m_preferred_language;

        [DataMember(Name = "preferred_units")]
        public string preferred_units
        {
            get { return m_preferred_units; }
            set { m_preferred_units = value; }
        }
        private string m_preferred_units;

        [DataMember(Name = "timezone")]
        public string timezone
        {
            get { return m_timezone; }
            set { m_timezone = value; }
        }
        private string m_timezone;

        [DataMember(Name = "user_reg_country_id")]
        public string user_reg_country_id
        {
            get { return m_user_reg_country_id; }
            set { m_user_reg_country_id = value; }
        }
        private string m_user_reg_country_id;

        [DataMember(Name = "user_reg_state_id")]
        public string user_reg_state_id
        {
            get { return m_user_reg_state_id; }
            set { m_user_reg_state_id = value; }
        }
        private string m_user_reg_state_id;

        [DataMember(Name = "level")]
        public int? level { get; set; }

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
    }
}
