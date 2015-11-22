# Route4Me Route Optimization SaaS C# SDK


### What does the Route4Me SDK permit me to do?
This SDK makes it easier for you use the Route4Me API, which creates optimally sequenced driving routes for many drivers.

### Who can use the Route4Me SDK (and API)?
The service is typically used by organizations who must route many drivers to many destinations. In addition to route optimization for new (future) routes, the API can also be used to analyze historical routes, and to distribute routes to field personnel.

### Who is prohibited from using the Route4Me SDK (and API)?
The Route4Me SDK and API cannot be resold or used in a product or system that competes directly with Route4Me. This means that developers cannot resell route optimization services to other businesses or developers. However, developers can integrate our route optimization SDK/API into their software applications. Developers and startups are also permitted to use our software for internal purposes (i.e. a same day delivery startup).

### Do optimized routes automatically appear inside my Route4Me account?
Every Route4Me SDK instance needs a unique API key. The API key can be retrieved inside your Route4Me.com account, inside the Settings tab called API.

### Can I test the SDK with other addresses without a valid API Key?
No. The sample API key only permits you to optimize routes with the sample address coordinates that are part of this SDK.

### Does the SDK have rate limits?
The number of requests you can make per second is limited by your current subscription plan. Typically, there are different rate limits for these core features:
Address Geocoding & Address Reverse Geocoding
Route Optimization & Management
Viewing a Route


### What is the recommended architecture for the Route4Me SDK?
There are two typical integration strategies that we recommend.  Using this SDK, you can make optimization requests and then the SDK polls the Route4Me API to detect state changes as the optimization progresses. Alternatively, you can provide a webhook/callback url, and the API will notify that callback URL every time there is a state change.

### I don't need route management or mobile capabilities. Is there a lower level Route4Me API just for the optimization engine?
Yes. Please contact support@route4me.com to learn about the low-level RESTful API.

### How fast is the route Route4Me Optimization Web Service?
Most routes having less than 200 destinations are optimized in 1 second or less.

### Can I disable optimization when planning routes?
Yes. You can send routes with optimization disabled if you want to conveniently see them on a map, or distribute them to your drivers in the order you prefer.

### Can the API be used for aerial vehicles such as drones or self-driving cars?
Yes. The API can accept lat/lng and an unlimited amount of per-address metadata. The metadata will be preserved as passthrough data by our API, so that the receiving device will have access to critical data when our API invokes a webhook callback to the device.

### Are all my optimized stored permanently stored in the Route4Me database?
Yes. All routes are permanently stored from the database and are no longer accessible to you after your subscription is terminated.


### Installation and Usage

1. Add reference to Route4MeSDKLibrary.dll
2. Use the class Route4MeSDK.Route4MeManager for accessing the Route4ME API
3. Use methods Route4MeManager.GetRoute(), Route4MeManager.UpdateOptimization() etc. to access the main functionality of Route4Me API.
4. Use generic methods Route4MeManager.GetStringResponseFromAPI() and Route4MeManager.GetJsonObjectFromAPI<T>() for accessing any Route4Me API functionally via custom defined classes (see example in Route4MeSDKTest.SingleDriverRoundTripGeneric.cs)

### Examples and Tests

1. See project Route4MeSDKTest (class Route4MeSDKTest.Examples) for some examples of using Route4MeSDKLibrary
2. See an example of creating a simple route below

### Creating a Simple Route

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
                        //designate the point of origination in the route
                        //single driver routes should have one point of origin (single depot)
                        //multiple depot routes can have multiple addresses marked as depots
                        //and the optimization algorithm will determine the optimal departure depot
                        //for each generated route
                        IsDepot       = true, 
                        Latitude      = 40.7636197, //all addresses must be properly geocoded
                        Longitude     = -73.9744388, //all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "717 5th Ave New York, NY 10022",
                        Alias         = "Giorgio Armani",
                        Latitude      = 40.7669692, //all addresses must be properly geocoded
                        Longitude     = -73.9693864, //all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "888 Madison Ave New York, NY 10014",
                        Alias         = "Ralph Lauren Women's and Home",
                        Latitude      = 40.7715154, //all addresses must be properly geocoded
                        Longitude     = -73.9669241, //all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "1011 Madison Ave New York, NY 10075",
                        Alias         = "Yigal Azrou'l",
                        Latitude      = 40.7772129, //all addresses must be properly geocoded
                        Longitude     = -73.9669, //all addresses must be properly geocoded
                        Time          = 0 },

         new Address() { AddressString = "440 Columbus Ave New York, NY 10024",
                        Alias         = "Frank Stella Clothier",
                        Latitude      = 40.7808364, //all addresses must be properly geocoded
                        Longitude     = -73.9732729,//all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "324 Columbus Ave #1 New York, NY 10023",
                        Alias         = "Liana",
                        Latitude      = 40.7803123,//all addresses must be properly geocoded
                        Longitude     = -73.9793079,//all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "110 W End Ave New York, NY 10023",
                        Alias         = "Toga Bike Shop",
                        Latitude      = 40.7753077,//all addresses must be properly geocoded
                        Longitude     = -73.9861529,//all addresses must be properly geocoded
                        Time          = 0 }, 

        new Address() { AddressString = "555 W 57th St New York, NY 10019",
                        Alias         = "BMW of Manhattan",
                        Latitude      = 40.7718005,//all addresses must be properly geocoded
                        Longitude     = -73.9897716,//all addresses must be properly geocoded
                        Time          = 0 },

        new Address() { AddressString = "57 W 57th St New York, NY 10019",
                        Alias         = "Verizon Wireless",
                        Latitude      = 40.7558695,//all addresses must be properly geocoded
                        Longitude     = -73.9862019,//all addresses must be properly geocoded
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
