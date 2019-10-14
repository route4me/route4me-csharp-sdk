using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Activities Destination Marked as Departed
        /// </summary>
        public void SearchDestinationMarkedAsDeparted()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            ActivityParameters activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed",
                RouteId = "03CEF546324F727239ABA69EFF3766E1"
            };

            // Run the query
            string errorString = "";
            Activity[] activities = route4Me.GetActivityFeed(activityParameters, out errorString);

            Console.WriteLine("");

            if (activities != null)
            {
                Console.WriteLine("SearchDestinationMarkedAsDeparted executed successfully, {0} activities returned", activities.Length);
                Console.WriteLine("");

                foreach (Activity Activity in activities)
                {
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId);
                }

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("SearchDestinationMarkedAsDeparted error: {0}", errorString);
            }

        }
    }
}
