# Route4Me C# SDK

## Installation and Usage

1. Add reference to Route4MeSDKLibrary.dll
2. Use the class Route4MeSDK.Route4MeManager for accessing the Route4ME API
3. Use methods Route4MeManager.GetRoute(), Route4MeManager.UpdateOptimization() etc. to access the main functionality of Route4Me API.
4. Use generic methods Route4MeManager.GetStringResponseFromAPI() and Route4MeManager.GetJsonObjectFromAPI<T>() for accessing any Route4Me API functionally via custom defined classes (see example in Route4MeSDKTest.SingleDriverRoundTripGeneric.cs)

## Examples and Tests

1. See project Route4MeSDKTest (class Route4MeSDKTest.Examples) for some examples of using Route4MeSDKLibrary
2. See an example of creating a simple route below

## Creating a Simple Route

``` C#
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void SingleDriverRoundTrip()
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

        RouteTime            = 0,
        RouteMaxDuration     = 86400,
        VehicleCapacity      = "1",
        VehicleMaxDistanceMI = "10000",

        Optimize     = Optimize.Distance.Description(),
        DistanceUnit = DistanceUnit.MI.Description(),
        DeviceType   = DeviceType.Web.Description(),
        TravelMode   = TravelMode.Driving.Description(),
      };

      OptimizatonParameters optimizatonParameters = new OptimizatonParameters()
      {
        Addresses = addresses,
        Parameters = parameters
      };

      // Run the query
      string errorString;
      DataObject dataObject = route4Me.RunOptimization(optimizatonParameters, out errorString);

      // Output the result
      PrintExampleOptimizationResult("SingleDriverRoundTrip", dataObject, errorString);
    }
  }
}

```
