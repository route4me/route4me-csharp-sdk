﻿namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Helper class, for setting GPS data
    /// Used to create the suitable query string
    /// See example in Route4MeExamples.SetGPSPosition()
    /// </summary>
    public sealed class GPSParameters : GenericParameters
    {
        /// <summary>
        /// Response format.
        /// <para>Available values: <value>json, xml</value></para>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "format")]
        public string Format { get; set; }

        /// <summary>
        /// Unique ID of a member.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id")]
        public int MemberId { get; set; }

        /// <summary>
        /// Unique ID of a route the device assigned to.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_id")]
        public string RouteId { get; set; }

        /// <summary>
        /// Unique ID of a GPS points group.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "tx_id")]
        public string TxId { get; set; }

        /// <summary>
        /// Unique ID of a vehicle.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_id")]
        public int VehicleId { get; set; }

        /// <summary>
        /// The direction in degrees in which the vehicle is heading
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "course")]
        public int Course { get; set; }

        /// <summary>
        /// Vehicle speed.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Latitude of a device position.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of a device position.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// If true, returns a response with a last known position of a device.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "last_position")]
        public bool LastPosition { get; set; }

        /// <summary>
        /// If equal to 'custom' a time filter will work.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "time_period")]
        public string TimePeriod { get; set; }

        /// <summary>
        /// Start of a time filter.
        /// <remarks><para>Time format: UNIX timestamp.</para></remarks>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "start_date")]
        public long StartDate { get; set; }

        /// <summary>
        /// End of a time filter.
        /// <remarks><para>Time format: UNIX timestamp.</para></remarks>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "end_date")]
        public long EndDate { get; set; }

        /// <summary>
        /// Altitude of a device position.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "altitude", EmitDefaultValue = false)]
        public double Altitude { get; set; }

        /// <summary>
        /// Device type.
        /// <para>Available values: </para>
        /// <value>'web', 'iphone', 'ipad', 'android phone', 'android tablet' etc</value>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_type")]
        public string DeviceType { get; set; }

        /// <summary>
        /// Globally unique identifier of a device.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_guid")]
        public string DeviceGuid { get; set; }

        /// <summary>
        /// The timestamp on the local (remote relative to the server) device.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_timestamp", EmitDefaultValue = false)]
        public string DeviceTimestamp { get; set; }

        /// <summary>
        /// The version of the app submitting the data.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "app_version", EmitDefaultValue = false)]
        public string AppVersion { get; set; }
    }
}
