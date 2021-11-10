namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove custom note type from the user account.
        /// </summary>
        public void RemoveCustomNoteType()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateCustomNoteType();

            int? customNoteTypeId = GetCustomNoteIdByName("To Do 5");

            if (customNoteTypeId == null) return;

            // Run the query
            var response = route4Me.RemoveCustomNoteType(
                (int)customNoteTypeId,
                out string errorString);

            PrintExampleCustomNoteType(response, errorString);
        }
    }
}
