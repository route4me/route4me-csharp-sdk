using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    public sealed class VehicleParameters : GenericParameters
    {
        /// <summary>
        ///     If true, returned vehicles array will be paginated
        /// </summary>
        [HttpQueryMemberAttribute(Name = "with_pagination", EmitDefaultValue = false)]
        public bool WithPagination { get; set; }


        /// <summary>
        ///     Current page number in the vehicles collection
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }


        /// <summary>
        ///     Returned vehicles number per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        ///     An array of the Vehicle IDs.
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public string[] VehicleIDs { get; set; }

        /// <summary>
        ///     Vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Vehicle license plate
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }
    }
}