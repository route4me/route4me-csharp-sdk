using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Specific Configuration Key Value (v4)
        /// </summary>
        public void GetSpecificConfigurationKeyData()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateConfigKey();

            string newConfigKey = configKeysToRemove[configKeysToRemove.Count - 1];

            var @params = new MemberConfigurationParameters { ConfigKey = newConfigKey };

            // Run the query
            MemberConfigurationDataResponse result = route4Me
                .GetConfigurationData(@params, out string errorString);

            PrintConfigKey(result, errorString);

            RemoveConfigKeys();
        }
    }
}
