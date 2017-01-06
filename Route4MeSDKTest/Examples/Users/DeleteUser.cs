using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove User
        /// </summary>
        public void DeleteUser()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberParametersV4 @params = new MemberParametersV4 { member_id = 147824 };

            // Run the query
            string errorString = "";
            bool result = route4Me.UserDelete(@params, out errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("DeleteUser executed successfully");
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("DeleteUser error: {0}", errorString);
            }
        }
    }
}
