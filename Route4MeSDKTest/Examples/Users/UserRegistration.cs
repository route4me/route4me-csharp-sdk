using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// User Registration
        /// </summary>
        public void UserRegistration()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberParameters @params = new MemberParameters
            {
                StrEmail = "thewelco@gmail.com",
                StrPassword_1 = "11111111",
                StrPassword_2 = "11111111",
                StrFirstName = "Olman",
                StrLastName = "Progman",
                StrIndustry = "Transportation",
                Format = "json",
                ChkTerms = 1,
                DeviceType = "web",
                Plan = "free",
                MemberType = 5
            };
            // Run the query
            string errorString = "";
            MemberResponse result = route4Me.UserRegistration(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UserRegistration executed successfully");
                Console.WriteLine("status: " + result.Status);
                Console.WriteLine("api_key: " + result.ApiKey);
                Console.WriteLine("member_id: " + result.MemberId);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UserRegistration error: {0}", errorString);
            }
        }
    }
}
