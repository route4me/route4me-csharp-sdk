using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the get address book contacts request
    /// </summary>
    [DataContract]
    public sealed class AddressBookContactsResponse : GenericParameters
    {
        /// <summary>
        ///     An array of the visible addresses
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        /// <summary>
        ///     An array of the AddressBookContact type objects
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public AddressBookContact[] Results { get; set; }

        /// <summary>
        ///     Total number of the returned address book contacts
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}