
namespace Route4MeSDK.QueryTypes
{
  public sealed class OrderParameters : GenericParameters
  {

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

    /// <summary>
    /// if query is array search engine will search by fields, if query is string will search by all text fields. Array / string.
    /// </summary>
    [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
    public string Query { get; set; }

    /// <summary>
    /// Use it for get specific fields. String / coma separated
    /// </summary>
    [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
    public string Fields { get; set; }

    /// <summary>
    /// filter routed/unrouted. enum(all,routed,unrouted)
    /// </summary>
    [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
    public string Display { get; set; }

    /// <summary>
    /// Order ID.
    /// </summary>
    [HttpQueryMemberAttribute(Name = "order_id", EmitDefaultValue = false)]
    public string OrderId
    {
        get { return m_OrderId; }
        set { m_OrderId = value; }
    }
    private string m_OrderId;

    /// <summary>
    /// Date an order was inserted
    /// </summary>
    [HttpQueryMemberAttribute(Name = "day_added_YYMMDD", EmitDefaultValue = false)]
    public string DayAddedYYMMDD
    {
        get { return m_DayAddedYYMMDD; }
        set { m_DayAddedYYMMDD = value; }
    }
    private string m_DayAddedYYMMDD;

    /// <summary>
    /// Date an order was scheduled for
    /// </summary>
    [HttpQueryMemberAttribute(Name = "scheduled_for_YYMMDD", EmitDefaultValue = false)]
    public string ScheduledForYYMMDD
    {
        get { return m_ScheduledForYYMMDD; }
        set { m_ScheduledForYYMMDD = value; }
    }
    private string m_ScheduledForYYMMDD;
  }
}
