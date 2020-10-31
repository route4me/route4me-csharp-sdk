using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get User By ID
        /// </summary>
        public void GetUserById()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            MemberParametersV4 @params = new MemberParametersV4 { member_id = 45844 };

            // Run the query
            string errorString = "";
            MemberResponseV4 result = route4Me.GetUserById(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("GetUserById executed successfully");
                Console.WriteLine("User: " + result.member_first_name + " " + result.member_last_name);
                Console.WriteLine("member_id: " + result.member_id);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("GetUserById error: {0}", errorString);
            }
        }
    }
}
