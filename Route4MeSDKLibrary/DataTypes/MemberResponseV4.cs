using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the user endpoint (/api.v4/user.php) request
    /// </summary>
    [DataContract]
    public sealed class MemberResponseV4
    {
        /// <summary>
        /// If true, the nonfuture routes will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public string HIDE_NONFUTURE_ROUTES { get; set; }

        /// <summary>
        /// If true, the routed addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_ROUTED_ADDRESSES { get; set; }

        /// <summary>
        /// If true, the visited addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_VISITED_ADDRESSES { get; set; }

        /// <summary>
        /// The member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public string member_id { get; set; }

        /// <summary>
        /// The user's account owner ID
        /// </summary>
        [DataMember(Name = "OWNER_MEMBER_ID")]
        public string OWNER_MEMBER_ID { get; set; }

        /// <summary>
        /// If true, the user has read-only access type.
        /// </summary>
        [DataMember(Name = "READONLY_USER")]
        public string READONLY_USER { get; set; }

        /// <summary>
        /// If true, all drivers are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_DRIVERS")]
        public string SHOW_ALL_DRIVERS { get; set; }

        /// <summary>
        /// If true, all vehicles are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_VEHICLES")]
        public string SHOW_ALL_VEHICLES { get; set; }

        /// <summary>
        /// Birthdate of the user.
        /// </summary>
        [DataMember(Name = "date_of_birth")]
        public string date_of_birth { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [DataMember(Name = "member_email")]
        public string member_email { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [DataMember(Name = "member_first_name")]
        public string member_first_name { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [DataMember(Name = "member_last_name")]
        public string member_last_name { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        [DataMember(Name = "member_phone")]
        public string member_phone { get; set; }

        /// <summary>
        /// A link to the user's picture
        /// </summary>
        [DataMember(Name = "member_picture")]
        public string member_picture { get; set; }

        /// <summary>
        /// Member type. Available values:
        /// <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        /// <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        /// </summary>
        [DataMember(Name = "member_type")]
        public string member_type { get; set; }

        /// <summary>
        /// User zipcode
        /// </summary>
        [DataMember(Name = "member_zipcode")]
        public string member_zipcode { get; set; }

        /// <summary>
        /// Preferred language (en, fr)
        /// </summary>
        [DataMember(Name = "preferred_language")]
        public string preferred_language { get; set; }

        /// <summary>
        /// Preferred unit (mi, km)
        /// </summary>
        [DataMember(Name = "preferred_units")]
        public string preferred_units { get; set; }

        /// <summary>
        /// Member's location timezone
        /// </summary>
        [DataMember(Name = "timezone")]
        public string timezone { get; set; }

        /// <summary>
        /// Registration country ID of a user.
        /// </summary>
        [DataMember(Name = "user_reg_country_id")]
        public string user_reg_country_id { get; set; }

        /// <summary>
        /// Registration state ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_state_id")]
        public string user_reg_state_id { get; set; }

        /// <summary>
        /// Subordination level. 0 is the highest level.
        /// </summary>
        [DataMember(Name = "level")]
        public int? level { get; set; }

        /// <summary>
        /// The user's custom data
        /// </summary>
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
