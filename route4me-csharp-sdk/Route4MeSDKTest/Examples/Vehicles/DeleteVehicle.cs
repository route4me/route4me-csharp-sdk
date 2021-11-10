using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting a vehicle.
        /// </summary>
        public void DeleteVehicle()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestVehcile();

            var vehicleParams = new VehicleV4Parameters()
            {
                VehicleId = vehiclesToRemove[vehiclesToRemove.Count - 1]
            };

            // Run the query
            var vehicles = route4Me.DeleteVehicle(vehicleParams, out string errorString);

            PrintTestVehciles(vehicles, errorString);
        }
    }
}