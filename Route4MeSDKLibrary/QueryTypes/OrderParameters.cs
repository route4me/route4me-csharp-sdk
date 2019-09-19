namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parametters for the order(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class OrderParameters : GenericParameters
    {
        /// <summary>
        /// Limit per page, if you use 0 you will get all records.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Only records from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        /// <summary>
        /// If a query is an array, the search engine will search by fields, 
        /// if a query is a string - will search by all text fields. Array / string.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        /// Use it for get specific fields. String / coma separated.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
        public string Fields { get; set; }

        /// <summary>
        /// filter routed/unrouted. enum(all,routed,unrouted).
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }

        /// <summary>
        /// Unque ID of an order.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "order_id", EmitDefaultValue = false)]
        public string order_id { get; set; }

        /// <summary>
        /// Date an order was inserted.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "day_added_YYMMDD", EmitDefaultValue = false)]
        public string DayAddedYYMMDD { get; set; }

        /// <summary>
        /// Date an order was scheduled for.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "scheduled_for_YYMMDD", EmitDefaultValue = false)]
        public string ScheduledForYYMMDD { get; set; }
    }
}
