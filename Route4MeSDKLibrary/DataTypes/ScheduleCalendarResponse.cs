using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Data structure of the schedule calendar response.
    /// </summary>
    [DataContract]
    public sealed class ScheduleCalendarResponse : GenericParameters
    {
        /// <summary>
        /// The address book contact quantity by dates (dates are in the string format, e.g. 2020-10-18).
        /// </summary>
        [DataMember(Name = "address_book", EmitDefaultValue = false)]
        public Dictionary<string, int> AddressBook { get; set; }

        /// <summary>
        /// The order quantity by dates (dates are in the string format, e.g. 2020-10-18).
        /// </summary>
        [DataMember(Name = "orders", EmitDefaultValue = false)]
        public Dictionary<string, int> Orders { get; set; }

        /// <summary>
        /// The order quantity by dates (dates are in the string format, e.g. 2020-10-18).
        /// </summary>
        [DataMember(Name = "routes_count", EmitDefaultValue = false)]
        public Dictionary<string, int> RoutesCount { get; set; }
    }
}
