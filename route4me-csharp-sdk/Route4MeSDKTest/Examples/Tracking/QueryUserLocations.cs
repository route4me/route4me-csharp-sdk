using Route4MeSDK.QueryTypes;
using System;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of searching by query text the user location.
        /// </summary>
        public void QueryUserLocations()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Retrieve All User Locations

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(genericParameters, out string errorString);

            if (userLocations == null)
            {
                Console.WriteLine("Cannot retrieve all user locations. " + errorString);
                return;
            }

            #endregion

            #region Get First User's Email

            var userLocation = userLocations.Where(x => x.UserTracking != null).FirstOrDefault();

            string email = userLocation.MemberData.MemberEmail;

            #endregion

            // Run query
            genericParameters.ParametersCollection.Add("query", email);
            var queriedUserLocations = route4Me.GetUserLocations(genericParameters, out errorString);

            Console.WriteLine(
                queriedUserLocations != null
                ? "QueryUserLocations executed successfully"
                : "QueryUserLocations failed"
            );
        }
    }
}
