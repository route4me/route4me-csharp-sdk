using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressBookGroupRule
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "field", EmitDefaultValue = false)]
        public string Field { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "input", EmitDefaultValue = false)]
        public string Input { get; set; }

        [DataMember(Name = "operator", EmitDefaultValue = false)]
        public string Operator { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        public AddressBookGroupRule()
        {

        }

        public AddressBookGroupRule(string _field, string _operator, string _value, string _type = null, string _input = null)
        {
            Field = _field;
            Operator = _operator;
            Value = _value;
            if (_type != null) Type = _type;
            if (_input != null) Input = _input;
        }
    }
}
