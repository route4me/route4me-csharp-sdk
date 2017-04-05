using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// User Authetntication
        /// </summary>
        public void UserAuthentication()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberParameters @params = new MemberParameters
            {
                StrEmail = "demo333@yahoo.com",
                StrPassword = "1111111",
                Format = "json"
            };
            // Run the query
            string errorString = "";
            MemberResponse result = route4Me.UserAuthentication(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UserAuthentication executed successfully");
                Console.WriteLine("status: " + result.Status);
                Console.WriteLine("api_key: " + result.ApiKey);
                Console.WriteLine("member_id: " + result.MemberId);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UserAuthentication error: {0}", errorString);
            }
        }
    }
}