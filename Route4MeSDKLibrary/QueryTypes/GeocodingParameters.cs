namespace Route4MeSDK.QueryTypes
{
    public sealed class GeocodingParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public string Addresses { get; set; }

        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        [HttpQueryMemberAttribute(Name = "strExportFormat", EmitDefaultValue = false)]
        public string ExportFormat { get; set; }

        [HttpQueryMemberAttribute(Name = "pk", EmitDefaultValue = false)]
        public int Pk { get; set; }

        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }

        [HttpQueryMemberAttribute(Name = "zipcode", EmitDefaultValue = false)]
        public string Zipcode { get; set; }

        [HttpQueryMemberAttribute(Name = "housenumber", EmitDefaultValue = false)]
        public string Housenumber { get; set; }
    }
}
