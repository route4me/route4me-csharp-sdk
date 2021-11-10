using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a new vehicle.
        /// </summary>
        public void CreatetVehicle()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var class7TruckParams = new VehicleV4Parameters()
            {
                VehicleName = "FORD F750",
                VehicleAlias = "FORD F750",
                VehicleVin = "1NPAX6EX2YD550743",
                VehicleLicensePlate = "FFV9547",
                VehicleModel = "F-750",
                VehicleModelYear = 2010,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleTypeID = "livestock_carrier",
                VehicleAxleCount = 2,
                MpgCity = 8,
                MpgHighway = 15,
                FuelType = "diesel",
                HeightInches = 96,
                HeightMetric = 244,
                WeightLb = 26000,
                MaxWeightPerAxleGroupInPounds = 15000,
                MaxWeightPerAxleGroupMetric = 6800,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 312,
                LengthMetric = 793,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "YES",
                DividedHighwayAvoidPreference = "FAVOR",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "26_STRAIGHT_TRUCK",
                TollRoadUsage = "ALWAYS_AVOID",
                InternationalBordersOpen = "NO",
                PurchasedNew = true
            };

            // Run the query
            var result = route4Me.CreateVehicle(class7TruckParams, out string errorString);

            PrintTestVehciles(result, errorString);

            if (result != null && result.GetType() == typeof(VehicleV4CreateResponse))
            {
                Console.WriteLine("The test vehicle {0} created successfully.", result.VehicleGuid);

                vehiclesToRemove.Add(result.VehicleGuid);

                RemoveTestVehicles();
            }
        }
    }
}