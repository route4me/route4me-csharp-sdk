namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for the telematics vendor(s) search request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class TelematicsVendorParameters : GenericParameters
    {
        /// <summary>
        ///     An unique ID of a telematics vendor.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public uint? VendorID { get; set; }

        /// <summary>
        ///     Specifies if a vendor is integrated in the Route4Me system.
        ///     <para>Available values:
        ///         <value>1 - is integrated, 0 - is not integrated.</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_integrated", EmitDefaultValue = false)]
        public uint? IsIntegrated { get; set; }

        /// <summary>
        ///     Filter vendors by page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        ///     The number of records (vendors) to display per page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        ///     Filter vendors by country code.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        ///     Filter vendors by feature slug.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "feature", EmitDefaultValue = false)]
        public string Feature { get; set; }

        /// <summary>
        ///     Search the vendors by text.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "s", EmitDefaultValue = false)]
        public string Search { get; set; }

        /// <summary>
        ///     Comma-delimited list of the vendors IDs.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendors", EmitDefaultValue = false)]
        public string Vendors { get; set; }

        /// <summary>
        ///     Owner of a telematicss connection.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public uint? MemberID { get; set; }

        /// <summary>
        ///     Is a user real or virtual
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_virtual", EmitDefaultValue = false)]
        public uint? IsVirtual { get; set; }

        /// <summary>
        ///     API key
        /// </summary>
        [HttpQueryMemberAttribute(Name = "api_key", EmitDefaultValue = false)]
        public string ApiKey { get; set; }

        /// <summary>
        ///     If true, remote credentials validated.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "validate_remote_credentials", EmitDefaultValue = false)]
        public bool? ValidateRemoteCredentials { get; set; }

        /// <summary>
        ///     API token.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "api_token", EmitDefaultValue = false)]
        public string ApiToken { get; set; }
    }
}