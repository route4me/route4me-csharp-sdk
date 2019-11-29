using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class TelematicsVendorParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public uint? vendorID { get; set; }

        [HttpQueryMemberAttribute(Name = "is_integrated", EmitDefaultValue = false)]
        public uint? isIntegrated { get; set; }

        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public uint? perPage { get; set; }

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
