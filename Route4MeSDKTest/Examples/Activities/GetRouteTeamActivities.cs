using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Team Activities on a Route
        /// </summary>
        public void GetRouteTeamActivities()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            string routeId = "06B655F27E0D6A74BD37F6F9758E4D2E";

            ActivityParameters activityParameters = new ActivityParameters
            {
                RouteId = routeId,
                Team = "true"
            };

            // Run the query
            string errorString = "";
            Activity[] activities = route4Me.GetActivityFeed(activityParameters, out errorString);

            Console.WriteLine("");

            if (activities != null)
            {
                Console.WriteLine("GetActivities executed successfully, {0} activities returned", activities.Length);
                Console.WriteLine("");

                foreach (Activity Activity in activities)
                {
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId);
                }

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("GetActivities error: {0}", errorString);
            }

        }
    }
}
