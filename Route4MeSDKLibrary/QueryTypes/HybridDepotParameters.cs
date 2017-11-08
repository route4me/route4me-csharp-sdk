using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{

    [DataContract]
    public sealed class HybridDepotParameters : GenericParameters
    {
        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string optimization_problem_id { get; set; }

        [DataMember(Name = "delete_old_depots", EmitDefaultValue = false)]
        public bool delete_old_depots { get; set; }

        [DataMember(Name = "new_depots", EmitDefaultValue = false)]
        public Address[] new_depots { get; set; }
    }
}
