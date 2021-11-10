using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// User Registration (v4)
        /// </summary>
        public void CreateUser()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParametersV4
            {
                HIDE_ROUTED_ADDRESSES = "FALSE",
                member_phone = "571-259-5939",
                member_zipcode = "22102",
                member_email = "skrynkovskyy+newdispatcher" + DateTime.Now.ToString("yyMMddHHmmss") + "@gmail.com",
                HIDE_VISITED_ADDRESSES = "FALSE",
                READONLY_USER = "FALSE",
                member_type = "SUB_ACCOUNT_DISPATCHER",
                date_of_birth = "2010",
                member_first_name = "Clay",
                member_password = "123456",
                HIDE_NONFUTURE_ROUTES = "FALSE",
                member_last_name = "Abraham",
                SHOW_ALL_VEHICLES = "FALSE",
                SHOW_ALL_DRIVERS = "FALSE"
            };

            // Run the query
            MemberResponseV4 result = route4Me.CreateUser(@params, out string errorString);

            PrintTestUsers(result, errorString);

            if (result != null && result.GetType() == typeof(MemberResponseV4))
            {
                usersToRemove = new List<string>();
                usersToRemove.Add(result.MemberId);

                RemoveTestUsers();
            }
        }
    }
}
