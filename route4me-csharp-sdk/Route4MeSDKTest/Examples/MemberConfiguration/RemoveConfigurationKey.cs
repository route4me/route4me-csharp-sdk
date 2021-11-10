using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove a Configuration Key
        /// </summary>
        public void RemoveConfigurationKey()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateConfigKey();

            string newConfigKey = configKeysToRemove[configKeysToRemove.Count - 1];

            var @params = new MemberConfigurationParameters { ConfigKey = newConfigKey };

            // Run the query
            MemberConfigurationResponse result = route4Me
                .RemoveConfigurationKey(@params, out string errorString);

            PrintConfigKey(result, errorString);
        }
    }
}
