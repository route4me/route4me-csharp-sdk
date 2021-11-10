using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddAddressNote()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunSingleDriverRoundTrip();

            OptimizationsToRemove = new List<string>() { SDRT_optimization_problem_id };

            string routeIdToMoveTo = SDRT_route_id;

            int addressId = (int)SDRT_route.Addresses[1].RouteDestinationId;

            double lat = SDRT_route.Addresses.Length > 1
                ? SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            double lng = SDRT_route.Addresses.Length > 1
                ? SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            // Run the query
            string contents = "Test Note Contents " + DateTime.Now.ToString();
            var note = route4Me.AddAddressNote(noteParameters, contents, out string errorString);

            PrintExampleAddressNote(note, errorString);

            RemoveTestOptimizations();
        }
    }
}
