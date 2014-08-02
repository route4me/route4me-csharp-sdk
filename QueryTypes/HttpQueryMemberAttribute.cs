using System;

namespace Route4MeSDK.QueryTypes
{
  public sealed class HttpQueryMemberAttribute : Attribute
  {
    public string Name             { get; set; }
    public bool   EmitDefaultValue { get; set; }
    public bool   IsRequired       { get; set; }
    public object DefaultValue     { get; set; }

    public HttpQueryMemberAttribute()
    {
      EmitDefaultValue = true;
    }
  }
}
