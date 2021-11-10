using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes.V5;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddSkillsToDriver()
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

            string[] skills = new string[]
            {
                "Class A CDL", "Forklift", "Skid Steer Loader"
            };

            var updatedMember = route4Me.AddSkillsToDriver(queryParams,
                                                            skills,
                                                            out ResultResponse resultResponse);

            PrintTeamMembers(updatedMember, resultResponse);

            Console.WriteLine("");
            Console.WriteLine(
                (updatedMember?.CustomData?.ContainsKey("driver_skills") ?? false) == true
                ? "Driver skills :" + updatedMember.CustomData["driver_skills"]
                : "Cannot add skills to the driver"
            );

            RemoveTestTeamMembers();
        }
    }
}
