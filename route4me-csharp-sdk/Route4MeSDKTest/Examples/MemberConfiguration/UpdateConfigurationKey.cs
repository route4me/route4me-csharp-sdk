using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update a Configuration Key Value
        /// </summary>
        public void UpdateConfigurationKey()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateConfigKey();

            string newConfigKey = configKeysToRemove[configKeysToRemove.Count - 1];

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = newConfigKey,
                ConfigValue = "Test Config Value Updated"
            };

            // Run the query
            MemberConfigurationResponse result = route4Me
                .UpdateConfigurationKey(@params, out string errorString);

            PrintConfigKey(result, errorString);

            RemoveConfigKeys();
        }
    }
}
