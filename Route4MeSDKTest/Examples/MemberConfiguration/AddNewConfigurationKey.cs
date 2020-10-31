using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            MemberConfigurationParameters @params = new MemberConfigurationParameters
            {
                config_key = "destination_icon_uri",
                config_value = "value"
            };

            // Run the query
            string errorString = "";
            MemberConfigurationResponse result = route4Me.CreateNewConfigurationKey(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("AddNewConfigurationKey executed successfully");
                Console.WriteLine("Result: " + result.result);
                Console.WriteLine("Affected: " + result.affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("AddNewConfigurationKey error: {0}", errorString);
            }
        }
    }
}
