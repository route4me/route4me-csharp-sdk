using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting all telematics vendors.
        /// </summary>
        public void GetAllVendors()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendorParameters = new TelematicsVendorParameters();

            // Run the query
            var vendors = route4Me.GetAllTelematicsVendors(
                                            vendorParameters,
                                            out string errorString);

            PrintExampleTelematicsVendor(vendors, errorString);
        }
    }
}
