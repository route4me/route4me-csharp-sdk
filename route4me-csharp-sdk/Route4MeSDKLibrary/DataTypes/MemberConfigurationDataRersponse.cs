using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response structure for the member's configuration data request.
    /// </summary>
    [DataContract]
    public sealed class MemberConfigurationDataResponse : GenericParameters
    {
        /// <summary>
        ///     Result of the member configuration request.
        ///     <para>
        ///         <remarks>If equal to 'OK' request finished successfully.</remarks>
        ///     </para>
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public string Result { get; set; }

        /// <summary>
        ///     The member's configutration data. See <see cref="MemberConfigurationData" />
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public MemberConfigurationData[] Data { get; set; }

        /// <summary>
        ///     Number of the affected member configuration records.
        /// </summary>
        [DataMember(Name = "affected", EmitDefaultValue = false)]
        public int Affected { get; set; }
    }
}