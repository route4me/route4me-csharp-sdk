using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for an address book group(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressBookGroupParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of a address book group.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "group_id")]
        public string groupID { get; set; }

        /// <summary>
        /// An array of the fields.
        /// <para><remarks>If specified, the response will contain only the values of the specified fields.</remarks></para>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "offset")]
        public int offset { get; set; }

        /// <summary>
        /// Limit the number of records in response.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "limit")]
        public int limit { get; set; }

        /// <summary>
        /// The AddressBookGroupFilterParameter type object as a group filter. 
        /// See <see cref="AddressBookGroupFilterParameter"/>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "filter")]
        public AddressBookGroupFilterParameter filter { get; set; }

        /// <summary>
        /// Unique ID of a address book group.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "group_id", EmitDefaultValue = false)]
        public string GroupId { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        /// <summary>
        /// Limit the number of records in response.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }
    }

    /// <summary>
    /// Subclass of the class AddressBookGroupParameters. 
    /// See <see cref="AddressBookGroupParameters"/>
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressBookGroupFilterParameter : GenericParameters
    {
        /// <summary>
        /// The query parameter for the address book group(s) request.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Specifies which address book contacts to display.
        /// <para>Available values are:
        /// <value>'all', 'routed', 'unrouted'</value>
        /// </para>
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "display")]
        public string Display { get; set; }
    }
}
