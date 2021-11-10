using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

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
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParameters
            {
                StrEmail = "skrynkovskyy+newdispatcher" + DateTime.Now.ToString("yyMMddHHmmss") + "@gmail.com",
                StrPassword_1 = "11111111",
                StrPassword_2 = "11111111",
                StrFirstName = "Olas",
                StrLastName = "Progman",
                StrIndustry = "Transportation",
                Format = "json",
                ChkTerms = 1,
                DeviceType = "web",
                Plan = "free",
                MemberType = 5
            };

            // Run the query
            MemberResponse result = route4Me.UserRegistration(@params, out string errorString);

            if (result != null && result.GetType() == typeof(MemberResponse))
            {
                usersToRemove = new List<string>();
                usersToRemove.Add(result.MemberId.ToString());
            }

            PrintTestUsers(result, errorString);

            RemoveTestUsers();
        }
    }
}
