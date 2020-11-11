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
                    config_key = "Test Height 5",
                    config_value = "155"
                },
                new MemberConfigurationParameters
                {
                    config_key = "Test Weight 5",
                    config_value = "55"
                },
            };

            // Run the query
            var result = route4Me
                .CreateNewConfigurationKey(parametersArray, out string errorString);

            if ((result?.result ?? null) == "OK" && (result?.affected ?? 0) > 0)
            {
                configKeysToRemove.Add("Test Height 5");
                configKeysToRemove.Add("Test Weight 5");
            }

            PrintConfigKey(result, errorString);

            RemoveConfigKeys();
        }
    }
}
