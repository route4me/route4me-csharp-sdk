using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressBookGroup : GenericParameters
    {
        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public string groupID { get; set; }

        [DataMember(Name = "group_name", EmitDefaultValue = false)]
        public string groupName { get; set; }

        [DataMember(Name = "group_color", EmitDefaultValue = false)]
        public string groupColor { get; set; }

        [DataMember(Name = "group_icon", EmitDefaultValue = false)]
        public string groupIcon { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string memberId { get; set; }

        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressBookGroupFilter Filter { get; set; }
    }
}
