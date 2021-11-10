using System;
using System.Linq;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of bulk creating the team members.
        /// </summary>
        public void BulkCreateTeamMembers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            int? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            var memberParameters1 = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "John1",
                MemberLastName = "Doe1",
                MemberCompany = "Test Member Created",
                MemberEmail = GetTestEmail().Replace("+", "1+"),
                OwnerMemberId = (int)ownerId
            };

            memberParameters1.SetMemberType(MemberTypes.Driver);

            var memberParameters2 = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "John2",
                MemberLastName = "Doe2",
                MemberCompany = "Test Member Created",
                MemberEmail = GetTestEmail().Replace("+", "2+"),
                OwnerMemberId = (int)ownerId
            };

            memberParameters2.SetMemberType(MemberTypes.Driver);

            var members = new TeamRequest[] { memberParameters1, memberParameters2 };

            var result = route4Me.BulkCreateTeamMembers(members, out ResultResponse errorResponse);

            Console.WriteLine(result.Code);

            #region Remove Created Members

            if ((errorResponse?.Messages?.Count ?? 0) < 1)
            {
                var allMembers = route4Me.GetTeamMembers(out ResultResponse errorResponse0);

                var membersCreated = allMembers.Where(x =>
                    x.MemberCompany == "Test Member Created" &&
                    Array.IndexOf(new string[] { "John1", "John2" }, x.MemberFirstName) > -1 &&
                    Array.IndexOf(new string[] { "Doe1", "Doe2" }, x.MemberLastName) > -1
                );

                if ((allMembers?.Length ?? 0) > 0)
                {
                    membersToRemove = membersCreated.ToList();
                    RemoveTestTeamMembers();
                }
            }

            #endregion
        }
    }
}
