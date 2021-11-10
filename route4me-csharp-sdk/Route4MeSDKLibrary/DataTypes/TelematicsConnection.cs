using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response structure from the endpoint/connections
    /// </summary>
    [DataContract]
    public sealed class TelematicsConnection : GenericParameters
    {
        /// <summary>
        ///     Telematics connection access account_id
        /// </summary>
        [DataMember(Name = "account_id", EmitDefaultValue = false)]
        public string AccountId { get; set; }

        /// <summary>
        ///     Telematics connection access username
        /// </summary>
        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string UserName { get; set; }

        /// <summary>
        ///     Telematics connection access password
        /// </summary>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        ///     Telematics connection access host
        /// </summary>
        [DataMember(Name = "host", EmitDefaultValue = false)]
        public string Host { get; set; }

        /// <summary>
        ///     Telematics connection access api_key
        /// </summary>
        [DataMember(Name = "api_key", EmitDefaultValue = false)]
        public string ApiKey { get; set; }

        /// <summary>
        ///     Telemetics connection type ID
        /// </summary>
        [DataMember(Name = "vendor_id", EmitDefaultValue = false)]
        public int? VendorId { get; set; }

        /// <summary>
        ///     Telemetics connection name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Vehicle tracking interval in seconds
        /// </summary>
        [DataMember(Name = "vehicle_position_refresh_rate", EmitDefaultValue = false)]
        public int? VehiclePositionRefreshRate { get; set; }

        /// <summary>
        ///     Maximum idle time
        /// </summary>
        [DataMember(Name = "max_idle_time", EmitDefaultValue = false)]
        public int? MaxIdleTime { get; set; }

        /// <summary>
        ///     Disable/enable vehicle tracking
        /// </summary>
        [DataMember(Name = "is_enabled", EmitDefaultValue = false)]
        public bool? IsEnabled { get; set; }

        /// <summary>
        ///     The last timestamp the vehicles reloaded
        /// </summary>
        [DataMember(Name = "last_vehicles_reload", EmitDefaultValue = false)]
        public string LastVehiclesReload { get; set; }

        /// <summary>
        ///     The last timestamp the addresses reloaded
        /// </summary>
        [DataMember(Name = "last_addresses_reload", EmitDefaultValue = false)]
        public string LastAddressesReload { get; set; }

        /// <summary>
        ///     The last timestamp the postions reloaded
        /// </summary>
        [DataMember(Name = "last_position_reload", EmitDefaultValue = false)]
        public string LastPositionReload { get; set; }

        /// <summary>
        ///     Telematics connection access token
        /// </summary>
        [DataMember(Name = "connection_token", EmitDefaultValue = false)]
        public string ConnectionToken { get; set; }

        /// <summary>
        ///     Connection user ID
        /// </summary>
        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public int? UserId { get; set; }

        /// <summary>
        ///     When the connection updated
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     When the connection created
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Telemetics connection ID
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int? Id { get; set; }

        /// <summary>
        ///     Metadata, custom key-value storage.
        /// </summary>
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        ///     Total vehicles count
        /// </summary>
        [DataMember(Name = "total_vehicles_count", EmitDefaultValue = false)]
        public int? TotalVehiclesCount { get; set; }

        /// <summary>
        ///     Total addresses count
        /// </summary>
        [DataMember(Name = "total_addresses_count", EmitDefaultValue = false)]
        public int? TotalAddressesCount { get; set; }

        /// <summary>
        ///     Syncronized vehicles count
        /// </summary>
        [DataMember(Name = "synced_vehicles_count", EmitDefaultValue = false)]
        public int? SyncedVehiclesCount { get; set; }

        /// <summary>
        ///     Telemetics connection vendor <see cref="Enum.TelematicsVendorType" />
        /// </summary>
        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public string Vendor { get; set; }
    }
}