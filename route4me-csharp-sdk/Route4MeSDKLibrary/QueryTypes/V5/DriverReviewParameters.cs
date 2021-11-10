using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    public sealed class DriverReviewParameters : GenericParameters
    {
        /// <summary>
        ///     The driver rating to search for.
        ///     Available values: 1,2,3,4
        /// </summary>
        [HttpQueryMemberAttribute(Name = "rating", EmitDefaultValue = false)]
        public int? Rating { get; set; }

        /// <summary>
        ///     The driver ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Start of the time filter. (e.g. "2020-11-26")
        /// </summary>
        [HttpQueryMemberAttribute(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        /// <summary>
        ///     End of the time filter. (e.g. "2020-11-29")
        /// </summary>
        [HttpQueryMemberAttribute(Name = "end", EmitDefaultValue = false)]
        public string End { get; set; }

        /// <summary>
        ///     A page number of the driver reviews collection to retrieve.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        ///     Diver reviews per page.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        ///     Rating ID
        /// </summary>
        [DataMember(Name = "rating_id")]
        public string RatingId { get; set; }
    }
}