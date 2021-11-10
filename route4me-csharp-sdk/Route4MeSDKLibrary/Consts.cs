namespace Route4MeSDK
{
    /// <summary>
    ///     Route4Me infrastructure settings
    ///     Api version 4 hosts constants
    /// </summary>
    public static class R4MEInfrastructureSettings
    {
        #region Api V4

        public const string ApiVersion = "4";

        public const string MainHost = "https://api.route4me.com";

        public const string ApiHost = MainHost + "/api.v4/optimization_problem.php";
        public const string ShowRouteHost = MainHost + "/route4me.php";
        public const string RouteHost = MainHost + "/api.v4/route.php";
        public const string SetGpsHost = MainHost + "/track/set.php";
        public const string GetUsersHost = MainHost + "/api.v4/user.php";
        public const string UserAuthentication = MainHost + "/actions/authenticate.php";
        public const string ValidateSession = MainHost + "/datafeed/session/validate_session.php";
        public const string UserRegistration = MainHost + "/actions/register_action.php";
        public const string UserConfiguration = MainHost + "/api.v4/configuration-settings.php";
        public const string AddRouteNotesHost = MainHost + "/actions/addRouteNotes.php";

        public const string ActivityFeedHost = MainHost + "/api.v4/activity_feed.php";
        public const string GetActivitiesHost = MainHost + "/api/get_activities.php";

        public const string GetAddress = MainHost + "/api.v4/address.php";
        public const string DuplicateRoute = MainHost + "/actions/duplicate_route.php";
        public const string MoveRouteDestination = MainHost + "/actions/route/move_route_destination.php";

        public const string AddressBook = MainHost + "/api.v4/address_book.php";
        public const string AddressBookGroup = MainHost + "/api.v4/address_book_group.php";
        public const string AddressBookGroupSearch = MainHost + "/api/address_book/get_search_group_addresses.php";

        public const string Avoidance = MainHost + "/api.v4/avoidance.php";
        public const string Territory = MainHost + "/api.v4/territory.php";
        public const string Order = MainHost + "/api.v4/order.php";
        public const string OrderCustomField = MainHost + "/api.v4/order_custom_user_fields.php";
        public const string MergeRoutes = MainHost + "/actions/merge_routes.php";
        public const string RouteReoptimize = MainHost + "/api.v3/route/reoptimize_2.php";
        public const string RouteSharing = MainHost + "/actions/route/share_route.php";

        public const string MarkAddressDeparted = MainHost + "/api/route/mark_address_departed.php";
        public const string MarkAddressVisited = MainHost + "/actions/address/update_address_visited.php";

        public const string Geocoder = MainHost + "/api/geocoder.php";
        public const string FastGeocoder = MainHost + "/actions/upload/json-geocode.php";
        public const string SaveGeocodedAddresses = MainHost + "/api/address_book/save_geocoded.php";
        public const string r4meValidator = "https://validator.route4me.com:443/";
        public const string RapidStreetData = "https://rapid.route4me.com/street_data";
        public const string RapidStreetZipcode = "https://rapid.route4me.com/street_data/zipcode";
        public const string RapidStreetService = "https://rapid.route4me.com/street_data/service";

        //public const string AssetTracking = MainHost + "/api/asset/find_route.php";
        public const string AssetTracking = MainHost + "/api.v4/status.php";
        public const string DeviceLocation = MainHost + "/api/track/get_device_location.php";
        public const string UserLocation = MainHost + "/api/track/view_user_locations.php";

        public const string ViewVehicles = MainHost + "/api/vehicles/view_vehicles.php";
        public const string Vehicle_V4_API = MainHost + "/api.v4/vehicle.php";
        public const string Vehicle_V4 = "https://wh.route4me.com/modules/api/vehicles";

        public const string HybridOptimization = MainHost + "/api.v4/hybrid_date_optimization.php";
        public const string HybridDepots = MainHost + "/api/change_hybrid_optimization_depot.php";

        public const string CustomNoteType = MainHost + "/api.v4/note_custom_types.php";

        public const string TelematicsVendorsHost = "https://telematics.route4me.com/api/vendors.php";
        public const string TelematicsRegisterHost = MainHost + "/api.v4/telematics/register.php";
        public const string TelematicsConnection = MainHost + "/api.v4/telematics/connections.php";
        public const string TelematicsVendorsInfo = MainHost + "/api.v4/telematics/vendors.php";

        public const string MemberCapabilities = MainHost + "/api/member/capabilities.php";

        public const string ScheduleCalendar = MainHost + "/api/schedule_calendar_data.php";

        #endregion
    }

    /// <summary>
    ///     Route4Me infrastructure settings
    ///     Api version 5 hosts constants
    /// </summary>
    public static class R4MEInfrastructureSettingsV5
    {
        #region Api V5

        public const string ApiVersion = "5";

        public const string MainHost = "https://wh.route4me.com/modules/api/v5.0";

        public const string MainHostWeb = "https://wh.route4me.com/modules/webapi/v5.0";

        public const string Routes = MainHost + "/routes";
        public const string RoutesDuplicate = MainHost + "/routes/duplicate";
        public const string RoutesMerge = MainHost + "/routes/merge";
        public const string RoutesPaginate = MainHost + "/routes/paginate";
        public const string RoutesFallbackPaginate = MainHost + "/routes/fallback/paginate";
        public const string RoutesFallbackDatatable = MainHost + "/routes/fallback/datatable";
        public const string RoutesFallback = MainHost + "/routes/fallback";
        public const string RoutesReindexCallback = MainHost + "/routes/reindex-callback";
        public const string RoutesDatatable = MainHost + "/routes/datatable";
        public const string RoutesDatatableConfig = MainHost + "/routes/datatable/config";
        public const string RoutesDatatableConfigFallback = MainHost + "/routes/fallback/datatable/config";

        public const string TeamUsers = MainHost + "/team/users";

        public const string TeamUsersBulkCreate = MainHost + "/team/bulk-insert";

        public const string DriverReview = MainHost + "/driver-reviews";

        public const string AccountProfile = MainHost + "/profile-api";

        #region Vehicles

        public const string Vehicles = MainHost + "/vehicles";

        public const string VehicleTemporary = MainHost + "/vehicles/assign";

        public const string VehicleExecuteOrder = MainHost + "/vehicles/execute";

        public const string VehicleLocation = MainHost + "/vehicles/location";

        public const string VehicleProfiles = MainHost + "/vehicle-profiles";

        public const string VehicleLicense = MainHost + "/vehicles/license";

        public const string VehicleSearch = MainHost + "/vehicles/search";

        #endregion

        #region Telematics Platform

        public const string StagingHost = "https://virtserver.swaggerhub.com/Route4Me/telematics-gateway/1.0.0";

        public const string TelematicsConnection = StagingHost + "/connections";
        public const string TelematicsConnectionVehicles = StagingHost + "/connections/{connection_token}/vehicles";

        public const string TelematicsAccessToken = StagingHost + "/access-tokens";
        public const string TelematicsAccessTokenSchedules = StagingHost + "/access-token-schedules";

        public const string TelematicsAccessTokenScheduleItems =
            StagingHost + "/access-token-schedules/{schedule_id}/items";

        public const string TelematicsVehicleGroups = StagingHost + "/vehicle-groups";

        public const string TelematicsVehicleGroupsRelation =
            StagingHost + "/vehicle-groups/{vehicle_group_id}/{relation}";

        public const string TelematicsVehiclesRelation = StagingHost + "/vehicles/{vehicle_id}/{relation}";

        public const string TelematicsInfoMembers = StagingHost + "/info/members";
        public const string TelematicsInfoVehicles = StagingHost + "/info/vehicles";
        public const string TelematicsInfoVehicle = StagingHost + "/info/vehicle/{vehicle_id}/track";
        public const string TelematicsInfoModules = StagingHost + "/info/members";

        public const string TelematicsAddresses = StagingHost + "/addresses";

        public const string TelematicsErrors = StagingHost + "/errors";

        public const string TelematicsCustomerNotifications =
            StagingHost + "​/customers​/{customer_id}​/notifications​";

        public const string TelematicsCustomers = StagingHost + "/customers";
        public const string TelematicsCustomerId = StagingHost + "/customers/{customer_id}";

        public const string TelematicsNotificationScheduleItems =
            StagingHost + "/notification-schedules/{notification_schedule_id}/items";

        public const string TelematicsNotificationSchedules = StagingHost + "/notification-schedules";
        public const string TelematicsNotificationScheduleId = StagingHost + "/notification-schedules/{schedule_id}";
        public const string TelematicsOneTimeNotifications = StagingHost + "​/one-time-notifications";

        public const string TelematicsMember = StagingHost;

        public const string TelematicsMemberModules = StagingHost + "​/user-activated-modules";

        public const string TelematicsMemberModuleId = StagingHost + "​/user-activated-modules/{module_id}";

        public const string TelematicsMemberModuleVehicles =
            StagingHost + "​​/user-activated-modules​/{module_id}​/vehicles";

        public const string TelematicsMemberModuleVehicleId =
            StagingHost + "​​​/user-activated-modules​/{module_id}​/vehicles​/{vehicle_id}";

        public const string TelematicsVendors = StagingHost + "​/vendors";
        public const string TelematicsVendorId = StagingHost + "​​/vendors​/{vendor_id}";

        #endregion

        #region Address Book Contacts

        public const string ContactHost = MainHost + "/address-book";

        public const string ContactsGetAll = ContactHost + "/addresses/index/all";
        public const string ContactsGetAllPaginated = ContactHost + "/addresses/index/pagination";
        public const string ContactsGetClusters = ContactHost + "/addresses/index/clustering";
        public const string ContactsFind = ContactHost + "/addresses/show";
        public const string ContactsAddNew = ContactHost + "/addresses";
        public const string ContactsAddMultiple = ContactHost + "/addresses/batch-create";
        public const string ContactsUpdateById = ContactHost + "/addresses/{address_id}";
        public const string ContactsUpdateMultiple = ContactHost + "/addresses/batch-update";
        public const string ContactsUpdateByAreas = ContactHost + "/addresses/update-by-areas";
        public const string ContactsDeleteMultiple = ContactHost + "/addresses/delete";
        public const string ContactsDeleteByAreas = ContactHost + "/addresses/delete-by-areas";
        public const string ContactsGetCustomFields = ContactHost + "/addresses/custom-fields";
        public const string ContactsGetDepots = ContactHost + "/addresses/depots";
        public const string ContactsReindexCallback = ContactHost + "/reindex-callback";
        public const string ContactsExport = ContactHost + "/addresses/export";
        public const string ContactsExportByAreas = ContactHost + "/addresses/export-by-areas";
        public const string ContactsExportByAreaIds = ContactHost + "/addresses/export-by-area-ids";
        public const string ContactsGetAsyncJobStatus = ContactHost + "/addresses/job-tracker/status/{job_id}";
        public const string ContactsGetAsyncJobResult = ContactHost + "/addresses/job-tracker/result/{job_id}";

        #endregion

        public const string AddressBarcodes = MainHost + "/address-barcodes";

        #endregion
    }
}