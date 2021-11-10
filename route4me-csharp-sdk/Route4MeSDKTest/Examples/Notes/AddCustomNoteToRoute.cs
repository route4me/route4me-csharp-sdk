using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add custom note to the specified route
        /// </summary>
        public void AddCustomNoteToRoute()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunSingleDriverRoundTrip();
            OptimizationsToRemove = new List<string>() { SDRT_optimization_problem_id };

            var noteParameters = new NoteParameters()
            {
                RouteId = SDRT_route.RouteId,
                AddressId = SDRT_route.Addresses[1].RouteDestinationId != null
                    ? (int)SDRT_route.Addresses[1].RouteDestinationId
                    : 0,
                Format = "json",
                Latitude = SDRT_route.Addresses[1].Latitude,
                Longitude = SDRT_route.Addresses[1].Longitude
            };

            var customNotes = new Dictionary<string, string>()
            {
                {"custom_note_type[11]", "slippery"},
                {"custom_note_type[10]", "Backdoor"},
                {"strUpdateType", "dropoff"},
                {"strNoteContents", "test1111"}
            };

            var response = route4Me.AddCustomNoteToRoute(noteParameters, customNotes, out string errorString);

            PrintExampleAddressNote(response, errorString);

            RemoveTestOptimizations();
        }
    }
}
