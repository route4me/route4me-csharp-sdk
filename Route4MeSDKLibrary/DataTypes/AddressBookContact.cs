using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Address book contact class. 
    /// <para>See the JSON schema at <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Addressbook_v4.dtd">link</see> </para>
    /// <para>Note: 'contact' means 'address book contact' and 'address' means 'geographic address of the contact'.</para>
    /// </summary>
    [DataContract]
    public sealed class AddressBookContact : GenericParameters
    {
        /// <summary>
        /// A territory shape name the contact belongs.
        /// </summary>
        [DataMember(Name = "territory_name", EmitDefaultValue = false)]
        public string territory_name { get; set; }

        /// <summary>
        /// Time when the contact was created.
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public int created_timestamp { get; set; }

        /// <summary>
        /// Unique ID of the contact.
        /// </summary>
        [DataMember(Name = "address_id", EmitDefaultValue = false)]
        public int? address_id { get; set; }

        /// <summary>
        /// The geographic address of the contact.
        /// </summary>
        [DataMember(Name = "address_1")]
        public string address_1 { get; set; }

        /// <summary>
        /// Second geographic address of the contact.
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string address_2 { get; set; }

        /// <summary>
        /// The contact's alias.
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string address_alias { get; set; }

        /// <summary>
        /// A group the contact belongs.
        /// </summary>
        [DataMember(Name = "address_group", EmitDefaultValue = false)]
        public string address_group { get; set; }

        /// <summary>
        /// The first name of the contact person.
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string first_name { get; set; }

        /// <summary>
        /// The last name of the contact person.
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string last_name { get; set; }

        /// <summary>
        /// Start of the contact's local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_start", EmitDefaultValue = false)]
        public int? local_time_window_start { get; set; }

        /// <summary>
        /// End of the contact's local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_end", EmitDefaultValue = false)]
        public int? local_time_window_end { get; set; }

        /// <summary>
        /// Start of the contact's second local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_start_2", EmitDefaultValue = false)]
        public int? local_time_window_start_2 { get; set; }

        /// <summary>
        /// End of the contact's second local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_end_2", EmitDefaultValue = false)]
        public int? local_time_window_end_2 { get; set; }

        /// <summary>
        /// The contact's email.
        /// </summary>
        [DataMember(Name = "address_email", EmitDefaultValue = false)]
        public string address_email { get; set; }

        /// <summary>
        /// The contact's phone number.
        /// </summary>
        [DataMember(Name = "address_phone_number", EmitDefaultValue = false)]
        public string address_phone_number { get; set; }

        /// <summary>
        /// A latitude of the contact's cached position.
        /// </summary>
        [DataMember(Name = "cached_lat")]
        public double cached_lat { get; set; }

        /// <summary>
        /// A longitude of the contact's cached position.
        /// </summary>
        [DataMember(Name = "cached_lng")]
        public double cached_lng { get; set; }

        /// <summary>
        /// A latitude of the contact's curbside.
        /// </summary>
        [DataMember(Name = "curbside_lat")]
        public double? curbside_lat { get; set; }

        /// <summary>
        /// A longitude of the contact's curbside.
        /// </summary>
        [DataMember(Name = "curbside_lng")]
        public double? curbside_lng { get; set; }
        
        /// <summary>
        /// A city the contact belongs.
        /// </summary>
        [DataMember(Name = "address_city", EmitDefaultValue = false)]
        public string address_city { get; set; }

        /// <summary>
        /// The ID of the state the contact belongs.
        /// </summary>
        [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
        public string address_state_id { get; set; }

        /// <summary>
        /// The ID of the country the contact belongs.
        /// </summary>
        [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
        public string address_country_id { get; set; }

        /// <summary>
        /// The contact's ZIP code.
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string address_zip { get; set; }

        /// <summary>
        /// An array of the contact's custom field-value pairs.
        /// </summary>
        [DataMember(Name = "address_custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> address_custom_data
        {
            get
            {
                if (_address_custom_data == null)
                {
                    return null;
                }
                else
                {
                    var v1 = (Dictionary<string, string>)_address_custom_data;

                    Dictionary<string, string> v2 = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> kv1 in v1)
                    {
                        if (kv1.Key != null)
                        {
                            if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value.ToString()); else v2.Add(kv1.Key, "");
                        }
                        else continue;
                    }

                    return v2;
                }
            }
            set
            {
                if (value == null)
                {
                    _address_custom_data = null;
                }
                else
                {
                    var v1 = (Dictionary<string, string>)value;
                    Dictionary<string, string> v2 = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> kv1 in v1)
                    {
                        if (kv1.Key != null)
                        {
                            if (kv1.Value != null) v2.Add(kv1.Key, kv1.Value.ToString()); else v2.Add(kv1.Key, "");
                        }
                        else continue;
                    }
                    _address_custom_data = v2;
                }
            }
        }
        private Dictionary<string, string> _address_custom_data;

        /// <summary>
        /// An array of the contact's schedules.
        /// </summary>
        [DataMember(Name = "schedule", EmitDefaultValue = false)]
        public IList<Schedule> schedule { get; set; }

        /// <summary>
        /// The list of dates that should be omitted from the schedules.
        /// </summary>
        [DataMember(Name = "schedule_blacklist", EmitDefaultValue = false)]
        public string[] schedule_blacklist { get; set; }

        /// <summary>
        /// The service time at the contact's address.
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public int? service_time { get; set; }

        /// <summary>
        /// The contact's local timezone.
        /// </summary>
        [DataMember(Name = "local_timezone_string", EmitDefaultValue = false)]
        public string local_timezone_string { get; set; }

        /// <summary>
        /// The contact's color on the map.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string color { get; set; }

        /// <summary>
        /// The contact's icon on the map.
        /// </summary>
        [DataMember(Name = "address_icon", EmitDefaultValue = false)]
        public string address_icon { get; set; }

        /// <summary>
        /// The contact's stop type.
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        /// The cubic volume of the contact's cargo.
        /// </summary>
        [DataMember(Name = "address_cube", EmitDefaultValue = false)]
        public object AddressCube { get; set; }

        /// <summary>
        /// The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        /// </summary>
        [DataMember(Name = "address_pieces", EmitDefaultValue = false)]
        public object AddressPieces { get; set; }

        /// <summary>
        /// The reference number of the address.
        /// </summary>
        [DataMember(Name = "address_reference_no", EmitDefaultValue = false)]
        public object AddressReferenceNo { get; set; }

        /// <summary>
        /// The revenue from the contact.
        /// </summary>
        [DataMember(Name = "address_revenue", EmitDefaultValue = false)]
        public object AddressRevenue { get; set; }

        /// <summary>
        /// The weight of the contact's cargo.
        /// </summary>
        [DataMember(Name = "address_weight", EmitDefaultValue = false)]
        public object AddressWeight { get; set; }

        /// <summary>
        /// If present, the priority will sequence addresses in all the optimal routes so that
        /// higher priority addresses are general at the beginning of the route sequence.
        /// 1 is the highest priority, 100000 is the lowest.
        /// </summary>
        [DataMember(Name = "address_priority", EmitDefaultValue = false)]
        public int? AddressPriority { get; set; }

        /// <summary>
        /// The customer purchase order of the contact.
        /// </summary>
        [DataMember(Name = "address_customer_po", EmitDefaultValue = false)]
        public object AddressCustomerPo { get; set; }
    }
}
