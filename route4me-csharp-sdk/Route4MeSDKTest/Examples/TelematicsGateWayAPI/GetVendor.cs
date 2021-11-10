using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetVendor()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendors = route4Me.GetAllTelematicsVendors(
                                        new TelematicsVendorParameters(),
                                        out string errorString);

            int randomNumber = (new Random()).Next(0, vendors.Vendors.Length - 1);

            string randomVendorID = vendors.Vendors[randomNumber].ID;

            var vendorParameters = new TelematicsVendorParameters()
            {
                VendorID = Convert.ToUInt32(randomVendorID)
            };

            var vendor = route4Me.GetTelematicsVendor(vendorParameters, out errorString);

            PrintExampleTelematicsVendor(vendor, errorString);
        }
    }
}
