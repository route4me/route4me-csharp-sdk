using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of the updating a driver review.
        /// </summary>
        public void UpdateDriverReview()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var existingReview = GetLastDriverReview();

            var driverReview = new DriverReview()
            {
                RatingId = existingReview.RatingId,
                TrackingNumber = existingReview.TrackingNumber,
                Rating = existingReview.Rating,
                Review = "Updated Review"
            };

            var updatedDriverReview = route4Me.UpdateDriverReview(
                                                    driverReview,
                                                    HttpMethodType.Patch, // other availbale option is the Put method
                                                    out ResultResponse resultResponse);

            PrintDriverReview(updatedDriverReview, resultResponse);
        }
    }
}
