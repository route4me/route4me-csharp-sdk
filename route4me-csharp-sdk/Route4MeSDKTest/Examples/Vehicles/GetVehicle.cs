using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle using the vehicleID path parameter.
        /// </summary>
        public void GetVehicle()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestVehcile();

            var vehicleParams = new VehicleParameters()
            {
                VehicleId = vehiclesToRemove[vehiclesToRemove.Count - 1]
            };

            // Run the query
            var vehicle = route4Me.GetVehicle(vehicleParams, out string errorString);

            PrintTestVehciles(vehicle, errorString);

            RemoveTestVehicles();
        }
    }
}