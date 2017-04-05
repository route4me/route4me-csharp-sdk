using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberParametersV4 : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public System.Nullable<int> member_id
        {
            get { return m_member_id; }
            set { m_member_id = value; }
        }
        private System.Nullable<int> m_member_id;

        [DataMember(Name = "HIDE_ROUTED_ADDRESSES", EmitDefaultValue = false)]
        public bool HIDE_ROUTED_ADDRESSES
        {
            get { return m_HIDE_ROUTED_ADDRESSES; }
            set { m_HIDE_ROUTED_ADDRESSES = value; }
        }
        private bool m_HIDE_ROUTED_ADDRESSES;


        [DataMember(Name = "HIDE_VISITED_ADDRESSES", EmitDefaultValue = false)]
        public bool HIDE_VISITED_ADDRESSES
        {
            get { return m_HIDE_VISITED_ADDRESSES; }
            set { m_HIDE_VISITED_ADDRESSES = value; }
        }
        private bool m_HIDE_VISITED_ADDRESSES;

        [DataMember(Name = "READONLY_USER", EmitDefaultValue = false)]
        public bool READONLY_USER
        {
            get { return m_READONLY_USER; }
            set { m_READONLY_USER = value; }
        }
        private bool m_READONLY_USER;


        [DataMember(Name = "HIDE_NONFUTURE_ROUTES", EmitDefaultValue = false)]
        public bool HIDE_NONFUTURE_ROUTES
        {
            get { return m_HIDE_NONFUTURE_ROUTES; }
            set { m_HIDE_NONFUTURE_ROUTES = value; }
        }
        private bool m_HIDE_NONFUTURE_ROUTES;

        [DataMember(Name = "SHOW_ALL_VEHICLES", EmitDefaultValue = false)]
        public bool SHOW_ALL_VEHICLES
        {
            get { return m_SHOW_ALL_VEHICLES; }
            set { m_SHOW_ALL_VEHICLES = value; }
        }
        private bool m_SHOW_ALL_VEHICLES;

        [DataMember(Name = "SHOW_ALL_DRIVERS", EmitDefaultValue = false)]
        public bool SHOW_ALL_DRIVERS
        {
            get { return m_SHOW_ALL_DRIVERS; }
            set { m_SHOW_ALL_DRIVERS = value; }
        }
        private bool m_SHOW_ALL_DRIVERS;

        [DataMember(Name = "member_phone", EmitDefaultValue = false)]
        public string member_phone
        {
            get { return m_member_phone; }
            set { m_member_phone = value; }
        }
        private string m_member_phone;

        [DataMember(Name = "member_zipcode", EmitDefaultValue = false)]
        public string member_zipcode
        {
            get { return m_member_zipcode; }
            set { m_member_zipcode = value; }
        }
        private string m_member_zipcode;

        [HttpQueryMemberAttribute(Name = "route_count", EmitDefaultValue = false)]
        public System.Nullable<int> route_count
        {
            get { return m_route_count; }
            set { m_route_count = value; }
        }
        private System.Nullable<int> m_route_count;

        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string member_email
        {
            get { return m_member_email; }
            set { m_member_email = value; }
        }
        private string m_member_email;

        [DataMember(Name = "member_type", EmitDefaultValue = false)]
        public string member_type
        {
            get { return m_member_type; }
            set { m_member_type = value; }
        }
        private string m_member_type;

        [DataMember(Name = "date_of_birth", EmitDefaultValue = false)]
        public string date_of_birth
        {
            get { return m_date_of_birth; }
            set { m_date_of_birth = value; }
        }
        private string m_date_of_birth;

        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string member_first_name
        {
            get { return m_member_first_name; }
            set { m_member_first_name = value; }
        }
        private string m_member_first_name;

        [DataMember(Name = "member_password", EmitDefaultValue = false)]
        public string member_password
        {
            get { return m_member_password; }
            set { m_member_password = value; }
        }
        private string m_member_password;

        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string member_last_name
        {
            get { return m_member_last_name; }
            set { m_member_last_name = value; }
        }
        private string m_member_last_name;
    }
}
