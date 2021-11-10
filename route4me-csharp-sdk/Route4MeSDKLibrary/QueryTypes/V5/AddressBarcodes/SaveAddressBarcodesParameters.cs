using System.Runtime.Serialization;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Parameters for the add address barcodes request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class SaveAddressBarcodesParameters : GenericParameters
    {
        /// <summary>
        ///     The route ID.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int RouteDestinationId { get; set; }

        /// <summary>
        ///     Barcodes to be saved
        /// </summary>
        [DataMember(Name = "barcodes", EmitDefaultValue = false)]
        public BarcodeDataRequest[] Barcodes { get; set; }
    }
}