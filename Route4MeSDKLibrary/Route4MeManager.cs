using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Route4MeSDK
{
  /// <summary>
  /// This class encapsulates the Route4Me REST API
  /// 1. Create an instance of Route4MeManager with the api_key
  /// 1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most popular functionality.
  ///    See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
  /// 2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or Route4MeManager.GetStringResponseFromAPI())
  ///    to access any availaible functionality.
  ///    See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
  /// </summary>
  public sealed class Route4MeManager
  {
    #region Fields

    private readonly string m_ApiKey;
    private readonly TimeSpan m_DefaultTimeOut = new TimeSpan(TimeSpan.TicksPerMinute * 30); // Default timeout - 30 minutes
    //private bool m_isTestMode = false;

    #endregion

    #region Methods

    #region Constructors

    public Route4MeManager(string apiKey)
    {
      m_ApiKey = apiKey;
    }

    #endregion

    #region Route4Me Shortcut Methods

    #region Optimizations

    public DataObject RunOptimization(OptimizationParameters optimizationParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Post,
                                                    false,
                                                    out errorString);

      return result;
    }

    public DataObject GetOptimization(OptimizationParameters optimizationParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Get,
                                                    out errorString);

      return result;
    }

    /// <summary>
    /// </summary>
    [DataContract]
    private sealed class DataObjectOptimizations
    {
      [DataMember(Name = "optimizations")]
      public DataObject[] Optimizations { get; set; }
    }

    public DataObject[] GetOptimizations(RouteParametersQuery queryParameters, out string errorString)
    {
      DataObjectOptimizations dataObjectOptimizations = GetJsonObjectFromAPI<DataObjectOptimizations>(queryParameters,
                                                             R4MEInfrastructureSettings.ApiHost,
                                                             HttpMethodType.Get,
                                                             out errorString);
      DataObject[] result = null;
      if (dataObjectOptimizations != null)
      {
        result = dataObjectOptimizations.Optimizations;
      }
      return result;
    }

    public DataObject UpdateOptimization(OptimizationParameters optimizationParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Put,
                                                    false,
                                                    out errorString);

      return result;
    }

    [DataContract]
    private sealed class RemoveOptimizationResponse
    {
      [DataMember(Name = "status")]
      public bool Status { get; set; }
      [DataMember(Name = "removed")]
      public bool Removed { get; set; }
    }

    [DataContract()]
    private sealed class RemoveOptimizationRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public int redirect {get; set;}

        [DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
        public string[] optimization_problem_ids { get; set; }
    }

    /// <summary>
    /// Remove an existing optimization belonging to an user.
    /// </summary>
    /// <param name="optimizationProblemID"> Optimization Problem ID </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Result status true/false </returns>
    public bool RemoveOptimization(string[] optimizationProblemIDs, out string errorString)
    {
        RemoveOptimizationRequest remParameters = new RemoveOptimizationRequest()
        {
            redirect = 0,
            optimization_problem_ids = optimizationProblemIDs
        };

        RemoveOptimizationResponse response = GetJsonObjectFromAPI<RemoveOptimizationResponse>(remParameters,
                                                             R4MEInfrastructureSettings.ApiHost,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      if (response != null && response.Status && response.Removed)
      {
        return true;
      }
      else
      {
        if (errorString == "")
          errorString = "Error removing optimization";
        return false;
      }
    }

    [DataContract]
    private sealed class RemoveDestinationFromOptimizationResponse
    {
      [DataMember(Name = "deleted")]
      public Boolean Deleted { get; set; }

      [DataMember(Name = "route_destination_id")]
      public int RouteDestinationId { get; set; }
    }

    public bool RemoveDestinationFromOptimization(string optimizationId, int destinationId, out string errorString)
    {
      GenericParameters genericParameters = new GenericParameters();
      genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationId);
      genericParameters.ParametersCollection.Add("route_destination_id", destinationId.ToString());
      RemoveDestinationFromOptimizationResponse response = GetJsonObjectFromAPI<RemoveDestinationFromOptimizationResponse>(genericParameters,
                                                             R4MEInfrastructureSettings.GetAddress,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      if (response != null && response.Deleted)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Hybrid Optimization
    public DataObject GetOHybridptimization(HybridOptimizationParameters hybridOptimizationParameters, out string errorString)
    {
        var result = GetJsonObjectFromAPI<DataObject>(hybridOptimizationParameters,
                                                      R4MEInfrastructureSettings.HybridOptimization,
                                                      HttpMethodType.Get,
                                                      out errorString);

        return result;
    }
    #endregion

    #region Routes

    public DataObjectRoute GetRoute(RouteParametersQuery routeParameters, out string errorString)
    {
        var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                                                      R4MEInfrastructureSettings.RouteHost,
                                                      HttpMethodType.Get,
                                                      out errorString);

        return result;
    }

    public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                                                           R4MEInfrastructureSettings.RouteHost,
                                                           HttpMethodType.Get,
                                                           out errorString);

      return result;
    }

    public string GetRouteId(string optimizationProblemId, out string errorString)
    {
      GenericParameters genericParameters = new GenericParameters();
      genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationProblemId);
      genericParameters.ParametersCollection.Add("wait_for_final_state", "1");
      DataObject response = GetJsonObjectFromAPI<DataObject>(genericParameters,
                                                             R4MEInfrastructureSettings.ApiHost,
                                                             HttpMethodType.Get,
                                                             out errorString);
      if (response != null && response.Routes != null && response.Routes.Length > 0)
      {
        string routeId = response.Routes[0].RouteID;
        return routeId;
      }
      return null;
    }

    public DataObjectRoute UpdateRoute(RouteParametersQuery routeParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                                                    R4MEInfrastructureSettings.RouteHost,
                                                    HttpMethodType.Put,
                                                    out errorString);

      return result;
    }

    [DataContract]
    private sealed class DuplicateRouteResponse
    {
      [DataMember(Name = "optimization_problem_id")]
      public string OptimizationProblemId { get; set; }

      [DataMember(Name = "success")]
      public Boolean Success { get; set; }
    }

    public string DuplicateRoute(RouteParametersQuery queryParameters, out string errorString)
    {
      //if (queryParameters.ParametersCollection["to"] == null)
      //  queryParameters.ParametersCollection.Add("to", "none");
      // Redirect to page or return json for none
      queryParameters.ParametersCollection["to"] = "none";
      DuplicateRouteResponse response = GetJsonObjectFromAPI<DuplicateRouteResponse>(queryParameters,
                                                             R4MEInfrastructureSettings.DuplicateRoute,
                                                             HttpMethodType.Get,
                                                             out errorString);
      string routeId = null;
      if (response != null && response.Success)
      {
        string optimizationProblemId = response.OptimizationProblemId;
        if (optimizationProblemId != null)
        {
          routeId = this.GetRouteId(optimizationProblemId, out errorString);
        }
      }
      return routeId;
    }

    [DataContract]
    private sealed class DeleteRouteResponse
    {
      [DataMember(Name = "deleted")]
      public Boolean Deleted { get; set; }

      [DataMember(Name = "errors")]
      public List<String> Errors { get; set; }

      [DataMember(Name = "route_id")]
      public string routeId { get; set; }

      [DataMember(Name = "route_ids")]
      public string[] routeIds { get; set; }
    }

    public string[] DeleteRoutes(string[] routeIds, out string errorString)
    {
      string str_route_ids = "";
      foreach (string routeId in routeIds)
      {
        if (str_route_ids.Length > 0)
          str_route_ids += ",";
        str_route_ids += routeId;
      }
      GenericParameters genericParameters = new GenericParameters();
      genericParameters.ParametersCollection.Add("route_id", str_route_ids);
      DeleteRouteResponse response = GetJsonObjectFromAPI<DeleteRouteResponse>(genericParameters,
                                                             R4MEInfrastructureSettings.RouteHost,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      string[] deletedRouteIds = null;
      if (response != null)
      {
        deletedRouteIds = response.routeIds;
      }
      return deletedRouteIds;
    }

    public bool MergeRoutes(MergeRoutesQuery mergeRoutesParameters, out string errorString)
    {
        GenericParameters roParames = new GenericParameters();

        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

        keyValues.Add(new KeyValuePair<string, string>("route_ids", mergeRoutesParameters.RouteIds));
        keyValues.Add(new KeyValuePair<string, string>("depot_address", mergeRoutesParameters.DepotAddress));
        keyValues.Add(new KeyValuePair<string, string>("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()));
        keyValues.Add(new KeyValuePair<string, string>("depot_lat", mergeRoutesParameters.DepotLat.ToString()));
        keyValues.Add(new KeyValuePair<string, string>("depot_lng", mergeRoutesParameters.DepotLng.ToString()));

        HttpContent httpContent = new FormUrlEncodedContent(keyValues);

        ResequenceReoptimizeRouteResponse response = GetJsonObjectFromAPI<ResequenceReoptimizeRouteResponse>
            (roParames, R4MEInfrastructureSettings.MergeRoutes, 
            HttpMethodType.Post, httpContent, out errorString);

        if (response != null && response.Status)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [DataContract()]
    private sealed class ResequenceReoptimizeRouteResponse
    {
        [DataMember(Name = "status")]
        public Boolean Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        private Boolean m_Status;
    }

    public bool ResequenceReoptimizeRoute(Dictionary<string, string> roParames, out string errorString)
    {
        RouteParametersQuery request = new RouteParametersQuery
        {
            RouteId = roParames["route_id"],
            DisableOptimization = roParames["disable_optimization"]=="1" ? true : false,
            Optimize = roParames["optimize"]
        };

        ResequenceReoptimizeRouteResponse response = GetJsonObjectFromAPI<ResequenceReoptimizeRouteResponse>(request, R4MEInfrastructureSettings.RouteReoptimize, HttpMethodType.Get, out errorString);

        if (response != null && response.Status)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RouteSharing(RouteParametersQuery roParames, string Email, out string errorString)
    {
        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
        
        keyValues.Add(new KeyValuePair<string, string>("recipient_email", Email));
        HttpContent httpContent = new FormUrlEncodedContent(keyValues);

        ResequenceReoptimizeRouteResponse response = GetJsonObjectFromAPI<ResequenceReoptimizeRouteResponse>(roParames, R4MEInfrastructureSettings.RouteSharing, HttpMethodType.Post, httpContent, out errorString);

        if (response != null && response.Status)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [DataContract()]
    private sealed class UpdateRouteCustomDataRequest : GenericParameters
    {

        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId
        {
            get { return m_RouteId; }
            set { m_RouteId = value; }
        }

        private string m_RouteId;
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public System.Nullable<int> RouteDestinationId
        {
            get { return m_RouteDestinationId; }
            set { m_RouteDestinationId = value; }
        }

        private System.Nullable<int> m_RouteDestinationId;

        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomFields
        {
            get { return m_CustomFields; }
            set { m_CustomFields = value; }
        }
        private Dictionary<string, string> m_CustomFields;
    }

    public Address UpdateRouteCustomData(RouteParametersQuery routeParameters, Dictionary<string, string> customData, out string errorString)
    {
        UpdateRouteCustomDataRequest request = new UpdateRouteCustomDataRequest
        {
            RouteId = routeParameters.RouteId,
            RouteDestinationId = routeParameters.RouteDestinationId,
            CustomFields = customData
        };

        var result = GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);

        return result;
    }

    [DataContract()]
    private sealed class UpdateRouteDestinationRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId
        {
            get { return m_RouteId; }
            set { m_RouteId = value; }
        }

        private string m_RouteId;
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public System.Nullable<int> RouteDestinationId
        {
            get { return m_RouteDestinationId; }
            set { m_RouteDestinationId = value; }
        }
        private System.Nullable<int> m_RouteDestinationId;

        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string AddressString { get; set; }

        [DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
        public string AddressStopType { get; set; }

        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }

        //the latitude of this address
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Latitude { get; set; }

        //the longitude of this address
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Longitude { get; set; }

        [DataMember(Name = "sequence_no", EmitDefaultValue = false)]
        public int? SequenceNo { get; set; }

        //status flag to mark an address as visited (aka check in)
        [DataMember(Name = "is_visited", EmitDefaultValue = false)]
        public bool? IsVisited { get; set; }

        //status flag to mark an address as departed (aka check out)
        [DataMember(Name = "is_departed", EmitDefaultValue = false)]
        public bool? IsDeparted { get; set; }

        //the last known visited timestamp of this address
        [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
        public uint? TimestampLastVisited { get; set; }

        //the last known departed timestamp of this address
        [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
        public uint? TimestampLastDeparted { get; set; }

        [DataMember(Name = "group", EmitDefaultValue = false)]
        public object Group { get; set; }

        //pass-through data about this route destination
        //the data will be visible on the manifest, website, and mobile apps
        [DataMember(Name = "customer_po", EmitDefaultValue = false)]
        public object CustomerPo { get; set; }

        //pass-through data about this route destination
        //the data will be visible on the manifest, website, and mobile apps
        [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
        public object InvoiceNo { get; set; }

        //pass-through data about this route destination
        //the data will be visible on the manifest, website, and mobile apps
        [DataMember(Name = "reference_no", EmitDefaultValue = false)]
        public object ReferenceNo { get; set; }

        //pass-through data about this route destination
        //the data will be visible on the manifest, website, and mobile apps
        [DataMember(Name = "order_no", EmitDefaultValue = false)]
        public object OrderNo { get; set; }

        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? OrderId { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public object Weight { get; set; }

        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public object Cost { get; set; }

        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public object Revenue { get; set; }

        //the cubic volume that this destination/order/line-item consumes/contains
        //this is how much space it will take up on a vehicle
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public object Cube { get; set; }

        //the number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public object Pieces { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public int? TimeWindowStart { get; set; }

        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public int? TimeWindowEnd { get; set; }

        //the expected amount of time that will be spent at this address by the driver/user
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public int? Time { get; set; }

        //if present, the priority will sequence addresses in all the optimal routes so that
        //higher priority addresses are general at the beginning of the route sequence
        //1 is the highest priority, 100000 is the lowest
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public int? Priority { get; set; }

        //generate optimal routes and driving directions to this curbside lat
        [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
        public double? CurbsideLatitude { get; set; }

        //generate optimal routes and driving directions to the curbside lang
        [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
        public double? CurbsideLongitude { get; set; }

        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public int? TimeWindowStart2 { get; set; }

        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public int? TimeWindowEnd2 { get; set; }

        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomFields
        {
            get { return m_CustomFields; }
            set { m_CustomFields = value; }
        }
        private Dictionary<string, string> m_CustomFields;
    }

    public Address UpdateRouteDestination(Address addressParameters, out string errorString)
    {
        UpdateRouteDestinationRequest request = new UpdateRouteDestinationRequest
        {
            RouteId = addressParameters.RouteId,
            RouteDestinationId = addressParameters.RouteDestinationId,
        };

        if (addressParameters.Alias != null) request.Alias = addressParameters.Alias;
        if (addressParameters.FirstName != null) request.FirstName = addressParameters.FirstName;
        if (addressParameters.LastName != null) request.LastName = addressParameters.LastName;
        if (addressParameters.AddressString != null) request.AddressString = addressParameters.AddressString;
        if (addressParameters.AddressStopType != null) request.AddressStopType = addressParameters.AddressStopType;
        if (addressParameters.IsDepot != null) request.IsDepot = addressParameters.IsDepot;
        if (addressParameters.Latitude != null) request.Latitude = addressParameters.Latitude;
        if (addressParameters.Longitude != null) request.Longitude = addressParameters.Longitude;

        if (addressParameters.SequenceNo != null) request.SequenceNo = addressParameters.SequenceNo;
        if (addressParameters.IsVisited != null) request.IsVisited = addressParameters.IsVisited;
        if (addressParameters.IsDeparted != null) request.IsDeparted = addressParameters.IsDeparted;
        if (addressParameters.TimestampLastVisited != null) request.TimestampLastVisited = addressParameters.TimestampLastVisited;
        if (addressParameters.TimestampLastDeparted != null) request.TimestampLastDeparted = addressParameters.TimestampLastDeparted;
        if (addressParameters.Group != null) request.Group = addressParameters.Group;
        if (addressParameters.CustomerPo != null) request.CustomerPo = addressParameters.CustomerPo;
        if (addressParameters.InvoiceNo != null) request.InvoiceNo = addressParameters.InvoiceNo;
        if (addressParameters.ReferenceNo != null) request.ReferenceNo = addressParameters.ReferenceNo;
        if (addressParameters.OrderNo != null) request.OrderNo = addressParameters.OrderNo;
        if (addressParameters.OrderId != null) request.OrderId = addressParameters.OrderId;
        if (addressParameters.Weight != null) request.Weight = addressParameters.Weight;
        if (addressParameters.Cost != null) request.Cost = addressParameters.Cost;
        if (addressParameters.Revenue != null) request.Revenue = addressParameters.Revenue;
        if (addressParameters.Cube != null) request.Cube = addressParameters.Cube;
        if (addressParameters.Pieces != null) request.Pieces = addressParameters.Pieces;
        if (addressParameters.Phone != null) request.Phone = addressParameters.Phone.ToString();

        if (addressParameters.TimeWindowStart != null) request.TimeWindowStart = addressParameters.TimeWindowStart;
        if (addressParameters.TimeWindowEnd != null) request.TimeWindowEnd = addressParameters.TimeWindowEnd;

        if (addressParameters.Priority != null) request.Priority = addressParameters.Priority;
        if (addressParameters.CurbsideLatitude != null) request.CurbsideLatitude = addressParameters.CurbsideLatitude;
        if (addressParameters.CurbsideLongitude != null) request.CurbsideLongitude = addressParameters.CurbsideLongitude;
        if (addressParameters.TimeWindowStart2 != null) request.TimeWindowStart2 = addressParameters.TimeWindowStart2;
        if (addressParameters.TimeWindowEnd2 != null) request.TimeWindowEnd2 = addressParameters.TimeWindowEnd2;
        if (addressParameters.CustomFields != null) request.CustomFields = addressParameters.CustomFields;

        var result = GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);

        return result;
    }

    #endregion

    #region Tracking

    public DataObject GetLastLocation(GenericParameters parameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(parameters,
                                                    R4MEInfrastructureSettings.RouteHost,
                                                    HttpMethodType.Get,
                                                    false,
                                                    out errorString);

      return result;
    }

    public string SetGPS(GPSParameters gpsParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<string>(gpsParameters,
                                                R4MEInfrastructureSettings.SetGpsHost,
                                                HttpMethodType.Get,
                                                true,
                                                out errorString);

      return result;
    }

    [DataContract()]
    private sealed class FindAssetRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "tracking", EmitDefaultValue = false)]
        public string Tracking
        {
            get { return m_Tracking; }
            set { m_Tracking = value; }
        }

        private string m_Tracking;
    }

    public FindAssetResponse FindAsset(string tracking, out string errorString)
    {
        FindAssetRequest request = new FindAssetRequest { Tracking = tracking };

        FindAssetResponse result = GetJsonObjectFromAPI<FindAssetResponse>(request, R4MEInfrastructureSettings.AssetTracking, HttpMethodType.Get, false, out errorString);

        return result;
    }

    #endregion

    #region Users

    public User[] GetUsers(GenericParameters parameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<User[]>(parameters,
                                                           R4MEInfrastructureSettings.GetUsersHost,
                                                           HttpMethodType.Get,
                                                           out errorString);

      return result;
    }

    public MemberResponseV4 CreateUser(MemberParametersV4 memParams, out string errorString)
    {
        MemberResponseV4 response = GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Post, out errorString);
        return response;
    }

    [DataContract()]
    public sealed class UserDeleteResponse
    {
        [DataMember(Name = "status")]
        public bool Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        private bool m_Status;
    }

    public bool UserDelete(MemberParametersV4 memParams, out string errorString)
    {
        UserDeleteResponse response = GetJsonObjectFromAPI<UserDeleteResponse>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Delete, out errorString);

        if (response == null) return false;

        if (response.Status) return true; else return false;
    }

    public MemberResponseV4 GetUserById(MemberParametersV4 memParams, out string errorString)
    {
        MemberResponseV4 response = GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Get, out errorString);
        return response;
    }

    public MemberResponseV4 UserUpdate(MemberParametersV4 memParams, out string errorString)
    {
        MemberResponseV4 response = GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Put, out errorString);
        return response;
    }

    public MemberResponse UserAuthentication(MemberParameters memParams, out string errorString)
    {
        MemberParameters roParams = new MemberParameters();

        var keyValues = new List<KeyValuePair<string, string>>();
        keyValues.Add(new KeyValuePair<string, string>("strEmail", memParams.StrEmail));
        keyValues.Add(new KeyValuePair<string, string>("strPassword", memParams.StrPassword));
        keyValues.Add(new KeyValuePair<string, string>("format", memParams.Format));

        HttpContent httpContent = new FormUrlEncodedContent(keyValues);

        MemberResponse response = GetJsonObjectFromAPI<MemberResponse>(roParams, R4MEInfrastructureSettings.UserAuthentication, HttpMethodType.Post, httpContent, out errorString);

        return response;
    }

    public MemberResponse UserRegistration(MemberParameters memParams, out string errorString)
    {
        MemberParameters roParams = new MemberParameters();
        roParams.Plan = memParams.Plan;
        roParams.MemberType = memParams.MemberType;

        var keyValues = new List<KeyValuePair<string, string>>();
        keyValues.Add(new KeyValuePair<string, string>("strIndustry", memParams.StrIndustry));
        keyValues.Add(new KeyValuePair<string, string>("strFirstName", memParams.StrFirstName));
        keyValues.Add(new KeyValuePair<string, string>("strLastName", memParams.StrLastName));
        keyValues.Add(new KeyValuePair<string, string>("strEmail", memParams.StrEmail));
        keyValues.Add(new KeyValuePair<string, string>("format", memParams.Format));
        keyValues.Add(new KeyValuePair<string, string>("chkTerms", memParams.ChkTerms==1 ? "1" : "0"));
        keyValues.Add(new KeyValuePair<string, string>("device_type", memParams.DeviceType));
        keyValues.Add(new KeyValuePair<string, string>("strPassword_1", memParams.StrPassword_1));
        keyValues.Add(new KeyValuePair<string, string>("strPassword_2", memParams.StrPassword_2));

        HttpContent httpContent = new FormUrlEncodedContent(keyValues);

        MemberResponse response = GetJsonObjectFromAPI<MemberResponse>(roParams, R4MEInfrastructureSettings.UserRegistration, HttpMethodType.Post, httpContent, out errorString);

        return response;
    }

    [DataContract()]
    private sealed class ValidateSessionRequest : GenericParameters
    {

        [HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
        public string SessionGuid
        {
            get { return m_SessionGuid; }
            set { m_SessionGuid = value; }
        }

        private string m_SessionGuid;
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public System.Nullable<int> MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }

        private System.Nullable<int> m_MemberId;
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        private string m_Format;
    }

    public MemberResponse ValidateSession(MemberParameters memParams, out string errorString)
    {
        ValidateSessionRequest request = new ValidateSessionRequest
        {
            SessionGuid = memParams.SessionGuid,
            MemberId = memParams.MemberId,
            Format = memParams.Format
        };

        MemberResponse result = GetJsonObjectFromAPI<MemberResponse>(request, R4MEInfrastructureSettings.ValidateSession, HttpMethodType.Get, false, out errorString);

        return result;
    }

    public MemberConfigurationResponse CreateNewConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
    {
        MemberConfigurationResponse response = GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post, out errorString);

        return response;
    }

    public MemberConfigurationResponse RemoveConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
    {
        MemberConfigurationResponse response = GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Delete, out errorString);

        return response;
    }

    [DataContract()]
    private sealed class GetConfigurationDataRequest : GenericParameters
    {

        [HttpQueryMemberAttribute(Name = "config_key", EmitDefaultValue = false)]
        public string config_key
        {
            get { return m_config_key; }
            set { m_config_key = value; }
        }

        private string m_config_key;
    }

    public MemberConfigurationDataRersponse GetConfigurationData(MemberConfigurationParameters confParams, out string errorString)
    {
        GetConfigurationDataRequest mParams = default(GetConfigurationDataRequest);
        mParams = new GetConfigurationDataRequest();
        if ((confParams != null))
        {
            mParams.config_key = confParams.config_key;
        }

        MemberConfigurationDataRersponse response = GetJsonObjectFromAPI<MemberConfigurationDataRersponse>(mParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Get, out errorString);

        return response;
    }

    public MemberConfigurationResponse UpdateConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
    {
        MemberConfigurationResponse response = GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Put, out errorString);

        return response;
    }

    #endregion

    #region Address Notes

    public AddressNote[] GetAddressNotes(NoteParameters noteParameters, out string errorString)
    {
      AddressParameters addressParameters = new AddressParameters()
      {
        RouteId = noteParameters.RouteId,
        RouteDestinationId = noteParameters.AddressId,
        Notes = true
      };
      Address address = this.GetAddress(addressParameters, out errorString);
      if (address != null)
      {
        return address.Notes;
      }
      else
      {
        return null;
      }
    }

    [DataContract]
    private sealed class AddAddressNoteResponse
    {
      [DataMember(Name = "status")]
      public bool Status { get; set; }

      [DataMember(Name = "note")]
      public AddressNote Note { get; set; }
    }

    public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, string attachmentFilePath, out string errorString)
    {
      var strUpdateType = "unclassified";
      if (noteParameters.ActivityType != null && noteParameters.ActivityType.Length > 0)
      {
        strUpdateType = noteParameters.ActivityType;
      }
      HttpContent httpContent = null;
      FileStream attachmentFileStream = null;
      StreamContent attachmentStreamContent = null;
      if (attachmentFilePath != null)
      {
        attachmentFileStream = File.OpenRead(attachmentFilePath);
        attachmentStreamContent = new StreamContent(attachmentFileStream);
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Add(attachmentStreamContent, "strFilename", Path.GetFileName(attachmentFilePath));
        multipartFormDataContent.Add(new StringContent(strUpdateType), "strUpdateType");
        multipartFormDataContent.Add(new StringContent(noteContents), "strNoteContents");
        httpContent = multipartFormDataContent;
      }
      else
      {
        var keyValues = new List<KeyValuePair<string, string>>();
        keyValues.Add(new KeyValuePair<string, string>("strUpdateType", strUpdateType));
        keyValues.Add(new KeyValuePair<string, string>("strNoteContents", noteContents));
        httpContent = new FormUrlEncodedContent(keyValues);
      }
      AddAddressNoteResponse response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
                                                           R4MEInfrastructureSettings.AddRouteNotesHost,
                                                           HttpMethodType.Post,
                                                           httpContent,
                                                           out errorString);
      if (attachmentStreamContent != null)
      {
        attachmentStreamContent.Dispose();
      }
      if (attachmentFileStream != null)
      {
        attachmentFileStream.Dispose();
      }
      if (response != null)
      {
        if (response.Note != null)
        {
          return response.Note;
        }
        else
        {
          if (response.Status == false)
          {
            errorString = "Note not added";
          }
          return null;
        }
      }
      else
      {
        return null;
      }
    }

    public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, out string errorString)
    {
      return this.AddAddressNote(noteParameters, noteContents, null, out errorString);
    }

    #endregion

    #region Activities

    [DataContract]
    private sealed class GetActivitiesResponse
    {
      [DataMember(Name = "results")]
      public Activity[] Results { get; set; }

      [DataMember(Name = "total")]
      public uint Total { get; set; }
    }

    /// <summary>
    /// Get Activity Feed
    /// </summary>
    /// <param name="activityParameters"> Input parameters </param>
    /// <param name="errorString"> Error string </param>
    /// <returns> List of Activity objects </returns>
    public Activity[] GetActivityFeed(ActivityParameters activityParameters, out string errorString)
    {
      GetActivitiesResponse response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                                                           R4MEInfrastructureSettings.ActivityFeedHost,
                                                           HttpMethodType.Get,
                                                           out errorString);
      Activity[] result = null;
      if (response != null)
      {
        result = response.Results;
      }
      return result;
    }

    [DataContract]
    private sealed class LogCustomActivityResponse
    {
      [DataMember(Name = "status")]
      public bool Status { get; set; }
    }

    /// <summary>
    /// Create User Activity. Send custom message to Activity Stream.
    /// </summary>
    /// <param name="activity"> Input Activity object to add </param>
    /// <param name="errorString"> Error string </param>
    /// <returns> True/False </returns>
    public bool LogCustomActivity(Activity activity, out string errorString)
    {
      activity.PrepareForSerialization();
      LogCustomActivityResponse response = GetJsonObjectFromAPI<LogCustomActivityResponse>(activity,
                                                             R4MEInfrastructureSettings.ActivityFeedHost,
                                                             HttpMethodType.Post,
                                                             out errorString);
      if (response != null && response.Status)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Destinations

    public Address GetAddress(AddressParameters addressParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<Address>(addressParameters,
                                                           R4MEInfrastructureSettings.GetAddress,
                                                           HttpMethodType.Get,
                                                           out errorString);

      return result;
    }

    [DataContract]
    private sealed class AddRouteDestinationRequest : GenericParameters
    {
      [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
      public string RouteId { get; set; }

      [DataMember(Name = "addresses", EmitDefaultValue = false)]
      public Address[] Addresses { get; set; }

      /// <summary>
      /// If true, an address will be inserted at optimal position of a route
      /// </summary>
      [DataMember(Name = "optimal_position", EmitDefaultValue = true)]
      public bool OptimalPosition { get; set; }
    }

    /// <summary>
    /// Add address(es) into a route.
    /// </summary>
    /// <param name="routeId"> Route ID </param>
    /// <param name="addresses"> Valid array of Address objects. </param>
    /// <param name="optimalPosition"> If true, an address will be inserted at optimal position of a route </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> IDs of added addresses </returns>
    public int[] AddRouteDestinations(string routeId, Address[] addresses, bool optimalPosition, out string errorString)
    {
      AddRouteDestinationRequest request = new AddRouteDestinationRequest()
      {
        RouteId = routeId,
        Addresses = addresses,
        OptimalPosition = optimalPosition
      };
      DataObject response = GetJsonObjectFromAPI<DataObject>(request,
                                                             R4MEInfrastructureSettings.RouteHost,
                                                             HttpMethodType.Put,
                                                             out errorString);
      int[] destinationIds = null;
      if (response != null && response.Addresses != null)
      {
        List<int> arrDestinationIds = new List<int>();
        foreach (Address addressNew in addresses)
        {
          int destinationId = 0;
          foreach (Address addressResp in response.Addresses)
          {
            if (addressResp.AddressString == addressNew.AddressString
              && addressResp.Latitude == addressNew.Latitude
              && addressResp.Longitude == addressNew.Longitude
              && addressResp.RouteDestinationId != null)
            {
              destinationId = (int)addressResp.RouteDestinationId;
            }
          }
          arrDestinationIds.Add(destinationId);
        }
        destinationIds = arrDestinationIds.ToArray();
      }
      return destinationIds;
    }

    /// <summary>
    /// Add address(es) into a route.
    /// </summary>
    /// <param name="routeId"> Route ID </param>
    /// <param name="addresses"> Valid array of Address objects. </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> IDs of added addresses </returns>
    public int[] AddRouteDestinations(string routeId, Address[] addresses, out string errorString)
    {
      return this.AddRouteDestinations(routeId, addresses, true, out errorString);
    }


    [DataContract]
    private sealed class RemoveRouteDestinationRequest : GenericParameters
    {
      [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
      public string RouteId { get; set; }

      [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
      public int RouteDestinationId { get; set; }
    }

    [DataContract]
    private sealed class RemoveRouteDestinationResponse
    {
      [DataMember(Name = "deleted")]
      public Boolean Deleted { get; set; }

      [DataMember(Name = "route_destination_id")]
      public int RouteDestinationId { get; set; }
    }

    public bool RemoveRouteDestination(string routeId, int destinationId, out string errorString)
    {
      RemoveRouteDestinationRequest request = new RemoveRouteDestinationRequest()
      {
        RouteId = routeId,
        RouteDestinationId = destinationId
      };
      RemoveRouteDestinationResponse response = GetJsonObjectFromAPI<RemoveRouteDestinationResponse>(request,
                                                             R4MEInfrastructureSettings.GetAddress,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      if (response != null && response.Deleted)
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    [DataContract]
    private sealed class MoveDestinationToRouteResponse
    {
      [DataMember(Name = "success")]
      public Boolean Success { get; set; }

      [DataMember(Name = "error")]
      public string error { get; set; }
    }

    public bool MoveDestinationToRoute(string toRouteId, int routeDestinationId, int afterDestinationId, out string errorString)
    {
      var keyValues = new List<KeyValuePair<string, string>>();
      keyValues.Add(new KeyValuePair<string, string>("to_route_id", toRouteId));
      keyValues.Add(new KeyValuePair<string, string>("route_destination_id", Convert.ToString(routeDestinationId)));
      keyValues.Add(new KeyValuePair<string, string>("after_destination_id", Convert.ToString(afterDestinationId)));
      HttpContent httpContent = new FormUrlEncodedContent(keyValues);

      MoveDestinationToRouteResponse response = GetJsonObjectFromAPI<MoveDestinationToRouteResponse>(new GenericParameters(),
                                                           R4MEInfrastructureSettings.MoveRouteDestination,
                                                           HttpMethodType.Post,
                                                           httpContent,
                                                           out errorString);
      if (response != null)
      {
        if (!response.Success && response.error != null)
        {
          errorString = response.error;
        }
        return response.Success;
      }
      return false;
    }

    [DataContract]
    private sealed class MarkAddressDepartedRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId
        {
            get { return m_RouteId; }
            set { m_RouteId = value; }
        }
        private string m_RouteId;

        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public System.Nullable<int> AddressId
        {
            get { return m_AddressId; }
            set { m_AddressId = value; }
        }
        private System.Nullable<int> m_AddressId;

        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "is_departed", EmitDefaultValue = false)]
        public bool IsDeparted
        {
            get { return m_IsDeparted; }
            set { m_IsDeparted = value; }
        }
        private bool m_IsDeparted;

        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "is_visited", EmitDefaultValue = false)]
        public bool IsVisited
        {
            get { return m_IsVisited; }
            set { m_IsVisited = value; }
        }
        private bool m_IsVisited;

        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public System.Nullable<int> MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private System.Nullable<int> m_MemberId;
    }

    public int MarkAddressVisited(AddressParameters aParams, out string errorString)
    {
        MarkAddressDepartedRequest request = new MarkAddressDepartedRequest
        {
            RouteId = aParams.RouteId,
            AddressId = aParams.AddressId,
            IsVisited = aParams.IsVisited,
            MemberId = 1
        };

        string response = GetJsonObjectFromAPI<string>(request, R4MEInfrastructureSettings.MarkAddressVisited, HttpMethodType.Get, out errorString);
        int iResponse = 0;
        if (int.TryParse(response.ToString(), out iResponse)) iResponse = Convert.ToInt32(response);
        return iResponse;
    }

    [DataContract]
    private sealed class MarkAddressDepartedResponse
    {
        [DataMember(Name = "status")]
        public Boolean Status { get; set; }

        [DataMember(Name = "error")]
        public string error { get; set; }
    }

    public int MarkAddressDeparted(AddressParameters aParams, out string errorString)
    {
        MarkAddressDepartedRequest request = new MarkAddressDepartedRequest
        {
            RouteId = aParams.RouteId,
            AddressId = aParams.AddressId,
            IsDeparted = aParams.IsDeparted,
            MemberId = 1
        };

        MarkAddressDepartedResponse response = GetJsonObjectFromAPI<MarkAddressDepartedResponse>(request, R4MEInfrastructureSettings.MarkAddressDeparted, HttpMethodType.Get, out errorString);

        if (response != null)
        {
            if (response.Status) return 1; else return 0;
        } else return 0;
    }

    [DataContract()]
    private sealed class MarkAddressAsMarkedAsDepartedRequest : GenericParameters
    {

        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId
        {
            get { return m_RouteId; }
            set { m_RouteId = value; }
        }

        private string m_RouteId;
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public System.Nullable<int> RouteDestinationId
        {
            get { return m_RouteDestinationId; }
            set { m_RouteDestinationId = value; }
        }

        private System.Nullable<int> m_RouteDestinationId;
        [IgnoreDataMember()]
        [DataMember(Name = "is_departed")]
        public bool IsDeparted
        {
            get { return m_IsDeparted; }
            set { m_IsDeparted = value; }
        }

        private bool m_IsDeparted;
        [IgnoreDataMember()]
        [DataMember(Name = "is_visited")]
        public bool IsVisited
        {
            get { return m_IsVisited; }
            set { m_IsVisited = value; }
        }
        private bool m_IsVisited;
    }

    public Address MarkAddressAsMarkedAsVisited(AddressParameters aParams, out string errorString)
    {
        MarkAddressAsMarkedAsDepartedRequest request = new MarkAddressAsMarkedAsDepartedRequest
        {
            RouteId = aParams.RouteId,
            RouteDestinationId = aParams.RouteDestinationId,
            IsVisited = aParams.IsVisited
        };

        Address response = GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);

        return response;
    }

    public Address MarkAddressAsMarkedAsDeparted(AddressParameters aParams, out string errorString)
    {
        MarkAddressAsMarkedAsDepartedRequest request = new MarkAddressAsMarkedAsDepartedRequest
        {
            RouteId = aParams.RouteId,
            RouteDestinationId = aParams.RouteDestinationId,
            IsDeparted = aParams.IsDeparted
        };

        Address response = GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);

        return response;
    }

    #endregion

    #region Address Book

    [DataContract]
    private sealed class GetAddressBookContactsResponse : GenericParameters
    {
      [DataMember(Name = "results", IsRequired = false)]
      public AddressBookContact[] results { get; set; }

      [DataMember(Name = "total", IsRequired = false)]
      public uint total { get; set; }
    }

    public AddressBookContact[] GetAddressBookContacts(AddressBookParameters addressBookParameters, out uint total, out string errorString)
    {
      total = 0;
      var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters,
                                                           R4MEInfrastructureSettings.AddressBook,
                                                           HttpMethodType.Get,
                                                           out errorString);
      AddressBookContact[] result = null;
      if (response != null)
      {
        result = response.results;
        total = response.total;
      }
      return result;
    }

    public AddressBookContact[] GetAddressBookLocation(AddressBookParameters addressBookParameters, out uint total, out string errorString)
    {
        total = 0;

        var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Get, out errorString);
        AddressBookContact[] result = null;
        if (response != null)
        {
            result = response.results;
            total = response.total;
        }
        return result;
    }

    [DataContract()]
    private sealed class SearchAddressBookLocationRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query
        {
            get { return m_Query; }
            set { m_Query = value; }
        }

        private string m_Query;
        [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
        public string Fields
        {
            get { return m_Fields; }
            set { m_Fields = value; }
        }

        private string m_Fields;
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public System.Nullable<int> Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }

        private System.Nullable<int> m_Offset;
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public System.Nullable<int> Limit
        {
            get { return m_Limit; }
            set { m_Limit = value; }
        }

        private System.Nullable<int> m_Limit;
    }

    [DataContract()]
    private sealed class SearchAddressBookLocationResponse
    {
        [DataMember(Name = "results")]
        public List<string[]> Results
        {
            get { return m_Results; }
            set { m_Results = value; }
        }

        private List<string[]> m_Results;
        [DataMember(Name = "total")]
        public uint Total
        {
            get { return m_Total; }
            set { m_Total = value; }
        }

        private uint m_Total;
        [DataMember(Name = "fields")]
        public string[] Fields
        {
            get { return m_Fields; }
            set { m_Fields = value; }
        }
        private string[] m_Fields;
    }

    public List<string[]> SearchAddressBookLocation(AddressBookParameters addressBookParameters, out uint total, out string errorString)
    {
        total = 0;

        SearchAddressBookLocationRequest request = new SearchAddressBookLocationRequest
        {
            Query = addressBookParameters.Query,
            Fields = addressBookParameters.Fields,
            Offset = addressBookParameters.Offset >= 0 ? (int)addressBookParameters.Offset : 0,
            Limit = addressBookParameters.Limit >= 0 ? (int)addressBookParameters.Limit : 0
        };

        var response = GetJsonObjectFromAPI<SearchAddressBookLocationResponse>(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Get, out errorString);
        List<string[]> result = null;
        if (response != null)
        {
            result = response.Results;
            total = response.Total;
        }
        return result;
    }

    public AddressBookContact AddAddressBookContact(AddressBookContact contact, out string errorString)
    {
      contact.PrepareForSerialization();
      AddressBookContact result = GetJsonObjectFromAPI<AddressBookContact>(contact,
                                                           R4MEInfrastructureSettings.AddressBook,
                                                           HttpMethodType.Post,
                                                           out errorString);
      return result;
    }


    public AddressBookContact UpdateAddressBookContact(AddressBookContact contact, out string errorString)
    {
      contact.PrepareForSerialization();
      AddressBookContact result = GetJsonObjectFromAPI<AddressBookContact>(contact,
                                                           R4MEInfrastructureSettings.AddressBook,
                                                           HttpMethodType.Put,
                                                           out errorString);
      return result;
    }


    [DataContract]
    private sealed class RemoveAddressBookContactsRequest : GenericParameters
    {
      [DataMember(Name = "address_ids", EmitDefaultValue = false)]
      public string[] AddressIds { get; set; }
    }

    [DataContract]
    private sealed class RemoveAddressBookContactsResponse
    {
      [DataMember(Name = "status")]
      public bool Status { get; set; }
    }

    public bool RemoveAddressBookContacts(string[] addressIds, out string errorString)
    {
      RemoveAddressBookContactsRequest request = new RemoveAddressBookContactsRequest()
      {
        AddressIds = addressIds
      };
      RemoveAddressBookContactsResponse response = GetJsonObjectFromAPI<RemoveAddressBookContactsResponse>(request,
                                                             R4MEInfrastructureSettings.AddressBook,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      if (response != null && response.Status)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Avoidance Zones

    /// <summary>
    /// Create avoidance zone
    /// </summary>
    /// <param name="avoidanceZoneParameters"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object </returns>
    public AvoidanceZone AddAvoidanceZone(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
    {
      AvoidanceZone avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneParameters,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Post,
                                                             out errorString);
      return avoidanceZone;
    }

    /// <summary>
    /// Get avoidance zones
    /// </summary>
    /// <param name="avoidanceZoneQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object list </returns>
    public AvoidanceZone[] GetAvoidanceZones(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
    {
      AvoidanceZone[] avoidanceZones = GetJsonObjectFromAPI<AvoidanceZone[]>(avoidanceZoneQuery,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Get,
                                                             out errorString);
      return avoidanceZones;
    }

    /// <summary>
    /// Get avoidance zone by parameters (territory id, device id)
    /// </summary>
    /// <param name="avoidanceZoneQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object </returns>
    public AvoidanceZone GetAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
    {
      AvoidanceZone avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneQuery,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Get,
                                                             out errorString);
      return avoidanceZone;
    }

    /// <summary>
    /// Update avoidance zone (by territory id, device id)
    /// </summary>
    /// <param name="avoidanceZoneParameters"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object </returns>
    public AvoidanceZone UpdateAvoidanceZone(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
    {
      AvoidanceZone avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneParameters,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Put,
                                                             out errorString);
      return avoidanceZone;
    }

    [DataContract]
    private sealed class DeleteAvoidanceZoneResponse
    {
        [DataMember(Name = "status")]
        public Boolean status { get; set; }

    }
    /// <summary>
    /// Delete avoidance zone (by territory id, device id)
    /// </summary>
    /// <param name="avoidanceZoneQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Result status true/false </returns>
    public bool DeleteAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
    {
        var result = GetJsonObjectFromAPI<DeleteAvoidanceZoneResponse>(avoidanceZoneQuery,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Delete,
                                                             out errorString);

        return result.status;
    }

    #endregion

    #region Orders

    [DataContract]
    private sealed class GetOrdersResponse
    {
      [DataMember(Name = "results")]
      public Order[] Results { get; set; }

      [DataMember(Name = "total")]
      public uint Total { get; set; }
    }

    /// <summary>
    /// Get Orders
    /// </summary>
    /// <param name="ordersQuery"> Parameters for request </param>
    /// <param name="total"> out: Total number of orders </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Order object list </returns>
    public Order[] GetOrders(OrderParameters ordersQuery, out uint total, out string errorString)
    {
      total = 0;
      GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(ordersQuery,
                                                           R4MEInfrastructureSettings.Order,
                                                           HttpMethodType.Get,
                                                           out errorString);
      Order[] result = null;
      if (response != null)
      {
        result = response.Results;
        total = response.Total;
      }
      return result;
    }

    public Order[] GetOrderByID(OrderParameters orderQuery, out string errorString)
    {
        string[] ids = orderQuery.order_id.Split(',');
        if (ids.Length == 1) orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id;
        GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);

        return response.Results;
    }

    public Order[] SearchOrders(OrderParameters orderQuery, out string errorString)
    {
        GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);

        Order[] result = null;
        if (response != null)
        {
            result = response.Results;
        }
        return result;
    }

    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="order"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Order object </returns>
    public Order AddOrder(Order order, out string errorString)
    {
      order.PrepareForSerialization();
      Order resultOrder = GetJsonObjectFromAPI<Order>(order,
                                                             R4MEInfrastructureSettings.Order,
                                                             HttpMethodType.Post,
                                                             out errorString);
      return resultOrder;
    }

    /// <summary>
    /// Update Order
    /// </summary>
    /// <param name="order"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Order Object </returns>
    public Order UpdateOrder(Order order, out string errorString)
    {
      order.PrepareForSerialization();
      Order resultOrder = GetJsonObjectFromAPI<Order>(order,
                                                           R4MEInfrastructureSettings.Order,
                                                           HttpMethodType.Put,
                                                           out errorString);
      return resultOrder;
    }

    [DataContract]
    private sealed class RemoveOrdersRequest : GenericParameters
    {
      [DataMember(Name = "order_ids", EmitDefaultValue = false)]
      public string[] OrderIds { get; set; }
    }

    [DataContract]
    private sealed class RemoveOrdersResponse
    {
      [DataMember(Name = "status")]
      public bool Status { get; set; }
    }

    /// <summary>
    /// Remove Orders
    /// </summary>
    /// <param name="orderIds"> Order IDs </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Result status true/false </returns>
    public bool RemoveOrders(string[] orderIds, out string errorString)
    {
      RemoveOrdersRequest request = new RemoveOrdersRequest()
      {
        OrderIds = orderIds
      };
      RemoveOrdersResponse response = GetJsonObjectFromAPI<RemoveOrdersResponse>(request,
                                                             R4MEInfrastructureSettings.Order,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      if (response != null && response.Status)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    [DataContract()]
    private sealed class AddOrdersToRouteRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId
        {
            get { return m_RouteId; }
            set { m_RouteId = value; }
        }

        private string m_RouteId;
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public System.Nullable<int> Redirect
        {
            get { return m_Redirect; }
            set { m_Redirect = value; }
        }

        private System.Nullable<int> m_Redirect;
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses
        {
            get { return m_Addresses; }
            set { m_Addresses = value; }
        }

        private Address[] m_Addresses;
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }

        private RouteParameters m_Parameters;
    }

    public RouteResponse AddOrdersToRoute(RouteParametersQuery rQueryParams, Address[] addresses, RouteParameters rParams, out string errorString)
    {
        AddOrdersToRouteRequest request = new AddOrdersToRouteRequest
        {
            RouteId = rQueryParams.RouteId,
            Redirect = rQueryParams.Redirect==true ? 1: 0,
            Addresses = addresses,
            Parameters = rParams
        };

        RouteResponse response = GetJsonObjectFromAPI<RouteResponse>(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, false, out errorString);

        return response;
    }

    [DataContract()]
    private sealed class AddOrdersToOptimizationRequest : GenericParameters
    {
        [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId
        {
            get { return m_OptimizationProblemId; }
            set { m_OptimizationProblemId = value; }
        }

        private string m_OptimizationProblemId;
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public System.Nullable<int> Redirect
        {
            get { return m_Redirect; }
            set { m_Redirect = value; }
        }

        private System.Nullable<int> m_Redirect;
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses
        {
            get { return m_Addresses; }
            set { m_Addresses = value; }
        }

        private Address[] m_Addresses;
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }

        private RouteParameters m_Parameters;
    }

    public DataObject AddOrdersToOptimization(OptimizationParameters rQueryParams, Address[] addresses, RouteParameters rParams, out string errorString)
    {
        AddOrdersToOptimizationRequest request = new AddOrdersToOptimizationRequest
        {
            OptimizationProblemId = rQueryParams.OptimizationProblemID,
            Redirect = rQueryParams.Redirect==true ? 1 : 0,
            Addresses = addresses,
            Parameters = rParams
        };

        DataObject response = GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, false, out errorString);

        return response;
    }

    #endregion

    #region Geocoding

    [DataContract()]
    private sealed class GeocodingRequest : GenericParameters
    {

        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public string Addresses
        {
            get { return m_Addresses; }
            set { m_Addresses = value; }
        }

        private string m_Addresses;
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        private string m_Format;
    }

    [DataContract()]
    private sealed class RapidStreetResponse
    {
        [DataMember(Name = "zipcode")]
        public string Zipcode
        {
            get { return m_Zipcode; }
            set { m_Zipcode = value; }
        }

        private string m_Zipcode;
        [DataMember(Name = "street_name")]
        public string StreetName
        {
            get { return m_StreetName; }
            set { m_StreetName = value; }
        }
        private string m_StreetName;
    }

    public string Geocoding(GeocodingParameters geoParams, out string errorString)
    {
        GeocodingRequest request = new GeocodingRequest
        {
            Addresses = geoParams.Addresses,
            Format = geoParams.Format
        };

        string response = GetXmlObjectFromAPI<string>(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, (HttpContent)null, true, out errorString);

        return response.ToString();
    }

    public string BatchGeocoding(GeocodingParameters geoParams, out string errorString)
    {
            GeocodingRequest request = new GeocodingRequest { };

            var keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("strExportFormat", geoParams.Format));
            keyValues.Add(new KeyValuePair<string, string>("addresses", geoParams.Addresses));

            HttpContent httpContent = new FormUrlEncodedContent(keyValues);

            string response = GetJsonObjectFromAPI<string>(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, httpContent, true, out errorString);

            return response.ToString();
        }

    public ArrayList RapidStreetData(GeocodingParameters geoParams, out string errorString)
    {
	    GeocodingRequest request = new GeocodingRequest {
		    Addresses = geoParams.Addresses,
		    Format = geoParams.Format
	    };
	    string url = R4MEInfrastructureSettings.RapidStreetData;

	    ArrayList result = new ArrayList();

	    if (geoParams.Pk > 0) {
		    url = url + "/" + geoParams.Pk + "/";
		    RapidStreetResponse response = GetJsonObjectFromAPI<RapidStreetResponse>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);
		    Dictionary<string, string> dresult = new Dictionary<string, string>();
		    if ((response != null)) {
			    dresult["zipcode"] = response.Zipcode;
			    dresult["street_name"] = response.StreetName;
			    result.Add(dresult);
		    }
	    } else {
		    if (geoParams.Offset > 0 | geoParams.Limit > 0) {
			    url = url + "/" + geoParams.Offset + "/" + geoParams.Limit + "/";
		    }
		    RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);
		    if ((response != null)) {
			    foreach (var resp1 in response) {
				    Dictionary<string, string> dresult = new Dictionary<string, string>();
				    dresult["zipcode"] = resp1.Zipcode;
				    dresult["street_name"] = resp1.StreetName;
				    result.Add(dresult);
			    }
		    }
	    }

	    return result;
    }

    public ArrayList RapidStreetZipcode(GeocodingParameters geoParams, out string errorString)
    {
	    GeocodingRequest request = new GeocodingRequest {
		    Addresses = geoParams.Addresses,
		    Format = geoParams.Format
	    };
	    string url = R4MEInfrastructureSettings.RapidStreetZipcode;

	    ArrayList result = new ArrayList();

	    if (geoParams.Zipcode != null) {
		    url = url + "/" + geoParams.Zipcode + "/";
	    } else {
            errorString = "Zipcode is not defined!...";
		    return result;
	    }
	    if (geoParams.Offset > 0 | geoParams.Limit > 0) {
		    url = url + geoParams.Offset + "/" + geoParams.Limit + "/";
	    }

	    RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);
	    if ((response != null)) {
		    foreach (var resp1 in response) {
			    Dictionary<string, string> dresult = new Dictionary<string, string>();
			    dresult["zipcode"] = resp1.Zipcode;
			    dresult["street_name"] = resp1.StreetName;
			    result.Add(dresult);
		    }
	    }

	    return result;
    }

    public ArrayList RapidStreetService(GeocodingParameters geoParams, out string errorString)
    {
	    GeocodingRequest request = new GeocodingRequest {
		    Addresses = geoParams.Addresses,
		    Format = geoParams.Format
	    };
	    string url = R4MEInfrastructureSettings.RapidStreetService;

	    ArrayList result = new ArrayList();

	    if (geoParams.Zipcode != null) {
		    url = url + "/" + geoParams.Zipcode + "/";
	    } else {
            errorString = "Zipcode is not defined!...";
		    return result;
	    }
	    if (geoParams.Housenumber != null) {
		    url = url + geoParams.Housenumber + "/";
	    } else {
            errorString = "Housenumber is not defined!...";
		    return result;
	    }
	    if (geoParams.Offset > 0 | geoParams.Limit > 0) {
		    url = url + geoParams.Offset + "/" + geoParams.Limit + "/";
	    }

	    RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);
	    if ((response != null)) {
		    foreach (var resp1 in response) {
			    Dictionary<string, string> dresult = new Dictionary<string, string>();
			    dresult["zipcode"] = resp1.Zipcode;
			    dresult["street_name"] = resp1.StreetName;
			    result.Add(dresult);
		    }
	    }

	    return result;
    }

    #endregion

    #region Vehicles

    /// <summary>
    /// Get Vehicles
    /// </summary>
    /// <param name="vehParams"> Parameters for request </param>
    /// <param name="total"> out: Total number of Vehicles </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Vehicle object list </returns>
    public VehicleResponse[] GetVehicles(VehicleParameters vehParams, out string errorString)
    {
        VehicleResponse[] response = GetJsonObjectFromAPI<VehicleResponse[]>(vehParams, R4MEInfrastructureSettings.ViewVehicles, HttpMethodType.Get, out errorString);

        return response;
    }

    #endregion

    #region Territories
    /// <summary>
    /// Create territory
    /// </summary>
    /// <param name="avoidanceZoneParameters"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Territory Object </returns>
    public TerritoryZone CreateTerritory(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
    {
        TerritoryZone avoidanceZone = GetJsonObjectFromAPI<TerritoryZone>(avoidanceZoneParameters, R4MEInfrastructureSettings.Territory, HttpMethodType.Post, out errorString);
        return avoidanceZone;
    }

    /// <summary>
    /// Get territories by parameters
    /// </summary>
    /// <param name="avoidanceZoneQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Territory zone Objects </returns>
    public AvoidanceZone[] GetTerritories(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
    {
        AvoidanceZone[] territories = GetJsonObjectFromAPI<AvoidanceZone[]>(avoidanceZoneQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.Get, out errorString);
        return territories;
    }

    /// <summary>
    /// Get territory by parameters (territory id, device id, addresses)
    /// </summary>
    /// <param name="territoryQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Territory zone Object </returns>
    public TerritoryZone GetTerritory(TerritoryQuery territoryQuery, out string errorString)
    {
        TerritoryZone territory = GetJsonObjectFromAPI<TerritoryZone>(territoryQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.Get, out errorString);
        return territory;
    }

    [DataContract]
    private sealed class RemoveTerritoryResponse
    {
        [DataMember(Name = "status")]
        public Boolean status { get; set; }

    }
    /// <summary>
    /// Remove Territory (by territory id, device id)
    /// </summary>
    /// <param name="territoryQuery"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Result status true/false </returns>
    public bool RemoveTerritory(AvoidanceZoneQuery territoryQuery, out string errorString)
    {
        var result = GetJsonObjectFromAPI<RemoveTerritoryResponse>(territoryQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.Delete, out errorString);

        return result.status;
    }

    /// <summary>
    /// Update Territory (by territory id, device id)
    /// </summary>
    /// <param name="tereritoryParameters"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Territory Object </returns>
    public AvoidanceZone UpdateTerritory(AvoidanceZoneParameters tereritoryParameters, out string errorString)
    {
        AvoidanceZone territory = GetJsonObjectFromAPI<AvoidanceZone>(tereritoryParameters, R4MEInfrastructureSettings.Territory, HttpMethodType.Put, out errorString);
        return territory;
    }

    #endregion

    #endregion

    #region Generic Methods

    public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
                                           string            url,
                                           HttpMethodType    httpMethod,
                                           out string        errorMessage)
    {
      string result = GetJsonObjectFromAPI<string>(optimizationParameters,
                                                   url,
                                                   httpMethod,
                                                   true,
                                                   out errorMessage);

      return result;
    }

    public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                     string            url,
                                     HttpMethodType    httpMethod,
                                     out string        errorMessage)
      where T : class
    {
      T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                         url,
                                         httpMethod,
                                         false,
                                         out errorMessage);

      return result;
    }

    public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                     string url,
                                     HttpMethodType httpMethod,
                                     HttpContent httpContent,
                                     out string errorMessage)
      where T : class
    {
      T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                         url,
                                         httpMethod,
                                         httpContent,
                                         false,
                                         out errorMessage);

      return result;
    }

    private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                      string            url,
                                      HttpMethodType    httpMethod,
                                      bool              isString,
                                      out string        errorMessage)
      where T : class
    {
      T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                         url,
                                         httpMethod,
                                         (HttpContent)null,
                                         isString,
                                         out errorMessage);

      return result;
    }

    private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                      string            url,
                                      HttpMethodType    httpMethod,
                                      HttpContent       httpContent,
                                      bool              isString,
                                      out string        errorMessage)
      where T : class
    {
      T result = default(T);
      errorMessage = string.Empty;

      try
      {
        using (HttpClient httpClient = CreateHttpClient(url))
        {
          // Get the parameters
          string parametersURI = optimizationParameters.Serialize(m_ApiKey);

          switch (httpMethod)
          {
            case HttpMethodType.Get:
            {
              var response = httpClient.GetStreamAsync(parametersURI);
              response.Wait();

              if (response.IsCompleted)
              {
                //var test = m_isTestMode ? response.Result.ReadString() : null;
                //var test = response.Result.ReadString();
                result = isString ? response.Result.ReadString() as T :
                                    response.Result.ReadObject<T>();
              }

              break;
            }
            case HttpMethodType.Post:
            case HttpMethodType.Put:
            case HttpMethodType.Delete:
            {
              bool isPut = httpMethod == HttpMethodType.Put;
              bool isDelete = httpMethod == HttpMethodType.Delete;
              HttpContent content = null;
              if (httpContent != null)
              {
                content = httpContent;
              }
              else
              {
                string jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                content = new StringContent(jsonString);
              }

              Task<HttpResponseMessage> response = null;
              if (isPut)
              {
                response = httpClient.PutAsync(parametersURI, content);
              }
              else if(isDelete)
              {
                HttpRequestMessage request = new HttpRequestMessage
                {
                  Content = content,
                  Method = HttpMethod.Delete,
                  RequestUri = new Uri(parametersURI, UriKind.Relative)
                };
                response = httpClient.SendAsync(request);
              }
              else
              {
                response = httpClient.PostAsync(parametersURI, content);
              }

              // Wait for response
              response.Wait();

              // Check if successful
              if (response.IsCompleted &&
                  response.Result.IsSuccessStatusCode &&
                  response.Result.Content is StreamContent)
              {
                var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                streamTask.Wait();

                if (streamTask.IsCompleted)
                {
                  //var test = m_isTestMode ? streamTask.Result.ReadString() : null;
                  //var test = streamTask.Result.ReadString();
                  result = isString ? streamTask.Result.ReadString() as T :
                                      streamTask.Result.ReadObject<T>();
                }
              }
              else
              {
                  var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                  streamTask.Wait();
                  ErrorResponse errorResponse = null;
                  try
                  {
                      errorResponse = streamTask.Result.ReadObject<ErrorResponse>();
                  }
                  catch// (Exception e)
                  {
                      errorResponse = default(ErrorResponse);
                  }
                  if (errorResponse != null && errorResponse.Errors != null && errorResponse.Errors.Count > 0)
                  {
                      foreach (String error in errorResponse.Errors)
                      {
                          if (errorMessage.Length > 0)
                              errorMessage += "; ";
                          errorMessage += error;
                      }
                  }
                  else
                  {
                      var responseStream = response.Result.Content.ReadAsStringAsync();
                      responseStream.Wait();
                      String responseString = responseStream.Result;
                      if (responseString != null)
                         errorMessage = "Response: " + responseString;
                  }
              }

              break;
            }
          }
        }
      }
      catch (Exception e)
      {
        errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;
        result = default(T);
      }

      return result;
    }

    private string GetXmlObjectFromAPI<T>(GenericParameters optimizationParameters, string url, HttpMethodType httpMethod__1, HttpContent httpContent, bool isString, out string errorMessage) where T : class
    {
        string result = string.Empty;
        errorMessage = string.Empty;

        try
        {
            using (HttpClient httpClient = CreateHttpClient(url))
            {
                // Get the parameters
                string parametersURI = optimizationParameters.Serialize(m_ApiKey);

                switch (httpMethod__1)
                {
                    case HttpMethodType.Get:
                        if (true)
                        {
                            var response = httpClient.GetStreamAsync(parametersURI);
                            response.Wait();

                            if (response.IsCompleted)
                            {
                                //var test = m_isTestMode ? response.Result.ReadString() : null;
                                result = isString ? response.Result.ReadString() as String : response.Result.ReadObject<String>(); // Oleg T -> String
                            }
                        }
                        break;
                    case HttpMethodType.Post:
                        if (true)
                        {
                            var response = httpClient.GetStreamAsync(parametersURI);
                            response.Wait();

                            if (response.IsCompleted)
                            {
                                //var test = m_isTestMode ? response.Result.ReadString() : null;
                                result = isString ? response.Result.ReadString() as String : response.Result.ReadObject<String>(); // Oleg T -> String
                            }
                        }
                        break;
                    case HttpMethodType.Put:
                        break;
                    case HttpMethodType.Delete:
                        if (true)
                        {
                            bool isPut = httpMethod__1 == HttpMethodType.Put;
                            bool isDelete = httpMethod__1 == HttpMethodType.Delete;
                            HttpContent content = null;
                            if (httpContent != null)
                            {
                                content = httpContent;
                            }
                            else
                            {
                                string jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                            }

                            Task<HttpResponseMessage> response = null;
                            if (isPut)
                            {
                                response = httpClient.PutAsync(parametersURI, content);
                            }
                            else if (isDelete)
                            {
                                HttpRequestMessage request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(parametersURI, UriKind.Relative)
                                };
                                response = httpClient.SendAsync(request);
                            }
                            else
                            {
                                response = httpClient.PostAsync(parametersURI, content);
                            }

                            // Wait for response
                            response.Wait();

                            // Check if successful
                            if (response.IsCompleted && response.Result.IsSuccessStatusCode && response.Result.Content is StreamContent)
                            {
                                var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                                streamTask.Wait();

                                if (streamTask.IsCompleted)
                                {
                                    //var test = m_isTestMode ? streamTask.Result.ReadString() : null;
                                    //var test = streamTask.Result.ReadString();
                                    result = streamTask.Result.ReadString();
                                    //result = If(isString, TryCast(streamTask.Result.ReadString(), XmlDocument), streamTask.Result.ReadObject(Of XmlDocument)())
                                }
                            }
                            else
                            {
                                var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                                streamTask.Wait();
                                ErrorResponse errorResponse = null;
                                try
                                {
                                    errorResponse = streamTask.Result.ReadObject<ErrorResponse>();
                                }
                                catch
                                {
                                    // (Exception e)
                                    errorResponse = null;
                                }
                                if (errorResponse != null && errorResponse.Errors != null && errorResponse.Errors.Count > 0)
                                {
                                    foreach (String error in errorResponse.Errors)
                                    {
                                        if (errorMessage.Length > 0)
                                        {
                                            errorMessage += "; ";
                                        }
                                        errorMessage += error;
                                    }
                                }
                                else
                                {
                                    var responseStream = response.Result.Content.ReadAsStringAsync();
                                    responseStream.Wait();
                                    String responseString = responseStream.Result;
                                    if (responseString != null)
                                    {
                                        errorMessage = "Response: " + responseString;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        catch (Exception e)
        {
            errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;
            result = null;
        }

        return result;
    }

    private HttpClient CreateHttpClient(string url)
    {
      HttpClient result = new HttpClient() { BaseAddress = new Uri(url) };

      result.Timeout = m_DefaultTimeOut;
      result.DefaultRequestHeaders.Accept.Clear();
      result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return result;
    }

    #endregion

    #endregion
  }
}
