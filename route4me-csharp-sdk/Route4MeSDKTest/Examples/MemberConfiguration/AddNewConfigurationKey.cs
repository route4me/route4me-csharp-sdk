using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create New Configuration Key
        /// </summary>
        public void AddNewConfigurationKey()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test Config Key 5",
                ConfigValue = "Test Config Value 5"
            };

            // Run the query
            MemberConfigurationResponse result = route4Me
                .CreateNewConfigurationKey(@params, out string errorString);

            if ((result?.Result ?? null) == "OK" &&
                (result?.Affected ?? 0) > 0) configKeysToRemove.Add("Test Config Key 5");

            PrintConfigKey(result, errorString);

            RemoveConfigKeys();
        }
    }
}
