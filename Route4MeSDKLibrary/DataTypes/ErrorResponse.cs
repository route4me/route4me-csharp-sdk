using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    /// Errors data-structure
    /// </summary>
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "errors")]
        public List<String> Errors { get; set; }
    }
}
