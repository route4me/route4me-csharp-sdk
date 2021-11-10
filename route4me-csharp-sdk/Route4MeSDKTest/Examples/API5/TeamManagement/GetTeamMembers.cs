using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example referes to the process of retrieving all the team members of the account.
        /// </summary>
        public void GetTeamMembers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            var members = route4Me.GetTeamMembers(out ResultResponse failResponse);

            PrintTeamMembers(members, failResponse);
        }
    }
}
