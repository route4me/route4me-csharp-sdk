using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     User data structure.
    /// </summary>
    [DataContract]
    public sealed class User
    {
        /// <summary>
        ///     Unique ID of the member inside the Route4Me system.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Unique ID of the account type.
        /// </summary>
        [DataMember(Name = "account_type_id", EmitDefaultValue = false)]
        public int? AccountTypeId { get; set; }

        /// <summary>
        ///     Member type of the user account.
        /// </summary>
        [DataMember(Name = "member_type", EmitDefaultValue = false)]
        public string MemberType { get; set; }

        /// <summary>
        ///     First name of the user.
        /// </summary>
        [DataMember(Name = "member_first_name")]
        public string MemberFirstName { get; set; }

        /// <summary>
        ///     Last name of the user.
        /// </summary>
        [DataMember(Name = "member_last_name")]
        public string MemberLastName { get; set; }

        /// <summary>
        ///     User email.
        /// </summary>
        [DataMember(Name = "member_email")]
        public string MemberEmail { get; set; }

        /// <summary>
        ///     Phone number of the user.
        /// </summary>
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     If true, the user has readonly access right.
        /// </summary>
        [DataMember(Name = "readonly_user", EmitDefaultValue = false)]
        public bool? ReadonlyUser { get; set; }

        /// <summary>
        ///     If true, the superuser's addresses are visible to the user.
        /// </summary>
        [DataMember(Name = "show_superuser_addresses", EmitDefaultValue = false)]
        public bool? ShowSuperUserAddresses { get; set; }
    }
}