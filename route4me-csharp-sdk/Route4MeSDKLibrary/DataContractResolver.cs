using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Route4MeSDK
{
    public class DataContractResolver : DefaultContractResolver
    {
        public DataContractResolver()
        {
        }

        public DataContractResolver(string[] mandatoryFields)
        {
            MandatoryFields = mandatoryFields;
        }

        public string[] MandatoryFields { get; set; }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            //property.NullValueHandling = NullValueHandling.Include;
            property.DefaultValueHandling = DefaultValueHandling.Include;
            if ((MandatoryFields?.Length ?? 0) > 0)
            {
                var shouldSerialized = MandatoryFields.Contains(property.PropertyName) ? true : false;
                property.ShouldSerialize = o => shouldSerialized;
            }

            //property.ShouldSerialize = o => true;
            return property;
        }
    }
}