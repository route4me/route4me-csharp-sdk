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
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestUser();

            int memberId = Convert.ToInt32(usersToRemove[usersToRemove.Count - 1]);

            var @params = new MemberParametersV4 { member_id = memberId };

            // Run the query
            MemberResponseV4 result = route4Me.GetUserById(@params, out string errorString);

            PrintTestUsers(result, errorString);

            RemoveTestUsers();
        }
    }
}
