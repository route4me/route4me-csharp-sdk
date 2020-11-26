using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK
{
    /// <summary>
	/// This class encapsulates the Route4Me REST API 5
	/// 1. Create an instance of Route4MeManager with the api_key
	/// 1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most popular functionality.
	///    See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
	/// 2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or Route4MeManager.GetStringResponseFromAPI())
	///    to access any availaible functionality.
	///    See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
	/// </summary>
    public sealed class Route4MeManagerV5
    {
        #region Fields

        private readonly string m_ApiKey;
        private readonly TimeSpan m_DefaultTimeOut = new TimeSpan(TimeSpan.TicksPerMinute * 30); // Default timeout - 30 minutes
                                                                                                 //private bool m_isTestMode = false;

        private bool parseWithNewtonJson;
        #endregion

        #region Constructors

        public Route4MeManagerV5(string apiKey)
        {
            m_ApiKey = apiKey;
            parseWithNewtonJson = false;
        }

        #endregion

        #region Team Management

        /// <summary>
        /// The request parameters for retrieving team members.
        /// </summary>
		[DataContract()]
        public sealed class MemberQueryParameters : GenericParameters
        {
            /// <value>Team user ID</value>
			[HttpQueryMemberAttribute(Name = "user_id", EmitDefaultValue = false)]
            public string UserId { get; set; }
        }

        /// <summary>
        /// The request class to bulk create the team members.
        /// </summary>
        [DataContract]
        private sealed class BulkMembersRequest : GenericParameters
        {
            // Array of the team member requests
            [DataMember(Name = "users")]
            public TeamRequest[] Users { get; set; }
        }

        /// <summary>
        /// Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public TeamResponse[] GetTeamMembers(out ResultResponse failResponse)
        {
            var parameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<TeamResponse[]>(parameters,
                                R4MEInfrastructureSettingsV5.TeamUsers,
                                HttpMethodType.Get,
                                out failResponse);

            return result;
        }

        /// <summary>
        /// Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns></returns>
        public TeamResponse GetTeamMemberById(MemberQueryParameters parameters, 
                                              out ResultResponse resultResponse)
        {
            if ((parameters?.UserId ?? null)==null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The UserId parameter is not specified" } }
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<TeamResponse>(parameters,
                                R4MEInfrastructureSettingsV5.TeamUsers+"/"+ parameters.UserId,
                                HttpMethodType.Get,
                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type TeamResponse</returns>
		public TeamResponse CreateTeamMember(TeamRequest memberParams, 
                                            out ResultResponse resultResponse)
        {
            if (!memberParams.ValidateMemberCreateRequest(out string error0))
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { error0 } }
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<TeamResponse>(
                            memberParams,
                            R4MEInfrastructureSettingsV5.TeamUsers, 
                            HttpMethodType.Post, 
                            out resultResponse);
        }

        /// <summary>
        /// Bulk create the team members
        /// TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <param name="errorString"></param>
        /// <returns></returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, out ResultResponse resultResponse)
        {
            resultResponse = default(ResultResponse);

            if ((membersParams?.Length ?? 0)<1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The array of the user parameters is empty" } }
                    }
                };

                return null;
            }

            foreach (var memberParams in membersParams)
            {
                if (!memberParams.ValidateMemberCreateRequest(out string error0))
                {
                    resultResponse = new ResultResponse()
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>()
                        {
                            { "Error", new string[] { error0 } }
                        }
                    };

                    return null;
                }
            }

            var newMemberParams = new BulkMembersRequest()
            {
                Users = membersParams
            };

            var result = GetJsonObjectFromAPI<ResultResponse>(
                            newMemberParams,
                            R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                            HttpMethodType.Post,
                            out resultResponse);

            return result;
        }

        /// <summary>
        /// Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type TeamResponse</returns>
		public TeamResponse RemoveTeamMember(MemberQueryParameters parameters, 
                                                out ResultResponse resultResponse)
        {
            if ((parameters?.UserId ?? null) == null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The UserId parameter is not specified" } }
                    }
                };
                
                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                                    parameters, 
                                    R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId, 
                                    HttpMethodType.Delete, 
                                    out resultResponse);

            return response;
        }


        /// <summary>
        /// Update a team member
        /// </summary>
        /// <param name="parameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <param name="errorString">>Error message text</param>
        /// <returns>An object of the type TeamResponse</returns>
        public TeamResponse UpdateTeamMember(MemberQueryParameters queryParameters, 
                                             TeamRequest requestPayload, 
                                             out ResultResponse resultResponse)
        {
            if ((queryParameters?.UserId ?? null) == null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The UserId parameter is not specified" } }
                    }
                };

                return null;
            }

            if (requestPayload == null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The team request object is empty" } }
                    }
                };

                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                                    requestPayload,
                                    R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                                    HttpMethodType.Patch,
                                    out resultResponse);

            return response;
        }

        #endregion

        #region Driver Review

        /// <summary>
        /// Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>List of the drive reviews</returns>
        public DriverReviewsResponse GetDriverReviewList(DriverReviewParameters parameters, 
                                                         out ResultResponse resultResponse)
        {
            

            parseWithNewtonJson = true;

            var result = GetJsonObjectFromAPI<DriverReviewsResponse>(parameters,
                                R4MEInfrastructureSettingsV5.DriverReview,
                                HttpMethodType.Get,
                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview GetDriverReviewById(DriverReviewParameters parameters,
                                                         out ResultResponse resultResponse)
        {
            if ((parameters?.RatingId ?? null) == null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The RatingId parameter is not specified" } }
                    }
                };

                return null;
            }

            parseWithNewtonJson = true;

            var result = GetJsonObjectFromAPI<DriverReview>(parameters,
                                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                                HttpMethodType.Get,
                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview CreateDriverReview(DriverReview driverReview, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DriverReview>(
                            driverReview,
                            R4MEInfrastructureSettingsV5.DriverReview,
                            HttpMethodType.Post,
                            out resultResponse);
        }

        public DriverReview UpdateDriverReview(DriverReview driverReview,
                                                HttpMethodType method, 
                                                out ResultResponse resultResponse)
        {
            if (method!= HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The parameter method has an incorect value." } }
                    }
                };

                return null;
            }

            if (driverReview.RatingId==null)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The parameters doesn't contain parameter RatingId." } }
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<DriverReview>(
                            driverReview,
                            R4MEInfrastructureSettingsV5.DriverReview+"/"+ driverReview.RatingId,
                            method,
                            out resultResponse);
        }

        #endregion

        #region Generic Methods


        public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
                                               string url,
                                               HttpMethodType httpMethod,
                                               out ResultResponse resultResponse)
        {
            string result = GetJsonObjectFromAPI<string>(optimizationParameters,
                                                         url,
                                                         httpMethod,
                                                         true,
                                                         out resultResponse);

            return result;
        }

        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                         string url,
                                         HttpMethodType httpMethod,
                                         out ResultResponse resultResponse)
          where T : class
        {
            T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                               url,
                                               httpMethod,
                                               false,
                                               out resultResponse);

            return result;
        }

        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                         string url,
                                         HttpMethodType httpMethod,
                                         HttpContent httpContent,
                                         out ResultResponse resultResponse)
          where T : class
        {
            T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                               url,
                                               httpMethod,
                                               httpContent,
                                               false,
                                               out resultResponse);

            return result;
        }

        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                          string url,
                                          HttpMethodType httpMethod,
                                          bool isString,
                                          out ResultResponse resultResponse)
          where T : class
        {
            T result = GetJsonObjectFromAPI<T>(optimizationParameters,
                                               url,
                                               httpMethod,
                                               (HttpContent)null,
                                               isString,
                                               out resultResponse);

            return result;
        }

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
                                        string url,
                                        HttpMethodType httpMethod,
                                        bool isString)
        where T : class
        {
            return await Task.Run(() =>
            {
                Task<Tuple<T, ResultResponse>> result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                                               url,
                                               httpMethod,
                                               (HttpContent)null,
                                               isString);

                return result;
            });


        }

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
                                       string url,
                                       HttpMethodType httpMethod,
                                       HttpContent httpContent,
                                       bool isString)
       where T : class
        {
            //out string errorMessage return this parameter in the tuple

            T result = default(T);
            ResultResponse resultResponse = default(ResultResponse);

            try
            {
                using (HttpClient httpClient = CreateAsyncHttpClient(url))
                {
                    // Get the parameters
                    string parametersURI = optimizationParameters.Serialize(m_ApiKey);

                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                            {
                                var response = await httpClient.GetStreamAsync(parametersURI);

                                result = isString ? response.ReadString() as T :
                                                        response.ReadObject<T>();

                                break;
                            }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                            {
                                bool isPut = httpMethod == HttpMethodType.Put;
                                bool isPatch = httpMethod == HttpMethodType.Patch;
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
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                }

                                HttpResponseMessage response = null;
                                if (isPut)
                                {
                                    response = await httpClient.PutAsync(parametersURI, content);
                                }
                                else if (isPatch)
                                {
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                    response = await httpClient.PatchAsync(parametersURI, content);
                                }
                                else if (isDelete)
                                {
                                    var request = new HttpRequestMessage
                                    {
                                        Content = content,
                                        Method = HttpMethod.Delete,
                                        RequestUri = new Uri(parametersURI, UriKind.Relative)
                                    };
                                    response = await httpClient.SendAsync(request);
                                }
                                else
                                {
                                    var request = new HttpRequestMessage();
                                    response = await httpClient.PostAsync(parametersURI, content).ConfigureAwait(true);
                                }

                                // Check if successful
                                if (response.Content is StreamContent)
                                {
                                    var streamTask = await ((StreamContent)response.Content).ReadAsStreamAsync();

                                    result = isString ? streamTask.ReadString() as T :
                                                            streamTask.ReadObject<T>();
                                }
                                else if (response.Content
                                    .GetType().ToString().ToLower()
                                    .Contains("httpconnectionresponsecontent"))
                                {
                                    var streamTask2 = response.Content.ReadAsStreamAsync();
                                    streamTask2.Wait();

                                    if (streamTask2.IsCompleted)
                                    {
                                        HttpContent content2 = response.Content;

                                        if (isString)
                                        {
                                            result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                        }
                                        else
                                        {
                                            result = parseWithNewtonJson
                                                ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                                : content2.ReadAsStreamAsync().Result.ReadObject<T>();

                                            parseWithNewtonJson = false;
                                        }
                                    }
                                }
                                else
                                {
                                    var streamTask = await ((StreamContent)response.Content).ReadAsStreamAsync();

                                    Task<string> errorMessageContent = null;

                                    if (response.Content.GetType() != typeof(StreamContent)) 
                                        errorMessageContent = response.Content.ReadAsStringAsync();


                                    try
                                    {
                                        resultResponse = streamTask.ReadObject<ResultResponse>();
                                    }
                                    catch// (Exception e)
                                    {
                                        resultResponse = default(ResultResponse);
                                    }
                                    if (resultResponse != null && resultResponse.Messages != null && resultResponse.Messages.Count > 0)
                                    {
                                        //foreach (String error in errorResponse.Errors)
                                        //{
                                        //    if (errorMessage.Length > 0)
                                        //        errorMessage += "; ";
                                        //    errorMessage += error;
                                        //}
                                    }
                                    else if (errorMessageContent != null)
                                    {
                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"ErrorMessageContent", new string[] { errorMessageContent.Result } }
                                            }
                                        };
                                    }
                                    else
                                    {
                                        var responseStream = await response.Content.ReadAsStringAsync();
                                        String responseString = responseStream;

                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"Response", new string[] { responseString } }
                                            }
                                        };
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            catch (HttpListenerException e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message != null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("Error", new string[] { e.InnerException.Message });
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message != null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("InnerException Error", new string[] { e.InnerException.Message });
                }

                result = default(T);
            }

            return new Tuple<T, ResultResponse>(result, resultResponse);
        }


        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
                                              string url,
                                              HttpMethodType httpMethod,
                                              HttpContent httpContent,
                                              bool isString,
                                              out ResultResponse resultResponse)
              where T : class
        {
            T result = default(T);
            resultResponse = default(ResultResponse);

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
                                    if (isString)
                                    {
                                        result = response.Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = parseWithNewtonJson
                                            ? response.Result.ReadObjectNew<T>()
                                            : response.Result.ReadObject<T>();

                                        parseWithNewtonJson = false;
                                    }
                                }

                                break;
                            }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                            {
                                bool isPut = httpMethod == HttpMethodType.Put;
                                bool isPatch = httpMethod == HttpMethodType.Patch;
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
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                }

                                Task<HttpResponseMessage> response = null;
                                if (isPut)
                                {
                                    response = httpClient.PutAsync(parametersURI, content);
                                }
                                else if (isPatch)
                                {
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                    response = httpClient.PatchAsync(parametersURI, content);
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
                                    var cts = new CancellationTokenSource();
                                    cts.CancelAfter(1000 * 60 * 5); // 3 seconds

                                    var request = new HttpRequestMessage();
                                    response = httpClient.PostAsync(parametersURI, content, cts.Token);
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
                                        if (isString)
                                        {
                                            result = streamTask.Result.ReadString() as T;
                                        }
                                        else
                                        {
                                            result = parseWithNewtonJson
                                                ? streamTask.Result.ReadObjectNew<T>()
                                                : streamTask.Result.ReadObject<T>();

                                            parseWithNewtonJson = false;
                                        }
                                    }
                                }
                                else if (response.IsCompleted &&
                                    response.Result.IsSuccessStatusCode &&
                                    response.Result.Content
                                    .GetType().ToString().ToLower()
                                    .Contains("httpconnectionresponsecontent"))
                                {
                                    var streamTask2 = response.Result.Content.ReadAsStreamAsync();
                                    streamTask2.Wait();

                                    if (streamTask2.IsCompleted)
                                    {
                                        HttpContent content2 = response.Result.Content;

                                        if (isString)
                                        {
                                            result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                        }
                                        else
                                        {
                                            result = parseWithNewtonJson
                                                ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                                : content2.ReadAsStreamAsync().Result.ReadObject<T>();

                                            parseWithNewtonJson = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Task<Stream> streamTask = null;
                                    Task<string> errorMessageContent = null;

                                    if (response.Result.Content.GetType() == typeof(StreamContent))
                                        streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                                    else
                                        errorMessageContent = response.Result.Content.ReadAsStringAsync();
                                    //var streamTask = response.Result.Content.GetType() ==typeof(StreamContent) 
                                    //    ? ((StreamContent)response.Result.Content).ReadAsStreamAsync() 
                                    //    : response.Result.Content.ReadAsStringAsync();

                                    streamTask?.Wait();
                                    errorMessageContent?.Wait();

                                    try
                                    {
                                        resultResponse = streamTask.Result.ReadObject<ResultResponse>();
                                    }
                                    catch// (Exception e)
                                    {
                                        resultResponse = default(ResultResponse);
                                    }


                                    if (resultResponse != null && resultResponse.Messages != null && resultResponse.Messages.Count > 0)
                                    {
                                        //foreach (var error in resultResponse.Messages)
                                        //{
                                        //    if (errorMessage.Length > 0)
                                        //        errorMessage += "; ";
                                        //    errorMessage += error.Key + ":" + error.Value;
                                        //}
                                    }
                                    else if (errorMessageContent != null)
                                    {
                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"ErrorMessageContent", new string[] { errorMessageContent.Result } }
                                            }
                                        };
                                    }
                                    else
                                    {
                                        var responseStream = response.Result.Content.ReadAsStringAsync();
                                        responseStream.Wait();
                                        String responseString = responseStream.Result;

                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"Response", new string[] { responseString } }
                                            }
                                        };
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            catch (HttpListenerException e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message != null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("Error", new string[] { e.InnerException.Message });
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message!=null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>() 
                    { 
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("InnerException Error", new string[] { e.InnerException.Message });
                }

                result = default(T);
            }

            return result;
        }

        private string GetXmlObjectFromAPI<T>(GenericParameters optimizationParameters, 
                                                string url, 
                                                HttpMethodType httpMethod__1, 
                                                HttpContent httpContent, 
                                                bool isString,
                                                out ResultResponse resultResponse) where T : class
        {
            string result = string.Empty;
            resultResponse = default(ResultResponse);

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
                                    result = isString 
                                        ? response.Result.ReadString() as String 
                                        : response.Result.ReadObject<String>(); // Oleg T -> String
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
                                    result = isString 
                                        ? response.Result.ReadString() as String 
                                        : response.Result.ReadObject<String>(); // Oleg T -> String
                                }
                            }
                            break;
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                            if (true)
                            {
                                bool isPut = httpMethod__1 == HttpMethodType.Put;
                                bool isPatch = httpMethod__1 == HttpMethodType.Patch;
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
                                else if (isPatch)
                                {
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                    response = httpClient.PatchAsync(parametersURI, content);
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
                                        result = streamTask.Result.ReadString();
                                    }
                                }
                                else
                                {
                                    var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
                                    streamTask.Wait();

                                    ErrorResponse errorResponse = null;

                                    Task<string> errorMessageContent = response.Result.Content.GetType() != typeof(StreamContent) 
                                        ? errorMessageContent = response.Result.Content.ReadAsStringAsync()
                                        : null;

                                    try
                                    {
                                        resultResponse = streamTask.Result.ReadObject<ResultResponse>();
                                    }
                                    catch
                                    {
                                        resultResponse = default(ResultResponse);
                                    }
                                    if (errorResponse != null && errorResponse.Errors != null && errorResponse.Errors.Count > 0)
                                    {
                                        //foreach (String error in errorResponse.Errors)
                                        //{
                                        //    if (errorMessage.Length > 0)
                                        //    {
                                        //        errorMessage += "; ";
                                        //    }
                                        //    errorMessage += error;
                                        //}
                                    }
                                    else if (errorMessageContent != null)
                                    {
                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"ErrorMessageContent", new string[] { errorMessageContent.Result } }
                                            }
                                        };
                                    }
                                    else
                                    {
                                        var responseStream = response.Result.Content.ReadAsStringAsync();
                                        responseStream.Wait();
                                        String responseString = responseStream.Result;

                                        resultResponse = new ResultResponse()
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>()
                                            {
                                                {"Response", new string[] { responseString } }
                                            }
                                        };
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (HttpListenerException e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message != null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("Error", new string[] { e.InnerException.Message });
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false
                };

                if (e.Message != null)
                {
                    resultResponse.Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { e.Message } }
                    };
                }

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("InnerException Error", new string[] { e.InnerException.Message });
                }

                result = null;
            }

            return result;
        }

        private HttpClient CreateHttpClient(string url)
        {
            // Uncomment code lines below when is tested broono (no signed cert)
            /*
			ServicePointManager.ServerCertificateValidationCallback +=
		(sender, cert, chain, sslPolicyErrors) => true;


			var handler = new HttpClientHandler()
			{
				AllowAutoRedirect = true,
				MaxAutomaticRedirections = 4
			};

			var supportsAutoRdirect = handler.SupportsAutomaticDecompression;

			Console.WriteLine("Supports redirection -> " + supportsAutoRdirect);
			*/

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072 | (SecurityProtocolType)12288;

            HttpClient result = new HttpClient() { BaseAddress = new Uri(url) };

            result.Timeout = m_DefaultTimeOut;
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return result;
        }

        private HttpClient CreateAsyncHttpClient(string url)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            var supprotsAutoRdirect = handler.SupportsAutomaticDecompression;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072 | (SecurityProtocolType)12288;
            HttpClient result = new HttpClient(handler) { BaseAddress = new Uri(url) };

            result.Timeout = m_DefaultTimeOut;
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return result;
        }

        #endregion
    }
}
