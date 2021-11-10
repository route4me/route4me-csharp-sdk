using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Account profile
    /// </summary>
    [DataContract]
    public sealed class AccountProfile
    {
        /// <summary>
        ///     Account profile email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        ///     Acount member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberIId { get; set; }

        /// <summary>
        ///     Account API key
        /// </summary>
        [DataMember(Name = "api_key", EmitDefaultValue = false)]
        public string ApiKey { get; set; }

        /// <summary>
        ///     Account root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>
        ///     Prefered unnits of the account
        /// </summary>
        [DataMember(Name = "preferred_units", EmitDefaultValue = false)]
        public string PreferredUnits { get; set; }
    }
}