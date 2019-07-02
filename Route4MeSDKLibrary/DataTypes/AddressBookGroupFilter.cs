using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressBookGroupFilter
    {
        [DataMember(Name = "condition", EmitDefaultValue = false)]
        public string Condition { get; set; }

        [DataMember(Name = "rules", EmitDefaultValue = false)]
        public AddressBookGroupRule[] Rules { get; set; }

        public AddressBookGroupFilter()
        {

        }

        public AddressBookGroupFilter(string _condition, AddressBookGroupRule[] _rules)
        {
            Condition = _condition;
            Rules = _rules;
        }
    }
    
}
