
namespace Route4MeSDK
{
  /// <summary>
  /// Route4Me infrastructure settings
  /// Api version 4 hosts constants
  /// </summary>
  public static class R4MEInfrastructureSettings
  {
    #region Api V4

    public const string ApiVersion = "4";

    public const string MainHost      = "https://api.route4me.com";
    public const string ApiHost       = MainHost + "/api.v4/optimization_problem.php";
    public const string ShowRouteHost = MainHost + "/route4me.php";
    public const string RouteHost     = MainHost + "/api.v4/route.php";
    public const string SetGpsHost    = MainHost + "/track/set.php";
    public const string GetUsersHost = MainHost + "/api.v4/user.php";
    public const string UserAuthentication = MainHost + "/actions/authenticate.php";
    public const string ValidateSession = MainHost + "/datafeed/session/validate_session.php";
    public const string UserRegistration = MainHost + "/actions/register_action.php";
    public const string UserConfiguration = MainHost + "/api.v4/configuration-settings.php";
    public const string AddRouteNotesHost = MainHost + "/actions/addRouteNotes.php";
    public const string ActivityFeedHost = MainHost + "/api.v4/activity_feed.php";
    public const string GetAddress = MainHost + "/api.v4/address.php";
    public const string DuplicateRoute = MainHost + "/actions/duplicate_route.php";
    public const string MoveRouteDestination = MainHost + "/actions/route/move_route_destination.php";
    public const string AddressBook = MainHost + "/api.v4/address_book.php";
    public const string Avoidance = MainHost + "/api.v4/avoidance.php";
    public const string Territory = MainHost + "/api.v4/territory.php";
    public const string Order = MainHost + "/api.v4/order.php";
    public const string MergeRoutes = MainHost + "/actions/merge_routes.php";
    public const string RouteReoptimize = MainHost + "/api.v3/route/reoptimize_2.php";
    public const string RouteSharing = MainHost + "/actions/route/share_route.php";

    public const string MarkAddressDeparted = MainHost + "/api/route/mark_address_departed.php";
    public const string MarkAddressVisited = MainHost + "/actions/address/update_address_visited.php";

    public const string Geocoder = MainHost + "/api/geocoder.php";
    public const string FastGeocoder = MainHost + "/actions/upload/json-geocode.php";
    public const string r4meValidator = "https://validator.route4me.com:443/";
    public const string RapidStreetData = "https://rapid.route4me.com/street_data";
    public const string RapidStreetZipcode = "https://rapid.route4me.com/street_data/zipcode";
    public const string RapidStreetService = "https://rapid.route4me.com/street_data/service";

    //public const string AssetTracking = MainHost + "/api/asset/find_route.php";
    public const string AssetTracking = MainHost + "/api.v4/status.php";
    public const string DeviceLocation = MainHost + "/api/track/get_device_location.php";

    public const string ViewVehicles = MainHost + "/api/vehicles/view_vehicles.php";
    //public const string Vehicle_V4 = MainHost + "/api.v4/vehicle.php";
    public const string Vehicle_V4 = "https://wh.route4me.com/modules/api/vehicles";

    public const string HybridOptimization = MainHost + "/api.v4/hybrid_date_optimization.php";
    public const string HybridDepots = MainHost + "/api/change_hybrid_optimization_depot.php";

    public const string CustomNoteType = MainHost + "/api.v4/note_custom_types.php";

    #endregion
  }
}
