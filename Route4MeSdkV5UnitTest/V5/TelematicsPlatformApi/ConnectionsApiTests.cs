using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes.V5;
using Xunit;
using Xunit.Abstractions;

namespace Route4MeSdkV5UnitTest.V5.TelematicsPlatformApi.Connections
{
    public class ConnectionsApiTests : IDisposable
    {
        static string c_ApiKey = ApiKeys.actualApiKey;

        static List<Connection> lsTelematicsConnetions;

        public ConnectionsApiTests(ITestOutputHelper output)
        {
            lsTelematicsConnetions = new List<Connection>();

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var connectionParams = new ConnectionParameters()
            {
                Vendor = TelematicsVendorType.Tomtom.Description(),
                VendorId = 154,
                Name = "Connection to GeoTab",
                Host = "telematics.tomtom.com/en_au/webfleet/",
                ApiKey = "11111111111111111111111111111111",
                AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                UserName = "John Doe",
                Password = "11111",
                VehiclePositionRefreshRate = 30,
                ValidateRemoteCredentials = false,
                IsEnabled = true,
                Metadata = "string"
            };

            var telematicsConnetion = route4Me.RegisterTelematicsConnection(
                connectionParams,
                out ResultResponse resultResponse);

            Assert.NotNull(telematicsConnetion);
            Assert.IsType<Connection>(telematicsConnetion);

            lsTelematicsConnetions.Add(telematicsConnetion);
        }

        public void Dispose()
        {
            if (lsTelematicsConnetions.Count < 1) return;

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            foreach (Connection connectionToken in lsTelematicsConnetions)
            {
                var connectionParams = new ConnectionParameters()
                {
                    ConnectionToken = connectionToken.ConnectionToken
                };

                var result = route4Me.DeleteTelematicsConnection(connectionParams, out ResultResponse resultResponse);

                Assert.NotNull(result);
                Assert.IsType<Connection>(result);
            }

            lsTelematicsConnetions = new List<Connection>();
        }

        [Fact]
        public void GetTelematicsConnectionsTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.GetTelematicsConnections(out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Connection[]>(result);
        }

        [Fact]
        public void GetTelematicsConnectionByTokenTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var connectionParams = new ConnectionParameters()
            {
                ConnectionToken = lsTelematicsConnetions[0].ConnectionToken
            };

            var result = route4Me.GetTelematicsConnectionByToken(connectionParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Connection>(result);
        }

        [Fact]
        public void RegisterTelematicsConnectionTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var connectionParams = new ConnectionParameters()
            {
                Vendor = TelematicsVendorType.Geotab.Description(),
                VendorId = 154,
                Name = "Connection to GeoTab",
                Host = "https://www.geotab.com",
                ApiKey = "11111111111111111111111111111111",
                AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                UserName = "John Doe",
                Password = "11111",
                VehiclePositionRefreshRate = 30,
                ValidateRemoteCredentials = false,
                IsEnabled = true,
                Metadata = "string"
            };

            var result = route4Me.RegisterTelematicsConnection(
                connectionParams, 
                out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Connection>(result);

            lsTelematicsConnetions.Add(result);
        }

        [Fact]
        public void DeleteTelematicsConnectionTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var connectionParams = new ConnectionParameters()
            {
                ConnectionToken = lsTelematicsConnetions[lsTelematicsConnetions.Count - 1].ConnectionToken
            };

            var result = route4Me.DeleteTelematicsConnection(connectionParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Connection>(result);

            lsTelematicsConnetions.RemoveAt(lsTelematicsConnetions.Count - 1);
        }

        [Fact]
        public void UpdateTelematicsConnectionTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var connectionParams = new ConnectionParameters()
            {
                ConnectionToken = lsTelematicsConnetions[lsTelematicsConnetions.Count - 1].ConnectionToken,
                Vendor = TelematicsVendorType.Geotab.Description(),
                VendorId = 154,
                Name = "Connection to GeoTab",
                ApiKey = "11111111111111111111111111111111",
                AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                UserName = "John Doe",
                Password = "11111",
                VehiclePositionRefreshRate = 60,
                ValidateRemoteCredentials = false,
                IsEnabled = true,
                Metadata = "string"
            };

            var result = route4Me.RegisterTelematicsConnection(
                connectionParams,
                out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Connection>(result);
        }
    }
}
