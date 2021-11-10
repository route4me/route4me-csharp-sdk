using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the process of a telematics member registering.
    /// </summary>
    [DataContract]
    public sealed class TelematicsRegisterMemberResponse
    {
        /// <summary>
        ///     API token
        ///     Use for the operations: Get Telematics Connections, Register Telematics Connection
        /// </summary>
        [DataMember(Name = "api_token", EmitDefaultValue = false)]
        public string ApiToken { get; set; }

        /// <summary>
        ///     When the registered member updated
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     When the registered member created
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Telemetics member  ID
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int? Id { get; set; }
    }
}