using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a route member.
        /// </summary>
        public void CreateTeamMember()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            int? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            var newMemberParameters = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "John",
                MemberLastName = "Doe",
                MemberCompany = "Test Member Created",
                MemberEmail = GetTestEmail(),
                OwnerMemberId = (int)ownerId
            };

            newMemberParameters.SetMemberType(MemberTypes.Driver);

            // Run the query
            var member = route4Me.CreateTeamMember(newMemberParameters,
                                                    out ResultResponse resultResponse);

            if (member != null && member.GetType() == typeof(TeamResponse)) membersToRemove.Add(member);

            PrintTeamMembers(member, resultResponse);

            RemoveTestTeamMembers();
        }
    }
}
