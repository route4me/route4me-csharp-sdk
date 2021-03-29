using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes.V5;
//using Route4MeSDK.QueryTypes;
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
using System.Runtime.Serialization.Json;
using System.Text;
//using Route4MeSDK.DataTypes;

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
        
        #region Address Book Contacts

        /// <summary>
        /// The request parameter for the address book contacts removing process.
        /// </summary>
        [DataContract]
        private sealed class RemoveAddressBookContactsRequest : QueryTypes.GenericParameters
        {
            /// <value>The array of the address IDs</value>
			[DataMember(Name = "address_ids", EmitDefaultValue = false)]
            public string[] AddressIds { get; set; }
        }

        /// <summary>
        /// Remove the address book contacts.
        /// </summary>
        /// <param name="addressIds">The array of the address IDs</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>If true the contacts were removed successfully</returns>
		public bool RemoveAddressBookContacts(string[] addressIds, out ResultResponse resultResponse)
        {
            var request = new RemoveAddressBookContactsRequest()
            {
                AddressIds = addressIds
            };

            var response = GetJsonObjectFromAPI<StatusResponse>(request,
                                R4MEInfrastructureSettings.AddressBook,
                                HttpMethodType.Delete,
                                out resultResponse);

            return (response != null && response.status) ? true : false;
        }

        #endregion

        #region Account Profile

        /// <summary>
        /// Get account profile
        /// </summary>
        /// <param name="failResponse">Error response</param>
        /// <returns>Account profile</returns>
        public AccountProfile GetAccountProfile(out ResultResponse failResponse)
        {
            var parameters = new QueryTypes.GenericParameters();

            var result = GetJsonObjectFromAPI<AccountProfile>(parameters,
                                R4MEInfrastructureSettingsV5.AccountProfile,
                                HttpMethodType.Get,
                                out failResponse);

            return result;
        }

        public string GetAccountPreferedUnit(out ResultResponse failResponse)
        {
            var accountProfile = this.GetAccountProfile(out failResponse);

            var ownerId = accountProfile.RootMemberId;

            var r4me = new Route4MeManager(this.m_ApiKey);

            var memPars = new QueryTypes.MemberParametersV4() { member_id = ownerId };

            var user = r4me.GetUserById(memPars, out string errorString);

            string prefUnit = user.preferred_units;

            return prefUnit;
        }

        #endregion

        #region Team Management

        /// <summary>
        /// The request parameters for retrieving team members.
        /// </summary>
        [DataContract()]
        public sealed class MemberQueryParameters : QueryTypes.GenericParameters
        {
            /// <value>Team user ID</value>
			[QueryTypes.HttpQueryMemberAttribute(Name = "user_id", EmitDefaultValue = false)]
            public string UserId { get; set; }
        }

        /// <summary>
        /// The request class to bulk create the team members.
        /// </summary>
        [DataContract]
        private sealed class BulkMembersRequest : QueryTypes.GenericParameters
        {
            // Array of the team member requests
            [DataMember(Name = "users")]
            public TeamRequest[] Users { get; set; }
        }

        /// <summary>
        /// Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public TeamResponse[] GetTeamMembers(out ResultResponse failResponse)
        {
            var parameters = new QueryTypes.GenericParameters();

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
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Retrieved team member</returns>
        public TeamResponse GetTeamMemberById(MemberQueryParameters parameters,
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

            var result = GetJsonObjectFromAPI<TeamResponse>(parameters,
                                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                                HttpMethodType.Get,
                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created team member</returns>
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
        /// <param name="membersParams">Member request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Response with state code</returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, out ResultResponse resultResponse)
        {
            resultResponse = default(ResultResponse);

            if ((membersParams?.Length ?? 0) < 1)
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
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed team member</returns>
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
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
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

        /// <summary>
        /// Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
        public TeamResponse AddSkillsToDriver(MemberQueryParameters queryParameters,
                                             string[] skills,
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

            if ((skills?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                        { "Error", new string[] { "The driver skills array is empty." } }
                    }
                };

                return null;
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string>();

            driverSkills.Add("driver_skills", String.Join(",", skills));

            var requestPayload = new TeamRequest()
            {
                CustomData = driverSkills
            };

            #endregion

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
        /// <returns>List of the driver reviews</returns>
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

        /// <summary>
        /// Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview UpdateDriverReview(DriverReview driverReview,
                                                HttpMethodType method,
                                                out ResultResponse resultResponse)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
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

            if (driverReview.RatingId == null)
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
                            R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                            method,
                            out resultResponse);
        }

        #endregion

        #region Routes

        /// <summary>
        /// Get all Routes of the User filtered by specifying the corresponding query parameters.
        /// </summary>
        /// <param name="routeParameters">Route query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                                                                 R4MEInfrastructureSettingsV5.Routes,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// Get all Routes of the User filtered by specifying the corresponding query parameters.
        /// </summary>
        /// <param name="routeParameters">Route query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetAllRoutesWithPagination(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                                                                 R4MEInfrastructureSettingsV5.RoutesPaginate,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// Get all Routes of the User filtered by specifying the corresponding query parameters (without using ElasticSearch).
        /// </summary>
        /// <param name="routeParameters">Route query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetPaginatedRouteListWithoutElasticSearch(RouteParametersQuery routeParameters, 
                                                                            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                                                                 R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// Get route list using Elastic Search.
        /// </summary>
        /// <param name="routeFilterParameters">Route filter parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRouteDataTableWithoutElasticSearch(RouteFilterParameters routeFilterParameters, 
                                                                    out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                            routeFilterParameters,
                            R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                            HttpMethodType.Post,
                            out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetRouteDataTableWithElasticSearch(RouteFilterParameters routeFilterParameters,
                                                                    out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                            routeFilterParameters,
                            R4MEInfrastructureSettingsV5.RoutesDatatable,
                            HttpMethodType.Post,
                            out resultResponse);

            return result;
        }

        /// <summary>
        /// Get route list without using Elastic Search.
        /// </summary>
        /// <param name="routeParameters">Route filter parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                                                                 R4MEInfrastructureSettingsV5.RoutesFallback,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// Duplicate multiple Routes by sending a body payload with the array of the corresponding Route IDs.
        /// </summary>
        /// <param name="routeIDs">An array of the route ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A response with status code</returns>
        public RouteDuplicateResponse DuplicateRoute(string[] routeIDs, out ResultResponse resultResponse)
        {
            var duplicateParameter = new Dictionary<string, string[]>()
            {
                {
                    "duplicate_routes_id" , routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, System.Text.Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate, 
                HttpMethodType.Post, 
                content, 
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Delete multiple Routes by sending a comma-delimited list of the corresponding Route IDs as a query string.
        /// </summary>
        /// <param name="routeIds">An array of the route IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A response with status code</returns>
        public RoutesDeleteResponse DeleteRoutes(string[] routeIds, out ResultResponse resultResponse)
        {
            string str_route_ids = "";

            foreach (string routeId in routeIds)
            {
                if (str_route_ids.Length > 0) str_route_ids += ",";
                str_route_ids += routeId;
            }

            var genericParameters = new QueryTypes.GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", str_route_ids);

            var response = GetJsonObjectFromAPI<RoutesDeleteResponse>(genericParameters,
                                                R4MEInfrastructureSettingsV5.Routes,
                                                HttpMethodType.Delete,
                                                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the datatable configuration.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A response with status code</returns>
        public RouteDataTableConfigResponse GetRouteDataTableConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new QueryTypes.GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                                                                 R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// Get a datatable fallback configuration request.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>RouteDataTableConfigResponse type object</returns>
        public RouteDataTableConfigResponse GetRouteDataTableFallbackConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new QueryTypes.GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                                                                 R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                                                                 HttpMethodType.Get,
                                                                 out resultResponse);

            return result;
        }

        /// <summary>
        /// You can update a route in two ways:
        /// 1. Modify existing route and put in this function the route object as parameter.
        /// 2. Create an empty route object and assign values to the parameters:
        /// - RouteID;
        /// - Parameters (optional);
        /// - Addresses (Optional).
        /// </summary>
        /// <param name="route">Route object</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated route</returns>
        [Obsolete("Will be finished after implementing Route Destinations API")]
        public DataObjectRoute UpdateRoute(DataObjectRoute route, out ResultResponse resultResponse)
        {
            var routeQueryParams = new RouteParametersQuery();

            routeQueryParams.RouteId = route.RouteID;

            if (route.Parameters != null) routeQueryParams.Parameters = route.Parameters;

            if (route.Addresses != null && route.Addresses.Length>0) routeQueryParams.Addresses = route.Addresses;

            var response = GetJsonObjectFromAPI<DataObjectRoute>(
                                    routeQueryParams,
                                    R4MEInfrastructureSettingsV5.Routes,
                                    HttpMethodType.Put,
                                    out resultResponse);

            return response;
        }

        #endregion

        #region Optimizations

        /// <summary>
        /// Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">The input parameters for the routes optimization, which encapsulates:
        /// the route parameters and the addresses. </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                                                          R4MEInfrastructureSettings.ApiHost,
                                                          HttpMethodType.Post,
                                                          false,
                                                          out resultResponse);

            return result;
        }

        /// <summary>
        /// The response returned by the remove optimization command
        /// </summary>
		[DataContract]
        private sealed class RemoveOptimizationResponse
        {
            /// <value>True if an optimization was removed successfuly </value>
			[DataMember(Name = "status")]
            public bool Status { get; set; }

            /// <value>The number of the removed optimizations </value>
			[DataMember(Name = "removed")]
            public int Removed { get; set; }
        }

        /// <summary>
        /// The request parameters for an optimization removing
        /// </summary>
		[DataContract()]
        private sealed class RemoveOptimizationRequest : QueryTypes.GenericParameters
        {
            /// <value>If true will be redirected</value>
			[QueryTypes.HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
            public int redirect { get; set; }

            /// <value>The array of the optimization problem IDs to be removed</value>
			[DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
            public string[] optimization_problem_ids { get; set; }
        }

        /// <summary>
        /// Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemID"> Optimization Problem IDs </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out ResultResponse resultResponse)
        {
            var remParameters = new RemoveOptimizationRequest()
            {
                redirect = 0,
                optimization_problem_ids = optimizationProblemIDs
            };

            var response = GetJsonObjectFromAPI<RemoveOptimizationResponse>(remParameters,
                                                                 R4MEInfrastructureSettings.ApiHost,
                                                                 HttpMethodType.Delete,
                                                                 out resultResponse);
            if (response != null)
            {
                if (response.Status && response.Removed > 0) return true; else return false;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Vehicles

        /// <summary>
        /// Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The created vehicle </returns>
        public Vehicle CreateVehicle(Vehicle vehicle, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle>(
                                                vehicle,
                                                R4MEInfrastructureSettingsV5.Vehicles,
                                                HttpMethodType.Post,
                                                out resultResponse);
        }

        /// <summary>
        /// Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetPaginatedVehiclesList(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle[]>(
                                                vehicleParams,
                                                R4MEInfrastructureSettingsV5.Vehicles,
                                                HttpMethodType.Get,
                                                out resultResponse);
        }

        /// <summary>
        /// Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
        /// <param name="errorString"> Failing response </param>
        /// <returns>The removed vehicle</returns>
		public Vehicle DeleteVehicle(string vehicleId, out ResultResponse resultResponse)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>() 
                    {
                         { "Error", new string[] { "The vehicle ID is not specified" } }
                    }
                };
                
                return null;
            }

            return GetJsonObjectFromAPI<Vehicle>(new QueryTypes.GenericParameters(),
                            R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehicleId,
                            HttpMethodType.Delete,
                            out resultResponse);
        }

        /// <summary>
        /// Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A result with an order ID</returns>
        public VehicleTemporary CreateTemporaryVehicle(VehicleTemporary vehParams, out ResultResponse resultResponse)
        {
            var result =  GetJsonObjectFromAPI<VehicleTemporary>(
                                                vehParams,
                                                R4MEInfrastructureSettingsV5.VehicleTemporary,
                                                HttpMethodType.Post,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created vehicle order</returns>
        public VehicleOrderResponse ExecuteVehicleOrder(VehicleOrderParameters vehOrderParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleOrderResponse>(
                                                vehOrderParams,
                                                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                                                HttpMethodType.Post,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Data with vehicles</returns>
        public VehicleLocationResponse GetVehicleLocations(VehicleParameters vehParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                                                vehParams,
                                                R4MEInfrastructureSettingsV5.VehicleLocation,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle object</returns>
        public Vehicle GetVehicleById(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                                                new VehicleParameters(),
                                                R4MEInfrastructureSettingsV5.Vehicles+"/"+ vehicleId,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle track object</returns>
        public VehicleTrackResponse GetVehicleTrack(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleTrackResponse>(
                                                new VehicleParameters(),
                                                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId + "/" + "track",
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleProfilesResponse GetVehicleProfiles(VehicleProfileParameters profileParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfilesResponse>(
                                                profileParams,
                                                R4MEInfrastructureSettingsV5.VehicleProfiles,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle profile</returns>
        public VehicleProfile CreateVehicleProfile(VehicleProfile vehicleProfileParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfile>(
                                                vehicleProfileParams,
                                                R4MEInfrastructureSettingsV5.VehicleProfiles,
                                                HttpMethodType.Post,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Remove a vehicle profile from database.
        /// TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle profile</returns>
        public object DeleteVehicleProfile(VehicleProfileParameters vehicleProfileParams, out ResultResponse resultResponse)
        {
            if ((vehicleProfileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The vehicle ID is not specified" } }
                    }
                };

                return null;
            }

            var result =  GetJsonObjectFromAPI<object>(new QueryTypes.GenericParameters(),
                            R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                            HttpMethodType.Delete,
                            out resultResponse);

            return result;
        }

        /// <summary>
        /// Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle profile</returns>
        public VehicleProfile GetVehicleProfileById(VehicleProfileParameters vehicleProfileParams, out ResultResponse resultResponse)
        {
            if ((vehicleProfileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The vehicle ID is not specified" } }
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                                                new VehicleParameters(),
                                                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleLicensePlate?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The vehicle license plate is not specified" } }
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleResponse>(
                                                vehicleParams,
                                                R4MEInfrastructureSettingsV5.VehicleLicense,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found vehicles</returns>
        [ObsoleteAttribute("This method is deprecated until resolving the response issue.")]
        public Vehicle[] SearchVehicles(VehicleSearchParameters searchParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle[]>(
                                                searchParams,
                                                R4MEInfrastructureSettingsV5.VehicleSearch,
                                                HttpMethodType.Post,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle </returns>
        public Vehicle UpdateVehicle(Vehicle vehicleParams, out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The vehicle ID is not specified" } }
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<Vehicle>(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle profile</returns>
        public VehicleProfile UpdateVehicleProfile(VehicleProfile profileParams, out ResultResponse resultResponse)
        {
            if ((profileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The vehicle profile ID is not specified" } }
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + profileParams.VehicleProfileId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        #endregion

        #region Teleamtics Platform

        #region Connection

        /// <summary>
        /// Get all registered telematics connections.
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>An array of the Connection type objects</returns>
        public Connection[] GetTelematicsConnections(out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection[]>(
                                                new QueryTypes.GenericParameters(),
                                                R4MEInfrastructureSettingsV5.TelematicsConnection,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">The telematics query paramaters 
        /// as ConnectionPaameters type object</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection GetTelematicsConnectionByToken(ConnectionParameters connectionParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                                                new QueryTypes.GenericParameters(),
                                                R4MEInfrastructureSettingsV5.TelematicsConnection+"/"+connectionParams.ConnectionToken,
                                                HttpMethodType.Get,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">The telematics query paramaters 
        /// as ConnectionPaameters type object</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection RegisterTelematicsConnection(ConnectionParameters connectionParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                                                connectionParams,
                                                R4MEInfrastructureSettingsV5.TelematicsConnection,
                                                HttpMethodType.Post,
                                                out resultResponse);

            return result;
        }

        /// <summary>
        /// Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">The telematics query paramaters 
        /// as ConnectionPaameters type object</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Removed teleamtics connection</returns>
        public Connection DeleteTelematicsConnection(ConnectionParameters connectionParams, out ResultResponse resultResponse)
        {
            if ((connectionParams?.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The connection token is not specified" } }
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(new QueryTypes.GenericParameters(),
                            R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                            HttpMethodType.Delete,
                            out resultResponse);

            return result;
        }

        /// <summary>
        /// Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">The telematics query paramaters 
        /// as ConnectionPaameters type object</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated teleamtics connection</returns>
        public Connection UpdateTelematicsConnection(ConnectionParameters connectionParams, out ResultResponse resultResponse)
        {
            if ((connectionParams?.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse()
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>()
                    {
                         { "Error", new string[] { "The connection token is not specified" } }
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(
                                                connectionParams,
                                                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                                                HttpMethodType.Put,
                                                out resultResponse);

            return result;
        }

        #endregion

        #endregion

        #region Generic Methods


        public string GetStringResponseFromAPI(QueryTypes.GenericParameters optimizationParameters,
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



        public T GetJsonObjectFromAPI<T>(QueryTypes.GenericParameters optimizationParameters,
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

        public T GetJsonObjectFromAPI<T>(QueryTypes.GenericParameters optimizationParameters,
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

        private T GetJsonObjectFromAPI<T>(QueryTypes.GenericParameters optimizationParameters,
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

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(QueryTypes.GenericParameters optimizationParameters,
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

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(QueryTypes.GenericParameters optimizationParameters,
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
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

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
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("InnerException Error", new string[] { e.InnerException.Message });
                }

                result = default(T);
            }

            return new Tuple<T, ResultResponse>(result, resultResponse);
        }


        private T GetJsonObjectFromAPI<T>(QueryTypes.GenericParameters optimizationParameters,
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
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

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

            return result;
        }

        private string GetXmlObjectFromAPI<T>(QueryTypes.GenericParameters optimizationParameters,
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
