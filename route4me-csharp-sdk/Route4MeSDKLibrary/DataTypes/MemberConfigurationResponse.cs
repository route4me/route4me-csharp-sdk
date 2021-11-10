using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the member configuration request
    /// </summary>
    [DataContract]
    public sealed class MemberConfigurationResponse
    {
        /// <summary>
        ///     Configuration result
        /// </summary>
        [DataMember(Name = "result")]
        public string Result { get; set; }

        /// <summary>
        ///     How many configuration key -> data pairs affected
        /// </summary>
        [DataMember(Name = "affected")]
        public int Affected { get; set; }
    }
}