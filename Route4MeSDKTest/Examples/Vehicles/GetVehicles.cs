using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// GEt Vehicles List
        /// </summary>
        public void GetVehicles()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            VehicleParameters vehicleParameters = new VehicleParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            string errorString = "";
            VehicleResponse[] vehicles = route4Me.GetVehicles(vehicleParameters, out errorString);

            Console.WriteLine("");

            if (vehicles != null)
            {
                Console.WriteLine("GetVehicles executed successfully, {0} vehicles returned", vehicles.Length);
                Console.WriteLine("");

                foreach (VehicleResponse vehicle in vehicles)
                {
                    Console.WriteLine("Vehicle ID: {0}", vehicle.VehicleId);
                }

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("GetVehicles error: {0}", errorString);
            }
        }
    }
}