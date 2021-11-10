using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Barcode data response
    /// </summary>
    [DataContract]
    public class BarcodeDataResponse
    {
        /// <summary>
        ///     The route ID.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int RouteDestinationId { get; set; }

        /// <summary>
        ///     Order id
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? OrderId { get; set; }

        /// <summary>
        ///     Barcode
        /// </summary>
        [DataMember(Name = "barcode", EmitDefaultValue = false)]
        public string Barcode { get; set; }

        /// <summary>
        ///     Scan type
        /// </summary>
        [DataMember(Name = "scan_type", EmitDefaultValue = false)]
        public string ScanType { get; set; }

        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Latitude { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lan", EmitDefaultValue = false)]
        public double Longitude { get; set; }

        /// <summary>
        ///     Date
        /// </summary>
        [DataMember(Name = "timestamp_date", EmitDefaultValue = false)]
        public long TimestampDate { get; set; }

        /// <summary>
        ///     UTC date
        /// </summary>
        [DataMember(Name = "timestamp_utc", EmitDefaultValue = false)]
        public long TimestampUtc { get; set; }

        /// <summary>
        ///     Scanned at
        /// </summary>
        [DataMember(Name = "scanned_at", EmitDefaultValue = false)]
        public string ScannedAt { get; set; }
    }
}