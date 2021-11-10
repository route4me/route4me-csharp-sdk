using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Member Created
        /// </summary>
        public void SearchMemberCreated()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var activityParameters = new ActivityParameters { ActivityType = "member-created" };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);
        }
    }
}
