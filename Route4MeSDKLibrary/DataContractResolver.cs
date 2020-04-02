using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Route4MeSDK
{
    public class DataContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.NullValueHandling = NullValueHandling.Include;
            property.DefaultValueHandling = DefaultValueHandling.Include;
            property.ShouldSerialize = o => true;
            return property;
        }
    }
}
