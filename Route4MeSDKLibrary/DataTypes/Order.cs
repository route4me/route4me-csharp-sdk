using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The order data structure
    /// </summary>
    [DataContract]
    public sealed class Order : GenericParameters
    {

        /// <summary>
        /// Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? order_id { get; set; }

        /// <summary>
        /// Address 1 field. Required
        /// </summary>
        [DataMember(Name = "address_1")]
        public string address_1 { get; set; }

        /// <summary>
        /// Address 2 field
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string address_2 { get; set; }

        /// <summary>
        /// Geo latitude. Required
        /// </summary>
        [DataMember(Name = "cached_lat")]
        public double cached_lat { get; set; }

        /// <summary>
        /// Geo longitude. Required
        /// </summary>
        [DataMember(Name = "cached_lng")]
        public double cached_lng { get; set; }

        /// <summary>
        /// Generate optimal routes and driving directions to this curbside latitude
        /// </summary>
        [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
        public double? curbside_lat { get; set; }

        /// <summary>
        /// Generate optimal routes and driving directions to the curbside langitude
        /// </summary>
        [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
        public double? curbside_lng { get; set; }

        /// <summary>
        /// Scheduled day
        /// </summary>
        [DataMember(Name = "day_scheduled_for_YYMMDD", EmitDefaultValue = false)]
        public string day_scheduled_for_YYMMDD { get; set; }

        /// <summary>
        /// Address Alias. Required
        /// </summary>
        [DataMember(Name = "address_alias")]
        public string address_alias { get; set; }

        /// <summary>
        /// Local time window start
        /// </summary>
        [DataMember(Name = "local_time_window_start", EmitDefaultValue = false)]
        public int? local_time_window_start { get; set; }

        /// <summary>
        /// Local time window end
        /// </summary>
        [DataMember(Name = "local_time_window_end", EmitDefaultValue = false)]
        public int? local_time_window_end { get; set; }

        /// <summary>
        /// Second Local time window start
        /// </summary>
        [DataMember(Name = "local_time_window_start_2", EmitDefaultValue = false)]
        public int? local_time_window_start_2 { get; set; }

        /// <summary>
        /// Second local time window end
        /// </summary>
        [DataMember(Name = "local_time_window_end_2", EmitDefaultValue = false)]
        public int? local_time_window_end_2 { get; set; }

        /// <summary>
        /// Service time
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public int? service_time { get; set; }

        /// <summary>
        /// Address City
        /// </summary>
        [DataMember(Name = "address_city", EmitDefaultValue = false)]
        public string address_city { get; set; }

        /// <summary>
        /// Address state ID
        /// </summary>
        [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
        public string address_state_id { get; set; }

        /// <summary>
        /// Address country ID
        /// </summary>
        [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
        public string address_country_id { get; set; }

        /// <summary>
        /// Address ZIP code
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string address_zip { get; set; }

        /// <summary>
        /// Order status ID
        /// </summary>
        [DataMember(Name = "order_status_id", EmitDefaultValue = false)]
        public int order_status_id { get; set; }

        /// <summary>
        /// The id of the member inside the route4me system
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int member_id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [DataMember(Name = "EXT_FIELD_first_name", EmitDefaultValue = false)]
        public string EXT_FIELD_first_name { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "EXT_FIELD_last_name", EmitDefaultValue = false)]
        public string EXT_FIELD_last_name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember(Name = "EXT_FIELD_email", EmitDefaultValue = false)]
        public string EXT_FIELD_email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [DataMember(Name = "EXT_FIELD_phone", EmitDefaultValue = false)]
        public string EXT_FIELD_phone { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        [DataMember(Name = "EXT_FIELD_custom_data", EmitDefaultValue = false)]
        public object EXT_FIELD_custom_data
        {
            get { return _ext_field_custom_data; }
            set
            {
                if (value.GetType().ToString() == "System.Collections.Generic.Dictionary") _ext_field_custom_data = value; else _ext_field_custom_data = null;
            }
        }
        private object _ext_field_custom_data;

        /// <summary>
        /// Local timezone string
        /// </summary>
        [DataMember(Name = "local_timezone_string", EmitDefaultValue = false)]
        public string local_timezone_string { get; set; }

        /// <summary>
        /// Order icon
        /// </summary>
        [DataMember(Name = "order_icon", EmitDefaultValue = false)]
        public string order_icon { get; set; }

    }
}
