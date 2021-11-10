using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting the existing sub-users.
        /// </summary>
        public void GetUsers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var parameters = new GenericParameters();

            // Run the query
            Route4MeManager.GetUsersResponse dataObjects = route4Me.GetUsers(parameters, out string errorString);

            PrintTestUsers(dataObjects, errorString);
        }
    }
}
