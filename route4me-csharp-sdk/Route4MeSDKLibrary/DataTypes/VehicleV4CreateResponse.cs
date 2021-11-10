using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the create vehicle request. See also <seealso cref="VehicleV4Response" />.
    /// </summary>
    [DataContract]
    public sealed class VehicleV4CreateResponse : GenericParameters
    {
        /// <summary>
        ///     Status of the request process.
        /// </summary>
        /// <value>
        ///     <c>true</c> if request finished successfully; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool status { get; set; }

        /// <summary>
        ///     The vehicle Guid.
        /// </summary>
        [DataMember(Name = "vehicle_guid", EmitDefaultValue = false)]
        public string VehicleGuid { get; set; }

        /// <summary>
        ///     <c>true</c> True, if the vehicle is new.
        /// </summary>
        [DataMember(Name = "new", EmitDefaultValue = false)]
        public bool New { get; set; }
    }
}