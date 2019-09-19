using System;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the telematics vendor(s) search request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class TelematicsVendorParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of a telematics vendor.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public Nullable<uint> vendorID { get; set; }

        /// <summary>
        /// Specifies if a vendor is integrated in the Route4Me system.
        /// <para>Available values: <value>1 - is integrated, 0 - is not integrated.</value></para> 
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_integrated", EmitDefaultValue = false)]
        public Nullable<uint> isIntegrated { get; set; }

        /// <summary>
        /// Filter vendors by page.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public Nullable<uint> Page { get; set; }

        /// <summary>
        /// The number of records (vendors) to display per page.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public Nullable<uint> perPage { get; set; }

        /// <summary>
        /// Filter vendors by country code.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// Filter vendors by feature slug.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "feature", EmitDefaultValue = false)]
        public string Feature { get; set; }

        /// <summary>
        /// Search the vendors by text.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "s", EmitDefaultValue = false)]
        public string Search { get; set; }

        /// <summary>
        /// Comma-delimited list of the vendors IDs.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendors", EmitDefaultValue = false)]
        public string Vendors { get; set; }
    }
}
