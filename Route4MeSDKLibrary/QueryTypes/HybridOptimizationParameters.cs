using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for generating hybrid optimization.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class HybridOptimizationParameters : GenericParameters
    {
        /// <summary>
        /// Target date for a hybrid optimization.
        /// <remarks><para>Date format: yyyy-MM-dd</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "target_date_string", EmitDefaultValue = false)]
        public string TargetDateString { get; set; }

        /// <summary>
        /// Local timezone offset in minutes.
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "timezone_offset_minutes", EmitDefaultValue = false)]
        public int TimezoneOffsetMinutes { get; set; }

    }
}
