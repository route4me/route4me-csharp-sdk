using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Vehicles List
        /// </summary>
        public void GetVehicles()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var vehicleParameters = new VehicleParameters
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            DataTypes.V5.Vehicle[] vehicles = route4Me.GetVehicles(vehicleParameters, out string errorString);

            PrintTestVehciles(vehicles, errorString);
        }
    }
}
