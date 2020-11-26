using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Response data structure. As default in the failure case, 
    /// sometimes - in the success case too.
    /// </summary>
    [DataContract]
    public sealed class ResultResponse
    {
        /// <summary>
        /// Status  (true/false)
        /// </summary>
        [DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Exit code
        /// </summary>
        [DataMember(Name = "exit_code")]
        public int ExitCode { get; set; }

        /// <summary>
        /// An array of the error messages.
        /// </summary>
        [DataMember(Name = "messages")]
        public Dictionary<string, string[]> Messages { get; set; }
    }
}
