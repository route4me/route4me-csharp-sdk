using System;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for the address(es) geocoding request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class GeocodingParameters : GenericParameters
    {
        /// <summary>
        ///     List of the addresses as a multiline text.
        ///     <remarks>
        ///         <para>The addresses are delimited with the newline character.</para>
        ///     </remarks>
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public string Addresses { get; set; }

        [Obsolete("This parameter doesn't work in the geocoding request")]
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        /// <summary>
        ///     Response export format.
        ///     <para>Availbale values:
        ///         <value>json, xml, csv.</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "strExportFormat", EmitDefaultValue = false)]
        public string ExportFormat { get; set; }

        /// <summary>
        ///     Rapis string data index.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "pk", EmitDefaultValue = false)]
        public int Pk { get; set; }

        /// <summary>
        ///     Only records from that offset will be considered.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        /// <summary>
        ///     Limit the number of records in response.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }

        /// <summary>
        ///     Zipcode.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "zipcode", EmitDefaultValue = false)]
        public string Zipcode { get; set; }

        /// <summary>
        ///     House number.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "housenumber", EmitDefaultValue = false)]
        public string Housenumber { get; set; }
    }
}