using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    [DataContract]
    public sealed class OrderFilterParameters : GenericParameters
    {
        /// <summary>
        /// filter for the orders
        /// </summary>
        [DataMember(Name = "filter")]
        public FilterDetails Filter { get; set; }
    }

    [DataContract]
    public class FilterDetails : GenericParameters
    {
        /// <summary>
        /// Available values: "all", "routed", "unrouted"
        /// </summary>
        [DataMember(Name = "display")]
        public string Display { get; set; }

        /// <summary>
        /// Start and end dates for filter the orders by schedule.
        /// e.g. ["2019-06-01", "2019-06-18"]
        /// </summary>
        [DataMember(Name = "scheduled_for_YYMMDD")]
        public string[] Scheduled_for_YYMMDD { get; set; }

        /// <summary>
        /// Limit per page, if you use 0 you will get all records
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }
    }
}
