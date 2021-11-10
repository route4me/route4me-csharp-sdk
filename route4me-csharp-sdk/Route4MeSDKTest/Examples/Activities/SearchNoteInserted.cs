using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get activities with the event Note Inserted
        /// </summary>
        public void SearchNoteInserted()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            string routeId = SD10Stops_route_id;

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            int addressId = (int)SD10Stops_route.Addresses[2].RouteDestinationId;

            var noteParams = new NoteParameters()
            {
                AddressId = addressId,
                RouteId = routeId,
                Latitude = SD10Stops_route.Addresses[2].Latitude,
                Longitude = SD10Stops_route.Addresses[2].Longitude,
                DeviceType = "web",
                StrNoteContents = "Note example for Destination",
                ActivityType = "dropoff"
            };

            var addrssNote = route4Me.AddAddressNote(noteParams, out string errorString0);

            if (addrssNote == null || addrssNote.GetType() != typeof(AddressNote))
            {
                Console.WriteLine(
                    "Cannot add a note to the address." +
                    Environment.NewLine +
                    errorString0);

                RemoveTestOptimizations();
                return;
            }

            var activityParameters = new ActivityParameters
            {
                ActivityType = "note-insert",
                RouteId = routeId
            };

            // Run the query
            Activity[] activities = route4Me.GetActivities(activityParameters, out string errorString);

            PrintExampleActivities(activities, errorString);
        }
    }
}
