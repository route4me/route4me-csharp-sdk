using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

    public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, out string errorString)
    {
      var keyValues = new List<KeyValuePair<string, string>>();
      var strUpdateType = "unclassified";
      if (noteParameters.ActivityType != null && noteParameters.ActivityType.Length > 0)
      {
        strUpdateType = noteParameters.ActivityType;
      }
      keyValues.Add(new KeyValuePair<string, string>("strUpdateType", strUpdateType));
      keyValues.Add(new KeyValuePair<string, string>("strNoteContents", noteContents));
      HttpContent httpContent = new FormUrlEncodedContent(keyValues);

      AddAddressNoteResponse response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
                                                           R4MEInfrastructureSettings.AddRouteNotesHost,
                                                           HttpMethodType.Post,
                                                           httpContent,
                                                           out errorString);
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

    public Activity[] GetActivityFeed(ActivityParameters activityParameters, out string errorString)
    {
      GetActivitiesResponse response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                                                           R4MEInfrastructureSettings.GetActivitiesHost,
                                                           HttpMethodType.Get,
                                                           out errorString);
      Activity[] result = null;
      if (response != null)
      {
        result = response.Results;
      }
      return result;
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
    }

    public int[] AddRouteDestinations(string routeId, Address[] addresses, out string errorString)
    {
      AddRouteDestinationRequest request = new AddRouteDestinationRequest()
      {
        RouteId = routeId,
        Addresses = addresses
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

    #endregion

    #region Address Book

    [DataContract]
    private sealed class GetAddressBookContactsResponse
    {
      [DataMember(Name = "results")]
      public AddressBookContact[] Results { get; set; }

      [DataMember(Name = "total")]
      public uint Total { get; set; }
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
    /// <param name="avoidanceZoneQuerry"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object list </returns>
    public AvoidanceZone[] GetAvoidanceZones(AvoidanceZoneQuerry avoidanceZoneQuerry, out string errorString)
    {
      AvoidanceZone[] avoidanceZones = GetJsonObjectFromAPI<AvoidanceZone[]>(avoidanceZoneQuerry,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Get,
                                                             out errorString);
      return avoidanceZones;
    }

    /// <summary>
    /// Get avoidance zone by parameters (territory id, device id)
    /// </summary>
    /// <param name="avoidanceZoneQuerry"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Avoidance zone Object </returns>
    public AvoidanceZone GetAvoidanceZone(AvoidanceZoneQuerry avoidanceZoneQuerry, out string errorString)
    {
      AvoidanceZone avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneQuerry,
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

    /// <summary>
    /// Delete avoidance zone (by territory id, device id)
    /// </summary>
    /// <param name="avoidanceZoneQuerry"> Parameters for request </param>
    /// <param name="errorString"> out: Error as string </param>
    /// <returns> Result status true/false </returns>
    public bool DeleteAvoidanceZone(AvoidanceZoneQuerry avoidanceZoneQuerry, out string errorString)
    {
      GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneQuerry,
                                                             R4MEInfrastructureSettings.Avoidance,
                                                             HttpMethodType.Delete,
                                                             out errorString);
      return errorString != "";
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
