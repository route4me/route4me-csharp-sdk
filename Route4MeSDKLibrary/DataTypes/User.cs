using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class User
  {
    //the id of the member inside the route4me system
    [DataMember(Name = "member_id", EmitDefaultValue = false)]
    public int? MemberId { get; set; }

    [DataMember(Name = "account_type_id", EmitDefaultValue = false)]
    public int? AccountTypeId { get; set; }

    [DataMember(Name = "member_type", EmitDefaultValue = false)]
    public string MemberType { get; set; }

    [DataMember(Name = "member_first_name")]
    public string MemberFirstName { get; set; }

    [DataMember(Name = "member_last_name")]
    public string MemberLasttName { get; set; }

    [DataMember(Name = "member_email")]
    public string MemberEmail { get; set; }

    [DataMember(Name = "phone_number")]
    public string PhoneNumber { get; set; }

    [DataMember(Name = "readonly_user", EmitDefaultValue = false)]
    public bool? ReadonlyUser { get; set; }

    [DataMember(Name = "show_superuser_addresses", EmitDefaultValue = false)]
    public bool? ShowSuperuserAddresses { get; set; }
  }
}
