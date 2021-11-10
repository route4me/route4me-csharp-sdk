using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The subclass of the AddressBookGroup class
    /// </summary>
    [DataContract]
    public sealed class AddressBookGroupFilter
    {
        /// <summary>
        ///     The class constructor
        /// </summary>
        public AddressBookGroupFilter()
        {
        }

        /// <summary>
        ///     The class constructor
        /// </summary>
        /// <param name="_condition">See <see cref="Condition" /></param>
        /// <param name="_rules">See <see cref="Rules" /></param>
        public AddressBookGroupFilter(string _condition, AddressBookGroupRule[] _rules)
        {
            Condition = _condition;
            Rules = _rules;
        }

        /// <summary>
        ///     A logic condition of the filter (AND, OR)
        /// </summary>
        [DataMember(Name = "condition", EmitDefaultValue = false)]
        public string Condition { get; set; }

        /// <summary>
        ///     An array of the AddressBookGroupRule type objects as the contacts filter rule.
        /// </summary>
        [DataMember(Name = "rules", EmitDefaultValue = false)]
        public AddressBookGroupRule[] Rules { get; set; }
    }
}