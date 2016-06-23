using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class Order : GenericParameters
  {

    /// <summary>
    /// Order ID.
    /// </summary>
    [DataMember(Name = "order_id", EmitDefaultValue = false)]
    public string OrderId { get; set; }

    /// <summary>
    /// Address 1 field. Required
    /// </summary>
    [DataMember(Name = "address_1")]
    public string Address1 { get; set; }

    /// <summary>
    /// Address 2 field
    /// </summary>
    [DataMember(Name = "address_2", EmitDefaultValue = false)]
    public string Address2 { get; set; }

    /// <summary>
    /// Address Alias. Required
    /// </summary>
    [DataMember(Name = "address_alias")]
    public string AddressAlias { get; set; }

    /// <summary>
    /// Geo latitude. Required
    /// </summary>
    [DataMember(Name = "cached_lat")]
    public double CachedLatitude { get; set; }

    /// <summary>
    /// Geo longitude. Required
    /// </summary>
    [DataMember(Name = "cached_lng")]
    public double CachedLongitude { get; set; }

    /// <summary>
    /// Generate optimal routes and driving directions to this curbside latitude
    /// </summary>
    [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
    public double? CurbsideLatitude { get; set; }

    /// <summary>
    /// Generate optimal routes and driving directions to the curbside langitude
    /// </summary>
    [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
    public double? CurbsideLongitude { get; set; }

    /// <summary>
    /// Address City
    /// </summary>
    [DataMember(Name = "address_city", EmitDefaultValue = false)]
    public string AddressCity { get; set; }

    /// <summary>
    /// Address state ID
    /// </summary>
    [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
    public string AddressStateId { get; set; }

    /// <summary>
    /// Address country ID
    /// </summary>
    [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
    public string AddressCountryId { get; set; }

    /// <summary>
    /// Address ZIP
    /// </summary>
    [DataMember(Name = "address_zip", EmitDefaultValue = false)]
    public string AddressZIP { get; set; }

    /// <summary>
    /// Order status ID
    /// </summary>
    [DataMember(Name = "order_status_id", EmitDefaultValue = false)]
    public string OrderStatusId { get; set; }

    /// <summary>
    /// The id of the member inside the route4me system
    /// </summary>
    [DataMember(Name = "member_id", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    [DataMember(Name = "EXT_FIELD_first_name", EmitDefaultValue = false)]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name
    /// </summary>
    [DataMember(Name = "EXT_FIELD_last_name", EmitDefaultValue = false)]
    public string LastName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [DataMember(Name = "EXT_FIELD_email", EmitDefaultValue = false)]
    public string Email { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    [DataMember(Name = "EXT_FIELD_phone", EmitDefaultValue = false)]
    public string Phone { get; set; }

    /// <summary>
    /// Custom data
    /// </summary>
    [DataMember(Name = "EXT_FIELD_custom_data", EmitDefaultValue = false)]
    public List<Dictionary<string, string>> CustomData { get; set; }

  }
}
