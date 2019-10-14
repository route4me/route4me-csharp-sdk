using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

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
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            MemberParametersV4 @params = new MemberParametersV4
            {
                HIDE_ROUTED_ADDRESSES = "FALSE",
                member_phone = "571-259-5939",
                member_zipcode = "22102",
                member_email = "skrynkovskyy+newdispatcher@gmail.com",
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
            string errorString = "";
            MemberResponseV4 result = route4Me.CreateUser(@params, out errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UserRegistration executed successfully");
                Console.WriteLine("User: " + result.member_first_name + " " + result.member_last_name);
                Console.WriteLine("member_id: " + result.member_id);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UserRegistration error: {0}", errorString);
            }
        }
    }
}
