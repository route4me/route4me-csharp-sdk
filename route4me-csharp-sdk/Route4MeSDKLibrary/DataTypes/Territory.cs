using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Territory data structure.
    /// </summary>
    [DataContract]
    public sealed class Territory
    {
        /// <summary>
        ///     Territory type (circle, rectangle, polygon)
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     Territory shape data
        /// </summary>
        [DataMember(Name = "data")]
        public string[] Data { get; set; }
    }
}