using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void MultipleDepotMultipleDriver()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);
   
      // Prepare the addresses1
      Address[] addresses = new Address[]
      {
        #region Addresses

        new Address() { AddressString = "3634 W Market St, Fairlawn, OH 44333",
                        IsDepot = true,
                        Latitude = 41.135762259364,
                        Longitude = -81.629313826561,
                        Time = 300,
                        TimeWindowStart = 28800,
                        TimeWindowEnd = 29465},

        new Address() { AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                        Latitude = 41.135762259364,
                        Longitude = -81.629313826561,
                        Time = 300,
                        TimeWindowStart = 29465,
                        TimeWindowEnd = 30529},

        new Address() { AddressString = "512 Florida Pl, Barberton, OH 44203",
                        Latitude = 41.003671512008,
                        Longitude = -81.598461046815,
                        Time = 300,
                        TimeWindowStart = 30529,
                        TimeWindowEnd = 33779},

        new Address() { AddressString = "512 Florida Pl, Barberton, OH 44203",
                        Latitude = 41.003671512008,
                        Longitude = -81.598461046815,
                        Time = 300,
                        TimeWindowStart = 33779,
                        TimeWindowEnd = 33944},

        new Address() { AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                        Latitude = 41.162971496582,
                        Longitude = -81.479049682617,
                        Time = 300,
                        TimeWindowStart = 33944,
                        TimeWindowEnd = 34801},

        new Address() { AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                        Latitude = 41.194505989552,
                        Longitude = -81.443351581693,
                        Time = 300,
                        TimeWindowStart = 34801,
                        TimeWindowEnd = 36366},

        new Address() { AddressString = "2705 N River Rd, Stow, OH 44224",
                        Latitude = 41.145240783691,
                        Longitude = -81.410247802734,
                        Time = 300,
                        TimeWindowStart = 36366,
                        TimeWindowEnd = 39173},

        new Address() { AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                        Latitude = 41.340042114258,
                        Longitude = -81.421226501465,
                        Time = 300,
                        TimeWindowStart = 39173,
                        TimeWindowEnd = 41617},

        new Address() { AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude = 41.148578643799,
                        Longitude = -81.429229736328,
                        Time = 300,
                        TimeWindowStart = 41617,
                        TimeWindowEnd = 43660},

        new Address() { AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude = 41.148578643799,
                        Longitude = -81.429229736328,
                        Time = 300,
                        TimeWindowStart = 43660,
                        TimeWindowEnd = 46392},

        new Address() { AddressString = "512 Florida Pl, Barberton, OH 44203",
                        Latitude = 41.003671512008,
                        Longitude = -81.598461046815,
                        Time = 300,
                        TimeWindowStart = 46392,
                        TimeWindowEnd = 48389},

        new Address() { AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                        Latitude = 41.315116882324,
                        Longitude = -81.558746337891,
                        Time = 300,
                        TimeWindowStart = 48389,
                        TimeWindowEnd = 48449},

        new Address() { AddressString = "3933 Klein Ave, Stow, OH 44224",
                        Latitude = 41.169467926025,
                        Longitude = -81.429420471191,
                        Time = 300,
                        TimeWindowStart = 48449,
                        TimeWindowEnd = 50152},

        new Address() { AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                        Latitude = 41.136692047119,
                        Longitude = -81.493492126465,
                        Time = 300,
                        TimeWindowStart = 50152,
                        TimeWindowEnd = 51982},

        new Address() { AddressString = "3731 Osage St, Stow, OH 44224",
                        Latitude = 41.161357879639,
                        Longitude = -81.42293548584,
                        Time = 300,
                        TimeWindowStart = 51982,
                        TimeWindowEnd = 52180},

        new Address() { AddressString = "3731 Osage St, Stow, OH 44224",
                        Latitude = 41.161357879639,
                        Longitude = -81.42293548584,
                        Time = 300,
                        TimeWindowStart = 52180,
                        TimeWindowEnd = 54379}

        #endregion
      };

      // Set parameters
      RouteParameters parameters = new RouteParameters()
      {
        AlgorithmType = AlgorithmType.CVRP_TW_MD,
        RouteName     = "Multiple Depot, Multiple Driver",
        StoreRoute    = false,

        RouteTime            = 0,
        RouteMaxDuration     = 86400,
        VehicleCapacity      = "1",
        VehicleMaxDistanceMI = "10000",

        Optimize     = Optimize.Distance.Description(),
        DistanceUnit = DistanceUnit.MI.Description(),
        DeviceType   = DeviceType.Web.Description(),
        TravelMode   = TravelMode.Driving.Description(),
        Metric       = Metric.Geodesic
      };

      OptimizatonParameters optimizatonParameters = new OptimizatonParameters()
      {
        Addresses = addresses,
        Parameters = parameters
      };

      // Run the query
      string errorString;
      DataObject dataObject = route4Me.RunOptimization(optimizatonParameters, out errorString);

      if (dataObject != null)
      {
        Console.WriteLine("MultipleDepotMultipleDriver executed successfully");
        Console.WriteLine("");

        Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
        Console.WriteLine("State: {0}", dataObject.State);

        dataObject.Addresses.ForEach(address =>
        {
          Console.WriteLine("Address: {0}", address.AddressString);
          Console.WriteLine("Route ID: {0}", address.RouteId);
        });
      }
      else
      {
        // TODO error handling
        Console.WriteLine("MultipleDepotMultipleDriver error: {0}", errorString);
      }
    }
  }
}
