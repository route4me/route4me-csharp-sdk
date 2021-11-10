using System;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetDriverReviewById()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Get Driver Review List

            var allQueryParameters = new DriverReviewParameters()
            {
                Page = 0,
                PerPage = 2
            };

            var reviews = route4Me.GetDriverReviewList(allQueryParameters,
                                                          out ResultResponse resultResponse);

            if ((reviews?.Data?.Length ?? 0) < 1)
            {
                Console.WriteLine("Cannot retrive driver reviews");
                return;
            }

            #endregion

            var queryParameters = new DriverReviewParameters()
            {
                RatingId = reviews.Data[0].RatingId
            };

            var review = route4Me.GetDriverReviewById(queryParameters,
                                                          out resultResponse);

            PrintDriverReview(review, resultResponse);
        }
    }
}
