using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

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

    #endregion

    #region Methods

    public Route4MeManager(string apiKey)
    {
      m_ApiKey = apiKey;
    }

    #region Route4Me Shortcut Methods

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

    public Address GetAddress(AddressParameters addressParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<Address>(addressParameters,
                                                           R4MEInfrastructureSettings.GetAddress,
                                                           HttpMethodType.Get,
                                                           out errorString);

      return result;
    }

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

    public DataObject UpdateOptimization(OptimizationParameters optimizationParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Put,
                                                    false,
                                                    out errorString);

      return result;
    }

    public DataObject RunOptimization(OptimizationParameters optimizationParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Post,
                                                    false,
                                                    out errorString);

      return result;
    }

    public User[] GetUsers(GenericParameters parameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<User[]>(parameters,
                                                           R4MEInfrastructureSettings.GetUsersHost,
                                                           HttpMethodType.Get,
                                                           out errorString);

      return result;
    }

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
        //noteParameters.ActivityType = null;
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
                result = isString ? response.Result.ReadString() as T :
                                    response.Result.ReadObject<T>();
              }

              break;
            }
            case HttpMethodType.Post:
            case HttpMethodType.Put:
            {
              bool isPut = httpMethod == HttpMethodType.Put;
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

              // Post and wait for response
              var response = isPut ? httpClient.PutAsync(parametersURI, content) :
                                      httpClient.PostAsync(parametersURI, content);

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

    #endregion

    private HttpClient CreateHttpClient(string url)
    {
      HttpClient result = new HttpClient() { BaseAddress = new Uri(url) };

      result.Timeout = m_DefaultTimeOut;
      result.DefaultRequestHeaders.Accept.Clear();
      result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return result;
    }

    #endregion
  }
}
