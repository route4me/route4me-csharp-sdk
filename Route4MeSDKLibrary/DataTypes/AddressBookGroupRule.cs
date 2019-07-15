using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The subclass of the AddressBookGroup class
    /// </summary>
    [DataContract]
    public sealed class AddressBookGroupRule
    {
        /// <summary>
        /// An unique text for the rule
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        /// The address book contact field the rule belongs 
        /// </summary>
        [DataMember(Name = "field", EmitDefaultValue = false)]
        public string Field { get; set; }

        /// <summary>
        /// The address book contact field's type (string, datetime)
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Input value type (text)
        /// </summary>
        [DataMember(Name = "input", EmitDefaultValue = false)]
        public string Input { get; set; }

        /// <summary>
        /// The condition operator: 
        /// <para>equal, not equal, less, less or equal, greater, between</para>
        /// <para>contains, doesn't contain, begins with, ends with</para>
        /// <para>has word, is empty, is not empty</para>
        /// </summary>
        [DataMember(Name = "operator", EmitDefaultValue = false)]
        public string Operator { get; set; }

        /// <summary>
        /// The value for the operator
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// The class constructor
        /// </summary>
        public AddressBookGroupRule()
        {

        }

        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="_field">See <see cref="Field"/> </param>
        /// <param name="_operator">See <see cref="Operator"/></param>
        /// <param name="_value">See <see cref="Value"/></param>
        /// <param name="_type">See <see cref="Type"/></param>
        /// <param name="_input">See <see cref="Input"/></param>
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
