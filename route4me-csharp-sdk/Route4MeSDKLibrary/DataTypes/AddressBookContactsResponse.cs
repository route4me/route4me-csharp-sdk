using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class for a response from the address book contacts search request
    /// </summary>
    [DataContract]
    public sealed class AddressBookContactsResponse : GenericParameters
    {
        /// <summary>
        ///     An array of the AddressBookContact type objects
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public AddressBookContact[] Results { get; set; }

        /// <summary>
        ///     Total number of the returned contacts
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }

        /// <summary>
        ///     The contacts query in the JSON format
        /// </summary>
        [DataMember(Name = "index_query", EmitDefaultValue = false)]
        public string IndexQuery { get; set; }

        /// <summary>
        ///     An array of the field names to be shown
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}