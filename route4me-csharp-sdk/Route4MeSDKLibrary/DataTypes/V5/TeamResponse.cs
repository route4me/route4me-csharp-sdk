using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the user endpoint (/api/v5.0/team/users) request
    /// </summary>
    [DataContract]
    public sealed class TeamResponse
    {
        private Dictionary<string, string> _custom_data;

        /// <summary>
        ///     The member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        ///     The user's account owner ID
        /// </summary>
        [DataMember(Name = "OWNER_MEMBER_ID")]
        public int? OwnerMemberId { get; set; }

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
        ///     User's email
        /// </summary>
        [DataMember(Name = "member_email")]
        public string MemberEmail { get; set; }

        /// <summary>
        ///     User's phone number
        /// </summary>
        [DataMember(Name = "member_phone")]
        public string MemberPhone { get; set; }

        /// <summary>
        ///     Member's company name
        /// </summary>
        [DataMember(Name = "member_company", EmitDefaultValue = false)]
        public string MemberCompany { get; set; }

        /// <summary>
        ///     Birthdate of the user.
        /// </summary>
        [DataMember(Name = "date_of_birth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     Registration state ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_state_id")]
        public string UserRegStateId { get; set; }

        /// <summary>
        ///     Registration country ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_country_id")]
        public string UserRegCountryId { get; set; }

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
        ///     If true, the routed addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public bool HideRoutedAddresses { get; set; }

        /// <summary>
        ///     If true, the visited addresses will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public bool HideVisitedAddresses { get; set; }

        /// <summary>
        ///     If true, the nonfuture routes will be hidden.
        /// </summary>
        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public bool HideNonFutureRoutes { get; set; }

        /// <summary>
        ///     If true, the user has read-only access type.
        /// </summary>
        [DataMember(Name = "READONLY_USER")]
        public bool ReadOnlyUser { get; set; }

        /// <summary>
        ///     If true, the global address book contacts
        ///     are visible in a user account.
        /// </summary>
        [DataMember(Name = "SHOW_SUSR_ADDR")]
        public bool ShowGlobalAddresses { get; set; }

        /// <summary>
        ///     If true, the global orders are visible in a user account.
        /// </summary>
        [DataMember(Name = "SHOW_SUSR_ORDERS")]
        public bool ShowGlobalOrders { get; set; } = true;

        /// <summary>
        ///     If true, all drivers are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_DRIVERS")]
        public bool ShowAllDrivers { get; set; }

        /// <summary>
        ///     If true, all vehicles are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_VEHICLES")]
        public bool ShowAllVehicles { get; set; }

        /// <summary>
        ///     Allowed sub-member types in the user's account.
        ///     Available array item values:
        ///     "SUB_ACCOUNT_DRIVER", "SUB_ACCOUNT_DISPATCHER", "SUB_ACCOUNT_PLANNER",
        ///     "SUB_ACCOUNT_ANALYST", "SUB_ACCOUNT_ADMIN", "SUB_ACCOUNT_REGIONAL_MANAGER"
        /// </summary>
        [DataMember(Name = "allowed_submember_types")]
        public string[] AllowedSubmemberTypes { get; set; }

        /// <summary>
        ///     If true, the user can edit the account data.
        /// </summary>
        [DataMember(Name = "can_edit")]
        public bool CanEdit { get; set; }

        /// <summary>
        ///     If true, the user can delete the account data.
        /// </summary>
        [DataMember(Name = "can_delete")]
        public bool CanDelete { get; set; }

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
        ///     Hourly rate of a user
        /// </summary>
        [DataMember(Name = "DriverHourlyRate")]
        public double? DriverHourlyRate { get; set; }

        /// <summary>
        ///     User zipcode
        /// </summary>
        [DataMember(Name = "vendor_id")]
        public string VendorId { get; set; }

        /// <summary>
        ///     Driving rate of a user.
        /// </summary>
        [DataMember(Name = "driving_rate")]
        public double? DrivingRate { get; set; }

        /// <summary>
        ///     Working rate of a user.
        /// </summary>
        [DataMember(Name = "working_rate")]
        public double? WorkingRate { get; set; }

        /// <summary>
        ///     Mile rate of a user.
        /// </summary>
        [DataMember(Name = "mile_rate")]
        public double? MileRate { get; set; }

        /// <summary>
        ///     Mile rate of a user.
        /// </summary>
        [DataMember(Name = "idling_rate")]
        public double? IdlingRate { get; set; }

        /// <summary>
        ///     Display maximum_routes number of future days.
        /// </summary>
        [DataMember(Name = "display_max_routes_future_days", EmitDefaultValue = false)]
        public int? DisplayMaxRoutesFutureDays { get; set; }

        /// <summary>
        ///     Member's location timezone
        /// </summary>
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }
    }
}