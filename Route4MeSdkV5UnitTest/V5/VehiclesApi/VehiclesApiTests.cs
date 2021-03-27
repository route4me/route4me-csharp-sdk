using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSdkV5UnitTest.V5;
using Xunit;
using Xunit.Abstractions;

namespace Route4MeSdkV5UnitTest.VehiclesApi
{
    public class VehiclesApiTests : IDisposable
    {
        static string c_ApiKey = ApiKeys.actualApiKey;

        private readonly ITestOutputHelper _output;

        static List<Vehicle> lsVehicles;

        static List<VehicleProfile> lsVehicleProfiles;

        static string preferedUnit;

        public VehiclesApiTests(ITestOutputHelper output)
        {
            _output = output;

            #region Create Test Vehicles

            lsVehicles = new List<Vehicle>();

            var class6TruckParams = new Vehicle()
            {
                VehicleAlias = "GMC TopKick C5500 TST 6",
                VehicleVin = "SAJXA01A06FN08012",
                VehicleLicensePlate = "CVH4561",
                VehicleModel = "TopKick C5500",
                VehicleModelYear = 1995,
                VehicleYearAcquired = 2008,
                VehicleRegCountryId = 223,
                VehicleRegStateId = 12,
                VehicleMake = "GMC",
                VehicleTypeId = "pickup_truck",
                VehicleCostNew = 60000,
                PurchasedNew = true,
                MpgCity = 7,
                MpgHighway = 14,
                FuelConsumptionCity = 7,
                FuelConsumptionHighway = 14,
                FuelType = "diesel",
                LicenseStartDate = "2021-01-01",
                LicenseEndDate = "2031-01-01",
            };

            var class6Truck = createVehicle(class6TruckParams);

            Assert.NotNull(class6Truck);
            Assert.IsType<Vehicle>(class6Truck);

            lsVehicles.Add(class6Truck);

            var class7TruckParams = new Vehicle()
            {
                VehicleAlias = "FORD F750 TST 7",
                VehicleVin = "1NPAX6EX2YD550743",
                VehicleLicensePlate = "FFV9547",
                VehicleModel = "F-750",
                VehicleModelYear = 2010,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleTypeId = "livestock_carrier",
                VehicleCostNew = 60000,
                PurchasedNew = true,
                MpgCity = 7,
                MpgHighway = 14,
                FuelConsumptionCity = 7,
                FuelConsumptionHighway = 14,
                FuelType = "diesel",
                LicenseStartDate = "2021-01-01",
                LicenseEndDate = "2031-01-01",
            };

            var class7Truck = createVehicle(class7TruckParams);

            Assert.NotNull(class7Truck);
            Assert.IsType<Vehicle>(class7Truck);

            lsVehicles.Add(class7Truck);

            #endregion

            #region Create Test Vehicle Profiles

            lsVehicleProfiles = new List<VehicleProfile>();

            var route4me = new Route4MeManagerV5(c_ApiKey);

            preferedUnit = (route4me.GetAccountPreferedUnit(out ResultResponse resultResponse)?.ToLower() ?? "mi");

            var vehProfileParams1 = new VehicleProfile()
            {
                Name = "Heavy Duty - 28 Double Trailer " + DateTime.Now.ToString("yyMMddHHmmss"),
                IsPredefined = false,
                IsDefault = false,
                Height = preferedUnit == "mi" ? 4*3.28 : 4,
                Width = preferedUnit == "mi" ? 2.44*3.28 : 2.44,
                Length = preferedUnit == "mi" ? 12.2*3.28 : 12.2,
                WeightUnits = VehicleWeightUnits.Kilogram.Description(),
                Weight = 20400,
                MaxWeightPerAxle = 15400,
                FuelType = FuelTypes.Unleaded_91.Description(),
                FuelConsumptionCity = 6,
                FuelConsumptionHighway = 12,
                FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.Description(),
                FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.Description()
            };

            var vehProfile1 = route4me.CreateVehicleProfile(vehProfileParams1, out ResultResponse resultResponse2);

            if (vehProfile1 != null && vehProfile1.GetType() == typeof(VehicleProfile) && vehProfile1.VehicleProfileId > 0)
            {
                lsVehicleProfiles.Add(vehProfile1);
            }

            var vehProfileParams2 = new VehicleProfile()
            {
                Name = "Heavy Duty - 40 Straight Truck " + DateTime.Now.ToString("yyMMddHHmmss"),
                HeightUnits = VehicleSizeUnits.Meter.Description(),
                WidthUnits = VehicleSizeUnits.Meter.Description(),
                LengthUnits = VehicleSizeUnits.Meter.Description(),
                IsPredefined = false,
                IsDefault = false,
                Height = 4,
                Width = 2.44,
                Length = 14.6,
                WeightUnits = VehicleWeightUnits.Kilogram.Description(),
                Weight = 36300,
                MaxWeightPerAxle = 15400,
                FuelType = FuelTypes.Unleaded_87.Description(),
                FuelConsumptionCity = 5,
                FuelConsumptionHighway = 10,
                FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.Description(),
                FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.Description()
            };

            var vehProfile2 = route4me.CreateVehicleProfile(vehProfileParams2, out ResultResponse resultResponse3);

            if (vehProfile2 != null && vehProfile2.GetType() == typeof(VehicleProfile) && vehProfile2.VehicleProfileId > 0)
            {
                lsVehicleProfiles.Add(vehProfile2);
            }

            #endregion
        }



        public void Dispose()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsVehicles.Count>0)
            {
                foreach (var veh in lsVehicles)
                {
                    var result = route4Me.DeleteVehicle(veh.VehicleId, out ResultResponse resultResponse);
                }
            }

            if (lsVehicleProfiles.Count > 0)
            {
                foreach (var vehProf in lsVehicleProfiles)
                {
                    var vehProfParams = new VehicleProfileParameters() { VehicleProfileId = vehProf.VehicleProfileId }; 

                    var result = route4Me.DeleteVehicleProfile(vehProfParams, out ResultResponse resultResponse);
                }
            }
        }

        [Fact]
        public void GetVehiclesPaginatedListTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehParams = new VehicleParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var result = route4Me.GetPaginatedVehiclesList(vehParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Vehicle[]>(result);
        }

        [Fact]
        public void CreateVehicleTest()
        {
            var class7TruckParams = new Vehicle()
            {
                VehicleAlias = "FORD F750",
                VehicleVin = "1NPAX6EX2YD550743",
                VehicleLicensePlate = "FFV9547",
                VehicleModel = "F-750",
                VehicleModelYear = 2010,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleTypeId = "livestock_carrier",
                VehicleRegStateId = 12,
                VehicleCostNew = 70000,
                PurchasedNew = false,
                MpgCity = 6,
                MpgHighway = 12,
                FuelConsumptionCity = 6,
                FuelConsumptionHighway = 12,
                FuelType = "diesel",
                LicenseStartDate = "2020-03-01",
                LicenseEndDate = "2028-12-01",
            };

            var class7Truck = createVehicle(class7TruckParams);

            Assert.NotNull(class7Truck);
            Assert.IsType<Vehicle>(class7Truck);

            lsVehicles.Add(class7Truck);
        }

        public Vehicle createVehicle(Vehicle vehicleParams)
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var result = route4Me.CreateVehicle(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(result);

            return result;
        }

        [Fact (Skip = "The endpoint vehicles/assign is enabled for the accounts with the specified features")]
        public void CreateTemporaryVehicle()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var tempVehicleParams = new VehicleTemporary()
            {
                VehicleId = lsVehicles[lsVehicles.Count - 1].VehicleId,
                AssignedMemberId = "1",
                ExpiresAt = "2028-12-01",
                VehicleLicensePlate = "FFV9548",
                ForceAssignment = true
            };

            var result = route4Me.CreateTemporaryVehicle(tempVehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleTemporary>(result);
        }

        [Fact(Skip = "The endpoint vehicles/execute is enabled for the account with the specified features")]
        public void ExecuteVehicleOrder()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehOrderParams = new VehicleOrderParameters()
            {
                VehicleId = lsVehicles[lsVehicles.Count - 1].VehicleId,
                Latitude = 38.247605,
                Longitude = -85.746697
            };

            var result = route4Me.ExecuteVehicleOrder(vehOrderParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleOrderResponse>(result);
        }

        [Fact]
        public void GetLatestVehicleLocationsTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehicleParams = new VehicleParameters()
            {
                VehicleIDs = lsVehicles.Select(x=>x.VehicleId).ToArray()
            };

            var result = route4Me.GetVehicleLocations(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleLocationResponse>(result);
        }

        [Fact]
        public void GetVehicleByIdTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehicleId = lsVehicles[0].VehicleId;

            var result = route4Me.GetVehicleById(vehicleId, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Vehicle>(result);
            Assert.Equal(lsVehicles[0].VehicleId, result.VehicleId);
        }

        [Fact]
        public void GetVehicleTrackByIdTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehicleId = lsVehicles[0].VehicleId;

            var result = route4Me.GetVehicleTrack(vehicleId, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleTrackResponse>(result);
        }

        [Fact]
        public void DeleteVehicleTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehicleId = lsVehicles[lsVehicles.Count - 1].VehicleId;

            var result = route4Me.DeleteVehicle(vehicleId, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Vehicle>(result);

            lsVehicles.Remove(lsVehicles[lsVehicles.Count - 1]);
        }

        [Fact]
        public void GetPaginatedVehicleProfilesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            var result = route4Me.GetVehicleProfiles(profileParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleProfilesResponse>(result);
        }

        [Fact]
        public void CreateVehicleProfileTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehProfileParams3 = new VehicleProfile()
            {
                Name = "Heavy Duty - 48 Semitrailer " + DateTime.Now.ToString("yyMMddHHmmss"),
                Height = preferedUnit == "mi" ? 3.5 * 3.28 : 3.5,
                Width = preferedUnit == "mi" ? 2.5 * 3.28 : 2.5,
                Length = preferedUnit == "mi" ? 16 * 3.28 : 16,
                IsPredefined = false,
                IsDefault = false,
                WeightUnits = VehicleWeightUnits.Kilogram.Description(),
                Weight = 35000,
                MaxWeightPerAxle = 17500,
                FuelType = FuelTypes.Unleaded_87.Description(),
                FuelConsumptionCity = 6,
                FuelConsumptionHighway = 11,
                FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.Description(),
                FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.Description()
            };

            var result = route4Me.CreateVehicleProfile(vehProfileParams3, out ResultResponse resultResponse2);

            Assert.NotNull(result);
            Assert.IsType<VehicleProfile>(result);

            lsVehicleProfiles.Add(result);
        }

        [Fact]
        public void DeleteVehicleProfileTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehProfParams = new VehicleProfileParameters() 
            { 
                VehicleProfileId = lsVehicleProfiles[lsVehicleProfiles.Count-1].VehicleProfileId 
            };

            var result = route4Me.DeleteVehicleProfile(vehProfParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<object>(result);

            lsVehicleProfiles.Remove(lsVehicleProfiles[lsVehicleProfiles.Count - 1]);
        }

        [Fact]
        public void GetVehicleProfileByIdTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehProfParams = new VehicleProfileParameters()
            {
                VehicleProfileId = lsVehicleProfiles[0].VehicleProfileId
            };

            var result = route4Me.GetVehicleProfileById(vehProfParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleProfile>(result);
        }

        [Fact]
        public void GetVehicleByLicensePlateTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var vehParams = new VehicleParameters()
            {
               VehicleLicensePlate = lsVehicles[0].VehicleLicensePlate
            };

            var result = route4Me.GetVehicleByLicensePlate(vehParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleResponse>(result);
            Assert.Equal(lsVehicles[0].VehicleLicensePlate, (result?.Data?.Vehicle?.VehicleLicensePlate ?? null));
            //Assert.IsType<Vehicle>(result);
            //Assert.Equal(lsVehicles[0].VehicleLicensePlate, result.VehicleLicensePlate);
        }

        [Fact(Skip = "The tested method is temporary deprecated")]
        public void SearchVehiclesTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var searchParams = new VehicleSearchParameters()
            {
                VehicleIDs = new string[] {lsVehicles[0].VehicleId},
                Latitude = 29.748868,
                Longitude = -95.358473
            };

            var result = route4Me.SearchVehicles(searchParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Vehicle[]>(result);
        }

        [Fact]
        public void UpdateVehicleTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            lsVehicles[0].VehicleAlias += " Updated";
            lsVehicles[0].VehicleVin = "11111111111111111";

            var result = route4Me.UpdateVehicle(lsVehicles[0], out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<Vehicle>(result);
            Assert.Equal("11111111111111111", result.VehicleVin);
            Assert.Contains("Updated", result.VehicleAlias);
        }

        [Fact]
        public void UpdateVehicleProfileTest()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            lsVehicleProfiles[0].Name += " Updated";
            lsVehicleProfiles[0].FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.Description();
            lsVehicleProfiles[0].FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.Description();

            var result = route4Me.UpdateVehicleProfile(lsVehicleProfiles[0], out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.IsType<VehicleProfile>(result);
            Assert.Equal(FuelConsumptionUnits.MilesPerGallonUS.Description(), result.FuelConsumptionCityUnit);
            Assert.Equal(FuelConsumptionUnits.MilesPerGallonUS.Description(), result.FuelConsumptionHighwayUnit);
            Assert.Contains("Updated", result.Name);
        }
    }
}
