using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberConfigurationParameters @params = new MemberConfigurationParameters { config_key = "My height" };

            // Run the query
            string errorString = "";
            MemberConfigurationResponse result = route4Me.RemoveConfigurationKey(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("RemoveConfigurationKey executed successfully");
                Console.WriteLine("Result: " + result.result);
                Console.WriteLine("Affected: " + result.affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UserRegistration error: {0}", errorString);
            }

        }
    }
}
