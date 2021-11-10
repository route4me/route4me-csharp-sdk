using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of searching by query text. 
        /// </summary>
        public void SearchVendors()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendorParameters = new TelematicsVendorParameters()
            {
                //Country = "GB",  // uncomment this line for searching by Country
                IsIntegrated = 1,
                //Feature = "Satelite",  // uncomment this line for searching by Feature
                Search = "Fleet",
                Page = 1,
                PerPage = 15
            };

            // Run the query
            var vendors = route4Me.SearchTelematicsVendors(vendorParameters,
                                                           out string errorString);

            PrintExampleTelematicsVendor(vendors, errorString);
        }
    }
}
