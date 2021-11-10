using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a route member.
        /// </summary>
        public void UpdateTeamMember()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Create Member To Update

            membersToRemove = new List<TeamResponse>();
            CreateTestTeamMember();

            if (membersToRemove.Count < 1)
            {
                Console.WriteLine("Cannot create a team member to remove");
                return;
            }

            var member = membersToRemove[membersToRemove.Count - 1];

            #endregion

            var queryParams = new MemberQueryParameters()
            {
                UserId = member.MemberId.ToString()
            };

            var requestParams = new TeamRequest()
            {
                MemberPhone = "555-777-888",
                ReadOnlyUser = true,
                DrivingRate = 4
            };

            // Run the query
            var updatedMember = route4Me.UpdateTeamMember(
                                                queryParams,
                                                requestParams,
                                                out ResultResponse resultResponse);

            PrintTeamMembers(updatedMember, resultResponse);

            Console.WriteLine(
                (updatedMember?.MemberPhone ?? null) == requestParams.MemberPhone
                ? "The member phone updated"
                : "Cannot update the member phone"
            );

            Console.WriteLine(
                (updatedMember?.ReadOnlyUser ?? null) == requestParams.ReadOnlyUser
                ? "The member parameter ReadOnlyUser updated"
                : "Cannot update the member parameter ReadOnlyUser"
            );

            Console.WriteLine(
                (updatedMember?.DrivingRate ?? null) == requestParams.DrivingRate
                ? "The member parameter DrivingRate updated"
                : "Cannot update the member parameter DrivingRate"
            );

            RemoveTestTeamMembers();
        }
    }
}
