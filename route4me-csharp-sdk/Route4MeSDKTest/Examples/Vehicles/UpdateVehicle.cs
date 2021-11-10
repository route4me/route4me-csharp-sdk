using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle.
        /// </summary>
        public void UpdateVehicle()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestVehcile();

            // TO DO: on this stage specifying of the parameter vehicle_alias is mandatory. Will be checked later
            var vehicleParams = new VehicleV4Parameters()
            {
                VehicleModelYear = 1995,
                VehicleYearAcquired = 2018,
                VehicleMake = "Ford",
                VehicleAxleCount = 2,
                FuelType = "unleaded 93",
                HeightInches = 72,
                WeightLb = 2000
            };

            // Run the query
            var updatedVehicle = route4Me.UpdateVehicle(
                                        vehicleParams,
                                        vehiclesToRemove[vehiclesToRemove.Count - 1],
                                        out string errorString);

            PrintTestVehciles(updatedVehicle, errorString);

            RemoveTestVehicles();
        }
    }
}