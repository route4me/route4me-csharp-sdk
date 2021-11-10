using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get All Configuration Data
        /// </summary>
        public void GetAllConfigurationData()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberConfigurationParameters();

            // Run the query
            MemberConfigurationDataResponse result = route4Me
                .GetConfigurationData(@params, out string errorString);

            PrintConfigKey(result, errorString);
        }
    }
}
