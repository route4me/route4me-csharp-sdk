using Microsoft.VisualStudio.TestTools.UnitTesting;
using Route4MeSDK.DataTypes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update route by sending a modified whole route object.
        /// </summary>
        public void UpdateWholeRoute()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            string routeId = SD10Stops_route_id;

            var initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(SD10Stops_route);

            #region // Notes, Custom Type Notes, Note File Uploading

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out string errorString5);

            var allCustomNotes = customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[])
                ? (CustomNoteType[])customNotesResponse : null;

            string tempFilePath = null;
            using (Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Route4MeSDKTest.Resources.test.png"))
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

            SD10Stops_route.Addresses[1].Notes = new AddressNote[] {
                new AddressNote()
                {
                    NoteId = -1,
                    RouteId = SD10Stops_route.RouteId,
                    Latitude = SD10Stops_route.Addresses[1].Latitude,
                    Longitude = SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content",
                    CustomTypes = allCustomNotes.Length>0
                    ? new AddressCustomNote[]
                    {
                        new AddressCustomNote()
                        {
                            NoteCustomTypeID = allCustomNotes[0].NoteCustomTypeID.ToString(),
                            NoteCustomValue = allCustomNotes[0].NoteCustomTypeValues[0]
                        }
                    }
                    : null,
                    UploadUrl = tempFilePath
                }
             };

            var updatedRoute0 = route4Me.UpdateRoute(
                SD10Stops_route,
                initialRoute,
                out string errorString0
            );

            if ((updatedRoute0?.Addresses[1]?.Notes.Length ?? 0) != 1)
            {
                Console.WriteLine("UpdateRouteTest failed: cannot create a note");
            }

            if (allCustomNotes.Length > 0)
                Assert.IsTrue(updatedRoute0.Addresses[1].Notes[0].CustomTypes.Length == 1, "UpdateRouteTest failed: cannot create a custom type note");

            Assert.IsTrue(updatedRoute0.Addresses[1].Notes[0].UploadId.Length == 32, "UpdateRouteTest failed: cannot create a custom type note");

            #endregion

            SD10Stops_route.ApprovedForExecution = true;
            SD10Stops_route.Parameters.RouteName += " Edited";
            SD10Stops_route.Parameters.Metric = Metric.Manhattan;

            SD10Stops_route.Addresses[1].AddressString += " Edited";
            SD10Stops_route.Addresses[1].Group = "Example Group";
            SD10Stops_route.Addresses[1].CustomerPo = "CPO 456789";
            SD10Stops_route.Addresses[1].InvoiceNo = "INO 789654";
            SD10Stops_route.Addresses[1].ReferenceNo = "RNO 313264";
            SD10Stops_route.Addresses[1].OrderNo = "ONO 654878";
            SD10Stops_route.Addresses[1].Notes = new AddressNote[] {
                new AddressNote()
                {
                    RouteDestinationId = -1,
                    RouteId = SD10Stops_route.RouteId,
                    Latitude = SD10Stops_route.Addresses[1].Latitude,
                    Longitude = SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content"
                }
             };

            SD10Stops_route.Addresses[2].SequenceNo = 5;
            var addressID = SD10Stops_route.Addresses[2].RouteDestinationId;

            var dataObject = route4Me.UpdateRoute(
                SD10Stops_route,
                initialRoute,
                out string errorString
            );

            Assert.IsTrue(dataObject.Addresses.Where(x => x.RouteDestinationId == addressID)
                .FirstOrDefault()
                .SequenceNo == 5, "UpdateWholeRouteTest failed.");

            Assert.IsTrue(SD10Stops_route.ApprovedForExecution, "UpdateRouteTest failed, ApprovedForExecution cannot set to true");
            Assert.IsNotNull(dataObject, "UpdateRouteTest failed. " + errorString);
            Assert.IsTrue(dataObject.Parameters.RouteName.Contains("Edited"), "UpdateRouteTest failed, the route name not changed.");
            Assert.IsTrue(dataObject.Addresses[1].AddressString.Contains("Edited"), "UpdateRouteTest failed, second address name not changed.");

            Assert.IsTrue(dataObject.Addresses[1].Group == "Example Group", "UpdateWholeRouteTest failed.");
            Assert.IsTrue(dataObject.Addresses[1].CustomerPo == "CPO 456789", "UpdateWholeRouteTest failed.");
            Assert.IsTrue(dataObject.Addresses[1].InvoiceNo == "INO 789654", "UpdateWholeRouteTest failed.");
            Assert.IsTrue(dataObject.Addresses[1].ReferenceNo == "RNO 313264", "UpdateWholeRouteTest failed.");
            Assert.IsTrue(dataObject.Addresses[1].OrderNo == "ONO 654878", "UpdateWholeRouteTest failed.");

            initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(SD10Stops_route);

            SD10Stops_route.ApprovedForExecution = false;
            SD10Stops_route.Addresses[1].Group = null;
            SD10Stops_route.Addresses[1].CustomerPo = null;
            SD10Stops_route.Addresses[1].InvoiceNo = null;
            SD10Stops_route.Addresses[1].ReferenceNo = null;
            SD10Stops_route.Addresses[1].OrderNo = null;

            dataObject = route4Me.UpdateRoute(
                SD10Stops_route,
                initialRoute,
                out errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
