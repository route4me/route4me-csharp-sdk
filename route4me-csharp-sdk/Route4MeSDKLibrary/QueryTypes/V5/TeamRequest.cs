using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.QueryTypes.V5
{
    [DataContract]
    public sealed class TeamRequest : GenericParameters
    {
        private Dictionary<string, string> _custom_data;

        /// <summary>
        ///     A new passowrd of the user to loggin
        /// </summary>
        [DataMember(Name = "new_password", EmitDefaultValue = false)]
        public string NewPassword { get; set; }

        /// <summary>
        ///     An URL to a member picture file. e.g:
        ///     /uploads/cc6aba1a0e68ea429c51e3f9cb12e1ac/profile_c96135b77f6fc42be64cd98e0c21d341.jpg
        ///     or as base64 string: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD//2Q=="
        /// </summary>
        [DataMember(Name = "new_member_picture", EmitDefaultValue = false)]
        public string NewMemberPicture { get; set; }

        /// <summary>
        ///     User's first name
        /// </summary>
        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string MemberFirstName { get; set; }

        /// <summary>
        ///     User's last name
        /// </summary>
        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string MemberLastName { get; set; }

        /// <summary>
        ///     User's email
        /// </summary>
        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string MemberEmail { get; set; }

        /// <summary>
        ///     Member's company name
        /// </summary>
        [DataMember(Name = "member_company", EmitDefaultValue = false)]
        public string MemberCompany { get; set; }

        /// <summary>
        ///     Member type. Available values:
        ///     <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ///     <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        /// </summary>
        [DataMember(Name = "member_type", EmitDefaultValue = false)]
        public string MemberType { get; set; }

        /// <summary>
        ///     The user's account owner ID
        /// </summary>
        [DataMember(Name = "OWNER_MEMBER_ID", EmitDefaultValue = false)]
        public int? OwnerMemberId { get; set; }

        /// <summary>
        ///     User's phone number
        /// </summary>
        [DataMember(Name = "member_phone", EmitDefaultValue = false)]
        public string MemberPhone { get; set; }

        /// <summary>
        ///     Birthdate of the user.
        /// </summary>
        [DataMember(Name = "date_of_birth", EmitDefaultValue = false)]
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     Registration state ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_state_id", EmitDefaultValue = false)]
        public string UserRegStateId { get; set; }

        /// <summary>
        ///     Registration country ID of a user
        /// </summary>
        [DataMember(Name = "user_reg_country_id", EmitDefaultValue = false)]
        public string UserRegCountryId { get; set; }

        /// <summary>
        ///     Hourly rate of a user
        /// </summary>
        [DataMember(Name = "DriverHourlyRate", EmitDefaultValue = false)]
        public double? DriverHourlyRate { get; set; }

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
        [DataMember(Name = "READONLY_USER", EmitDefaultValue = false)]
        public bool ReadOnlyUser { get; set; }

        /// <summary>
        ///     If true, the global address book contacts
        ///     are visible in a user account.
        /// </summary>
        [DataMember(Name = "SHOW_SUSR_ADDR", EmitDefaultValue = false)]
        public bool ShowGlobalAddresses { get; set; }

        /// <summary>
        ///     If true, the global orders are visible in a user account.
        /// </summary>
        [DataMember(Name = "SHOW_SUSR_ORDERS", EmitDefaultValue = false)]
        public bool ShowGlobalOrders { get; set; } = true;

        /// <summary>
        ///     If true, all drivers are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_DRIVERS", EmitDefaultValue = false)]
        public bool ShowAllDrivers { get; set; }

        /// <summary>
        ///     If true, all vehicles are visible to the user.
        /// </summary>
        [DataMember(Name = "SHOW_ALL_VEHICLES", EmitDefaultValue = false)]
        public bool ShowAllVehicles { get; set; }

        /// <summary>
        ///     Display maximum_routes number of future days.
        /// </summary>
        [DataMember(Name = "display_max_routes_future_days", EmitDefaultValue = false)]
        public int? DisplayMaxRoutesFutureDays { get; set; }

        /// <summary>
        ///     User zipcode
        /// </summary>
        [DataMember(Name = "vendor_id", EmitDefaultValue = false)]
        public string VendorId { get; set; }

        /// <summary>
        ///     Driving rate of a user.
        /// </summary>
        [DataMember(Name = "driving_rate", EmitDefaultValue = false)]
        public double? DrivingRate { get; set; }

        /// <summary>
        ///     Working rate of a user.
        /// </summary>
        [DataMember(Name = "working_rate", EmitDefaultValue = false)]
        public double? WorkingRate { get; set; }

        /// <summary>
        ///     Mile rate of a user.
        /// </summary>
        [DataMember(Name = "mile_rate", EmitDefaultValue = false)]
        public double? MileRate { get; set; }

        /// <summary>
        ///     Mile rate of a user.
        /// </summary>
        [DataMember(Name = "idling_rate", EmitDefaultValue = false)]
        public double? IdlingRate { get; set; }

        /// <summary>
        ///     Member's location timezone
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

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
        ///     Validate this object
        /// </summary>
        /// <param name="errorString">Error message</param>
        /// <returns>True, if this object not valid for creating a team member</returns>
        public bool ValidateMemberCreateRequest(out string errorString)
        {
            var isValid = true;
            errorString = "";

            if (MemberFirstName == null)
            {
                errorString += "Member first name is empty";
                isValid = false;
            }

            if (MemberLastName == null)
            {
                errorString += (errorString != null ? Environment.NewLine : "") + "Member last name is empty";
                isValid = false;
            }

            if (MemberType == null)
            {
                errorString = (errorString != null ? Environment.NewLine : "") + "Member type is empty";
                isValid = false;
            }
            else
            {
                if (GetMemberType(MemberType) == null)
                {
                    errorString = (errorString != null ? Environment.NewLine : "") + "Member type " + MemberType +
                                  "is not valid";
                    isValid = false;
                }
            }

            if (MemberEmail == null)
            {
                errorString += (errorString != null ? Environment.NewLine : "") + "Member email is empty";
                isValid = false;
            }

            if (NewPassword == null)
            {
                errorString += (errorString != null ? Environment.NewLine : "") + "Member password is empty";
                isValid = false;
            }

            if (OwnerMemberId == null)
            {
                errorString += (errorString != null ? Environment.NewLine : "") + "Member owner ID is empty";
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        ///     Set a member type by MemberTypes enum type.
        /// </summary>
        /// <param name="memberType">MemberTypes enum type value</param>
        public void SetMemberType(MemberTypes memberType)
        {
            MemberType = memberType.Description();
        }

        public MemberTypes? GetMemberType(string memberType)
        {
            foreach (int i in Enum.GetValues(typeof(MemberTypes)))
                if (((MemberTypes) i).Description().Equals(memberType))
                    return (MemberTypes) i;

            return null;
        }
    }
}