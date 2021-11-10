using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the user endpoint (/api.v4/user.php) request
    /// </summary>
    [DataContract]
    public sealed class MemberResponseV4
    {
        private Dictionary<string, string> _custom_data;

        /// <summary>
        ///     If true, the nonfuture routes will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public string HideNonFutureRoutes { get; set; }

        /// <summary>
        ///     If true, the routed addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public string HideRoutedAddresses { get; set; }

        /// <summary>
        ///     If true, the visited addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public string HideVisitedAddresses { get; set; }

        /// <summary>
        ///     The member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public string MemberId { get; set; }

        /// <summary>
        ///     The user's account owner ID
        /// </summary>
        [DataMember(Name = "OWNER_MEMBER_ID")]
        public string OwnerMemberId { get; set; }

        /// <summary>
        ///     If true, the user has read-only access type.
        /// </summary>
        [DataMember(Name = "READONLY_USER")]
        public string ReadOnlyUser { get; set; }

        /// <summary>
        ///     If true, all drivers are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_DRIVERS")]
        public string ShowAllDrivers { get; set; }

        /// <summary>
        ///     If true, all vehicles are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_VEHICLES")]
        public string ShowAllVehicles { get; set; }

        /// <summary>
        ///     Birthdate of the user.
        /// </summary>
        [DataMember(Name = "date_of_birth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     User's email
        /// </summary>
        [DataMember(Name = "member_email")]
        public string MemberEmail { get; set; }

        /// <summary>
        ///     User's first name
        /// </summary>
        [DataMember(Name = "member_first_name")]
        public string MemberFirstName { get; set; }

        /// <summary>
        ///     User's last name
        /// </summary>
        [DataMember(Name = "member_last_name")]
        public string MemberLastName { get; set; }

        /// <summary>
        ///     User's phone number
        /// </summary>
        [DataMember(Name = "member_phone")]
        public string MemberPhone { get; set; }

        /// <summary>
        ///     A link to the user's picture
        /// </summary>
        [DataMember(Name = "member_picture")]
        public string MemberPicture { get; set; }

        /// <summary>
        ///     Member type. Available values:
        ///     <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ///     <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        /// </summary>
        [DataMember(Name = "member_type")]
        public string MemberType { get; set; }

        /// <summary>
        ///     User zipcode
        /// </summary>
        [DataMember(Name = "member_zipcode")]
        public string MemberZipCode { get; set; }

        /// <summary>
        ///     Preferred language (en, fr)
        /// </summary>
        [DataMember(Name = "preferred_language")]
        public string PreferredLanguage { get; set; }

        /// <summary>
        ///     Preferred unit (mi, km)
        /// </summary>
        [DataMember(Name = "preferred_units")]
        public string PreferredUnits { get; set; }

        /// <summary>
        ///     Member's location timezone
        /// </summary>
        [DataMember(Name = "timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        ///     Registration country ID of a user.
        /// </summary>
        [DataMember(Name = "user_reg_country_id")]
        public string UserRegCountryId { get; set; }

        /// <summary>
        ///     Registration state ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_state_id")]
        public string UserRegStateId { get; set; }

        /// <summary>
        ///     Subordination level. 0 is the highest level.
        /// </summary>
        [DataMember(Name = "level")]
        public int? Level { get; set; }

        /// <summary>
        ///     The user's custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomData
        {
            get
            {
                if (_custom_data == null) return null;

                var v1 = _custom_data;

                var v2 = new Dictionary<string, string>();
                foreach (var kv1 in v1)
                    if (kv1.Key != null)
                    {
                        if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value);
                        else v2.Add(kv1.Key, "");
                    }
                    else
                    {
                    }

                return v2;
            }
            set
            {
                if (value == null)
                {
                    _custom_data = null;
                }
                else
                {
                    var v1 = value;
                    var v2 = new Dictionary<string, string>();
                    foreach (var kv1 in v1)
                        if (kv1.Key != null)
                        {
                            if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value);
                            else v2.Add(kv1.Key, "");
                        }
                        else
                        {
                        }

                    _custom_data = v2;
                }
            }
        }

        /// <summary>
        ///     User API key.
        /// </summary>
        [DataMember(Name = "api_key")]
        public string ApiKey { get; set; }
    }
}