using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Address book group class.
    ///     <para>
    ///         See the JSON schema at
    ///         <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/AddressbookGroup_v4.dtd">link</see>
    ///     </para>
    /// </summary>
    [DataContract]
    public sealed class AddressBookGroup : GenericParameters
    {
        /// <summary>
        ///     An unique ID of the group
        /// </summary>
        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public string GroupId { get; set; }

        /// <summary>
        ///     The group name
        /// </summary>
        [DataMember(Name = "group_name", EmitDefaultValue = false)]
        public string GroupName { get; set; }

        /// <summary>
        ///     The group color
        /// </summary>
        [DataMember(Name = "group_color", EmitDefaultValue = false)]
        public string GroupColor { get; set; }

        /// <summary>
        ///     The group icon
        /// </summary>
        [DataMember(Name = "group_icon", EmitDefaultValue = false)]
        public string GroupIcon { get; set; }

        /// <summary>
        ///     A member ID the group belongs
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        ///     The AddressBookGroupFilter type object as a group filter for the address book contacs
        /// </summary>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressBookGroupFilter Filter { get; set; }

        /// <summary>
        ///     If true, the address book group is valid.
        /// </summary>
        [DataMember(Name = "valid", EmitDefaultValue = false)]
        public bool? Valid { get; set; }
    }
}