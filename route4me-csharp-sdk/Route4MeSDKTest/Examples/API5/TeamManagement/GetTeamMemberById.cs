using System;
using Route4MeSDK.DataTypes.V5;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example referes to the process of retrieving a team member specified by UserId.
        /// </summary>
        public void GetTeamMemberById()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Get Random member

            var randomMember = GetRandomTeamMember();

            if ((randomMember?.MemberId ?? 0) < 1)
            {
                Console.WriteLine("Cannot retrieve a random team member");
                return;
            }

            #endregion

            var memberParams = new MemberQueryParameters()
            {
                UserId = randomMember.MemberId.ToString()
            };

            // Run the query
            var member = route4Me.GetTeamMemberById(memberParams,
                                                    out ResultResponse resultResponse);

            PrintTeamMembers(member, resultResponse);
        }
    }
}
