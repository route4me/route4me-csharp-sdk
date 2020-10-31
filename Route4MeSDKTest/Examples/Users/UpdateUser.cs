using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an User
        /// </summary>
        public void UpdateUser()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            MemberParametersV4 @params = new MemberParametersV4
            {
                member_id = 220461,
                member_phone = "571-259-5939"
            };

            // Run the query
            string errorString = "";
            MemberResponseV4 result = route4Me.UserUpdate(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UpdateUser executed successfully");
                Console.WriteLine("status: " + result.member_first_name + " " + result.member_last_name);
                Console.WriteLine("member_id: " + result.member_id);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UpdateUser error: {0}", errorString);
            }
        }
    }
}
