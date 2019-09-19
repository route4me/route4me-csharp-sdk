using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberConfigurationParameters @params = new MemberConfigurationParameters
            {
                ConfigKey = "destination_icon_uri",
                ConfigValue = "444"
            };

            // Run the query
            string errorString = "";
            MemberConfigurationResponse result = route4Me.UpdateConfigurationKey(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UpdateConfigurationKey executed successfully");
                Console.WriteLine("Result: " + result.result);
                Console.WriteLine("Affected: " + result.affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UpdateConfigurationKey error: {0}", errorString);
            }
        }
    }
}
