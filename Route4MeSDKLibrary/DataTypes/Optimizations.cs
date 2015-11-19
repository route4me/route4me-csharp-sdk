using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// </summary>
    [DataContract]
    public sealed class DataObjectOptimizations
    {
        //[DataMember(Name = "optimizations")]
        //public Optimization[] Optimizations { get; set; }
        [DataMember(Name = "optimizations")]
        public DataObject[] Optimizations { get; set; }
    }
}
