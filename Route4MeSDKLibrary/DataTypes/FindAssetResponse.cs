using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the asset finding request
    /// </summary>
    [DataContract]
    public sealed class FindAssetResponse
    {
        /// <summary>
        /// Tracking number
        /// </summary>
        [DataMember(Name = "tracking_number")]
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Asset status history
        /// </summary>
        [DataMember(Name = "status_history")]
        public string[] StatusHistory { get; set; }

        /// <summary>
        /// An array of the asset locations. See <see cref="FindAssetResponseLocations"/>
        /// </summary>
        [DataMember(Name = "locations")]
        public FindAssetResponseLocations[] Locations { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomData { get; set; }

        /// <summary>
        /// Arrival time of the asset. See <see cref="FindAssetResponseArrival"/>
        /// </summary>
        [DataMember(Name = "arrival")]
        public FindAssetResponseArrival[] Arrival { get; set; }

        /// <summary>
        /// True if the asset was delivered
        /// </summary>
        [DataMember(Name = "delivered", EmitDefaultValue = false)]
        public System.Nullable<bool> Delivered { get; set; }
    }

    /// <summary>
    /// The subclass of the FindAssetResponse class. See <see cref="FindAssetResponse"/>
    /// </summary>
    [DataContract()]
    public sealed class FindAssetResponseLocations
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// The asset's location icon
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }

    /// <summary>
    /// The subclass of the FindAssetResponse class. See <see cref="FindAssetResponse"/>
    /// </summary>
    [DataContract()]
    public sealed class FindAssetResponseArrival
    {
        /// <summary>
        /// Start of the arrival time
        /// </summary>
        [DataMember(Name = "from_unix_timestamp")]
        public System.Nullable<int> FromUnixTimestamp { get; set; }

        /// <summary>
        /// End of the arrival time
        /// </summary>
        [DataMember(Name = "to_unix_timestamp")]
        public System.Nullable<int> ToUnixTimestamp { get; set; }
    }

}
