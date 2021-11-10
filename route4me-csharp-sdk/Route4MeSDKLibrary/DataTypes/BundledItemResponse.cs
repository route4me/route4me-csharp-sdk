using System.ComponentModel;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Bundled item data structure
    /// </summary>
    [DataContract]
    public class BundledItemResponse
    {
        /// <summary>
        ///     Summary cube value of the bundled addresses
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public double Cube { get; set; }

        /// <summary>
        ///     Summary revenue value of the bundled addresses
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public double Revenue { get; set; }

        /// <summary>
        ///     Summary pieces value of the bundled addresses
        /// </summary>
        [DataMember(Name = "pieces", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public int Pieces { get; set; }

        /// <summary>
        ///     Summary weight value of the bundled addresses
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public double Weight { get; set; }

        /// <summary>
        ///     Summary cost value of the bundled addresses
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = true)]
        [DefaultValue(0)]
        [ReadOnly(true)]
        public double Cost { get; set; }

        /// <summary>
        ///     Service time of the bundled addresses
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? ServiceTime { get; set; }

        /// <summary>
        ///     Time window start of the bundled addresses
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        ///     Time window emd of the bundled addresses
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        ///     TO DO: Adjust description
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public object CustomData { get; set; }

        /// <summary>
        ///     Array of the IDs of the bundeld addresses.
        /// </summary>
        [DataMember(Name = "addresses_id", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int[] AddressesId { get; set; }
    }
}