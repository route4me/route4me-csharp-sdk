using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class TelematicsVendorParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public System.Nullable<uint> vendorID { get; set; }

        [HttpQueryMemberAttribute(Name = "is_integrated", EmitDefaultValue = false)]
        public System.Nullable<uint> isIntegrated { get; set; }

        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public System.Nullable<uint> Page { get; set; }

        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public System.Nullable<uint> perPage { get; set; }

        [HttpQueryMemberAttribute(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        [HttpQueryMemberAttribute(Name = "feature", EmitDefaultValue = false)]
        public string Feature { get; set; }

        [HttpQueryMemberAttribute(Name = "search", EmitDefaultValue = false)]
        public string Search { get; set; }

        /// <summary>
        /// Comma-delimited list of the vendors IDs
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendors", EmitDefaultValue = false)]
        public string Vendors { get; set; }
    }
}
