using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// The class for the request/response data structure to/from the endpoint vehicles/assign.
    /// </summary>
    public sealed class VehicleTemporary : QueryTypes.GenericParameters
    {
        /// <summary>
        /// The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// A license plate of the vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        /// Member ID assigned to the temporary vehicle.
        /// </summary>
        [DataMember(Name = "assigned_member_id", EmitDefaultValue = false)]
        public string AssignedMemberId { get; set; }

        /// <summary>
        /// An expiration date of the temporary vehicle.
        /// </summary>
        [DataMember(Name = "expires_at", EmitDefaultValue = false)]
        public string ExpiresAt { get; set; }

        /// <summary>
        /// If true, an assignment forced.
        /// </summary>
        [DataMember(Name = "force-assignment", EmitDefaultValue = false)]
        public bool? ForceAssignment { get; set; }
    }
}
