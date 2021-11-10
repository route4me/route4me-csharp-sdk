using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddConfigurationKeyArray()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var parametersArray = new MemberConfigurationParameters[]
            {
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test Height 5",
                    ConfigValue = "155"
                },
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test Weight 5",
                    ConfigValue = "55"
                },
            };

            // Run the query
            var result = route4Me
                .CreateNewConfigurationKey(parametersArray, out string errorString);

            if ((result?.Result ?? null) == "OK" && (result?.Affected ?? 0) > 0)
            {
                configKeysToRemove.Add("Test Height 5");
                configKeysToRemove.Add("Test Weight 5");
            }

            PrintConfigKey(result, errorString);

            RemoveConfigKeys();
        }
    }
}
