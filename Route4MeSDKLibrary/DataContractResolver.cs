using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK
{
    public class DataContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.NullValueHandling = NullValueHandling.Include;
            property.DefaultValueHandling = DefaultValueHandling.Include;
            property.ShouldSerialize = instance => true;

            property.ShouldDeserialize = instance =>
            {
                if (property.PropertyName == "schedule" && instance != null)
                {
                    if (instance.GetType() == typeof(AddressBookContact))
                    {
                        var schedules = ((AddressBookContact)instance).schedule;

                        Console.WriteLine("schedules: " + (schedules != null ? schedules.GetType().ToString() : ""));

                        return schedules == null ? true : (schedules.GetType() != typeof(Schedule) ? true : false);
                        //return true;
                    }
                }

                return true;
            };

            return property;
        }
    }
}
