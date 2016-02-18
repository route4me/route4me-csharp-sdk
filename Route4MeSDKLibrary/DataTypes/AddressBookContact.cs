using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class AddressBookContact : GenericParameters
  {
    [DataMember(Name = "address_id", EmitDefaultValue = false)]
    public string AddressId { get; set; }

    [DataMember(Name = "address_group", EmitDefaultValue = false)]
    public string AddressGroup { get; set; }

    [DataMember(Name = "address_alias", EmitDefaultValue = false)]
    public string AddressAlias { get; set; }

    [DataMember(Name = "address_1")]
    public string Address1 { get; set; }

    [DataMember(Name = "address_2", EmitDefaultValue = false)]
    public string Address2 { get; set; }

    [DataMember(Name = "first_name", EmitDefaultValue = false)]
    public string FirstName { get; set; }

    [DataMember(Name = "last_name", EmitDefaultValue = false)]
    public string LastName { get; set; }

    [DataMember(Name = "address_email", EmitDefaultValue = false)]
    public string AddressEmail { get; set; }

    [DataMember(Name = "address_phone_number", EmitDefaultValue = false)]
    public string AddressPhoneNumber { get; set; }

    [DataMember(Name = "address_city", EmitDefaultValue = false)]
    public string AddressCity { get; set; }

    [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
    public string AddressStateId { get; set; }

    [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
    public string AddressCountryId { get; set; }

    [DataMember(Name = "address_zip", EmitDefaultValue = false)]
    public string AddressZip { get; set; }

    [DataMember(Name = "cached_lat")]
    public double CachedLat { get; set; }

    [DataMember(Name = "cached_lng")]
    public double CachedLng { get; set; }

    [DataMember(Name = "color", EmitDefaultValue = false)]
    public string Color { get; set; }
  }
}
