using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberConfigurationParameters @params = new MemberConfigurationParameters { ConfigKey = "destination_icon_uri" };

            // Run the query
            string errorString = "";
            MemberConfigurationDataResponse result = route4Me.GetConfigurationData(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("GetSpecificConfigurationKeyData executed successfully");
                Console.WriteLine("Result: " + result.Result);
                foreach (MemberConfigurationData mc_data in result.data)
                {
                    Console.WriteLine("member_id= " + mc_data.MemberId);
                    Console.WriteLine("config_key= " + mc_data.ConfigKey);
                    Console.WriteLine("config_value= " + mc_data.ConfigValue);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine("GetSpecificConfigurationKeyData error: {0}", errorString);
            }
        }
    }
}
