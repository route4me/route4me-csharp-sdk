using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Xunit;

namespace Route4MeSdkV5UnitTest.V5.AddressBarcodes
{
    public class AddressBarcodesTests
    {
        [Fact]
        public void AddressBarcodesGetSaveTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646
            };
            var readResult1 = route4me.GetAddressBarcodes(getAddressBarcodesParameters, out var resultResponse);
            Assert.NotEmpty(readResult1.Data);

            var saveAddressBarcodesResponse = route4me.SaveAddressBarcodes(new SaveAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646,
                Barcodes = new[]
                {
                    new BarcodeDataRequest
                    {
                        Barcode = "TEST2",
                        Latitude = 40.610804,
                        Longitude = -73.920172,
                        TimestampDate = 1634169600,
                        TimestampUtc = 1634198666,
                        ScanType = "picked_up",
                        ScannedAt = "2021-10-15 19:18:11"
                    }
                }
            }, out resultResponse);

            Assert.True(saveAddressBarcodesResponse.status);

            var readResult2 = route4me.GetAddressBarcodes(getAddressBarcodesParameters, out resultResponse);

            Assert.True(readResult2.Data.Length - readResult1.Data.Length == 1);
        }
    }
}