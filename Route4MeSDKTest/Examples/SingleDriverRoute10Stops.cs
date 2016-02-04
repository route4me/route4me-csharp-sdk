﻿using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public DataObject SingleDriverRoute10Stops()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Prepare the addresses
      Address[] addresses = new Address[]
      {
        #region Addresses

        new Address() { AddressString = "151 Arbor Way Milledgeville GA 31061",
                        //indicate that this is a departure stop
                        //single depot routes can only have one departure depot 
                        IsDepot = true, 
                        
                        //required coordinates for every departure and stop on the route
                        Latitude = 33.132675170898,
                        Longitude = -83.244743347168,
                        
                        //the expected time on site, in seconds. this value is incorporated into the optimization engine
                        //it also adjusts the estimated and dynamic eta's for a route
                        Time = 0, 
                        
                        
                        //input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest
                        CustomFields = new Dictionary<string, string>() {{"color", "red"}, {"size", "huge"}}
        },

        new Address() { AddressString = "230 Arbor Way Milledgeville GA 31061",
                        Latitude = 33.129695892334,
                        Longitude = -83.24577331543,
                        Time = 0 },

        new Address() { AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                        Latitude = 33.143497,
                        Longitude = -83.224487,
                        Time = 0 },

        new Address() { AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.141784667969,
                        Longitude = -83.237518310547,
                        Time = 0 },

        new Address() { AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.141086578369,
                        Longitude = -83.238258361816,
                        Time = 0 },

        new Address() { AddressString =  "131 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.142036437988,
                        Longitude = -83.238845825195,
                        Time = 0 },

        new Address() { AddressString =  "138 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.14307,
                        Longitude = -83.239334,
                        Time = 0 },

        new Address() { AddressString =  "139 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.142734527588,
                        Longitude = -83.237442016602,
                        Time = 0 },

        new Address() { AddressString =  "145 Bill Johnson Rd NE Milledgeville GA 31061",
                        Latitude = 33.143871307373,
                        Longitude = -83.237342834473,
                        Time = 0 },

        new Address() { AddressString =  "221 Blake Cir Milledgeville GA 31061",
                        Latitude = 33.081462860107,
                        Longitude = -83.208511352539,
                        Time = 0 }

        #endregion
      };

      // Set parameters
      RouteParameters parameters = new RouteParameters()
      {
        AlgorithmType = AlgorithmType.TSP,
        StoreRoute = false,
        RouteName = "Single Driver Route 10 Stops",

        Optimize     = Optimize.Distance.Description(),
        DistanceUnit = DistanceUnit.MI.Description(),
        DeviceType   = DeviceType.Web.Description()
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
      PrintExampleOptimizationResult("SingleDriverRoute10Stops", dataObject, errorString);

      return dataObject;
    }
  }
}