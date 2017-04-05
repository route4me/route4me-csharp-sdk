namespace Route4MeSDK.QueryTypes
{
    public sealed class GeocodingParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public string Addresses
        {
            get { return m_Addresses; }
            set { m_Addresses = value; }
        }
        private string m_Addresses;

        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }
        private string m_Format;

        [HttpQueryMemberAttribute(Name = "pk", EmitDefaultValue = false)]
        public int Pk
        {
            get { return m_pk; }
            set { m_pk = value; }
        }
        private int m_pk;

        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }
        private int m_Offset;

        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int Limit
        {
            get { return m_Limit; }
            set { m_Limit = value; }
        }
        private int m_Limit;

        [HttpQueryMemberAttribute(Name = "zipcode", EmitDefaultValue = false)]
        public string Zipcode
        {
            get { return m_Zipcode; }
            set { m_Zipcode = value; }
        }
        private string m_Zipcode;

        [HttpQueryMemberAttribute(Name = "housenumber", EmitDefaultValue = false)]
        public string Housenumber
        {
            get { return m_Housenumber; }
            set { m_Housenumber = value; }
        }
        private string m_Housenumber;
    }
}
