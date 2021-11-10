using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public List<TeamResponse> membersToRemove = new List<TeamResponse>();

        private const string testPassword = "pSw1_2_3_4@";

        private string testRatingId;

        #region Team Management

        private string GetTestEmail()
        {
            return "test" + DateTime.Now.ToString("yyMMddHHmmss") + "+evgenysoloshenko@gmail.com";
        }

        private void PrintTeamMembers(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(TeamResponse))
                {
                    var member = (TeamResponse)result;
                    Console.WriteLine("Member: {0}", member.MemberFirstName + " " + member.MemberLastName);
                }
                else if (result.GetType() == typeof(TeamResponse[]))
                {
                    var members = (TeamResponse[])result;

                    foreach (var member in members)
                    {
                        Console.WriteLine("Member: {0}", member.MemberFirstName + " " + member.MemberLastName);
                    }
                }
                else
                {
                    Console.WriteLine(testName + ": unknown response type");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, testName);
            }
        }

        private void PrintFailResponse(ResultResponse resultResponse, string testName)
        {
            Console.WriteLine(testName + " failed:");
            Console.WriteLine("Status: {0}", resultResponse?.Status.ToString() ?? "");
            Console.WriteLine("Status code: {0}", resultResponse?.Code.ToString() ?? "");
            Console.WriteLine("Exit code: {0}", resultResponse?.ExitCode.ToString() ?? "");

            if ((resultResponse?.Messages?.Count ?? 0) > 0)
            {
                foreach (var message in resultResponse.Messages)
                {
                    if (message.Key != null && (message.Value?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine((message.Key ?? "") + ":");

                        foreach (var msg in message.Value)
                        {
                            Console.WriteLine("    " + msg);
                        }
                    }
                }
            }
        }

        private TeamResponse GetRandomTeamMember()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            var members = route4Me.GetTeamMembers(out ResultResponse errorResponse);

            if (members == null) return null;

            Random rnd = new Random();
            int i = rnd.Next(0, members.Count() - 1);

            return members[i];
        }

        private int? GetOwnerMemberId()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var members = route4Me.GetTeamMembers(out ResultResponse errorResponse);

            var memberParams = new TeamRequest();

            var ownerMemberId = members
                .Where(x => memberParams.GetMemberType(x.MemberType) == MemberTypes.AccountOwner)
                .FirstOrDefault()
                ?.MemberId ?? null;

            if (ownerMemberId == null)
            {
                Console.WriteLine("Cannot retrieve the team owner - cannot create a member.");
            }

            return ownerMemberId;
        }

        private void CreateTestTeamMember()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            int? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            var newMemberParameters = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "John",
                MemberLastName = "Doe",
                MemberCompany = "Test Member To Remove",
                MemberEmail = GetTestEmail(),
                OwnerMemberId = (int)ownerId
            };

            newMemberParameters.SetMemberType(MemberTypes.Driver);

            TeamResponse member = route4Me.CreateTeamMember(newMemberParameters,
                                                            out ResultResponse resultResponse);

            if (member != null && member.GetType() == typeof(TeamResponse)) membersToRemove.Add(member);
        }

        private void RemoveTestTeamMembers()
        {
            if ((membersToRemove?.Count ?? 0) < 1) return;

            var route4Me = new Route4MeManagerV5(ActualApiKey);

            foreach (var member in membersToRemove)
            {
                var memberParams = new MemberQueryParameters()
                {
                    UserId = member.MemberId.ToString()
                };

                var removedMember = route4Me.RemoveTeamMember(memberParams,
                                                out ResultResponse resultResponse);

                Console.WriteLine(
                    (removedMember?.MemberEmail?.Contains(".deleted") ?? false)
                    ? String.Format("A test member {0} removed succsessfully", removedMember.MemberId)
                    : String.Format("Cannot remove a test member {0}", removedMember.MemberId)
                );
            }
        }

        #endregion

        #region Driver Review

        private void PrintDriverReview(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(DriverReview))
                {
                    var review = (DriverReview)result;

                    Console.WriteLine(
                            "Tracking number: {0}, Rating: {1}, Review: {2}",
                            review.TrackingNumber, review.Rating, review.Review
                        );
                }
                else if (result.GetType() == typeof(DriverReviewsResponse))
                {
                    var reviewResponse = (DriverReviewsResponse)result;

                    foreach (var review in reviewResponse.Data)
                    {
                        Console.WriteLine(
                            "Tracking number: {0}, Rating: {1}, Review: {2}",
                            review.TrackingNumber, review.Rating, review.Review
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Unexcepted response type");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, testName);
            }
        }

        private void CreateTestDriverReview()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var newDriverReview = new DriverReview()
            {
                TrackingNumber = "NDRK0M1V", // TO DO: take this value from generated test route later.
                Rating = 4,
                Review = "Test Review"
            };

            var driverReview = route4Me.CreateDriverReview(newDriverReview,
                                                          out ResultResponse resultResponse);

            PrintDriverReview(driverReview, resultResponse);

            testRatingId = driverReview?.RatingId ?? null;
        }

        private DriverReview GetLastDriverReview()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var queryParameters = new DriverReviewParameters()
            {
                Page = 0,
                PerPage = 20
            };

            var reviewList = route4Me.GetDriverReviewList(queryParameters,
                                                          out ResultResponse resultResponse);

            return (reviewList?.Data?.Length ?? 0) > 0
                ? reviewList.Data[reviewList.Data.Length - 1]
                : null;
        }

        #endregion
    }
}
