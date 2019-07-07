using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    [DataContract]
    public sealed class AddressBookGroupParameters : GenericParameters
    {
        [DataMember(Name = "group_id")]
        public string groupID { get; set; }

        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }

        [DataMember(Name = "offset")]
        public int offset { get; set; }

        [DataMember(Name = "limit")]
        public int limit { get; set; }

        [DataMember(Name = "filter")]
        public AddressBookGroupFilterParameter filter { get; set; }

        [HttpQueryMemberAttribute(Name = "group_id", EmitDefaultValue = false)]
        public string GroupId { get; set; }

        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }
    }

    [DataContract]
    public sealed class AddressBookGroupFilterParameter : GenericParameters
    {
        [DataMember(Name = "query")]
        public string query { get; set; }


        /// <summary>
        /// Available values are: "all", "routed", "unrouted"
        /// </summary>
        [DataMember(Name = "display")]
        public string display { get; set; }
    }
}
