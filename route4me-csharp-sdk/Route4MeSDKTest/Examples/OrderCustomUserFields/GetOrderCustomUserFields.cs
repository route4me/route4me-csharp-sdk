namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the order custom user fields.
        /// </summary>
        public void GetOrderCustomUserFields()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderCustomUserFields = route4Me
                .GetOrderCustomUserFields(out string errorString);

            PrintOrderCustomField(orderCustomUserFields, errorString);
        }
    }
}
