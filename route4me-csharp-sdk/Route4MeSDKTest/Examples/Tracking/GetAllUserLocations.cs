using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting all user locations.
        /// </summary>
        public void GetAllUserLocations()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(genericParameters, out string errorString);

            if (userLocations != null && userLocations.GetType() == typeof(UserLocation[]))
            {
                Console.WriteLine("GetAllUserLocations excuted successfully");

                if (userLocations.Length > 0)
                {
                    foreach (var userLocation in userLocations)
                    {
                        Console.WriteLine(
                            "The member: {0} {1}",
                            userLocation.MemberData.MemberFirstName,
                            userLocation.MemberData.MemberLastName
                        );

                        Console.WriteLine(
                            "Location: {0}, {1}",
                            userLocation?.UserTracking?.PositionLatitude ?? default(double),
                            userLocation?.UserTracking?.PositionLongitude ?? default(double)
                        );
                    }
                }
            }
            else
            {
                Console.WriteLine("GetAllUserLocations failed");
            }
        }
    }
}
