using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary;
using AddressBookParameters = Route4MeSDK.QueryTypes.V5.AddressBookParameters;
using OptimizationParameters = Route4MeSDK.QueryTypes.V5.OptimizationParameters;
using RouteParametersQuery = Route4MeSDK.QueryTypes.V5.RouteParametersQuery;
using VehicleParameters = Route4MeSDK.QueryTypes.V5.VehicleParameters;

namespace Route4MeSDK
{
    /// <summary>
    ///     This class encapsulates the Route4Me REST API 5
    ///     1. Create an instance of Route4MeManager with the api_key
    ///     1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most
    ///     popular functionality.
    ///     See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
    ///     2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or
    ///     Route4MeManager.GetStringResponseFromAPI())
    ///     to access any availaible functionality.
    ///     See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
    /// </summary>
    public sealed class Route4MeManagerV5
    {
        #region Constructors

        public Route4MeManagerV5(string apiKey)
        {
            _mApiKey = apiKey;
            _parseWithNewtonJson = false;
        }

        #endregion

        #region Fields

        private readonly string _mApiKey;

        private bool _parseWithNewtonJson;

        // Some endpoints rise error event if not all objects have the same fields (e.g. API 5 addressbook batch creating) 
        private string[] _mandatoryFields;

        #endregion

        #region Address Book Contacts

        /// <summary>
        /// The request parameter for the address book contacts removing process.
        /// </summary>
        //[DataContract]
        //      private sealed class RemoveAddressBookContactsRequest : QueryTypes.GenericParameters
        //      {
        //          /// <value>The array of the address IDs</value>
        //	[DataMember(Name = "address_ids", EmitDefaultValue = false)]
        //          public string[] AddressIds { get; set; }
        //      }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="addressIds">The array of the address IDs</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public bool RemoveAddressBookContacts(int[] contactIDs, out ResultResponse resultResponse)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            var response = GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsDeleteMultiple,
                HttpMethodType.Delete,
                out resultResponse);

            return resultResponse == null ? true : false;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">
        ///     An AddressParameters type object as the input parameters containg the parameters:
        ///     Offset, Limit
        /// </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContacts(AddressBookParameters addressBookParameters,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get an address book contact by ID
        /// </summary>
        /// <param name="contactId">contact ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContact type object</returns>
        public AddressBookContact GetAddressBookContactById(int contactId, out ResultResponse resultResponse)
        {
            var gparams = new GenericParameters();
            gparams.ParametersCollection.Add("address_id", contactId.ToString());

            var response = GetJsonObjectFromAPI<AddressBookContact>(gparams,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     The request parameter for the address book contacts removing process.
        /// </summary>
        [DataContract]
        public sealed class AddressBookContactsRequest : GenericParameters
        {
            /// The array of the address IDs
            [DataMember(Name = "address_ids", EmitDefaultValue = false)]
            public int[] AddressIds { get; set; }
        }

        /// <summary>
        ///     Get address book contacts by sending an array of address IDs.
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContactsByIds(int[] contactIDs,
            out ResultResponse resultResponse)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Add an address book contact to database.
        /// </summary>
        /// <param name="contactParams">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public AddressBookContact AddAddressBookContact(AddressBookContact contactParams,
            out ResultResponse resultResponse)
        {
            _parseWithNewtonJson = true;

            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<AddressBookContact>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddNew,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     The request parameter for the multiple address book contacts creating process.
        /// </summary>
        [DataContract]
        public sealed class BatchCreatingAddressBookContactsRequest : GenericParameters
        {
            /// The array of the address IDs
            [DataMember(Name = "data", EmitDefaultValue = false)]
            public AddressBookContact[] Data { get; set; }
        }

        /// <summary>
        ///     Add multiple address book contacts to database.
        /// </summary>
        /// <param name="contactParams">The data with multiple contacts parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public StatusResponse BatchCreateAdressBookContacts(BatchCreatingAddressBookContactsRequest contactParams,
            string[] mandatoryNullableFields,
            out ResultResponse resultResponse)
        {
            //parseWithNewtonJson = true;
            _mandatoryFields = mandatoryNullableFields;
            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<StatusResponse>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddMultiple,
                HttpMethodType.Post,
                out resultResponse);
        }

        #endregion

        #region Account Profile

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <param name="failResponse">Error response</param>
        /// <returns>Account profile</returns>
        public AccountProfile GetAccountProfile(out ResultResponse failResponse)
        {
            var parameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<AccountProfile>(parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.Get,
                out failResponse);

            return result;
        }

        public string GetAccountPreferedUnit(out ResultResponse failResponse)
        {
            var accountProfile = GetAccountProfile(out failResponse);

            var ownerId = accountProfile.RootMemberId;

            var r4me = new Route4MeManager(_mApiKey);

            var memPars = new MemberParametersV4 {member_id = ownerId};

            var user = r4me.GetUserById(memPars, out var errorString);

            var prefUnit = user.PreferredUnits;

            return prefUnit;
        }

        #endregion

        #region Barcodes

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object</returns>
        public GetAddressBarcodesResponse GetAddressBarcodes(GetAddressBarcodesParameters getAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<GetAddressBarcodesResponse>(getAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public StatusResponse SaveAddressBarcodes(SaveAddressBarcodesParameters saveAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            saveAddressBarcodesParameters.PrepareForSerialization();

            return GetJsonObjectFromAPI<StatusResponse>(saveAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Post,
                out resultResponse);
        }

        #endregion

        #region Team Management

        /// <summary>
        ///     The request parameters for retrieving team members.
        /// </summary>
        [DataContract]
        public sealed class MemberQueryParameters : GenericParameters
        {
            /// <value>Team user ID</value>
            [HttpQueryMember(Name = "user_id", EmitDefaultValue = false)]
            public string UserId { get; set; }
        }

        /// <summary>
        ///     The request class to bulk create the team members.
        /// </summary>
        [DataContract]
        private sealed class BulkMembersRequest : GenericParameters
        {
            // Array of the team member requests
            [DataMember(Name = "users")] public TeamRequest[] Users { get; set; }
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
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
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Retrieved team member</returns>
        public TeamResponse GetTeamMemberById(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if ((parameters?.UserId ?? null) == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
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
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created team member</returns>
        public TeamResponse CreateTeamMember(TeamRequest memberParams,
            out ResultResponse resultResponse)
        {
            if (!memberParams.ValidateMemberCreateRequest(out var error0))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {error0}}
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
        ///     Bulk create the team members
        ///     TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns></returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, out ResultResponse resultResponse)
        {
            resultResponse = default;

            if ((membersParams?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The array of the user parameters is empty"}}
                    }
                };

                return null;
            }

            foreach (var memberParams in membersParams)
                if (!memberParams.ValidateMemberCreateRequest(out var error0))
                {
                    resultResponse = new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {error0}}
                        }
                    };

                    return null;
                }

            var newMemberParams = new BulkMembersRequest
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
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed team member</returns>
        public TeamResponse RemoveTeamMember(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if ((parameters?.UserId ?? null) == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
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
        ///     Update a team member
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
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if (requestPayload == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The team request object is empty"}}
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
        ///     Add an array of skills to the driver.
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
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if ((skills?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The driver skills array is empty."}}
                    }
                };

                return null;
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string>();

            driverSkills.Add("driver_skills", string.Join(",", skills));

            var requestPayload = new TeamRequest
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
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>List of the driver reviews</returns>
        public DriverReviewsResponse GetDriverReviewList(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            _parseWithNewtonJson = true;

            var result = GetJsonObjectFromAPI<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview GetDriverReviewById(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            if ((parameters?.RatingId ?? null) == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The RatingId parameter is not specified"}}
                    }
                };

                return null;
            }

            _parseWithNewtonJson = true;

            var result = GetJsonObjectFromAPI<DriverReview>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Upload driver review to the server
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
        ///     Update a driver review.
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
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                };

                return null;
            }

            if (driverReview.RatingId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameters doesn't contain parameter RatingId."}}
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

        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetAllRoutesWithPagination(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetPaginatedRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetRouteDataTableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetRouteDatatableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public RouteDuplicateResponse DuplicateRoute(string[] routeIDs, out ResultResponse resultResponse)
        {
            var duplicateParameter = new Dictionary<string, string[]>
            {
                {
                    "duplicate_routes_id", routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                out resultResponse);

            return result;
        }

        public RoutesDeleteResponse DeleteRoutes(string[] routeIds, out ResultResponse resultResponse)
        {
            var str_route_ids = "";

            foreach (var routeId in routeIds)
            {
                if (str_route_ids.Length > 0) str_route_ids += ",";
                str_route_ids += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", str_route_ids);

            var response = GetJsonObjectFromAPI<RoutesDeleteResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Delete,
                out resultResponse);

            return response;
        }

        public RouteDataTableConfigResponse GetRouteDataTableConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public RouteDataTableConfigResponse GetRouteDataTableFallbackConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     You can update a route in two ways:
        ///     1. Modify existing route and put in this function the route object as parameter.
        ///     2. Create an empty route object and assign values to the parameters:
        ///     - RouteID;
        ///     - Parameters (optional);
        ///     - Addresses (Optional).
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

            if (route.Addresses != null && route.Addresses.Length > 0) routeQueryParams.Addresses = route.Addresses;

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
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <param name="errorString">Returned error string in case of an optimization processs failing</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                false,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     The response returned by the remove optimization command
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
        ///     The request parameters for an optimization removing
        /// </summary>
        [DataContract]
        private sealed class RemoveOptimizationRequest : GenericParameters
        {
            /// <value>If true will be redirected</value>
            [HttpQueryMember(Name = "redirect", EmitDefaultValue = false)]
            public int redirect { get; set; }

            /// <value>The array of the optimization problem IDs to be removed</value>
            [DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
            public string[] optimization_problem_ids { get; set; }
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemID"> Optimization Problem ID </param>
        /// <param name="errorString"> Returned error string in case of the processs failing </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out ResultResponse resultResponse)
        {
            var remParameters = new RemoveOptimizationRequest
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
                if (response.Status && response.Removed > 0) return true;
                return false;
            }

            return false;
        }

        #endregion

        #region Vehicles

        /// <summary>
        ///     Creates a vehicle
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
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
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
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
        /// <param name="errorString"> Failing response </param>
        /// <returns>The removed vehicle</returns>
        public Vehicle DeleteVehicle(string vehicleId, out ResultResponse resultResponse)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<Vehicle>(new GenericParameters(),
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehicleId,
                HttpMethodType.Delete,
                out resultResponse);
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A result with an order ID</returns>
        public VehicleTemporary CreateTemporaryVehicle(VehicleTemporary vehParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleTemporary>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created vehicle order</returns>
        public VehicleOrderResponse ExecuteVehicleOrder(VehicleOrderParameters vehOrderParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleOrderResponse>(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Data with vehicles</returns>
        public VehicleLocationResponse GetVehicleLocations(VehicleParameters vehParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle object</returns>
        public Vehicle GetVehicleById(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
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
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleProfilesResponse GetVehicleProfiles(VehicleProfileParameters profileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfilesResponse>(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle profile</returns>
        public VehicleProfile CreateVehicleProfile(VehicleProfile vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle profile</returns>
        public object DeleteVehicleProfile(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if ((vehicleProfileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<object>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle profile</returns>
        public VehicleProfile GetVehicleProfileById(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if ((vehicleProfileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
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
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(VehicleParameters vehicleParams,
            out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleLicensePlate?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
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
        ///     Search vehicles by sending request body.
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
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle </returns>
        public Vehicle UpdateVehicle(Vehicle vehicleParams, out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
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
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle profile</returns>
        public VehicleProfile UpdateVehicleProfile(VehicleProfile profileParams, out ResultResponse resultResponse)
        {
            if ((profileParams?.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
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
        ///     Get all registered telematics connections.
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>An array of the Connection type objects</returns>
        public Connection[] GetTelematicsConnections(out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection[]>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection GetTelematicsConnectionByToken(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection RegisterTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Removed teleamtics connection</returns>
        public Connection DeleteTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if ((connectionParams?.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated teleamtics connection</returns>
        public Connection UpdateTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if ((connectionParams?.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
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

        public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<string>(optimizationParameters,
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
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
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
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
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
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                isString,
                out resultResponse);

            return result;
        }

        private Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            bool isString)
            where T : class
        {
            var result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                isString);

            return result;
        }

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(
            GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool isString)
            where T : class
        {
            //out string errorMessage return this parameter in the tuple

            var result = default(T);
            var resultResponse = default(ResultResponse);

            var parametersURI = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersURI}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = await httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);

                            result = isString ? response.ReadString() as T : response.ReadObject<T>();

                            break;
                        }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                        {
                            var isPut = httpMethod == HttpMethodType.Put;
                            var isPatch = httpMethod == HttpMethodType.Patch;
                            var isDelete = httpMethod == HttpMethodType.Delete;
                            HttpContent content = null;
                            if (httpContent != null)
                            {
                                content = httpContent;
                            }
                            else
                            {
                                var jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            HttpResponseMessage response = null;
                            if (isPut)
                            {
                                response = await httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content);
                            }
                            else if (isPatch)
                            {
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                response = await httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = await httpClientHolder.HttpClient.SendAsync(request);
                            }
                            else
                            {
                                var request = new HttpRequestMessage();
                                response = await httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content)
                                    .ConfigureAwait(true);
                            }

                            // Check if successful
                            if (response.Content is StreamContent)
                            {
                                var streamTask = await ((StreamContent) response.Content).ReadAsStreamAsync();

                                result = isString ? streamTask.ReadString() as T : streamTask.ReadObject<T>();
                            }
                            else if (response.Content
                                .GetType().ToString().ToLower()
                                .Contains("httpconnectionresponsecontent"))
                            {
                                var streamTask2 = response.Content.ReadAsStreamAsync();
                                streamTask2.Wait();

                                if (streamTask2.IsCompleted)
                                {
                                    var content2 = response.Content;

                                    if (isString)
                                    {
                                        result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = _parseWithNewtonJson
                                            ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                            : content2.ReadAsStreamAsync().Result.ReadObject<T>();

                                        _parseWithNewtonJson = false;
                                    }
                                }
                            }
                            else
                            {
                                var streamTask = await ((StreamContent) response.Content).ReadAsStreamAsync();

                                Task<string> errorMessageContent = null;

                                if (response.Content.GetType() != typeof(StreamContent))
                                    errorMessageContent = response.Content.ReadAsStringAsync();


                                try
                                {
                                    resultResponse = streamTask.ReadObject<ResultResponse>();
                                }
                                catch // (Exception e)
                                {
                                    resultResponse = default;
                                }

                                if (resultResponse != null && resultResponse.Messages != null &&
                                    resultResponse.Messages.Count > 0)
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
                                    resultResponse = new ResultResponse
                                    {
                                        Status = false,
                                        Messages = new Dictionary<string, string[]>
                                        {
                                            {"ErrorMessageContent", new[] {errorMessageContent.Result}}
                                        }
                                    };
                                }
                                else
                                {
                                    var responseStream = await response.Content.ReadAsStringAsync();
                                    var responseString = responseStream;

                                    resultResponse = new ResultResponse
                                    {
                                        Status = false,
                                        Messages = new Dictionary<string, string[]>
                                        {
                                            {"Response", new[] {responseString}}
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
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("Error", new[] {e.InnerException.Message});
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("InnerException Error", new[] {e.InnerException.Message});
                }

                result = default;
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
            var result = default(T);
            resultResponse = default;

            var parametersURI = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersURI}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);
                            response.Wait();

                            if (response.IsCompleted)
                            {
                                if (isString)
                                {
                                    result = response.Result.ReadString() as T;
                                }
                                else
                                {
                                    result = _parseWithNewtonJson
                                        ? response.Result.ReadObjectNew<T>()
                                        : response.Result.ReadObject<T>();

                                    _parseWithNewtonJson = false;
                                }
                            }

                            break;
                        }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                        {
                            var isPut = httpMethod == HttpMethodType.Put;
                            var isPatch = httpMethod == HttpMethodType.Patch;
                            var isDelete = httpMethod == HttpMethodType.Delete;
                            HttpContent content = null;
                            if (httpContent != null)
                            {
                                content = httpContent;
                            }
                            else
                            {
                                var jsonString = (_mandatoryFields?.Length ?? 0) > 0
                                    ? R4MeUtils.SerializeObjectToJson(optimizationParameters, _mandatoryFields)
                                    : R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            Task<HttpResponseMessage> response = null;
                            if (isPut)
                            {
                                response = httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content);
                            }
                            else if (isPatch)
                            {
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                response = httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = httpClientHolder.HttpClient.SendAsync(request);
                            }
                            else
                            {
                                var cts = new CancellationTokenSource();
                                cts.CancelAfter(1000 * 60 * 5); // 3 seconds

                                var request = new HttpRequestMessage();
                                response = httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content, cts.Token);
                            }

                            // Wait for response
                            response.Wait();

                            // Check if successful
                            if (response.IsCompleted &&
                                response.Result.IsSuccessStatusCode &&
                                response.Result.Content is StreamContent)
                            {
                                var streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
                                streamTask.Wait();

                                if (streamTask.IsCompleted)
                                {
                                    if (isString)
                                    {
                                        result = streamTask.Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = _parseWithNewtonJson
                                            ? streamTask.Result.ReadObjectNew<T>()
                                            : streamTask.Result.ReadObject<T>();

                                        _parseWithNewtonJson = false;
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
                                    var content2 = response.Result.Content;

                                    if (isString)
                                    {
                                        result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = _parseWithNewtonJson
                                            ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                            : content2.ReadAsStreamAsync().Result.ReadObject<T>();

                                        _parseWithNewtonJson = false;
                                    }
                                }
                            }
                            else
                            {
                                Task<Stream> streamTask = null;
                                Task<string> errorMessageContent = null;

                                if (response.Result.Content.GetType() == typeof(StreamContent))
                                    streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
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
                                catch // (Exception e)
                                {
                                    resultResponse = default;
                                }


                                if (resultResponse != null && resultResponse.Messages != null &&
                                    resultResponse.Messages.Count > 0)
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
                                    resultResponse = new ResultResponse
                                    {
                                        Status = false,
                                        Messages = new Dictionary<string, string[]>
                                        {
                                            {"ErrorMessageContent", new[] {errorMessageContent.Result}}
                                        }
                                    };
                                }
                                else
                                {
                                    var responseStream = response.Result.Content.ReadAsStringAsync();
                                    responseStream.Wait();
                                    var responseString = responseStream.Result;

                                    resultResponse = new ResultResponse
                                    {
                                        Status = false,
                                        Messages = new Dictionary<string, string[]>
                                        {
                                            {"Response", new[] {responseString}}
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
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("Error", new[] {e.InnerException.Message});
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null) new Dictionary<string, string[]>();
                    resultResponse.Messages.Add("InnerException Error", new[] {e.InnerException.Message});
                }

                result = default;
            }

            return result;
        }

        #endregion
    }
}