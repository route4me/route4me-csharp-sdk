using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Activities Destination Inserted
        /// </summary>
        public void SearchDestinationInserted()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            ActivityParameters activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination",
                RouteId = "87B8873BAEA4E09942C68E2C92A9C4B7"
            };

            // Run the query
            string errorString = "";
            Activity[] activities = route4Me.GetActivityFeed(activityParameters, out errorString);

            Console.WriteLine("");

            if (activities != null)
            {
                Console.WriteLine("SearchDestinationInserted executed successfully, {0} activities returned", activities.Length);
                Console.WriteLine("");

                foreach (Activity Activity in activities)
                {
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId);
                }

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("SearchDestinationInserted error: {0}", errorString);
            }

        }
    }
}
