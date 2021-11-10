using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class VehicleParameters : GenericParameters
    {
        /// <summary>
        ///     If true, returned vehicles array will be paginated.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "with_pagination", EmitDefaultValue = false)]
        public bool WithPagination { get; set; }

        /// <summary>
        ///     Current page number in the vehicles collection.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        ///     Returned vehicles number per page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        ///     Unique ID of a Vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }
    }
}