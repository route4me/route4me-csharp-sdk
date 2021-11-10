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
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestUser();

            int createdMemberId = Convert.ToInt32(usersToRemove[usersToRemove.Count - 1]);

            var @params = new MemberParametersV4 { member_id = createdMemberId };

            // Run the query
            bool result = route4Me.UserDelete(@params, out string errorString);

            Console.WriteLine("");
            Console.WriteLine(
                    result
                    ? String.Format("DeleteUser executed successfully")
                    : String.Format("DeleteUser error: {0}", errorString)
                );
        }
    }
}
