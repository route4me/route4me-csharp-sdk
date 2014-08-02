using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Route4MeSDK
{
  /// <summary>
  /// This class encapsulates the Route4Me REST API
  /// 1. Create an instance of Route4MeManager with the api_key
  /// 1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetRoute()) to access the most popular functionality.
  ///    See examples Route4MeExamples.GetRoute(), Route4MeExamples.SingleDriverRoundTrip()
  /// 2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or Route4MeManager.GetStringResponseFromAPI())
  ///    to access any availaible functionality.
  ///    See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
  /// </summary>
  public sealed class Route4MeManager
  {
    #region Fields

    private readonly string m_ApiKey;

    #endregion

    #region Methods

    public Route4MeManager(string apiKey)
    {
      m_ApiKey = apiKey;
    }

    #region Route4Me Shortcut Methods

    public DataObject GetRoute(OptimizatonParameters optimizatonParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizatonParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
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

    public DataObject UpdateOptimization(OptimizatonParameters optimizatonParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizatonParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Put,
                                                    false,
                                                    out errorString);

      return result;
    }

    public DataObject RunOptimization(OptimizatonParameters optimizatonParameters, out string errorString)
    {
      var result = GetJsonObjectFromAPI<DataObject>(optimizatonParameters,
                                                    R4MEInfrastructureSettings.ApiHost,
                                                    HttpMethodType.Post,
                                                    false,
                                                    out errorString);

      return result;
    }

    #endregion

    #region Generic Methods

    public string GetStringResponseFromAPI(GenericParameters optimizatonParameters,
                                           string            url,
                                           HttpMethodType    httpMethod,
                                           out string        errorMessage)
    {
      string result = GetJsonObjectFromAPI<string>(optimizatonParameters,
                                                   url,
                                                   httpMethod,
                                                   true,
                                                   out errorMessage);

      return result;
    }

    public T GetJsonObjectFromAPI<T>(GenericParameters optimizatonParameters,
                                     string            url,
                                     HttpMethodType    httpMethod,
                                     out string        errorMessage)
      where T : class
    {
      T result = GetJsonObjectFromAPI<T>(optimizatonParameters,
                                         url,
                                         httpMethod,
                                         false,
                                         out errorMessage);

      return result;
    }

    private T GetJsonObjectFromAPI<T>(GenericParameters optimizatonParameters,
                                      string            url,
                                      HttpMethodType    httpMethod,
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
          string parametersURI = optimizatonParameters.Serialize(m_ApiKey);

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
                string jsonString = R4MeUtils.SerializeObjectToJson(optimizatonParameters);
                StringContent content = new StringContent(jsonString);

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

      result.DefaultRequestHeaders.Accept.Clear();
      result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return result;
    }

    #endregion
  }
}
