using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add complex note to the specified route address.
        /// </summary>
        public void AddComplexAddressNote()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunSingleDriverRoundTrip();
            OptimizationsToRemove = new List<string>() { SDRT_optimization_problem_id };

            string routeIdToMoveTo = SDRT_route_id;

            int addressId = (SDRT_route != null &&
                            SDRT_route.Addresses.Length > 1 &&
                            SDRT_route.Addresses[1].RouteDestinationId != null)
                ? SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            double lat = SDRT_route.Addresses.Length > 1 ? SDRT_route.Addresses[1].Latitude : 33.132675170898;
            double lng = SDRT_route.Addresses.Length > 1 ? SDRT_route.Addresses[1].Longitude : -83.244743347168;

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out string errorString0);

            Dictionary<string, string> customNotes = null;

            if (customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[]))
            {
                var allCustomNotes = (CustomNoteType[])customNotesResponse;

                if (allCustomNotes.Length > 0)
                {
                    customNotes = new Dictionary<string, string>()
                    {
                        {
                            "custom_note_type["+allCustomNotes[0].NoteCustomTypeID+"]",
                            allCustomNotes[0].NoteCustomTypeValues[0]
                        }
                    };
                }
            }

            var noteParameters = new NoteParameters()
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                StrNoteContents = "Test Note Contents " + DateTime.Now.ToString()
            };

            if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

            string tempFilePath = null;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Route4MeSDKTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    Console.WriteLine(tempFilePath);

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            noteParameters.StrFileName = tempFilePath;

            var note = route4Me.AddAddressNote(noteParameters, out string errorString);

            PrintExampleAddressNote(note, errorString);

            RemoveTestOptimizations();
        }
    }
}
