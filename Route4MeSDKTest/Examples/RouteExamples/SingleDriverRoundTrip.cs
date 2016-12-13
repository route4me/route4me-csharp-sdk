﻿using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public DataObject SingleDriverRoundTrip()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Prepare the addresses
      Address[] addresses = new Address[]
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
      RouteParameters parameters = new RouteParameters()
      {
        AlgorithmType = AlgorithmType.TSP,
        StoreRoute    = false,
        RouteName     = "Single Driver Round Trip",

        RouteDate            = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
        RouteTime            = 60 * 60 * 7,
        RouteMaxDuration     = 86400,
        VehicleCapacity      = "1",
        VehicleMaxDistanceMI = "10000",

        Optimize     = Optimize.Distance.Description(),
        DistanceUnit = DistanceUnit.MI.Description(),
        DeviceType   = DeviceType.Web.Description(),
        TravelMode   = TravelMode.Driving.Description(),
      };

      OptimizationParameters optimizationParameters = new OptimizationParameters()
      {
        Addresses = addresses,
        Parameters = parameters
      };

      // Run the query
      string errorString;
      DataObject dataObject = route4Me.RunOptimization(optimizationParameters, out errorString);

      // Output the result
      PrintExampleOptimizationResult("SingleDriverRoundTrip", dataObject, errorString);

      return dataObject;
    }
  }
}
