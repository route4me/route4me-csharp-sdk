using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The data structure of a retrieved driver review.
    /// </summary>
    [DataContract]
    public sealed class DriverReview : GenericParameters
    {
        /// <summary>
        ///     Driver Rating ID
        /// </summary>
        [DataMember(Name = "rating_id")]
        public string RatingId { get; set; }

        /// <summary>
        ///     The tracking number of the route destination
        /// </summary>
        [DataMember(Name = "tracking_number")]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     A review the driver got
        /// </summary>
        [DataMember(Name = "review")]
        public string Review { get; set; }

        /// <summary>
        ///     The rating assigned to the driver.
        ///     Available values: 1,2,3,4
        /// </summary>
        [DataMember(Name = "rating")]
        [Range(1, 4)]
        public double? Rating { get; set; }

        /// <summary>
        ///     When the review was created.
        /// </summary>
        [DataMember(Name = "added_at")]
        public string AddedAt { get; set; }
    }

    /// <summary>
    ///     The data structure of a retrieved driver reviews list.
    /// </summary>
    public sealed class DriverReviewsResponse
    {
        /// <summary>
        ///     An array of the driver reviews.
        /// </summary>
        [DataMember(Name = "data")]
        public DriverReview[] Data { get; set; }

        /// <summary>
        ///     The response pagination info.
        /// </summary>
        [DataMember(Name = "simple_pagination")]
        public SimplePaginationData SimplePagination { get; set; }

        /// <summary>
        ///     Statistics by driver rating.
        /// </summary>
        [DataMember(Name = "total")]
        public TypeQuantity[] Total { get; set; }
    }

    /// <summary>
    ///     Data structure of the response pagination info.
    /// </summary>
    public sealed class SimplePaginationData
    {
        /// <summary>
        ///     Driver reviews number per page.
        /// </summary>
        [DataMember(Name = "per_page")]
        public int? PerPage { get; set; }

        /// <summary>
        ///     Current page number in the driver reviews collection.
        /// </summary>
        [DataMember(Name = "current_page")]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     Path to the driver review addon.
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "first")] public string First { get; set; }

        [DataMember(Name = "prev")] public int? Previous { get; set; }

        [DataMember(Name = "next")] public int? Next { get; set; }
    }

    /// <summary>
    ///     Driver rating quantity by types.
    /// </summary>
    public sealed class TypeQuantity
    {
        [DataMember(Name = "type")] public int Type { get; set; }

        [DataMember(Name = "quantity")] public int Quantity { get; set; }
    }
}