using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of query all the activities from the last specified days.
        /// </summary>
        public void GetLastActivities()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var activitiesAfterTime = DateTime.Now - (new TimeSpan(7, 0, 0, 0));

            activitiesAfterTime = new DateTime(activitiesAfterTime.Year, activitiesAfterTime.Month, activitiesAfterTime.Day, 0, 0, 0);

            uint uiActivitiesAfterTime = (uint)Route4MeSDK.R4MeUtils.ConvertToUnixTimestamp(activitiesAfterTime);

            var activityParameters = new ActivityParameters()
            {
                Limit = 10,
                Offset = 0,
                Start = uiActivitiesAfterTime
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out string errorString);

            Console.WriteLine("");

            foreach (Activity activity in activities)
            {
                uint activityTime = activity.ActivityTimestamp != null ? (uint)activity.ActivityTimestamp : 0;

                if (activityTime < uiActivitiesAfterTime)
                {
                    Console.WriteLine("GetLastActivities failed - the last time filter not works.");
                    break;
                }
            }

            PrintExampleActivities(activities, errorString);
        }
    }
}
