using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get all custom note types from the user account.
        /// </summary>
        public void GetAllCustomNoteTypes()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var response = route4Me.GetAllCustomNoteTypes(out string errorString);

            Console.WriteLine(
                (response != null && response.GetType() == typeof(CustomNoteType[]))
                    ? "Retrieved the custom note types: " + ((CustomNoteType[])response).Length
                    : "Cannot retrieve custom note types"
                );
        }
    }
}
