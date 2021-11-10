using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for the member request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class MemberParametersV4 : GenericParameters
    {
        private Dictionary<string, string> _custom_data;

        /// <summary>
        ///     Unique ID of a member.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public int? member_id { get; set; }

        /// <summary>
        ///     Unique ID of a device.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string device_id { get; set; }

        /// <summary>
        ///     If true, routed addresses will be hidden.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_ROUTED_ADDRESSES { get; set; }

        /// <summary>
        ///     If true, visited addresses will be hidden.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public string HIDE_VISITED_ADDRESSES { get; set; }

        /// <summary>
        ///     If true, a user has readonly access.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "READONLY_USER", EmitDefaultValue = false)]
        public string READONLY_USER { get; set; }

        /// <summary>
        ///     If true, nonfuture routes will be hidden.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public string HIDE_NONFUTURE_ROUTES { get; set; }

        /// <summary>
        ///     If true, all vehicles are visible in a user's account.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "SHOW_ALL_VEHICLES", EmitDefaultValue = false)]
        public string SHOW_ALL_VEHICLES { get; set; }

        /// <summary>
        ///     If true, all drivers are visible in a user's account.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "SHOW_ALL_DRIVERS", EmitDefaultValue = false)]
        public string SHOW_ALL_DRIVERS { get; set; }

        /// <summary>
        ///     Member phone number.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_phone", EmitDefaultValue = false)]
        public string member_phone { get; set; }

        /// <summary>
        ///     Member location zipcode.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_zipcode", EmitDefaultValue = false)]
        public string member_zipcode { get; set; }

        /// <summary>
        ///     Registration country ID of a user.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "user_reg_country_id", EmitDefaultValue = false)]
        public int? user_reg_country_id { get; set; }

        /// <summary>
        ///     Registration state ID of a user.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "user_reg_state_id", EmitDefaultValue = false)]
        public int? user_reg_state_id { get; set; }

        /// <summary>
        ///     Preferred language.
        ///     <para>Available values: </para>
        ///     <value>'en', 'fr'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "preferred_language", EmitDefaultValue = false)]
        public string preferred_language { get; set; }

        /// <summary>
        ///     Preferred units.
        ///     <para>Available values: </para>
        ///     <value>'mi', 'km'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "preferred_units", EmitDefaultValue = false)]
        public string preferred_units { get; set; }

        /// <summary>
        ///     Member timezone zipcode.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string timezone { get; set; }

        /// <summary>
        ///     Custom data of a member.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> custom_data
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
        ///     Allowed maximum routes number per month.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_count", EmitDefaultValue = false)]
        public int? route_count { get; set; }

        /// <summary>
        ///     Member email.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string member_email { get; set; }

        /// <summary>
        ///     Member type. Available values:
        ///     <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ///     <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_type", EmitDefaultValue = false)]
        public string member_type { get; set; }

        /// <summary>
        ///     Member birth date.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "date_of_birth", EmitDefaultValue = false)]
        public string date_of_birth { get; set; }

        /// <summary>
        ///     Member password.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_password", EmitDefaultValue = false)]
        public string member_password { get; set; }

        /// <summary>
        ///     Unique ID of the member's account owner.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "OWNER_MEMBER_ID", EmitDefaultValue = false)]
        public int? OWNER_MEMBER_ID { get; set; }

        // <summary>
        /// First name of a member.
        /// <remarks>
        ///     <para>Data member parameter.</para>
        /// </remarks>
        /// </summary>
        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string member_first_name { get; set; }

        // <summary>
        /// Last name of a member.
        /// <remarks>
        ///     <para>Data member parameter.</para>
        /// </remarks>
        /// </summary>
        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string member_last_name { get; set; }
    }
}