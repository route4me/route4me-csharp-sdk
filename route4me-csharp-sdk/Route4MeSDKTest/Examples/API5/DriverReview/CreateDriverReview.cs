using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of the uploading a driver review.
        /// </summary>
        public void CreateDriverReview()
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
        }
    }
}
