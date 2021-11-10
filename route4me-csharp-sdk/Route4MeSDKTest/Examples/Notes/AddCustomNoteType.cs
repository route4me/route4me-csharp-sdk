using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add a custom note type to the user account.
        /// </summary>
        public void AddCustomNoteType()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            string customType = "To Do 5";

            string[] customValues = new string[]
            {
                "Pass a package 5", "Pickup package", "Do a service"
            };

            // Run the query
            var response = route4Me.AddCustomNoteType(
                    customType,
                    customValues,
                    out string errorString
                );

            PrintExampleCustomNoteType(response, errorString);

            CustomNoteTypesToRemove = new List<string>();

            if (response != null && response.GetType() == typeof(int))
                CustomNoteTypesToRemove.Add("To Do 5");

            RemoveCustomNoteTypes();
        }
    }
}
