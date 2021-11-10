using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities by member.
        /// </summary>
        public void GetActivitiesByMember()
        {
            if (ActualApiKey == DemoApiKey) return;

            var route4Me = new Route4MeManager(ActualApiKey);

            var parameters = new GenericParameters()
            {
            };

            var response = route4Me.GetUsers(parameters, out string userErrorString);

            if (response == null || response.Results.GetType() != typeof(MemberResponseV4[]))
            {
                Console.WriteLine("GetActivitiesByMemberTest failed - cannot get users");
                return;
            }

            if (response.Results.Length < 2)
            {
                Console.WriteLine("Cannot retrieve more than 1 users");
                return;
            }

            var activityParameters = new ActivityParameters()
            {
                MemberId = response.Results[1].MemberId != null ? Convert.ToInt32(response.Results[1].MemberId) : -1,
                Offset = 0,
                Limit = 10
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);
        }
    }
}
