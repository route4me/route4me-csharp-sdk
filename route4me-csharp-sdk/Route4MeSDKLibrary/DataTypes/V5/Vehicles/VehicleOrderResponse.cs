using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from execution of a vehicle order.
    /// </summary>
    public sealed class VehicleOrderResponse : GenericParameters
    {
        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Vehicle order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? OrderId { get; set; }
    }
}