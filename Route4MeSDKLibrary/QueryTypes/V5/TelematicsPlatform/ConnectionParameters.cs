using System;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// The telematics connection query parameters.
    /// Used for: create, update, get connectin(s).
    /// </summary>
    public sealed class ConnectionParameters : GenericParameters
    {
        /// <summary>
        /// Telemetics connection type <see cref="Enum.TelematicsVendorType" />
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor", EmitDefaultValue = false)]
        public string Vendor { get; set; }

        /// <summary>
        /// Telemetics connection type ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vendor_id", EmitDefaultValue = false)]
        public int? VendorId { get; set; }

        /// <summary>
        /// Telemetics connection name
        /// Required for telematics connection registration.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Telematics connection access host.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "host", EmitDefaultValue = false)]
        public string Host { get; set; }

        /// <summary>
        /// Telematics connection access api_key.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "api_key", EmitDefaultValue = false)]
        public string ApiKey { get; set; }

        /// <summary>
        /// Telematics connection access account_id.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "account_id", EmitDefaultValue = false)]
        public string AccountId { get; set; }

        /// <summary>
        /// Telematics connection access username
        /// </summary>
        [HttpQueryMemberAttribute(Name = "username", EmitDefaultValue = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Telematics connection access password.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        /// Vehicle tracking interval in seconds (default value 60).
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_position_refresh_rate", EmitDefaultValue = false)]
        public int? VehiclePositionRefreshRate { get; set; }

        /// <summary>
        /// Validate connections credentials.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "validate_remote_credentials", EmitDefaultValue = false)]
        public bool? ValidateRemoteCredentials { get; set; }

        /// <summary>
        /// Disable/enable vehicle tracking.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_enabled", EmitDefaultValue = false)]
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// Metadata
        /// </summary>
        [HttpQueryMemberAttribute(Name = "metadata", EmitDefaultValue = false)]
        public string Metadata { get; set; }

        /// <summary>
        /// Telematics connection access token.
        /// Required to show specified connection.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "connection_token", EmitDefaultValue = false)]
        public string ConnectionToken { get; set; }
    }
}
