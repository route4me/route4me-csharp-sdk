using System;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for requesting the telematics connection.
    /// </summary>
    public sealed class TelematicsConnectionParameters : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "account_id", EmitDefaultValue = false)]
        public string AccountId { get; set; }

        [HttpQueryMemberAttribute(Name = "username", EmitDefaultValue = false)]
        public string UserName { get; set; }

        [HttpQueryMemberAttribute(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        [HttpQueryMemberAttribute(Name = "host", EmitDefaultValue = false)]
        public string Host { get; set; }

        /// <summary>
        ///     An unique ID of a telematics vendor.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public uint? VendorID { get; set; }

        /// <summary>
        ///     Telematics connection name
        /// </summary>
        [HttpQueryMemberAttribute(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Vehicle tracking interval in seconds
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_position_refresh_rate", EmitDefaultValue = false)]
        public int? VehiclePositionRefreshRate { get; set; }

        /// <summary>
        ///     Connection token
        /// </summary>
        [HttpQueryMemberAttribute(Name = "connection_token", EmitDefaultValue = false)]
        public string ConnectionToken { get; set; }

        /// <summary>
        ///     Connection user ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "user_id", EmitDefaultValue = false)]
        public int? UserId { get; set; }

        /// <summary>
        ///     Connection ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "id", EmitDefaultValue = false)]
        public int? ID { get; set; }

        /// <summary>
        ///     Telemetics connection type <see cref="Enum.TelematicsVendorType" />
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor", EmitDefaultValue = false)]
        public string Vendor { get; set; }

        /// Validate connections credentials.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "validate_remote_credentials", EmitDefaultValue = false)]
        public bool? ValidateRemoteCredentials { get; set; }
    }
}