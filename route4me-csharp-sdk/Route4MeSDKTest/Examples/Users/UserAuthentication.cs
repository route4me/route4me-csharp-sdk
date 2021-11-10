using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

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
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestUser();

            var @params = new MemberParameters
            {
                StrEmail = lastCreatedUser.MemberEmail,
                StrPassword = "123456",
                Format = "json"
            };

            // Run the query
            MemberResponse result = route4Me.UserAuthentication(@params, out string errorString);

            PrintTestUsers(result, errorString);

            RemoveTestUsers();
        }
    }
}
