using System.ComponentModel;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Algorithm type
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum AlgorithmType
  {
    TSP        = 1,
    VRP        = 2,
    CVRP_TW_SD = 3,
    CVRP_TW_MD = 4,
    TSP_TW     = 5,
    TSP_TW_CR  = 6,
    BBCVRP     = 7,
  }

  /// <summary>
  /// The travel mode enum
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum TravelMode : uint
  {
    [Description("Driving")]
    Driving,

    [Description("Walking")]
    Walking,

    [Description("Trucking")]
    Trucking
  }

  /// <summary>
  /// The distance units type
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum DistanceUnit : uint
  {
    [Description("mi")]
    MI,

    [Description("km")]
    KM
  }

  /// <summary>
  /// Enum that specifies which road obstacles to avoid
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum Avoid
  {
    [Description("Highways")]
    Highways,

    [Description("Tolls")]
    Tolls,

    [Description("minimizeHighways")]
    MinimizeHighways,

    [Description("minimizeTolls")]
    MinimizeTolls,

    [Description("")]
    None
  }

  /// <summary>
  /// Enum that specifies the optimization type
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum Optimize : uint
  {
    [Description("Distance")]
    Distance,
      
    [Description("Time")]
    Time,

    [Description("timeWithTraffic")]
    TimeWithTraffic
  }

  /// <summary>
  /// The metric type
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum Metric : uint
  {
    Euclidean = 1,
    Manhattan = 2,
    Geodesic  = 3,
    Matrix    = 4,
    Exact_2D  = 5
  }

  /// <summary>
  /// The type of the device that is planning this route
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  public enum DeviceType
  {
    [Description("web")]
    Web,

    [Description("iphone")]
    IPhone,

    [Description("ipad")]
    IPad,

    [Description("android_phone")]
    AndroidPhone,

    [Description("android_tablet")]
    AndroidTablet
  }

  /// <summary>
  /// Specifies the format in which to return the route data.
  /// This parameter is ignored for now, as only JSON is supported.
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
  /// </summary>
  public enum Format
  {
    [Description("csv")]
    Csv,

    [Description("serialized")]
    Serialized,

    [Description("xml")]
    Xml
  }

  /// <summary>
  /// The optimization state
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
  /// </summary>
  public enum OptimizationState : uint
  {
    Initial             = 1,
    MatrixProcessing    = 2,
    Optimizing          = 3,
    Optimized           = 4,
    Error               = 5,
    ComputingDirections = 6
  }

  /// <summary>
  /// Spesifies whether to return the path points when returning the newly created route
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
  /// </summary>
  public enum RoutePathOutput : uint
  {
    [Description("None")]
    None,

    [Description("Points")]
    Points
  }
}
