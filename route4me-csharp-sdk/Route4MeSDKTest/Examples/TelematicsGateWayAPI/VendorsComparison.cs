using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of comparing selected vendors. 
        /// </summary>
        public void VendorsComparison()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendorParameters = new TelematicsVendorParameters()
            {
                Vendors = "55,56,57"
            };

            // Run the query
            var vendors = route4Me.SearchTelematicsVendors(vendorParameters,
                                                           out string errorString);

            PrintExampleTelematicsVendor(vendors, errorString);
        }
    }
}
