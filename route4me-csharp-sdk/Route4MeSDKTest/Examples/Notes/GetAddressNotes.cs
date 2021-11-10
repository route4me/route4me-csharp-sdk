using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetAddressNotes(string routeId = null, int? routeDestinationId = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = routeId == null ? true : false;

            if (isInnerExample) CreateAddressNote(out routeId, out routeDestinationId);

            var noteParameters = new NoteParameters()
            {
                RouteId = routeId,
                AddressId = (int)routeDestinationId
            };

            // Run the query
            AddressNote[] notes = route4Me.GetAddressNotes(noteParameters, out string errorString);

            Console.WriteLine("");

            if (notes != null)
            {
                Console.WriteLine(
                    "GetAddressNotes executed successfully, {0} notes returned",
                    notes.Length);

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("GetAddressNotes error: {0}", errorString);
                Console.WriteLine("");
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
