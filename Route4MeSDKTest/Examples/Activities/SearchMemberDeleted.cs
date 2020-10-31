using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Member Deleted
        /// </summary>
        public void SearchMemberDeleted()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var activityParameters = new ActivityParameters { ActivityType = "member-deleted" };

            // Run the query
            Activity[] activities = route4Me.GetActivityFeed(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);
        }
    }
}
