using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Address book contact class.
    ///     <para>
    ///         See the JSON schema at
    ///         <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Addressbook_v4.dtd">link</see>
    ///     </para>
    ///     <para>Note: 'contact' means 'address book contact' and 'address' means 'geographic address of the contact'.</para>
    /// </summary>
    [DataContract]
    public sealed class AddressBookContact : GenericParameters
    {
        private object _address_custom_data;

        /// <summary>
        ///     A territory shape name the contact belongs.
        /// </summary>
        [DataMember(Name = "territory_name", EmitDefaultValue = false)]
        public string TerritoryName { get; set; }

        /// <summary>
        ///     Time when the contact was created.
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long CreatedTimestamp { get; set; }

        /// <summary>
        ///     Unique ID of the contact.
        /// </summary>
        [DataMember(Name = "address_id", EmitDefaultValue = false)]
        public int? AddressId { get; set; }

        /// <summary>
        ///     The geographic address of the contact.
        /// </summary>
        [DataMember(Name = "address_1")]
        public string Address1 { get; set; }

        /// <summary>
        ///     Second geographic address of the contact.
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string Address2 { get; set; }

        /// <summary>
        ///     Unique ID of the member.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     The contact's alias.
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string AddressAlias { get; set; }

        /// <summary>
        ///     A group the contact belongs.
        /// </summary>
        [DataMember(Name = "address_group", EmitDefaultValue = false)]
        public string AddressGroup { get; set; }

        /// <summary>
        ///     The first name of the contact person.
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name of the contact person.
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        ///     Start of the contact's local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_start", EmitDefaultValue = false)]
        public long? LocalTimeWindowStart { get; set; }

        /// <summary>
        ///     End of the contact's local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_end", EmitDefaultValue = false)]
        public long? LocalTimeWindowEnd { get; set; }

        /// <summary>
        ///     Start of the contact's second local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_start_2", EmitDefaultValue = false)]
        public long? LocalTimeWindowStart2 { get; set; }

        /// <summary>
        ///     End of the contact's second local time window.
        /// </summary>
        [DataMember(Name = "local_time_window_end_2", EmitDefaultValue = false)]
        public long? LocalTimeWindowEnd2 { get; set; }

        /// <summary>
        ///     The contact's email.
        /// </summary>
        [DataMember(Name = "address_email", EmitDefaultValue = false)]
        public string AddressEmail { get; set; }

        /// <summary>
        ///     The contact's phone number.
        /// </summary>
        [DataMember(Name = "address_phone_number", EmitDefaultValue = false)]
        public string AddressPhoneNumber { get; set; }

        /// <summary>
        ///     A latitude of the contact's cached position.
        /// </summary>
        [DataMember(Name = "cached_lat")]
        public double CachedLat { get; set; }

        /// <summary>
        ///     A longitude of the contact's cached position.
        /// </summary>
        [DataMember(Name = "cached_lng")]
        public double CachedLng { get; set; }

        /// <summary>
        ///     A latitude of the contact's curbside.
        /// </summary>
        [DataMember(Name = "curbside_lat")]
        public double? CurbsideLat { get; set; }

        /// <summary>
        ///     A longitude of the contact's curbside.
        /// </summary>
        [DataMember(Name = "curbside_lng")]
        public double? CurbsideLng { get; set; }

        /// <summary>
        ///     A city the contact belongs.
        /// </summary>
        [DataMember(Name = "address_city", EmitDefaultValue = false)]
        public string AddressCity { get; set; }

        /// <summary>
        ///     The ID of the state the contact belongs.
        /// </summary>
        [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
        public string AddressStateId { get; set; }

        /// <summary>
        ///     The ID of the country the contact belongs.
        /// </summary>
        [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
        public string AddressCountryId { get; set; }

        /// <summary>
        ///     The contact's ZIP code.
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string AddressZip { get; set; }

        /// <summary>
        ///     An array of the contact's custom field-value pairs.
        /// </summary>
        [DataMember(Name = "address_custom_data", EmitDefaultValue = false)]
        public object AddressCustomData
        {
            get => _address_custom_data;
            set
            {
                try
                {
                    if (value == null || value.GetType() == typeof(Array))
                    {
                        _address_custom_data = null;
                    }
                    else
                    {
                        if (value.GetType() == typeof(JObject))
                            _address_custom_data = ((JObject) value).ToObject<Dictionary<string, string>>();
                        else if (value.GetType() == typeof(Dictionary<string, string>))
                            if (value == null || value.GetType() != typeof(Array))
                                _address_custom_data = value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     An array of the contact's schedules.
        /// </summary>
        [DataMember(Name = "schedule", EmitDefaultValue = false)]
        public IList<Schedule> Schedule { get; set; }

        /// <summary>
        ///     The list of dates that should be omitted from the schedules.
        /// </summary>
        [DataMember(Name = "schedule_blacklist", EmitDefaultValue = false)]
        public string[] ScheduleBlacklist { get; set; }

        /// <summary>
        ///     Number of the routes containing the contact.
        /// </summary>
        [DataMember(Name = "in_route_count", EmitDefaultValue = false)]
        public int? InRouteCount { get; set; }

        /// <summary>
        ///     Number of the visits to the contact.
        /// </summary>
        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int? VisitedCount { get; set; }

        /// <summary>
        ///     When the contact was last visited.
        /// </summary>
        [DataMember(Name = "last_visited_timestamp", EmitDefaultValue = false)]
        public long? LastVisitedTimestamp { get; set; }

        /// <summary>
        ///     When the contact was last routed.
        /// </summary>
        [DataMember(Name = "last_routed_timestamp", EmitDefaultValue = false)]
        public long? LastRoutedTimestamp { get; set; }

        /// <summary>
        ///     The service time at the contact's address.
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public long? ServiceTime { get; set; }

        /// <summary>
        ///     The contact's local timezone.
        /// </summary>
        [DataMember(Name = "local_timezone_string", EmitDefaultValue = false)]
        public string LocalTimezoneString { get; set; }

        /// <summary>
        ///     The contact's color on the map.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color { get; set; }

        /// <summary>
        ///     The contact's icon on the map.
        /// </summary>
        [DataMember(Name = "address_icon", EmitDefaultValue = false)]
        public string AddressIcon { get; set; }

        /// <summary>
        ///     The contact's stop type.
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        ///     The cubic volume of the contact's cargo.
        /// </summary>
        [DataMember(Name = "address_cube", EmitDefaultValue = false)]
        public double? AddressCube { get; set; }

        /// <summary>
        ///     The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        /// </summary>
        [DataMember(Name = "address_pieces", EmitDefaultValue = false)]
        public int? AddressPieces { get; set; }

        /// <summary>
        ///     The reference number of the address.
        /// </summary>
        [DataMember(Name = "address_reference_no", EmitDefaultValue = false)]
        public string AddressReferenceNo { get; set; }

        /// <summary>
        ///     The revenue from the contact.
        /// </summary>
        [DataMember(Name = "address_revenue", EmitDefaultValue = false)]
        public double? AddressRevenue { get; set; }

        /// <summary>
        ///     The weight of the contact's cargo.
        /// </summary>
        [DataMember(Name = "address_weight", EmitDefaultValue = false)]
        public double? AddressWeight { get; set; }

        /// <summary>
        ///     If present, the priority will sequence addresses in all the optimal routes so that
        ///     higher priority addresses are general at the beginning of the route sequence.
        ///     1 is the highest priority, 100000 is the lowest.
        /// </summary>
        [DataMember(Name = "address_priority", EmitDefaultValue = false)]
        public int? AddressPriority { get; set; }

        /// <summary>
        ///     The customer purchase order of the contact.
        /// </summary>
        [DataMember(Name = "address_customer_po", EmitDefaultValue = false)]
        public string AddressCustomerPo { get; set; }

        /// <summary>
        ///     If true, a location assigned to a route.
        /// </summary>
        [DataMember(Name = "is_assigned", EmitDefaultValue = false)]
        public bool? IsAssigned { get; set; }
    }
}