using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// This example demonstares how to use the API in a generic way, not bounded to the proposed Route4MeManager shortcucts
    /// For the same functionality using shortcuts check the Route4MeExamples.SingleDriverRoundTrip()
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        #region Types

        // Inherit from GenericParameters and add any JSON serializable content
        // Marked with attribute [DataMember]
        [DataContract]
        private class MyAddressAndParametersHolder : GenericParameters
        {
            [DataMember]
            public Address[] addresses { get; set; } // Using the defined class "Address", can use user-defined class instead

            [DataMember]
            public RouteParameters parameters { get; set; } // Using the defined "RouteParameters", can use user-defined class instead
        }

        // Generic class for returned JSON holder
        [DataContract]
        private class MyDataObjectGeneric
        {
            [DataMember(Name = "optimization_problem_id")]
            public string OptimizationProblemId { get; set; }

            [DataMember(Name = "state")]
            public int MyState { get; set; }

            [DataMember(Name = "addresses")]
            public Address[] Addresses { get; set; } // Using the defined class "Address", can use user-defined class instead

            [DataMember(Name = "parameters")]
            public RouteParameters Parameters { get; set; } // Using the defined "RouteParameters", can use user-defined class instead
        }

        #endregion

        #region Methods

        public void SingleDriverRoundTripGeneric()
        {
            const string uri = R4MEInfrastructureSettings.MainHost + "/api.v4/optimization_problem.php";
            string myApiKey = DemoApiKey;

            // Create the manager with the api key
            var route4Me = new Route4MeManager(myApiKey);

            // Prepare the addresses
            // Using the defined class, can use user-defined class instead
            var addresses = new Address[]
             {
        #region Addresses

        new Address() { AddressString = "754 5th Ave New York, NY 10019",
                        Alias         = "Bergdorf Goodman",
                        IsDepot       = true,
                        Latitude      = 40.7636197,
                        Longitude     = -73.9744388,
                        Time          = 0 },

        new Address() { AddressString = "717 5th Ave New York, NY 10022",
                        Alias         = "Giorgio Armani",
                        Latitude      = 40.7669692,
                        Longitude     = -73.9693864,
                        Time          = 0 },

        new Address() { AddressString = "888 Madison Ave New York, NY 10014",
                        Alias         = "Ralph Lauren Women's and Home",
                        Latitude      = 40.7715154,
                        Longitude     = -73.9669241,
                        Time          = 0 },

        new Address() { AddressString = "1011 Madison Ave New York, NY 10075",
                        Alias         = "Yigal Azrou'l",
                        Latitude      = 40.7772129,
                        Longitude     = -73.9669,
                        Time          = 0 },

         new Address() { AddressString = "440 Columbus Ave New York, NY 10024",
                        Alias         = "Frank Stella Clothier",
                        Latitude      = 40.7808364,
                        Longitude     = -73.9732729,
                        Time          = 0 },

        new Address() { AddressString = "324 Columbus Ave #1 New York, NY 10023",
                        Alias         = "Liana",
                        Latitude      = 40.7803123,
                        Longitude     = -73.9793079,
                        Time          = 0 },

        new Address() { AddressString = "110 W End Ave New York, NY 10023",
                        Alias         = "Toga Bike Shop",
                        Latitude      = 40.7753077,
                        Longitude     = -73.9861529,
                        Time          = 0 },

        new Address() { AddressString = "555 W 57th St New York, NY 10019",
                        Alias         = "BMW of Manhattan",
                        Latitude      = 40.7718005,
                        Longitude     = -73.9897716,
                        Time          = 0 },

        new Address() { AddressString = "57 W 57th St New York, NY 10019",
                        Alias         = "Verizon Wireless",
                        Latitude      = 40.7558695,
                        Longitude     = -73.9862019,
                        Time          = 0 },

                 #endregion
             };

            // Set parameters
            // Using the defined class, can use user-defined class instead
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
            };

            var myParameters = new MyAddressAndParametersHolder()
            {
                addresses = addresses,
                parameters = parameters
            };

            // Run the query
            MyDataObjectGeneric dataObject = route4Me
                      .GetJsonObjectFromAPI<MyDataObjectGeneric>(
                                              myParameters,
                                              uri,
                                              HttpMethodType.Post,
                                              out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("SingleDriverRoundTripGeneric executed successfully");
                Console.WriteLine("");

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                Console.WriteLine("State: {0}", dataObject.MyState);
                Console.WriteLine("");

                dataObject.Addresses.ForEach(address =>
                {
                    Console.WriteLine("Address: {0}", address.AddressString);
                    Console.WriteLine("Route ID: {0}", address.RouteId);
                });
            }
            else
            {
                Console.WriteLine("SingleDriverRoundTripGeneric error {0}", errorString);
            }

            RemoveTestOptimizations();
        }

        #endregion
    }
}
