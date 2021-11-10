using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the asset finding request
    /// </summary>
    [DataContract]
    public sealed class FindAssetResponse
    {
        /// <summary>
        ///     Tracking number
        /// </summary>
        [DataMember(Name = "tracking_number")]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     A link to a large logo
        /// </summary>
        [DataMember(Name = "large_logo_uri")]
        public string LargeLogoUri { get; set; }

        /// <summary>
        ///     A link to a large logo (2x)
        /// </summary>
        [DataMember(Name = "large_logo_uri_2x")]
        public string LargeLogoUri2x { get; set; }

        /// <summary>
        ///     A link to a mobile logo
        /// </summary>
        [DataMember(Name = "mobile_logo_uri")]
        public string MobileLogoUri { get; set; }

        /// <summary>
        ///     A link to a mobile logo (2x)
        /// </summary>
        [DataMember(Name = "mobile_logo_uri_2x")]
        public string MobileLogoUri2x { get; set; }

        /// <summary>
        ///     The asset color on a map
        /// </summary>
        [DataMember(Name = "map_color")]
        public string MapColor { get; set; }

        /// <summary>
        ///     An alignment of a large logo
        /// </summary>
        [DataMember(Name = "large_logo_alignment")]
        public string LargeLogoAlignment { get; set; }

        /// <summary>
        ///     An alignment of a mobile logo
        /// </summary>
        [DataMember(Name = "mobile_logo_alignment")]
        public string MobileLogoAlignment { get; set; }

        /// <summary>
        ///     Show map zoom controls
        /// </summary>
        [DataMember(Name = "show_map_zoom_controls")]
        public bool? ShowMapZoomControls { get; set; }

        /// <summary>
        ///     Driver phone number
        /// </summary>
        [DataMember(Name = "driver_phone")]
        public string DriverPhone { get; set; }

        /// <summary>
        ///     True if the route started
        /// </summary>
        [DataMember(Name = "route_started")]
        public bool? RouteStarted { get; set; }

        /// <summary>
        ///     Customer service phone
        /// </summary>
        [DataMember(Name = "customer_service_phone")]
        public string CustomerServicePhone { get; set; }

        /// <summary>
        ///     If true, Covid19 warning hidden
        /// </summary>
        [DataMember(Name = "hide_covid19_warning")]
        public string HideCovid19Warning { get; set; }

        /// <summary>
        ///     Driver name
        /// </summary>
        [DataMember(Name = "driver_name")]
        public string DriverName { get; set; }

        /// <summary>
        ///     A link to a driver picture file
        /// </summary>
        [DataMember(Name = "driver_picture")]
        public string DriverPicture { get; set; }

        /// <summary>
        ///     A subheadline of a tracking page
        /// </summary>
        [DataMember(Name = "tracking_page_subheadline")]
        public string TrackingPageSubheadline { get; set; }

        /// <summary>
        ///     A first destination address
        /// </summary>
        [DataMember(Name = "destination_address_1")]
        public string DestinationAddress1 { get; set; }

        /// <summary>
        ///     A second destination address
        /// </summary>
        [DataMember(Name = "destination_address_2")]
        public string DestinationAddress2 { get; set; }

        /// <summary>
        ///     Asset status history
        /// </summary>
        [DataMember(Name = "status_history")]
        public AssetStatusHistory[] StatusHistory { get; set; }

        /// <summary>
        ///     An array of the asset locations. See <see cref="FindAssetResponseLocations" />
        /// </summary>
        [DataMember(Name = "locations")]
        public FindAssetResponseLocations[] Locations { get; set; }

        /// <summary>
        ///     Custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomData { get; set; }

        /// <summary>
        ///     Arrival time of the asset. See <see cref="FindAssetResponseArrival" />
        /// </summary>
        [DataMember(Name = "arrival")]
        public FindAssetResponseArrival[] Arrival { get; set; }

        /// <summary>
        ///     True if the asset was delivered
        /// </summary>
        [DataMember(Name = "delivered", EmitDefaultValue = false)]
        public bool? Delivered { get; set; }

        /// <summary>
        ///     UNIX timestamp when a geofence visited event was triggered.
        /// </summary>
        [DataMember(Name = "timestamp_geofence_visited")]
        public long? TimestampGeofenceVisited { get; set; }

        /// <summary>
        ///     UNIX timestamp of a last visited event.
        /// </summary>
        [DataMember(Name = "timestamp_last_visited")]
        public long? TimestampLastVisited { get; set; }
    }

    /// <summary>
    ///     The subclass of the FindAssetResponse class. See <see cref="FindAssetResponse" />
    /// </summary>
    [DataContract]
    public sealed class FindAssetResponseLocations
    {
        /// <summary>
        ///     If true, current location is destination.
        /// </summary>
        [DataMember(Name = "is_destination")]
        public bool? IsDestination { get; set; }

        /// <summary>
        ///     Latitude.
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     Longitude.
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        ///     The asset's location icon.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Size of the icon.
        /// </summary>
        [DataMember(Name = "size")]
        public int? Size { get; set; }

        /// <summary>
        ///     A icon's acnhor position.
        /// </summary>
        [DataMember(Name = "anchor")]
        public int[] Anchor { get; set; }

        /// <summary>
        ///     Popup position of an icon.
        /// </summary>
        [DataMember(Name = "popupAnchor")]
        public int[] PopupAnchor { get; set; }

        /// <summary>
        ///     Rotation angle.
        /// </summary>
        [DataMember(Name = "angle")]
        public int? Angle { get; set; }

        /// <summary>
        ///     Information about a shipped package at a specified location.
        /// </summary>
        [DataMember(Name = "info")]
        public string Info { get; set; }
    }

    /// <summary>
    ///     The subclass of the FindAssetResponse class. See <see cref="FindAssetResponse" />
    /// </summary>
    [DataContract]
    public sealed class FindAssetResponseArrival
    {
        /// <summary>
        ///     Start of the arrival time
        /// </summary>
        [DataMember(Name = "from_unix_timestamp")]
        public long? FromUnixTimestamp { get; set; }

        /// <summary>
        ///     End of the arrival time
        /// </summary>
        [DataMember(Name = "to_unix_timestamp")]
        public long? ToUnixTimestamp { get; set; }
    }

    /// <summary>
    ///     The subclass of the FindAssetResponse class. See <see cref="FindAssetResponse" />
    /// </summary>
    [DataContract]
    public sealed class AssetStatusHistory
    {
        /// <summary>
        ///     Status getting timestamp
        /// </summary>
        [DataMember(Name = "unix_timestamp")]
        public long? UnixTimestamp { get; set; }

        /// <summary>
        ///     nformation about a shipped package.
        ///     enum: ["Order Received", "Order Assigned to Route", "Packing", "Loaded to Vehicle", "Out for Delivery"]
        /// </summary>
        [DataMember(Name = "info")]
        public string Info { get; set; }
    }
}