using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the get address barcodes request
    /// </summary>
    [DataContract]
    public sealed class GetAddressBarcodesResponse : GenericParameters
    {
        /// <summary>
        ///     The status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        ///     Reference to the next page data
        /// </summary>
        [DataMember(Name = "next_page_cursor", EmitDefaultValue = false)]
        public string NextPageData { get; set; }

        /// <summary>
        ///     An array of the <see cref="BarcodeDataResponse" /> type objects
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public BarcodeDataResponse[] Data { get; set; }
    }
}