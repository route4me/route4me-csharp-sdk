using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetAllVendors()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendorParameters = new TelematicsVendorParameters();

            var vendors = route4Me.GetAllTelematicsVendors(
                                            vendorParameters, 
                                            out string errorString);

            PrintExampleTelematicsVendor(vendors, errorString);
        }
    }
}
