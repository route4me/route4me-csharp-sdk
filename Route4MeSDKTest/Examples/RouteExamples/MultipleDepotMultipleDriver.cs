﻿using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public DataObject MultipleDepotMultipleDriver()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);
   
      // Prepare the addresses
      Address[] addresses = new Address[]
      {
        #region Addresses

        new Address() { AddressString   = "3634 W Market St, Fairlawn, OH 44333",
                        //all possible originating locations are depots, should be marked as true
                        //stylistically we recommend all depots should be at the top of the destinations list
                        IsDepot         = true, 
                        Latitude        = 41.135762259364,
                        Longitude       = -81.629313826561,
                        
                        //the number of seconds at destination
                        Time            = 300,
                        
                        //together these two specify the time window of a destination
                        //seconds offset relative to the route start time for the open availability of a destination
                        TimeWindowStart = 28800,  
                        
                        //seconds offset relative to the route end time for the open availability of a destination
                        TimeWindowEnd   = 29465},

        new Address() { AddressString   = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.135762259364,
                        Longitude       = -81.629313826561,
                        Time            = 300,
                        TimeWindowStart = 29465,
                        TimeWindowEnd   = 30529},

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 300,
                        TimeWindowStart = 30529,
                        TimeWindowEnd   = 33779},

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 100,
                        TimeWindowStart = 33779,
                        TimeWindowEnd   = 33944},

        new Address() { AddressString   = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.162971496582,
                        Longitude       = -81.479049682617,
                        Time            = 300,
                        TimeWindowStart = 33944,
                        TimeWindowEnd   = 34801},

        new Address() { AddressString   = "1659 Hibbard Dr, Stow, OH 44224",
                        Latitude        = 41.194505989552,
                        Longitude       = -81.443351581693,
                        Time            = 300,
                        TimeWindowStart = 34801,
                        TimeWindowEnd   = 36366},

        new Address() { AddressString   = "2705 N River Rd, Stow, OH 44224",
                        Latitude        = 41.145240783691,
                        Longitude       = -81.410247802734,
                        Time            = 300,
                        TimeWindowStart = 36366,
                        TimeWindowEnd   = 39173},

        new Address() { AddressString   = "10159 Bissell Dr, Twinsburg, OH 44087",
                        Latitude        = 41.340042114258,
                        Longitude       = -81.421226501465,
                        Time            = 300,
                        TimeWindowStart = 39173,
                        TimeWindowEnd   = 41617},

        new Address() { AddressString   = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude        = 41.148578643799,
                        Longitude       = -81.429229736328,
                        Time            = 300,
                        TimeWindowStart = 41617,
                        TimeWindowEnd   = 43660},

        new Address() { AddressString   = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude        = 41.148578643799,
                        Longitude       = -81.429229736328,
                        Time            = 300,
                        TimeWindowStart = 43660,
                        TimeWindowEnd   = 46392},

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 300,
                        TimeWindowStart = 46392,
                        TimeWindowEnd   = 48389},

        new Address() { AddressString   = "559 W Aurora Rd, Northfield, OH 44067",
                        Latitude        = 41.315116882324,
                        Longitude       = -81.558746337891,
                        Time            = 50,
                        TimeWindowStart = 48389,
                        TimeWindowEnd   = 48449},

        new Address() { AddressString   = "3933 Klein Ave, Stow, OH 44224",
                        Latitude        = 41.169467926025,
                        Longitude       = -81.429420471191,
                        Time            = 300,
                        TimeWindowStart = 48449,
                        TimeWindowEnd   = 50152},

        new Address() { AddressString   = "2148 8th St, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.136692047119,
                        Longitude       = -81.493492126465,
                        Time            = 300,
                        TimeWindowStart = 50152,
                        TimeWindowEnd   = 51982},

        new Address() { AddressString   = "3731 Osage St, Stow, OH 44224",
                        Latitude        = 41.161357879639,
                        Longitude       = -81.42293548584,
                        Time            = 100,
                        TimeWindowStart = 51982,
                        TimeWindowEnd   = 52180},

        new Address() { AddressString   = "3731 Osage St, Stow, OH 44224",
                        Latitude        = 41.161357879639,
                        Longitude       = -81.42293548584,
                        Time            = 300,
                        TimeWindowStart = 52180,
                        TimeWindowEnd   = 54379}

        #endregion
      };

      // Set parameters
      RouteParameters parameters = new RouteParameters()
      {
        //specify capacitated vehicle routing with time windows and multiple depots, with multiple drivers
        AlgorithmType = AlgorithmType.CVRP_TW_MD,
        
        //set an arbitrary route name
        //this value shows up in the website, and all the connected mobile device
        RouteName     = "Multiple Depot, Multiple Driver",

        //the route start date in UTC, unix timestamp seconds (Tomorrow)
        RouteDate     = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
        //the time in UTC when a route is starting (7AM)
        RouteTime     = 60 * 60 * 7,
        
        //the maximum duration of a route
        RouteMaxDuration     = 86400,
        VehicleCapacity      = "1",
        VehicleMaxDistanceMI = "10000",

        Optimize     = Optimize.Distance.Description(),
        DistanceUnit = DistanceUnit.MI.Description(),
        DeviceType   = DeviceType.Web.Description(),
        TravelMode   = TravelMode.Driving.Description(),
        Metric       = Metric.Geodesic
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
      PrintExampleOptimizationResult("MultipleDepotMultipleDriver", dataObject, errorString);

      return dataObject;
    }
  }
}
