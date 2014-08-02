using System.ComponentModel;

namespace Route4MeSDK.DataTypes
{
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

  public enum TravelMode : uint
  {
    [Description("Driving")]
    Driving,

    [Description("Walking")]
    Walking,

    [Description("Trucking")]
    Trucking
  }

  public enum DistanceUnit : uint
  {
    [Description("mi")]
    MI,

    [Description("km")]
    KM
  }

  public enum Avoid
  {
    [Description("Highways")]
    Highways,

    [Description("Highways")]
    Tolls,

    [Description("minimizeHighways")]
    MinimizeHighways,

    [Description("minimizeTolls")]
    MinimizeTolls,

    [Description("")]
    None
  }

  public enum Optimize : uint
  {
    [Description("Distance")]
    Distance,
      
    [Description("Time")]
    Time,

    [Description("timeWithTraffic")]
    TimeWithTraffic
  }

  public enum Metric : uint
  {
    Euclidean = 1,
    Manhattan = 2,
    Geodesic  = 3,
    Matrix    = 4,
    Exact_2D  = 5
  }

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
    android_tablet
  }

  public enum Format
  {
    [Description("csv")]
    Csv,

    [Description("serialized")]
    Serialized,

    [Description("xml")]
    Xml
  }

  public enum OptimizationState : uint
  {
    Initial             = 1,
    MatrixProcessing    = 2,
    Optimizing          = 3,
    Optimized           = 4,
    Error               = 5,
    ComputingDirections = 6
  }

  public enum RoutePathOutput : uint
  {
    [Description("None")]
    None,

    [Description("Points")]
    Points
  }
}
