using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for requesting the telematics vendors.
    /// </summary>
    public sealed class TelematicsVendorParameters : GenericParameters
    {
        /// <summary>
        /// An unique ID of a telematics vendor.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public uint? VendorID { get; set; }

        /// <summary>
        /// If equal to 1, the vendor is integrated in the Route4Me system.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_integrated", EmitDefaultValue = false)]
        public uint? isIntegrated { get; set; }

        /// <summary>
        /// Current page in the vendors collection
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        /// Vendors number per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public uint? perPage { get; set; }

        /// <summary>
        /// The vendor's country
        /// </summary>
        [HttpQueryMemberAttribute(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// A vendor's feature
        /// </summary>
        [HttpQueryMemberAttribute(Name = "feature", EmitDefaultValue = false)]
        public string Feature { get; set; }

        /// <summary>
        /// A query string
        /// </summary>
        [HttpQueryMemberAttribute(Name = "search", EmitDefaultValue = false)]
        public string Search { get; set; }

        /// <summary>
        /// Comma-delimited list of the vendors IDs
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendors", EmitDefaultValue = false)]
        public string Vendors { get; set; }

        /// <summary>
        /// Owner of a telematicss connection.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public uint? MemberID { get; set; }

        /// <summary>
        /// Is a user real or virtual
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_virtual", EmitDefaultValue = false)]
        public uint? isVirtual { get; set; }

        /// <summary>
        /// API key
        /// </summary>
        [HttpQueryMemberAttribute(Name = "api_key", EmitDefaultValue = false)]
        public string ApiKey { get; set; }

        /// <summary>
        /// If true, remote credentials validated.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "validate_remote_credentials", EmitDefaultValue = false)]
        public bool? ValidateRemoteCredentials { get; set; }

        /// <summary>
        /// API token.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "api_token", EmitDefaultValue = false)]
        public string ApiToken { get; set; }
    }
}
