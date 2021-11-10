using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of retrieving a list of driver reviews.
        /// </summary>
        public void GetDriverReviewList()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var queryParameters = new DriverReviewParameters()
            {
                Start = "2020-01-01",
                End = "2030-01-01",
                Page = 0,
                PerPage = 20
            };

            var reviewList = route4Me.GetDriverReviewList(queryParameters,
                                                          out ResultResponse resultResponse);

            PrintDriverReview(reviewList, resultResponse);
        }
    }
}
