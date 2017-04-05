using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class AddressBookContact : GenericParameters
  {
    [DataMember(Name = "territory_name", EmitDefaultValue = false)]
    public string territory_name { get; set; }

    [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
    public int created_timestamp { get; set; }

    [DataMember(Name = "address_id", EmitDefaultValue = false)]
    public int? address_id { get; set; }

    [DataMember(Name = "address_1")]
    public string address_1 { get; set; }

    [DataMember(Name = "address_2", EmitDefaultValue = false)]
    public string address_2 { get; set; }

    [DataMember(Name = "address_alias", EmitDefaultValue = false)]
    public string address_alias { get; set; }

    [DataMember(Name = "address_group", EmitDefaultValue = false)]
    public string address_group { get; set; }

    [DataMember(Name = "first_name", EmitDefaultValue = false)]
    public string first_name { get; set; }

    [DataMember(Name = "last_name", EmitDefaultValue = false)]
    public string last_name { get; set; }

    [DataMember(Name = "address_email", EmitDefaultValue = false)]
    public string address_email { get; set; }

    [DataMember(Name = "address_phone_number", EmitDefaultValue = false)]
    public string address_phone_number { get; set; }

    [DataMember(Name = "cached_lat")]
    public double cached_lat { get; set; }

    [DataMember(Name = "cached_lng")]
    public double cached_lng { get; set; }

    [DataMember(Name = "curbside_lat")]
    public double? curbside_lat { get; set; }

    [DataMember(Name = "curbside_lng")]
    public double? curbside_lng { get; set; }

    [DataMember(Name = "address_city", EmitDefaultValue = false)]
    public string address_city { get; set; }

    [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
    public string address_state_id { get; set; }

    [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
    public string address_country_id { get; set; }

    [DataMember(Name = "address_zip", EmitDefaultValue = false)]
    public string address_zip { get; set; }

    [DataMember(Name = "address_custom_data", EmitDefaultValue = false)]
    public object address_custom_data
    {
        get {
            if (m_address_custom_data == null) return null;
            string sTypeName = m_address_custom_data.GetType().Name;
            if (sTypeName == "Object[]") return new Dictionary<string, object>();
            return (Dictionary<string, object>)m_address_custom_data;
        }
        set { m_address_custom_data = value; }
    }
    private object m_address_custom_data;

    [DataMember(Name = "schedule", EmitDefaultValue = false)]
    public IList<Schedule> schedule { get; set; }

    [DataMember(Name = "schedule_blacklist", EmitDefaultValue = false)]
    public string[] schedule_blacklist { get; set; }

    [DataMember(Name = "service_time", EmitDefaultValue = false)]
    public int? service_time { get; set; }

    [DataMember(Name = "color", EmitDefaultValue = false)]
    public string color { get; set; }

    [DataMember(Name = "address_icon", EmitDefaultValue = false)]
    public string address_icon { get; set; }
  }
}
