using System;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// Parameters for the address book contact(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class AddressBookParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of an address book contact.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public string AddressId { get; set; }

        /// <summary>
        /// Limit the number of records in response.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        /// <summary>
        /// Query string for filtering of the address book contacts.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        /// An array of the fields.
        /// <para><remarks>If specified, the response will contain only the values of the listed fields.</remarks></para>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        /// <summary>
        /// Specifies which address book contacts to display.
        /// <para>Available values are:
        /// <value>'all', 'routed', 'unrouted'</value>
        /// </para>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }
    }
}
