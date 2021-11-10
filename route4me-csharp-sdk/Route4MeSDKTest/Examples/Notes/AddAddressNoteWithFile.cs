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
        public void AddAddressNoteWithFile()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var noteParameters = new NoteParameters()
            {
                RouteId = SD10Stops_route_id,
                AddressId = (int)SD10Stops_route.Addresses[1].RouteDestinationId,
                Latitude = 33.132675170898,
                Longitude = -83.244743347168,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string nm in names)
            {
                Console.WriteLine(nm);
            }

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

            // Run the query
            string contents = "Test Note Contents with Attachment " + DateTime.Now.ToString();
            AddressNote note = route4Me.AddAddressNote(
                noteParameters,
                contents,
                tempFilePath,
                out string errorString);

            PrintExampleAddressNote(note, errorString);

            RemoveTestOptimizations();
        }
    }
}
