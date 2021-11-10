using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes.V5;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // The example refers to the process of removing a team member from a user account.
        public void RemoveTeamMember()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Create Member To Remove

            membersToRemove = new List<TeamResponse>();
            CreateTestTeamMember();

            if (membersToRemove.Count < 1)
            {
                Console.WriteLine("Cannot create a team member to remove");
                return;
            }

            var member = membersToRemove[membersToRemove.Count - 1];

            #endregion

            var memberParams = new MemberQueryParameters()
            {
                UserId = member.MemberId.ToString()
            };

            // Run the query
            var removedMember = route4Me.RemoveTeamMember(memberParams,
                                                            out ResultResponse resultResponse);

            PrintTeamMembers(removedMember, resultResponse);

            Console.WriteLine(
                (removedMember?.MemberEmail?.Contains(".deleted") ?? false)
                ? String.Format("A member {0} removed succsessfully", removedMember.MemberId)
                : String.Format("Cannot remove a member {0}", removedMember.MemberId)
            );
        }
    }
}
