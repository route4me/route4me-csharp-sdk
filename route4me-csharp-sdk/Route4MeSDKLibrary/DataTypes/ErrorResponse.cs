using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     Errors data-structure
    /// </summary>
    [DataContract]
    public class ErrorResponse
    {
        /// <summary>
        ///     List of the error strings
        /// </summary>
        [DataMember(Name = "errors")]
        public List<string> Errors { get; set; }
    }
}