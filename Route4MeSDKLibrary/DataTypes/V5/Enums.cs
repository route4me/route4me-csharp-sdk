using System.ComponentModel;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Enumeration of the algorithm types:
    /// <para>TSP = 1, single depot, single driver route</para>
    /// <para>VRP = 2, single depot, multiple driver, no constraints, no time windows, no capacities</para>
    /// <para>CVRP_TW_SD = 3, single depot, multiple driver, capacitated, time windows</para>
    /// <para>CVRP_TW_MD = 4, multiple depot, multiple driver, capacitated, time windows</para>
    /// <para>TSP_TW = 5, single depot, single driver, time windows</para>
    /// <para>TSP_TW_CR = 6, single depot, single driver, time windows, continuous optimization (minimal location shifting)</para>
    /// <para>BBCVRP = 7, shifts addresses from one route to another over time on a recurring schedule</para>
    /// <para>ALG_NONE = 100</para>
    /// <para>ALG_LEGACY_DISTRIBUTED = 101</para>
    /// </summary>
    public enum AlgorithmType
    {
        STATE_NEW = 0,
        TSP = 1,
        VRP = 2,
        CVRP_TW_SD = 3,
        CVRP_TW_MD = 4,
        TSP_TW = 5,
        TSP_TW_CR = 6,
        OPTIMIZATION_STATE_IN_QUEUE = 7,
        ADVANCED_CVRP_TW = 9,
        ALG_NONE = 100,
        ALG_LEGACY_DISTRIBUTED = 101
    }

    /// <summary>
    /// Enumeration of the travel modes
    /// </summary>
    public enum TravelMode : uint
    {
        [Description("Driving")]
        Driving,

        [Description("Walking")]
        Walking
    }

    /// <summary>
    /// Enumeration of the distance units
    /// </summary>
    public enum DistanceUnit : uint
    {
        [Description("mi")]
        MI,

        [Description("km")]
        KM
    }

    /// <summary>
    /// Enumeration of the avoidance conditions
    /// </summary>
    public enum Avoid
    {
        [Description("")]
        None,

        [Description("minimizeHighways")]
        MinimizeHighways,

        [Description("minimizeTolls")]
        MinimizeTolls,

        [Description("Highways")]
        Highways,

        [Description("Tolls")]
        Tolls,

        [Description("highways,tolls")]
        HighwaysTolls
    }

    /// <summary>
    /// Enumeration of the optimization options
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
    /// Enumeration of the metric systems:
    /// <para>Euclidean = 1, measures point to point distance as a straight line</para>
    /// <para>Manhattan = 2, measures point to point distance as taxicab geometry line</para>
    /// <para>Geodesic = 3, measures point to point distance approximating curvature of the earth</para>
    /// <para>Matrix = 4, measures point to point distance by traversing the actual road network</para>
    /// <para>Exact_2D = 5, measures point to point distance using 2d rectilinear distance</para>
    /// </summary>
    public enum Metric : uint
    {
        Euclidean = 1,
        Manhattan = 2,
        Geodesic = 3,
        Matrix = 4,
        Exact_2D = 5
    }

    /// <summary>
    /// Enumeration of the input device types
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
    /// Enumeration of the response formats
    /// </summary>
    public enum Format
    {
        [Description("csv")]
        Csv,

        [Description("serialized")]
        Serialized,

        [Description("xml")]
        Xml,

        [Description("json")]
        Json
    }

    /// <summary>
    /// Enumeration of the optimization states.
    /// <remark>
    /// <para>An optimization problem can be at one state at any given time.</para>
    /// <para>Every state change invokes a socket notification associated member ID.</para>
    /// <para>Every state change invokes a callback webhook event invocation if it was provided during the initial optimization.</para>
    /// </remark>
    /// </summary>
    public enum OptimizationState : uint
    {
        Initial = 1,
        MatrixProcessing = 2,
        Optimizing = 3,
        Optimized = 4,
        Error = 5,
        ComputingDirections = 6
    }

    /// <summary>
    /// Route path output.
    /// <para>If the actual polylines of the driving path between all the stops on the route should be returned</para>
    /// </summary>
    public enum RoutePathOutput : uint
    {
        [Description("None")]
        None,

        [Description("Points")]
        Points
    }

    /// <summary>
    /// Enumeration of the route destination update types
    /// </summary>
    public enum StatusUpdateType
    {
        [Description("pickup")]
        Pickup,

        [Description("dropoff")]
        DropOff,

        [Description("noanswer")]
        NoAnswer,

        [Description("notfound")]
        NotFound,

        [Description("notpaid")]
        NotPaid,

        [Description("paid")]
        Paid,

        [Description("wrongdelivery")]
        WrongDelivery,

        [Description("wrongaddressrecipient")]
        WrongAddressRecipient,

        [Description("notpresent")]
        NotPresent,

        [Description("parts_missing")]
        PartsMissing,

        [Description("service_rendered")]
        ServiceRendered,

        [Description("follow_up")]
        FollowUp,

        [Description("left_information")]
        LeftInformation,

        [Description("spoke_with_decision_maker")]
        SpokeWithDecisionMaker,

        [Description("spoke_with_decision_influencer")]
        SpokeWithDecisionInfluencer,

        [Description("competitive_account")]
        CompetitiveAccount,

        [Description("scheduled_follow_up_meeting")]
        ScheduledFollowUpMeeting,

        [Description("scheduled_lunch")]
        ScheduledLunch,

        [Description("scheduled_product_demo")]
        ScheduledProductDemo,

        [Description("scheduled_clinical_demo")]
        ScheduledClinicalDemo,

        [Description("no_opportunity")]
        NoOpportunity
    }

    /// <summary>
    /// Enumeration of the territory types
    /// </summary>
    public enum TerritoryType : uint
    {
        [Description("circle")]
        Circle,

        [Description("poly")]
        Poly,

        [Description("rect")]
        Rect
    }

    #region Address Bundling

    /// <summary>
    /// Enumeration of the address bundling mode:
    /// <para>Address = 1, group locations by address</para>
    /// <para>Coordinates = 2, group locations by coordinates</para>
    /// <para>AddressId = 3, group locations by list of the address IDs</para>
    /// <para>Address = 4, group locations by address custom fields</para>
    /// </summary>
    public enum AddressBundlingMode : uint
    {
        Address = 1,
        Coordinates = 2,
        AddressId = 3,
        AddressCustomField = 4
    }

    /// <summary>
    /// Enumeration of the destinations merge mode:
    /// <para>KeepAsSeparateDestinations = 1, keep separate destinations in output</para>
    /// <para>MergeIntoSingleDestination = 2, merge the bundled destinations in one destination in output</para>
    /// </summary>
    public enum AddressBundlingMergeMode : uint
    {
        KeepAsSeparateDestinations = 1,
        MergeIntoSingleDestination = 2
    }

    /// <summary>
    /// Enumeration of the service time first item mode:
    /// <para>KeepOriginal = 1, keep original service time</para>
    /// <para>CustomTime = 2, set custom time to service time</para>
    /// </summary>
    public enum AddressBundlingFirstItemMode : uint
    {
        KeepOriginal = 1,
        CustomTime = 2
    }

    /// <summary>
    /// Enumeration of the service time additional items mode:
    /// <para>KeepOriginal = 1, preserve original address service time</para>
    /// <para>CustomTime = 2, set custom times</para>
    /// <para>InheritFromPrimary = 3, don't add service times</para>
    /// </summary>
    public enum AddressBundlingAdditionalItemsMode : uint
    {
        KeepOriginal = 1,
        CustomTime = 2,
        InheritFromPrimary = 3
    }

    #endregion

    public enum MemberTypes
    {
        [Description("PRIMARY_ACCOUNT")]
        AccountOwner,

        [Description("SUB_ACCOUNT_ADMIN")]
        Administrator,

        [Description("SUB_ACCOUNT_REGIONAL_MANAGER")]
        RegionalManager,

        [Description("SUB_ACCOUNT_DISPATCHER")]
        Dispatcher,

        [Description("SUB_ACCOUNT_PLANNER")]
        RoutePlanner,

        [Description("PRIMARY_ACCOUNT")]
        PrimaryAccount,

        [Description("SUB_ACCOUNT_DRIVER")]
        Driver,

        [Description("SUB_ACCOUNT_ANALYST")]
        Analyst,

        [Description("SUB_ACCOUNT_VENDOR")]
        Vendor
    }
}
