using Route4MeSDK.DataTypes;
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

        /// <summary>
        /// Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">The input parameters for the routes optimization, which encapsulates:
        /// the route parameters and the addresses. </param>
        /// <param name="errorString">Returned error string in case of an optimization processs failing</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
														  R4MEInfrastructureSettings.ApiHost,
														  HttpMethodType.Post,
														  false,
														  out errorString);

			return result;
		}

        /// <summary>
        /// Asynchronously generates an optimization problem. Especially important in case of the large addresses arrays.
        /// </summary>
        /// <param name="optimizationParameters">The input parameters for the routes optimization, which encapsulates:
        /// the route parameters and the addresses. </param>
        /// <param name="errorString">Returned error string in case of an optimization processs failing</param>
        /// <returns>Generated optimization problem object</returns>
		public DataObject RunAsyncOptimization(OptimizationParameters optimizationParameters, out string errorString)
		{
			Task<Tuple<DataObject, string>> result = GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
															   R4MEInfrastructureSettings.ApiHost,
															   HttpMethodType.Post,
															   false);

			result.Wait();

			errorString = "";
			if (result.IsFaulted || result.IsCanceled) errorString = result.Result.Item2;

			return result.Result.Item1;
		}

        /// <summary>
        /// Returns an optimization problem by the parameter OptimizationProblemID
        /// </summary>
        /// <param name="optimizationParameters">The optimization parameters bject containing the parameter OptimizationProblemID</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Optimization problem object</returns>
		public DataObject GetOptimization(OptimizationParameters optimizationParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
														  R4MEInfrastructureSettings.ApiHost,
														  HttpMethodType.Get,
														  out errorString);

			return result;
		}

		/// <summary>
        /// The response returned by the get optimizations command
		/// </summary>
		[DataContract]
		private sealed class DataObjectOptimizations
		{
            /// <value>Array of the returned optimization problems </value>
			[DataMember(Name = "optimizations")]
			public DataObject[] Optimizations { get; set; }

            /// <value>The number of the returned optimization problems </value>
            [DataMember(Name = "totalRecords")]
            public int TotalRecords { get; set; }
        }

        /// <summary>
        /// For getting optimization problems limited by the parameters: offset, limit.
        /// </summary>
        /// <param name="queryParameters">The array of the query parameters containing the parameters:
        /// <para><c>offset</c>: Search starting position</para>
        /// <para><c>limit</c>: The number of records to return.</para></param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Array of the optimization problems</returns>
		public DataObject[] GetOptimizations(OptimizationParameters queryParameters, out string errorString)
		{
			DataObjectOptimizations dataObjectOptimizations = GetJsonObjectFromAPI<DataObjectOptimizations>(queryParameters,
																   R4MEInfrastructureSettings.ApiHost,
																   HttpMethodType.Get,
																   out errorString);

            return dataObjectOptimizations != null ? dataObjectOptimizations.Optimizations : null;
		}

        /// <summary>
        /// Updates an existing optimization problem
        /// </summary>
        /// <param name="optimizationParameters"></param>
        /// <param name="errorString"></param>
        /// <returns></returns>
		public DataObject UpdateOptimization(OptimizationParameters optimizationParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
														  R4MEInfrastructureSettings.ApiHost,
														  HttpMethodType.Put,
														  false,
														  out errorString);

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
		private sealed class RemoveOptimizationRequest : GenericParameters
		{
            /// <value>If true will be redirected</value>
			[HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
			public int redirect { get; set; }

            /// <value>The array of the optimization problem IDs to be removed</value>
			[DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
			public string[] optimization_problem_ids { get; set; }
		}

        /// <summary>
        /// Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemID"> Optimization Problem ID </param>
        /// <param name="errorString"> Returned error string in case of the processs failing </param>
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
			if (response != null)
			{
				if (response.Status && response.Removed > 0) return true; else return false;
			}
			else
			{
				if (errorString == "")
					errorString = "Error removing optimization";
				return false;
			}
		}

        /// <summary>
        /// The response from a destination removing process from an optimization
        /// </summary>
		[DataContract]
		private sealed class RemoveDestinationFromOptimizationResponse
		{
            /// <value>True if a destination was successuly removed from an optimization</value>
			[DataMember(Name = "deleted")]
			public Boolean Deleted { get; set; }

            /// <value>ID of a removed destination</value>
			[DataMember(Name = "route_destination_id")]
			public int RouteDestinationId { get; set; }
		}

        /// <summary>
        /// Removes a destination from an optimization
        /// </summary>
        /// <param name="optimizationId">optimization problem ID</param>
        /// <param name="destinationId">ID of a destination to be removed from an optimziation</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if  destination as removed from an optimization, otherwise - false</returns>
		public bool RemoveDestinationFromOptimization(string optimizationId, int destinationId, out string errorString)
		{
			GenericParameters genericParameters = new GenericParameters();
			genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationId);
			genericParameters.ParametersCollection.Add("route_destination_id", destinationId.ToString());
			RemoveDestinationFromOptimizationResponse response = GetJsonObjectFromAPI<RemoveDestinationFromOptimizationResponse>(genericParameters,
																   R4MEInfrastructureSettings.GetAddress,
																   HttpMethodType.Delete,
																   out errorString);

            return (response != null && response.Deleted) ? true : false;
		}

        #endregion

        #region Hybrid Optimization

        /// <summary>
        /// Returns a hybrid optimization for the specified date
        /// </summary>
        /// <param name="hybridOptimizationParameters">The hybrid optimization parameters containing schedule date and timezone</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A hYbrid optimization object</returns>
        public DataObject GetHybridOptimization(HybridOptimizationParameters hybridOptimizationParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObject>(hybridOptimizationParameters,
															R4MEInfrastructureSettings.HybridOptimization,
															HttpMethodType.Get,
															out errorString);

			return result;
		}

        /// <summary>
        /// Adds the depots to a hybrid optimization
        /// </summary>
        /// <param name="hybridDepotParameters">The hybrid depot parameters containing parameters:
        /// optimization_problem_id, delete_old_depots, new_depots</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if the depots were added to an optimization successfuly, otherwise - false</returns>
		public bool AddDepotsToHybridOptimization(HybridDepotParameters hybridDepotParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<StatusResponse>(hybridDepotParameters,
															R4MEInfrastructureSettings.HybridDepots,
															HttpMethodType.Post,
															out errorString);

            return (result != null) ? (result.GetType() == typeof(StatusResponse) 
                ? ((StatusResponse)result).status : false) : false;
		}

        #endregion

        #region Routes

        /// <summary>
        /// Returns a route by route ID
        /// </summary>
        /// <param name="routeParameters">The route parameters containg a route ID</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route object</returns>
        public DataObjectRoute GetRoute(RouteParametersQuery routeParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
														  R4MEInfrastructureSettings.RouteHost,
														  HttpMethodType.Get,
														  out errorString);

			return result;
		}

        /// <summary>
        /// Returns array of the routes limited by the parameters: offset and limit.
        /// </summary>
        /// <param name="routeParameters">The route parameters containing the parameters: offset, limit</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>An array of the routes</returns>
		public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
																 R4MEInfrastructureSettings.RouteHost,
																 HttpMethodType.Get,
																 out errorString);

			return result;
		}

        /// <summary>
        /// Returns a route ID from the first route of an optimization
        /// </summary>
        /// <param name="optimizationProblemId"></param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route ID</returns>
		public string GetRouteId(string optimizationProblemId, out string errorString)
		{
			GenericParameters genericParameters = new GenericParameters();
			genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationProblemId);
			genericParameters.ParametersCollection.Add("wait_for_final_state", "1");

			DataObject response = GetJsonObjectFromAPI<DataObject>(genericParameters,
																   R4MEInfrastructureSettings.ApiHost,
																   HttpMethodType.Get,
																   out errorString);

            return (response != null && response.Routes != null && response.Routes.Length > 0)
                ? response.Routes[0].RouteID : null;
		}

        /// <summary>
        /// Updates route by the rotue parameters
        /// </summary>
        /// <param name="routeParameters">The route parameters</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route</returns>
		public DataObjectRoute UpdateRoute(RouteParametersQuery routeParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
														  R4MEInfrastructureSettings.RouteHost,
														  HttpMethodType.Put,
														  out errorString);

			return result;
		}

        /// <summary>
        /// Update route by changed DataObjectRoute object directly.
        /// </summary>
        /// <param name="route">A route of the DataObjectRoute type as input parameters.</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Updated route</returns>
        public DataObjectRoute UpdateRoute(DataObjectRoute route, out string errorString)
        {
            
            var routeParameters = new RouteParametersQuery()
            {
                RouteId = route.RouteID,
                ApprovedForExecution = route.ApprovedForExecution,
                Parameters = route.Parameters,
                Addresses = route.Addresses
            };

            routeParameters.PrepareForSerialization();

            var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                                                          R4MEInfrastructureSettings.RouteHost,
                                                          HttpMethodType.Put,
                                                          out errorString);

            return result;
        }

        /// <summary>
        /// The response from a route duplicating process
        /// </summary>
		[DataContract]
		private sealed class DuplicateRouteResponse
		{
            /// <value>ID of a duplicate optimization</value>
			[DataMember(Name = "optimization_problem_id")]
			public string OptimizationProblemId { get; set; }

            /// <value>True if a route duplicated successfuly</value>
			[DataMember(Name = "success")]
			public Boolean Success { get; set; }
		}

        /// <summary>
        /// Duplicates a route
        /// </summary>
        /// <param name="queryParameters">The query parameters containing a route ID to be duplicated</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>ID of a duplicate route</returns>
		public string DuplicateRoute(RouteParametersQuery queryParameters, out string errorString)
		{
			queryParameters.ParametersCollection["to"] = "none";
			DuplicateRouteResponse response = GetJsonObjectFromAPI<DuplicateRouteResponse>(queryParameters,
																   R4MEInfrastructureSettings.DuplicateRoute,
																   HttpMethodType.Get,
																   out errorString);

            return (response != null && response.Success) 
                ? (response.OptimizationProblemId != null 
                  ? this.GetRouteId(response.OptimizationProblemId, out errorString) : null)
                : null;
		}

        /// <summary>
        /// The response from the route deleting process
        /// </summary>
		[DataContract]
		private sealed class DeleteRouteResponse
		{
            /// <value>If true, the route was deleted successfuly</value>
			[DataMember(Name = "deleted")]
			public Boolean Deleted { get; set; }

            /// <value>The array of the error strings</value>
			[DataMember(Name = "errors")]
			public List<String> Errors { get; set; }

            /// <value>The deleted route ID</value>
			[DataMember(Name = "route_id")]
			public string routeId { get; set; }

            /// <value>The array of the deleted routes IDs</value>
			[DataMember(Name = "route_ids")]
			public string[] routeIds { get; set; }
		}

        /// <summary>
        /// Removes the routes from a user's account
        /// </summary>
        /// <param name="routeIds">The array of the route IDs to be removed</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Array of the removed routes IDs</returns>
		public string[] DeleteRoutes(string[] routeIds, out string errorString)
		{
			string str_route_ids = "";

			foreach (string routeId in routeIds)
			{
				if (str_route_ids.Length > 0) str_route_ids += ",";
				str_route_ids += routeId;
			}

			GenericParameters genericParameters = new GenericParameters();

			genericParameters.ParametersCollection.Add("route_id", str_route_ids);
			DeleteRouteResponse response = GetJsonObjectFromAPI<DeleteRouteResponse>(genericParameters,
																   R4MEInfrastructureSettings.RouteHost,
																   HttpMethodType.Delete,
																   out errorString);

            return (response != null) ? response.routeIds : null;
		}

        /// <summary>
        /// Merges the routes
        /// </summary>
        /// <param name="mergeRoutesParameters">The parameters containing:
        /// <para>RouteIds: IDs of the routes to be merged</para>
        /// <para>DepotAddress: a depot address of the merged route</para>
        /// <para>RemoveOrigin: if true, the origin routes will be removed</para>
        /// <para>DepotLat: the depot's latitude</para>
        /// <para>DepotLng: the depot's longitude</para>
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if the routes were merged successfuly</returns>
		public bool MergeRoutes(MergeRoutesQuery mergeRoutesParameters, out string errorString)
		{
			GenericParameters roParames = new GenericParameters();

			List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("route_ids", mergeRoutesParameters.RouteIds),
                new KeyValuePair<string, string>("depot_address", mergeRoutesParameters.DepotAddress),
                new KeyValuePair<string, string>("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()),
                new KeyValuePair<string, string>("depot_lat", mergeRoutesParameters.DepotLat.ToString()),
                new KeyValuePair<string, string>("depot_lng", mergeRoutesParameters.DepotLng.ToString())
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>
				(roParames, R4MEInfrastructureSettings.MergeRoutes,
				HttpMethodType.Post, httpContent, out errorString);

			return (response != null && response.status) ? true : false;
		}

        /// <summary>
        /// Resequences/roptimizes a route. TO DO: this endpoint seems to be deprecated and should be disabled
        /// </summary>
        /// <param name="roParames"></param>
        /// <param name="errorString"></param>
        /// <returns></returns>
		public bool ResequenceReoptimizeRoute(Dictionary<string, string> roParames, out string errorString)
		{
			RouteParametersQuery request = new RouteParametersQuery
			{
				RouteId = roParames["route_id"],
				DisableOptimization = roParames["disable_optimization"] == "1" ? true : false,
				Optimize = roParames["optimize"]
			};

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(request, R4MEInfrastructureSettings.RouteReoptimize, HttpMethodType.Get, out errorString);

			return (response != null && response.status) ? true : false;
		}

        /// <summary>
        /// The request parameters for manually resequencing of a route
        /// </summary>
		[DataContract()]
		private sealed class ManuallyResequenceRouteRequest : GenericParameters
		{
            /// <value>The route ID to be resequenced</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The manually resequenced addresses</value>
			[DataMember(Name = "addresses")]
			public AddressInfo[] Addresses { get; set; }
		}

        /// <summary>
        /// The information about an address
        /// </summary>
		[DataContract]
		class AddressInfo : GenericParameters
		{
            /// <value>The destination ID</value>
			[DataMember(Name = "route_destination_id")]
			public int DestinationId { get; set; }

            /// <value>The destination's sequence number in a route</value>
			[DataMember(Name = "sequence_no")]
			public int SequenceNo { get; set; }

            /// <value>If true the destination is depot</value>
			[DataMember(Name = "is_depot")]
			public bool IsDepot { get; set; }
		}

        /// <summary>
        /// Re-sequences manually a route
        /// </summary>
        /// <param name="rParams">The parameters object RouteParametersQuery containing the parameter RouteId </param>
        /// <param name="addresses">The route addresses</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A re-sequenced route</returns>
        public DataObjectRoute ManuallyResequenceRoute(RouteParametersQuery rParams, Address[] addresses, out string errorString)
		{
			ManuallyResequenceRouteRequest request = new ManuallyResequenceRouteRequest()
			{
				RouteId = rParams.RouteId,

			};

			List<AddressInfo> lsAddresses = new List<AddressInfo>();

			int iMaxSequenceNumber = 0;

			foreach (var address in addresses)
			{
				AddressInfo aInfo = new AddressInfo()
				{
					DestinationId = address.RouteDestinationId != null ? (int)address.RouteDestinationId : -1,
					SequenceNo = address.SequenceNo != null ? (int)address.SequenceNo : iMaxSequenceNumber
				};

				lsAddresses.Add(aInfo);

				iMaxSequenceNumber++;
			}

			request.Addresses = lsAddresses.ToArray();

			DataObjectRoute route1 = GetJsonObjectFromAPI<DataObjectRoute>(request,
											R4MEInfrastructureSettings.RouteHost,
											HttpMethodType.Put,
											out errorString);

			return route1;
		}

        /// <summary>
        /// Shares a route by an email
        /// </summary>
        /// <param name="roParames">The RouteParametersQuery parameters object contains parameters:
        /// <para>RouteId: a route ID to be shared</para>
        /// <para>ResponseFormat: the response format</para>
        /// </param>
        /// <param name="Email">Recipient email</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if a route was shared</returns>
		public bool RouteSharing(RouteParametersQuery roParames, string Email, out string errorString)
		{
			List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

			keyValues.Add(new KeyValuePair<string, string>("recipient_email", Email));
			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(roParames, R4MEInfrastructureSettings.RouteSharing, HttpMethodType.Post, httpContent, out errorString);

			return (response != null && response.status) ? true : false;
		}

        /// <summary>
        /// The request parameters for the route custom data updating process
        /// </summary>
		[DataContract()]
		private sealed class UpdateRouteCustomDataRequest : GenericParameters
		{
            /// <value>A route ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>A route destination ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public int? RouteDestinationId { get; set; }

            /// <value>The changed/new custom fields of a route destination</value>
			[DataMember(Name = "custom_fields", EmitDefaultValue = false)]
			public Dictionary<string, string> CustomFields { get; set; }
		}

        /// <summary>
        /// Updates a route's custom data
        /// </summary>
        /// <param name="routeParameters">The RouteParametersQuery object contains parameters:
        /// <para>RouteId: a route ID to be updated</para>
        /// <para>RouteDestinationId: the ID of the route destination to be updated</para>
        /// </param>
        /// <param name="customData">The keyvalue pairs of the type Dictionary<string, string></param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Updated route destination</returns>
		public Address UpdateRouteCustomData(RouteParametersQuery routeParameters, Dictionary<string, string> customData, out string errorString)
		{
			UpdateRouteCustomDataRequest request = new UpdateRouteCustomDataRequest
			{
				RouteId = routeParameters.RouteId,
				RouteDestinationId = routeParameters.RouteDestinationId,
				CustomFields = customData
			};

			return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);
		}

        /// <summary>
        /// The request parameters for the updating process of a route destination
        /// </summary>
		[DataContract()]
		private sealed class UpdateRouteDestinationRequest : Address
		{
            /// <value>A route ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>A optimization ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
            public string OptimizationProblemId { get; set; }

            /// <value>A route destination ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public int? RouteDestinationId { get; set; }
            /*
            /// <value>The route destination alias</value>
			[DataMember(Name = "alias", EmitDefaultValue = false)]
			public string Alias { get; set; }

            /// <value>The first name of a person at the destination</value>
			[DataMember(Name = "first_name", EmitDefaultValue = false)]
			public string FirstName { get; set; }

            /// <value>The first name of a person at the destination</value>
			[DataMember(Name = "last_name", EmitDefaultValue = false)]
			public string LastName { get; set; }

            /// <value>The destination's address</value>
			[DataMember(Name = "address", EmitDefaultValue = false)]
			public string AddressString { get; set; }

            /// <value>The address stop type</value>
			[DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
			public string AddressStopType { get; set; }

            /// <value>If true the destination is a depot</value>
			[DataMember(Name = "is_depot", EmitDefaultValue = false)]
			public bool? IsDepot { get; set; }

            /// <value>the latitude of the address</value>
            [DataMember(Name = "lat", EmitDefaultValue = false)]
			public double Latitude { get; set; }

            /// <value>the longitude of the address</value>
            [DataMember(Name = "lng", EmitDefaultValue = false)]
			public double Longitude { get; set; }

            /// <value>the sequence number of the address</value>
			[DataMember(Name = "sequence_no", EmitDefaultValue = false)]
			public int? SequenceNo { get; set; }

            /// <value>status flag to mark an address as visited (aka check in)</value>
            [DataMember(Name = "is_visited", EmitDefaultValue = false)]
			public bool? IsVisited { get; set; }

            /// <value>status flag to mark an address as departed (aka check out)</value>
            [DataMember(Name = "is_departed", EmitDefaultValue = false)]
			public bool? IsDeparted { get; set; }

            /// <value>the last known visited timestamp of this address</value>
            [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
			public uint? TimestampLastVisited { get; set; }

            /// <value>The last known departed timestamp of this address</value>
            [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
			public uint? TimestampLastDeparted { get; set; }

            /// <value>the address group</value>
			[DataMember(Name = "group", EmitDefaultValue = false)]
			public object Group { get; set; }

            //pass-through data about this route destination
            //the data will be visible on the manifest, website, and mobile apps
            /// <value>The customer PO of the address</value>
            [DataMember(Name = "customer_po", EmitDefaultValue = false)]
			public object CustomerPo { get; set; }

            //pass-through data about this route destination
            //the data will be visible on the manifest, website, and mobile apps
            /// <value>The invoice NO of the address</value>
            [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
			public object InvoiceNo { get; set; }

            //pass-through data about this route destination
            //the data will be visible on the manifest, website, and mobile apps
            /// <value>The reference NO of the address</value>
            [DataMember(Name = "reference_no", EmitDefaultValue = false)]
			public object ReferenceNo { get; set; }

            //pass-through data about this route destination
            //the data will be visible on the manifest, website, and mobile apps
            /// <value>The order NO of the address</value>
            [DataMember(Name = "order_no", EmitDefaultValue = false)]
			public object OrderNo { get; set; }

            /// <value>The order ID of the address</value>
			[DataMember(Name = "order_id", EmitDefaultValue = false)]
			public int? OrderId { get; set; }

            /// <value>The weight of the cargo</value>
			[DataMember(Name = "weight", EmitDefaultValue = false)]
			public object Weight { get; set; }

            /// <value>The cost of the address</value>
			[DataMember(Name = "cost", EmitDefaultValue = false)]
			public object Cost { get; set; }

            /// <value>The revenue from the address</value>
			[DataMember(Name = "revenue", EmitDefaultValue = false)]
			public object Revenue { get; set; }

            //the cubic volume that this destination/order/line-item consumes/contains
            //this is how much space it will take up on a vehicle
            /// <value>The cubic volume of the cargo</value>
            [DataMember(Name = "cube", EmitDefaultValue = false)]
			public object Cube { get; set; }

            //the number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle
            /// <value>Number of pieces for an address</value>
            [DataMember(Name = "pieces", EmitDefaultValue = false)]
			public object Pieces { get; set; }

            /// <value>The address email</value>
			[DataMember(Name = "email", EmitDefaultValue = false)]
			public string Email { get; set; }

            /// <value>The address phone</value>
			[DataMember(Name = "phone", EmitDefaultValue = false)]
			public string Phone { get; set; }

            /// <value>The time window start</value>
			[DataMember(Name = "time_window_start", EmitDefaultValue = false)]
			public long? TimeWindowStart { get; set; }

            /// <value>The time window end</value>
			[DataMember(Name = "time_window_end", EmitDefaultValue = false)]
			public long? TimeWindowEnd { get; set; }

            // <value>The expected amount of time that will be spent at this address by the driver/user</value>
            [DataMember(Name = "time", EmitDefaultValue = false)]
			public long? Time { get; set; }

            //if present, the priority will sequence addresses in all the optimal routes so that
            //higher priority addresses are general at the beginning of the route sequence
            //1 is the highest priority, 100000 is the lowest
            /// <value>The priority level of an address</value>
            [DataMember(Name = "priority", EmitDefaultValue = false)]
			public int? Priority { get; set; }

            //generate optimal routes and driving directions to this curbside lat
            /// <value>The curbside latitude</value>
            [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
			public double? CurbsideLatitude { get; set; }

            //generate optimal routes and driving directions to the curbside lang
            /// <value>The curbside longitude</value>
            [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
			public double? CurbsideLongitude { get; set; }

            /// <value>The time window start 2</value>
			[DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
			public long? TimeWindowStart2 { get; set; }

            /// <value>The time window end 2</value>
			[DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
			public long? TimeWindowEnd2 { get; set; }

            /// <value>The custom fields of an address</value>
			[DataMember(Name = "custom_fields", EmitDefaultValue = false)]
			public Dictionary<string, string> CustomFields { get; set; }

            /// <value>The person's contact ID at an address</value>
			[DataMember(Name = "contact_id", EmitDefaultValue = false)]
			public int? ContactId { get; set; }
            */
		}


        /// <summary>
        /// Updated a route destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>The updated address</returns>
		public Address UpdateRouteDestination(Address addressParameters, out string errorString)
		{
            
            //string[] addressProperties = new string[]
            //    {"Alias","FirstName","LastName","AddressString","AddressStopType","IsDepot","Latitude",
            //        "Longitude","SequenceNo","IsVisited","IsDeparted","TimestampLastVisited",
            //        "TimestampLastDeparted","Group","CustomerPo","InvoiceNo","ReferenceNo","OrderNo",
            //        "OrderId","Weight","Cost","Revenue","Cube","Pieces","Phone","TimeWindowStart",
            //        "TimeWindowEnd","Priority","CurbsideLatitude","CurbsideLongitude","TimeWindowStart2",
            //        "TimeWindowEnd2","CustomFields","ContactId" };

			var request = new UpdateRouteDestinationRequest
			{
				RouteId = addressParameters.RouteId,
				RouteDestinationId = addressParameters.RouteDestinationId,
			};

            foreach (var propInfo in typeof(Address).GetProperties())
            {
                //var propName = propInfo.Name;

                propInfo.SetValue(request, propInfo.GetValue(addressParameters));
                /*
                if (Array.Exists(addressProperties, element => element ==  propName))
                {
                    Object proprValue = propInfo.GetValue(addressParameters);
                    if (proprValue != null)
                    {
                        request.GetType().InvokeMember(propName, 
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, 
                            Type.DefaultBinder, request, new object[] { proprValue });
                    }
                        
                }
                */
            }
            
            //addressParameters.PrepareForSerialization();

            return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);
		}

        /// <summary>
        /// Updates an optimization destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>The updated address</returns>
        public Address UpdateOptimizationDestination(Address addressParameters, out string errorString)
        {
            var request = new UpdateRouteDestinationRequest
            {
                OptimizationProblemId = addressParameters.OptimizationProblemId
            };

            foreach (var propInfo in typeof(Address).GetProperties())
            {
                propInfo.SetValue(request, propInfo.GetValue(addressParameters));

            }

            var dataObject = GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, out errorString);

            return dataObject?.Addresses?.Where(x => x.RouteDestinationId == addressParameters.RouteDestinationId).FirstOrDefault() ?? null;
        }

        #endregion

        #region Tracking

        /// <summary>
        /// Returns a last location of the device on the route
        /// </summary>
        /// <param name="parameters">Contains the parameters:
        /// <para>route_id: the route ID</para>
        /// <para>device_tracking_history: If 1 device tracking history will be returned</para>
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>An optimization with the tracking data</returns>
        public DataObjectRoute GetLastLocation(GenericParameters parameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<DataObjectRoute>(parameters,
														  R4MEInfrastructureSettings.RouteHost,
														  HttpMethodType.Get,
														  false,
														  out errorString);

			return result;
		}

        /// <summary>
        /// The response from getting the device location history
        /// </summary>
		[DataContract]
		public sealed class GetDeviceLocationHistoryResponse
		{
            /// <value>The array of the TrackingHistory objects </value>
			[DataMember(Name = "data")]
			public TrackingHistory[] data { get; set; }
		}

		/// <summary>
		/// Returns device location history from the specified date range.
		/// </summary>
		/// <param name="gpsParameters">Query parameters</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>If response contains not null data, returns object of the type GetDeviceLocationHistoryResponse.
		/// If query was without error, but nothing was found, returns null.
		/// If query failed, return error string.
		/// </returns>
		public object GetDeviceLocationHistory(GPSParameters gpsParameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<GetDeviceLocationHistoryResponse>(gpsParameters,
													R4MEInfrastructureSettings.DeviceLocation,
													HttpMethodType.Get,
													false,
													out errorString);

            var dataLength = ((GetDeviceLocationHistoryResponse)result).data.Length;

            return (result == null && errorString != "") ? errorString 
                : ((dataLength == 0) ? null : (object)result);
		}

        /// <summary>
        /// Sets GPS info in the device
        /// </summary>
        /// <param name="gpsParameters">The parameters of the type GPSParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The response containing parameters:
        /// <para>status: if true GPD info was set successfuly on a device</para>
        /// <para>tx_id: tracking info ID</para>
        /// </returns>
		public SetGpsResponse SetGPS(GPSParameters gpsParameters, out string errorString)
		{
			SetGpsResponse result = GetJsonObjectFromAPI<SetGpsResponse>(gpsParameters,
													R4MEInfrastructureSettings.SetGpsHost,
													HttpMethodType.Get,
													false,
													out errorString);

			return result;
		}

        /// <summary>
        /// The request parameters for the find asset process.
        /// </summary>
		[DataContract()]
		private sealed class FindAssetRequest : GenericParameters
		{
            /// <value>The tracking code</value>
			[HttpQueryMemberAttribute(Name = "tracking", EmitDefaultValue = false)]
			public string Tracking { get; set; }
		}

        /// <summary>
        /// Searchs for an asset
        /// </summary>
        /// <param name="tracking">The tracking code</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The object of the type FindAssetResponse</returns>
		public FindAssetResponse FindAsset(string tracking, out string errorString)
		{
			FindAssetRequest request = new FindAssetRequest { Tracking = tracking };

            return GetJsonObjectFromAPI<FindAssetResponse>(request, R4MEInfrastructureSettings.AssetTracking, HttpMethodType.Get, false, out errorString);
		}

        /// <summary>
        /// Get user locations
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="errorString"></param>
        /// <returns></returns>
        public Dictionary<string,UserLocation> GetUserLocations(GenericParameters parameters, out string errorString)
        {
            var userLocations = GetJsonObjectFromAPI<Dictionary<string, UserLocation>>(parameters, 
                R4MEInfrastructureSettings.UserLocation, 
                HttpMethodType.Get, 
                false,out errorString);

            return userLocations;
        }

        public UserLocation[] GetUserLocationsAsync(GenericParameters parameters, out string errorString)
        {
            Task<Tuple<UserLocation[], string>> result = GetJsonObjectFromAPIAsync<UserLocation[]>(parameters,
                R4MEInfrastructureSettings.UserLocation,
                HttpMethodType.Get,
                false);

            result.Wait();

            errorString = "";
            if (result.IsFaulted || result.IsCanceled) errorString = result.Result.Item2;

            return result.Result.Item1;
        }

        #endregion

        #region Users

        /// <summary>
        /// The response for the get users process
        /// </summary>
        [DataContract]
		public sealed class GetUsersResponse
		{
            /// <value>The array of the User objects</value>
			[DataMember(Name = "results")]
			public MemberResponseV4[] results { get; set; }
		}

        /// <summary>
        /// Returns the object containing array of the user objects
        /// </summary>
        /// <param name="parameters">Empty GenericParameters object</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The object of the type GetUsersResponse</returns>
		public GetUsersResponse GetUsers(GenericParameters parameters, out string errorString)
		{
			var result = GetJsonObjectFromAPI<GetUsersResponse>(parameters,
																 R4MEInfrastructureSettings.GetUsersHost,
																 HttpMethodType.Get,
																 out errorString);

			return result;
		}

        /// <summary>
        /// Creates new sub-user(member) in the user's account
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
		public MemberResponseV4 CreateUser(MemberParametersV4 memParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Post, out errorString);
		}

        /// <summary>
        /// Removes a sub-user(member) from the user's account
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>True if a member was successfuly removed from the user's account</returns>
		public bool UserDelete(MemberParametersV4 memParams, out string errorString)
		{
			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Delete, out errorString);

            return (response == null) ? false : ((response.status) ? true : false);
		}

        /// <summary>
        /// Return a user by the parameter member_id
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
		public MemberResponseV4 GetUserById(MemberParametersV4 memParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Get, out errorString);
		}

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
		public MemberResponseV4 UserUpdate(MemberParametersV4 memParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, 
                HttpMethodType.Put, out errorString);
		}

        /// <summary>
        /// Authenticates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters containing the parameters:
        /// <para>StrEmail: user email</para>
        /// para>StrPassword: user password</para>
        /// para>Format: response format</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
		public MemberResponse UserAuthentication(MemberParameters memParams, out string errorString)
		{
			MemberParameters roParams = new MemberParameters();

			var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("strPassword", memParams.StrPassword),
                new KeyValuePair<string, string>("format", memParams.Format)
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

            return GetJsonObjectFromAPI<MemberResponse>(roParams, R4MEInfrastructureSettings.UserAuthentication, 
                HttpMethodType.Post, httpContent, out errorString);
		}

        /// <summary>
        /// Registrates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
		public MemberResponse UserRegistration(MemberParameters memParams, out string errorString)
		{
			MemberParameters roParams = new MemberParameters()
            {
                Plan = memParams.Plan,
                MemberType = memParams.MemberType
            };

			var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("strIndustry", memParams.StrIndustry),
                new KeyValuePair<string, string>("strFirstName", memParams.StrFirstName),
                new KeyValuePair<string, string>("strLastName", memParams.StrLastName),
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("format", memParams.Format),
                new KeyValuePair<string, string>("chkTerms", memParams.ChkTerms == 1 ? "1" : "0"),
                new KeyValuePair<string, string>("device_type", memParams.DeviceType),
                new KeyValuePair<string, string>("strPassword_1", memParams.StrPassword_1),
                new KeyValuePair<string, string>("strPassword_2", memParams.StrPassword_2)
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

            return GetJsonObjectFromAPI<MemberResponse>(roParams, R4MEInfrastructureSettings.UserRegistration,
                HttpMethodType.Post, httpContent, out errorString);
		}

        /// <summary>
        /// The request parameters for the session validation process.
        /// </summary>
		[DataContract()]
		private sealed class ValidateSessionRequest : GenericParameters
		{
            /// <value>The session ID</value>
			[HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
			public string SessionGuid { get; set; }

            /// <value>The member ID</value>
			[HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
			public int? MemberId { get; set; }

            /// <value>The response format (json, xml)</value>
			[HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
			public string Format { get; set; }
		}

        /// <summary>
        /// Validates user session
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
		public MemberResponse ValidateSession(MemberParameters memParams, out string errorString)
		{
			ValidateSessionRequest request = new ValidateSessionRequest
			{
				SessionGuid = memParams.SessionGuid,
				MemberId = memParams.MemberId,
				Format = memParams.Format
			};

            return GetJsonObjectFromAPI<MemberResponse>(request, R4MEInfrastructureSettings.ValidateSession, HttpMethodType.Get, false, out errorString);
		}

        /// <summary>
        /// Creates new user's configuration
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters containing the parameters:
        /// <para>config_key: configuration key</para>
        /// <para>config_value: configuration value</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
		public MemberConfigurationResponse CreateNewConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post, out errorString);
		}

        public MemberConfigurationResponse CreateNewConfigurationKey(MemberConfigurationParameters[] confParams, out string errorString)
        {
            GenericParameters genParams = new GenericParameters();

            var httpContent = new StringContent(fastJSON.JSON.ToJSON(confParams), System.Text.Encoding.UTF8, "application/json");

            var response = GetJsonObjectFromAPI<MemberConfigurationResponse>
                (genParams, R4MEInfrastructureSettings.UserConfiguration,
                HttpMethodType.Post, httpContent, out errorString);

            return response;
        }

        /// <summary>
        /// Removes a user's configuration key
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters containing the parameter config_key</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
		public MemberConfigurationResponse RemoveConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Delete, out errorString);
		}

        /// <summary>
        /// The request parameters for the configuration data request process.
        /// </summary>
		[DataContract()]
		private sealed class GetConfigurationDataRequest : GenericParameters
		{
            /// <value>A user's configuration key</value>
			[HttpQueryMemberAttribute(Name = "config_key", EmitDefaultValue = false)]
			public string config_key { get; set; }
		}

        /// <summary>
        /// Returns configuration data from a user's account.
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters (empty or containing the parameter config_key)</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
		public MemberConfigurationDataResponse GetConfigurationData(MemberConfigurationParameters confParams, out string errorString)
		{
			GetConfigurationDataRequest mParams = default(GetConfigurationDataRequest);

			mParams = new GetConfigurationDataRequest();
			if ((confParams != null)) mParams.config_key = confParams.config_key;

            return GetJsonObjectFromAPI<MemberConfigurationDataResponse>(mParams, 
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Get, out errorString);
		}

        /// <summary>
        /// Updates a configuration key.
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
		public MemberConfigurationResponse UpdateConfigurationKey(MemberConfigurationParameters confParams, out string errorString)
		{
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Put, out errorString);
		}

        #endregion

        #region Address Notes

        /// <summary>
        /// Returns an array of the address notes
        /// </summary>
        /// <param name="noteParameters">An object of the type NoteParameters containing the parameters:
        /// <para>RouteId: a route ID</para>
        /// <para>AddressId: a route destination ID</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the AddressNote type objects </returns>
        public AddressNote[] GetAddressNotes(NoteParameters noteParameters, out string errorString)
		{
			AddressParameters addressParameters = new AddressParameters()
			{
				RouteId = noteParameters.RouteId,
				RouteDestinationId = noteParameters.AddressId,
				Notes = true
			};

			Address address = this.GetAddress(addressParameters, out errorString);

            return (address != null) ? address.Notes : null;
		}

        /// <summary>
        /// The response from the address note adding process. 
        /// </summary>
		[DataContract]
		private sealed class AddAddressNoteResponse
		{
            /// <value>If true an address note added successfuly</value>
			[DataMember(Name = "status")]
			public bool Status { get; set; }

            /// <value>The address note ID</value>
			[DataMember(Name = "note_id")]
			public string NoteID { get; set; }

            /// <value>The upload ID</value>
			[DataMember(Name = "upload_id")]
			public string UploadID { get; set; }

            /// <value>The AddressNote type object</value>
			[DataMember(Name = "note")]
			public AddressNote Note { get; set; }
		}

        /// <summary>
        /// Adds a file as an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <param name="attachmentFilePath">An attached file path</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An AddressNote type object</returns>
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
				var keyValues = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("strUpdateType", strUpdateType),
                    new KeyValuePair<string, string>("strNoteContents", noteContents)
                };

				httpContent = new FormUrlEncodedContent(keyValues);
			}

			AddAddressNoteResponse response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
																 R4MEInfrastructureSettings.AddRouteNotesHost,
																 HttpMethodType.Post,
																 httpContent,
																 out errorString);


            if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
			if (attachmentFileStream != null) attachmentFileStream.Dispose();

            if (response != null && response.Note == null && response.Status == false) 
                errorString = "Note not added";

            return (response != null) ? (response.Note != null ? response.Note : null) : null;
		}

        /// <summary>
        /// Adds an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An AddressNote type object</returns>
		public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, out string errorString)
		{
			return this.AddAddressNote(noteParameters, noteContents, null, out errorString);
		}

        /// <summary>
        /// The request parameters for the custom note type adding process.
        /// </summary>
		[DataContract]
		private sealed class AddCustomNoteTypeRequest : GenericParameters
		{
            /// <value>The custom note type</value>
			[DataMember(Name = "type", EmitDefaultValue = false)]
			public string Type { get; set; }

            /// <value>An array of the custom note values</value>
			[DataMember(Name = "values", EmitDefaultValue = false)]
			public string[] Values { get; set; }
		}

        /// <summary>
        /// The response from the custom note type adding process.
        /// </summary>
		[DataContract]
		private sealed class AddCustomNoteTypeResponse
		{
            /// <value>Added custom note</value>
			[DataMember(Name = "result")]
			public string Result { get; set; }

            /// <value>How many destination were affected by adding process</value>
			[DataMember(Name = "affected")]
			public int Affected { get; set; }
		}

        /// <summary>
        /// Adds custom note type to a route destination.
        /// </summary>
        /// <param name="customType"></param>
        /// <param name="values">Array of the string type notes</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>If succefful, returns non-negative affected number, otherwise: -1</returns>
        public object AddCustomNoteType(string customType, string[] values, out string errorString)
		{
			AddCustomNoteTypeRequest request = new AddCustomNoteTypeRequest()
			{
				Type = customType,
				Values = values
			};

			AddCustomNoteTypeResponse response = GetJsonObjectFromAPI<AddCustomNoteTypeResponse>(request,
																 R4MEInfrastructureSettings.CustomNoteType,
																 HttpMethodType.Post,
																	out errorString);

            return (response != null) ? (response.Result == "OK" ? response.Affected : -1) : (object)errorString;
		}

        /// <summary>
        /// The request parameter for the customer removing process.
        /// </summary>
		[DataContract]
		private sealed class removeCustomNoteTypeRequest : GenericParameters
		{
            /// <value>A custom note type ID></value>
			[DataMember(Name = "id", EmitDefaultValue = false)]
			public int id { get; set; }
		}

        /// <summary>
        /// Removes a custom note type from a user's account.
        /// </summary>
        /// <param name="customNoteId">The custom note type ID</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>if succefful, returns non-negative affected number, otherwise: -1</returns>
        public object removeCustomNoteType(int customNoteId, out string errorString)
		{
			removeCustomNoteTypeRequest request = new removeCustomNoteTypeRequest() { id = customNoteId };

			AddCustomNoteTypeResponse response = GetJsonObjectFromAPI<AddCustomNoteTypeResponse>(request,
																R4MEInfrastructureSettings.CustomNoteType,
																HttpMethodType.Delete,
																   out errorString);

            return (response != null) ? (response.Result == "OK" ? response.Affected : -1) : (object)errorString;
		}

        /// <summary>
        /// Returns an array of the custom note types
        /// </summary>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the custom note types</returns>
		public object getAllCustomNoteTypes(out string errorString)
		{
            GenericParameters request = new GenericParameters();

            CustomNoteType[] response = GetJsonObjectFromAPI<CustomNoteType[]>(request,
																R4MEInfrastructureSettings.CustomNoteType,
																HttpMethodType.Get,
																   out errorString);

            return (response != null) ? response : (object)errorString;
		}

        /// <summary>
        /// The request parameters for the process of custom note adding to a route 
        /// </summary>
		[DataContract]
		private sealed class addCustomNoteToRouteRequest : GenericParameters
		{
            /// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The route destination ID</value>
			[HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
			public int AddressId { get; set; }

            /// <value>The device position latitude</value>
			[HttpQueryMemberAttribute(Name = "dev_lat")]
			public double Latitude { get; set; }

            /// <value>The device position longitude</value>
			[HttpQueryMemberAttribute(Name = "dev_lng")]
			public double Longitude { get; set; }

            /// <value>The response format (json, xml)</value>
			[HttpQueryMemberAttribute(Name = "format")]
			public double Format { get; set; }
		}

        /// <summary>
        /// Adds a custom note to aroute
        /// </summary>
        /// <param name="noteParameters">A NoteParameters type object</param>
        /// <param name="customNotes">The Dictionary<string, string> type object</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The AddAddressNoteResponse type object</returns>
		public object addCustomNoteToRoute(NoteParameters noteParameters, Dictionary<string, string> customNotes, out string errorString)
		{
			var keyValues = new List<KeyValuePair<string, string>>();

            customNotes.ForEach(kv1 => { keyValues.Add(new KeyValuePair<string, string>(kv1.Key, kv1.Value)); });

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

			AddAddressNoteResponse response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
															   R4MEInfrastructureSettings.AddRouteNotesHost,
															   HttpMethodType.Post,
															   httpContent,
															   out errorString);

            return (response == null) ? (object)errorString : (response.GetType() != typeof(AddAddressNoteResponse) 
                ? "Can not add custom note to the route" 
                : (response.Status ? (object)response.Note : "Can not add custom note to the route"));
		}

		#endregion

		#region Activities

        /// <summary>
        /// The response from the activities getting process.
        /// </summary>
		[DataContract]
		private sealed class GetActivitiesResponse
		{
            /// <value>An array of the Activity type objects</value>
			[DataMember(Name = "results")]
			public Activity[] Results { get; set; }

            /// <value>The number of the Activity type objects/value>
			[DataMember(Name = "total")]
			public uint Total { get; set; }
		}

        /// <summary>
        /// Returns the activity feed
        /// </summary>
        /// <param name="activityParameters"> Input parameters </param>
        /// <param name="errorString">Error message text</param>
        /// <returns> List of the Activity type objects </returns>
        public Activity[] GetActivityFeed(ActivityParameters activityParameters, out string errorString)
		{
			GetActivitiesResponse response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
																 R4MEInfrastructureSettings.ActivityFeedHost,
																 HttpMethodType.Get,
																 out errorString);

            return (response != null) ? response.Results : null;
		}

        public Activity[] GetActiviies(ActivityParameters activityParameters, out string errorString)
        {
            GetActivitiesResponse response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                                                                 R4MEInfrastructureSettings.GetActivitiesHost,
                                                                 HttpMethodType.Get,
                                                                 out errorString);

            return (response != null) ? response.Results : null;
        }

        /// <summary>
        /// Creates a user's activity by sending a custom message to the activity stream.
        /// </summary>
        /// <param name="activity"> The Activity type object to add </param>
        /// <param name="errorString"> Error message text </param>
        /// <returns> True if a custom message logged successfuly </returns>
        public bool LogCustomActivity(Activity activity, out string errorString)
		{
			activity.PrepareForSerialization();

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(activity,
																   R4MEInfrastructureSettings.ActivityFeedHost,
																   HttpMethodType.Post,
																   out errorString);

			return (response != null && response.status) ? true : false;
		}

        /// <summary>
        /// Returns the array of the Activity type objects as a response.
        /// </summary>
        /// <param name="activityParameters">The ActivityParameters type objects as an input parameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the Activity type objects</returns>
		public Activity[] GetAnalytics(ActivityParameters activityParameters, out string errorString)
		{
			GetActivitiesResponse response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
																 R4MEInfrastructureSettings.ActivityFeedHost,
																 HttpMethodType.Get,
																 out errorString);

            return (response != null) ? response.Results : null;
		}

        #endregion

        #region Destinations

        /// <summary>
        /// Returns an Address type object as the response
        /// </summary>
        /// <param name="addressParameters">An AddressParameters type object containing the route ID as the input parameter.</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The Address type object</returns>
        public Address GetAddress(AddressParameters addressParameters, out string errorString)
		{
            return GetJsonObjectFromAPI<Address>(addressParameters,
																 R4MEInfrastructureSettings.GetAddress,
																 HttpMethodType.Get,
																 out errorString);
		}

        /// <summary>
        /// The request parameters for the route destination adding process.
        /// </summary>
		[DataContract]
		private sealed class AddRouteDestinationRequest : GenericParameters
		{
            /// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The optimization ID</value>
			[HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
            public string OptimizationProblemId { get; set; }

            /// <value>The array of the Address type objects</value>
			[DataMember(Name = "addresses", EmitDefaultValue = false)]
			public Address[] Addresses { get; set; }

            /// <value>If true, an address will be inserted at optimal position of a route</value>
            [DataMember(Name = "optimal_position", EmitDefaultValue = true)]
			public bool OptimalPosition { get; set; }
		}

		/// <summary>
		/// Adds address(es) into a route.
		/// </summary>
		/// <param name="routeId"> The route ID </param>
		/// <param name="addresses"> Valid array of the Address type objects. </param>
		/// <param name="optimalPosition"> If true, an address will be inserted at optimal position of a route </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns> An array of the IDs of added addresses </returns>
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

            List<int> arrDestinationIds = new List<int>();

            if (response != null && response.Addresses != null)
			{
                addresses.ForEach(addressNew =>
                {
                    response.Addresses.Where(addressResp => (
                        addressResp.AddressString == addressNew.AddressString &&
                        addressResp.Latitude == addressNew.Latitude && 
                        addressResp.Longitude == addressNew.Longitude && 
                        addressResp.RouteDestinationId != null
                    )).ForEach(addrResp => {
                        arrDestinationIds.Add((int)addrResp.RouteDestinationId);
                    });
                });
			}

			return arrDestinationIds.ToArray();
		}

        /// <summary>
        /// Adds the address(es) into a route.
        /// </summary>
        /// <param name="routeId"> The route ID </param>
        /// <param name="addresses"> Valid array of the Address type objects. </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>An array of the IDs of added addresses </returns>
        public int[] AddRouteDestinations(string routeId, Address[] addresses, out string errorString)
		{
			return this.AddRouteDestinations(routeId, addresses, true, out errorString);
		}

        public int?[] AddOptimizationDestinations (string optimizationId, Address[] addresses, out string errorString)
        {
            var request = new AddRouteDestinationRequest()
            {
                OptimizationProblemId = optimizationId,
                Addresses = addresses,
            };

            var addressesList = addresses.Select(x => x.AddressString).ToList();

            var dataObject = GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, out errorString);

            return dataObject?.Addresses?.Where(x => addressesList.Contains(x.AddressString))
                ?.Select(y => y.RouteDestinationId).ToArray() ?? null;
        }

        /// <summary>
        /// The request parameters for a route destination removing process.
        /// </summary>
		[DataContract]
		private sealed class RemoveRouteDestinationRequest : GenericParameters
		{
            /// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The route destination ID</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public int RouteDestinationId { get; set; }
		}

        /// <summary>
        /// The response object from a route destination removing process.
        /// </summary>
		[DataContract]
		private sealed class RemoveRouteDestinationResponse
		{
            /// <value>If true the destination was removed successfully</value>
			[DataMember(Name = "deleted")]
			public Boolean Deleted { get; set; }

            /// <value>Removed route destination ID</value>
			[DataMember(Name = "route_destination_id")]
			public int RouteDestinationId { get; set; }
		}

        /// <summary>
        /// Removes a route dstination from a route
        /// </summary>
        /// <param name="routeId">The route ID</param>
        /// <param name="destinationId">The route destination ID</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>True if a destination removing finished successfully</returns>
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

            return (response != null && response.Deleted) ? true : false;
		}

        /// <summary>
        /// The response object from a route destination moving process.
        /// </summary>
		[DataContract]
		private sealed class MoveDestinationToRouteResponse
		{
            /// <value>If true the destination was removed successfully</value>
			[DataMember(Name = "success")]
			public Boolean Success { get; set; }

            /// <value>The error string</value>
			[DataMember(Name = "error")]
			public string error { get; set; }
		}

        /// <summary>
        /// Moves a route destination to other route.
        /// </summary>
        /// <param name="toRouteId">The destination route id</param>
        /// <param name="routeDestinationId">The route destiantion ID to be moved</param>
        /// <param name="afterDestinationId">The route destination ID after which will be inserted the moved destination </param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Ture if a destination was moved uccessfully</returns>
		public bool MoveDestinationToRoute(string toRouteId, int routeDestinationId, int afterDestinationId, out string errorString)
		{
			var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("to_route_id", toRouteId),
                new KeyValuePair<string, string>("route_destination_id", Convert.ToString(routeDestinationId)),
                new KeyValuePair<string, string>("after_destination_id", Convert.ToString(afterDestinationId))
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

			MoveDestinationToRouteResponse response = GetJsonObjectFromAPI<MoveDestinationToRouteResponse>(new GenericParameters(),
																 R4MEInfrastructureSettings.MoveRouteDestination,
																 HttpMethodType.Post,
																 httpContent,
																 out errorString);

            if (response.error != null) errorString = response.error;

            return (response != null) ? response.Success : false;
		}

        /// <summary>
        /// The request parameters for a address marking as the departed process.
        /// </summary>
		[DataContract]
		private sealed class MarkAddressDepartedRequest : GenericParameters
		{
            /// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The route destination ID</value>
			[HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
			public System.Nullable<int> AddressId { get; set; }

            /// <value>If true an addres will be marked as departed</value>
			[IgnoreDataMember()]
			[HttpQueryMemberAttribute(Name = "is_departed", EmitDefaultValue = false)]
			public bool IsDeparted { get; set; }

            /// <value>If true an addres will be marked as visited</value>
			[IgnoreDataMember()]
			[HttpQueryMemberAttribute(Name = "is_visited", EmitDefaultValue = false)]
			public bool IsVisited { get; set; }

            /// <value>The member ID</value>
			[HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
			public int? MemberId { get; set; }
		}

        /// <summary>
        /// Marks an address as visited
        /// </summary>
        /// <param name="aParams">An AddressParameters type object containing the route ID and route_destination_id as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Number of the marked addresses</returns>
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
            return (int.TryParse(response.ToString(), out iResponse)) ? Convert.ToInt32(response) : 0;
		}

        /// <summary>
        /// The response from an address marking process as departed.
        /// </summary>
		[DataContract]
		private sealed class MarkAddressDepartedResponse
		{
            /// <value>If true marking process finished successfully</value>
			[DataMember(Name = "status")]
			public Boolean Status { get; set; }

            /// <value>The error string</value>
			[DataMember(Name = "error")]
			public string error { get; set; }
		}

        /// <summary>
        /// Marks an address as deaprted.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Number of the marked addresses</returns>
		public int MarkAddressDeparted(AddressParameters aParams, out string errorString)
		{
			MarkAddressDepartedRequest request = new MarkAddressDepartedRequest
			{
				RouteId = aParams.RouteId,
				AddressId = aParams.AddressId,
				IsDeparted = aParams.IsDeparted,
				MemberId = 1
			};

			MarkAddressDepartedResponse response = GetJsonObjectFromAPI<MarkAddressDepartedResponse>(request, 
                R4MEInfrastructureSettings.MarkAddressDeparted, HttpMethodType.Get, out errorString);

            return (response != null) ? (response.Status ? 1 : 0) : 0;
		}

        /// <summary>
        /// The request parameters for an adress marking process as marked as departed.
        /// </summary>
		[DataContract()]
		private sealed class MarkAddressAsMarkedAsDepartedRequest : GenericParameters
		{
            /// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

            /// <value>The route destination ID</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public int? RouteDestinationId { get; set; }

            /// <value>If true an address will be marked as marked as departed</value>
			[IgnoreDataMember()]
			[DataMember(Name = "is_departed")]
			public bool IsDeparted { get; set; }

            /// <value>If true an address will be marked as marked as visited</value>
			[IgnoreDataMember()]
			[DataMember(Name = "is_visited")]
			public bool IsVisited { get; set; }
		}

        /// <summary>
        /// Marks an address as marked as visited.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An Address type object</returns>
		public Address MarkAddressAsMarkedAsVisited(AddressParameters aParams, out string errorString)
		{
			MarkAddressAsMarkedAsDepartedRequest request = new MarkAddressAsMarkedAsDepartedRequest
			{
				RouteId = aParams.RouteId,
				RouteDestinationId = aParams.RouteDestinationId,
				IsVisited = aParams.IsVisited
			};

            return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);
		}

        /// <summary>
        /// Marks an address as marked as departed.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An Address type object</returns>
		public Address MarkAddressAsMarkedAsDeparted(AddressParameters aParams, out string errorString)
		{
			MarkAddressAsMarkedAsDepartedRequest request = new MarkAddressAsMarkedAsDepartedRequest
			{
				RouteId = aParams.RouteId,
				RouteDestinationId = aParams.RouteDestinationId,
				IsDeparted = aParams.IsDeparted
			};

            return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, out errorString);
		}

		#endregion

		#region Address Book

        /// <summary>
        /// The response from the getting process of the address book contacts
        /// </summary>
		[DataContract]
		private sealed class GetAddressBookContactsResponse : GenericParameters
		{
            /// <value>Array of the AddressBookContact type objects</value>
			[DataMember(Name = "results", IsRequired = false)]
			public AddressBookContact[] results { get; set; }

            /// <value>Number of the returned address book contacts</value>
			[DataMember(Name = "total", IsRequired = false)]
			public uint total { get; set; }
		}

        /// <summary>
        /// Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">>An AddressParameters type object as the input parameters containg the parameters: Offset, Limit</param>
        /// <param name="total">out: Number of the returned contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The array of the address book contacts</returns>
		public AddressBookContact[] GetAddressBookContacts(AddressBookParameters addressBookParameters, out uint total, out string errorString)
		{
			var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters,
																 R4MEInfrastructureSettings.AddressBook,
																 HttpMethodType.Get,
																 out errorString);

            total = (response != null) ? response.total : 0;

            return (response != null) ? response.results : null;
		}

        /// <summary>
        /// Returns an address book contact
        /// </summary>
        /// <param name="addressBookParameters">An AddressParameters type object as the input parameter 
        /// containing the parameter AddressId (comma-delimited list of the address IDs)
        /// </param>
        /// <param name="total">out: Number of the returned contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The array of the address book contacts</returns>
		public AddressBookContact[] GetAddressBookLocation(AddressBookParameters addressBookParameters, out uint total, out string errorString)
		{
            if (addressBookParameters.AddressId!=null && !addressBookParameters.AddressId.Contains(","))
            {
                addressBookParameters.AddressId += "," + addressBookParameters.AddressId;
            }

			var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters, 
                                                                R4MEInfrastructureSettings.AddressBook, 
                                                                HttpMethodType.Get, 
                                                                out errorString);

            total = (response != null) ? response.total : 0;

            return (response != null) ? response.results : null;
		}
        
        /// <summary>
        /// The request parameters for the address book locations searching process.
        /// </summary>
		[DataContract()]
		private sealed class SearchAddressBookLocationRequest : GenericParameters
		{
            /// <value>Comma-delimited list of the contact IDs</value>
			[HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
            public string AddressId { get; set; }

            /// <value>The query text</value>
			[HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
            public string Query { get; set; }

            /// <value>The comma-delimited list of the fields</value>
            [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
			public string Fields { get; set; }

            /// <value>Search starting position</value>
			[HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
			public int? Offset { get; set; }

            /// <value>The number of records to return</value>
			[HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
			public int? Limit { get; set; }
		}

        /// <summary>
        /// The response from the address book locations searching process.
        /// </summary>
		[DataContract()]
		public sealed class SearchAddressBookLocationResponse
		{
            /// <value>The list of the selected fields values</value>
			[DataMember(Name = "results")]
			public List<object[]> Results { get; set; }

            /// <value>Number of the returned address book contacts</value>
			[DataMember(Name = "total")]
			public uint Total { get; set; }

            /// <value>Array of the selected fields</value>
			[DataMember(Name = "fields")]
			public string[] Fields { get; set; }
		}

        /// <summary>
        /// Searches for the address book locations 
        /// </summary>
        /// <param name="addressBookParameters">An AddressParameters type object as the input parameter</param>
        /// <param name="total">out: Number of the returned contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>List of the selected fields values</returns>
		public SearchAddressBookLocationResponse SearchAddressBookLocation(AddressBookParameters addressBookParameters, out string errorString)
		{
            var request = new SearchAddressBookLocationRequest();

            if (addressBookParameters.AddressId != null) request.AddressId = addressBookParameters.AddressId;
            if (addressBookParameters.Query != null) request.Query = addressBookParameters.Query;
            if (addressBookParameters.Fields != null) request.Fields = addressBookParameters.Fields;
            if (addressBookParameters.Offset != null) request.Offset = addressBookParameters.Offset >= 0 ? (int)addressBookParameters.Offset : 0;
            if (addressBookParameters.Limit != null) request.Limit = addressBookParameters.Limit >= 0 ? (int)addressBookParameters.Limit : 0;

            request.PrepareForSerialization();

            var response = GetJsonObjectFromAPI<SearchAddressBookLocationResponse>(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Get, out errorString);

            //total = (response != null) ? response.Total : 0;

            return (response != null) ? response : null;
		}

        /// <summary>
        /// Adds an address book contact to a user's account.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The AddressBookContact type object</returns>
		public AddressBookContact AddAddressBookContact(AddressBookContact contact, out string errorString)
		{
			contact.PrepareForSerialization();
            return GetJsonObjectFromAPI<AddressBookContact>(contact,
											R4MEInfrastructureSettings.AddressBook,
											HttpMethodType.Post,
											out errorString);
		}

        /// <summary>
        /// Updates an address book contact.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The AddressBookContact type object</returns>
		public AddressBookContact UpdateAddressBookContact(AddressBookContact contact, out string errorString)
		{
			contact.PrepareForSerialization();
            return GetJsonObjectFromAPI<AddressBookContact>(contact,
											R4MEInfrastructureSettings.AddressBook,
											HttpMethodType.Put,
											out errorString);
		}

        /// <summary>
        /// The request parameter for the address book contacts removing process.
        /// </summary>
		[DataContract]
		private sealed class RemoveAddressBookContactsRequest : GenericParameters
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
		public bool RemoveAddressBookContacts(string[] addressIds, out string errorString)
		{
			RemoveAddressBookContactsRequest request = new RemoveAddressBookContactsRequest()
			{
				AddressIds = addressIds
			};

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(request,
																   R4MEInfrastructureSettings.AddressBook,
																   HttpMethodType.Delete,
																   out errorString);

			return (response != null && response.status) ? true : false;
		}

		#endregion

		#region Address Book Group


		public AddressBookGroup[] GetAddressBookGroups(AddressBookGroupParameters addressBookGroupParameters, out string errorString)
		{
			var response = GetJsonObjectFromAPI<AddressBookGroup[]>(addressBookGroupParameters,
																 R4MEInfrastructureSettings.AddressBookGroup,
																 HttpMethodType.Get,
																 out errorString);
			return response;
		}

		public AddressBookGroup GetAddressBookGroup(AddressBookGroupParameters addressBookGroupParameters, out string errorString)
		{
			addressBookGroupParameters.PrepareForSerialization();
			var response = GetJsonObjectFromAPI<AddressBookGroup>(addressBookGroupParameters,
																 R4MEInfrastructureSettings.AddressBookGroup,
																 HttpMethodType.Get,
																 out errorString);
			return response;
		}

		public AddressBookContactsResponse GetAddressBookContactsByGroup(AddressBookGroupParameters addressBookGroupParameters, out string errorString)
		{
			addressBookGroupParameters.PrepareForSerialization();
			var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookGroupParameters,
																 R4MEInfrastructureSettings.AddressBookGroupSearch,
																 HttpMethodType.Post,
																 out errorString);
			return response;
		}

		public AddressBookContactsResponse SearchAddressBookContactsByFilter(AddressBookGroupParameters addressBookGroupParameters, out string errorString)
		{
			addressBookGroupParameters.PrepareForSerialization();
			var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookGroupParameters,
																 R4MEInfrastructureSettings.AddressBook,
																 HttpMethodType.Post,
																 out errorString);
			return response;
		}

		public AddressBookGroup AddAddressBookGroup(AddressBookGroup group, out string errorString)
		{
			group.PrepareForSerialization();
			AddressBookGroup result = GetJsonObjectFromAPI<AddressBookGroup>(group,
																 R4MEInfrastructureSettings.AddressBookGroup,
																 HttpMethodType.Post,
																 out errorString);
			return result;
		}

		public AddressBookGroup UpdateAddressBookGroup(AddressBookGroup group, out string errorString)
		{
			group.PrepareForSerialization();
			AddressBookGroup result = GetJsonObjectFromAPI<AddressBookGroup>(group,
																 R4MEInfrastructureSettings.AddressBookGroup,
																 HttpMethodType.Put,
																 out errorString);
			return result;
		}

		public StatusResponse RemoveAddressBookGroup(AddressBookGroupParameters groupID, out string errorString)
		{
			groupID.PrepareForSerialization();
			StatusResponse result = GetJsonObjectFromAPI<StatusResponse>(groupID,
																 R4MEInfrastructureSettings.AddressBookGroup,
																 HttpMethodType.Delete,
																 out errorString);
			return result;
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

		/// <summary>
		/// Delete avoidance zone (by territory id, device id)
		/// </summary>
		/// <param name="avoidanceZoneQuery"> Parameters for request </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns> Result status true/false </returns>
		public bool DeleteAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
		{
			var result = GetJsonObjectFromAPI<StatusResponse>(avoidanceZoneQuery,
																 R4MEInfrastructureSettings.Avoidance,
																 HttpMethodType.Delete,
																 out errorString);

			return result.status;
		}

		#endregion

		#region Orders

        /// <summary>
        /// The response for the orders getting process.
        /// </summary>
		[DataContract]
		private sealed class GetOrdersResponse
        {
            /// <value>An arrary of the Order type objects </value>
			[DataMember(Name = "results")]
			public Order[] Results { get; set; }

            /// <value>Number of the returned orders</value>
            [DataMember(Name = "total")]
			public uint Total { get; set; }
		}

		/// <summary>
		/// Gets the Orders
		/// </summary>
		/// <param name="ordersQuery"> The query parameters for the orders request process </param>
		/// <param name="total"> out: Total number of the orders </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns> List of the Order type objects </returns>
		public Order[] GetOrders(OrderParameters ordersQuery, out uint total, out string errorString)
		{
			GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(ordersQuery,
																 R4MEInfrastructureSettings.Order,
																 HttpMethodType.Get,
																 out errorString);

            total = (response != null) ? response.Total : 0;

            return (response != null) ? response.Results : null;
		}

        /// <summary>
        /// Gets an array of the Order type objects by list of the order IDs.
        /// </summary>
        /// <param name="orderQuery">The OrderParameters type object as the input parameters containing coma-delimited list of the order IDs.</param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>List of the Order type objects</returns>
		public Order[] GetOrderByID(OrderParameters orderQuery, out string errorString)
		{
			string[] ids = orderQuery.order_id.Split(',');
			if (ids.Length == 1) orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id;

			GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(orderQuery, 
                R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);

			return response.Results;
		}

        /// <summary>
        /// Searches for the orders.
        /// </summary>
        /// <param name="orderQuery">The OrderParameters type object as the query parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>List of the Order type objects</returns>
		public Order[] SearchOrders(OrderParameters orderQuery, out string errorString)
		{
			GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(orderQuery, 
                R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);

            return (response != null) ? response.Results : null;
		}

        /// <summary>
        /// Filter for the orders filtering
        /// </summary>
        /// <param name="orderFilter">The OrderFilterParameters object as a HTTP request payload</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Array of the Order type objects</returns>
        public Order[] FilterOrders(OrderFilterParameters orderFilter, out string errorString)
        {
            GetOrdersResponse response = GetJsonObjectFromAPI<GetOrdersResponse>(orderFilter,
                R4MEInfrastructureSettings.Order, HttpMethodType.Post, out errorString);

            return (response != null) ? response.Results : null;
        }

		/// <summary>
		/// Creates an order
		/// </summary>
		/// <param name="order"> The Order type object as the request payload </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns> Order object </returns>
		public Order AddOrder(Order order, out string errorString)
		{
			order.PrepareForSerialization();

            return GetJsonObjectFromAPI<Order>(order, R4MEInfrastructureSettings.Order,
															HttpMethodType.Post,
															out errorString);
		}

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="order"> The Order type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> An Order type object </returns>
        public Order UpdateOrder(Order order, out string errorString)
		{
			order.PrepareForSerialization();

            return GetJsonObjectFromAPI<Order>(order, R4MEInfrastructureSettings.Order,
															HttpMethodType.Put,
															out errorString);
		}

        /// <summary>
        /// The request parameter containing the array of the order IDs.
        /// </summary>
		[DataContract]
		private sealed class RemoveOrdersRequest : GenericParameters
		{
            /// <value>The array of the order IDs</value>
            [DataMember(Name = "order_ids", EmitDefaultValue = false)]
			public string[] OrderIds { get; set; }
		}


        /// <summary>
        /// Removes the orders
        /// </summary>
        /// <param name="orderIds"> The array of the order IDs </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status: true/false </returns>
        public bool RemoveOrders(string[] orderIds, out string errorString)
		{
			RemoveOrdersRequest request = new RemoveOrdersRequest()
			{
				OrderIds = orderIds
			};

			StatusResponse response = GetJsonObjectFromAPI<StatusResponse>(request,
																   R4MEInfrastructureSettings.Order,
																   HttpMethodType.Delete,
																   out errorString);

			return (response != null && response.status) ? true : false;
		}

        /// <summary>
        /// The request parameters for the process of adding the orders to a route.
        /// </summary>
		[DataContract()]
		private sealed class AddOrdersToRouteRequest : GenericParameters
		{
            /// <value>The route ID</value>
            [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
            public string RouteId { get; set; }

            /// <value>If equal to 1, it will be redirected</value>
            [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
			public int? Redirect { get; set; }

            /// <value>The array of the addresses</value>
            [DataMember(Name = "addresses", EmitDefaultValue = false)]
			public Address[] Addresses { get; set; }

            /// <value>The route parameters</value>
			[DataMember(Name = "parameters", EmitDefaultValue = false)]
			public RouteParameters Parameters { get; set; }
		}

        /// <summary>
        /// Adds the orders to a route.
        /// </summary>
        /// <param name="rQueryParams">The RouteParametersQuery type objects as the query parameters</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The RouteResponse type object</returns>
		public RouteResponse AddOrdersToRoute(RouteParametersQuery rQueryParams, Address[] addresses, RouteParameters rParams, out string errorString)
		{
			AddOrdersToRouteRequest request = new AddOrdersToRouteRequest
			{
				RouteId = rQueryParams.RouteId,
				Redirect = rQueryParams.Redirect == true ? 1 : 0,
				Addresses = addresses,
				Parameters = rParams
			};

            return GetJsonObjectFromAPI<RouteResponse>(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, false, out errorString);
		}

        /// <summary>
        /// The request parameters for the orders adding process to an optimization.
        /// </summary>
		[DataContract()]
		private sealed class AddOrdersToOptimizationRequest : GenericParameters
		{
            /// <value>The optimization problem ID</value>
            [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
			public string OptimizationProblemId { get; set; }

            /// <value>If equal to 1, it will be redirected</value>
			[HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
			public int? Redirect { get; set; }

            /// <value>The array of the addresses</value>
			[DataMember(Name = "addresses", EmitDefaultValue = false)]
			public Address[] Addresses { get; set; }

            /// <value>The route parameters</value>
			[DataMember(Name = "parameters", EmitDefaultValue = false)]
			public RouteParameters Parameters { get; set; }
		}

        /// <summary>
        /// Adds the orders to an optimization.
        /// </summary>
        /// <param name="rQueryParams"> The RouteParametersQuery type objects as the query parameters </param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An optimization problem object</returns>
		public DataObject AddOrdersToOptimization(OptimizationParameters rQueryParams, Address[] addresses, RouteParameters rParams, out string errorString)
		{
			AddOrdersToOptimizationRequest request = new AddOrdersToOptimizationRequest
			{
				OptimizationProblemId = rQueryParams.OptimizationProblemID,
				Redirect = rQueryParams.Redirect == true ? 1 : 0,
				Addresses = addresses,
				Parameters = rParams
			};

            return GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, false, out errorString);
		}

        #endregion

        #region Order Custom User Field

        public OrderCustomField[] GetOrderCustomUserFields(out string errorString)
        {
            var genParams = new GenericParameters(); 

            OrderCustomField[] response = GetJsonObjectFromAPI<OrderCustomField[]>(genParams,
                                                                 R4MEInfrastructureSettings.OrderCustomField,
                                                                 HttpMethodType.Get,
                                                                 out errorString);

            return response;
        }

        public OrderCustomFieldCreateResponse CreateOrderCustomUserField(OrderCustomFieldParameters orderCustomUserField, out string errorString)
        {
            return GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
                (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField, 
                HttpMethodType.Post, false, out errorString);

        }

        public OrderCustomFieldCreateResponse RemoveOrderCustomUserField(OrderCustomFieldParameters orderCustomUserField, out string errorString)
        {
            return GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
                (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField, 
                HttpMethodType.Delete, false, out errorString);
        }

        public OrderCustomFieldCreateResponse UpdateOrderCustomUserField(OrderCustomFieldParameters orderCustomUserFieldParams, out string errorString)
        {
            orderCustomUserFieldParams.PrepareForSerialization();

            var orderCustomField = GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
                (orderCustomUserFieldParams, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Put, false, out errorString);

            return orderCustomField;

        }

        #endregion

        #region Geocoding

        /// <summary>
        /// The request parameters for the geocoding process.
        /// </summary>
        [DataContract()]
		private sealed class GeocodingRequest : GenericParameters
		{
            /// <value>The list of the addresses delimited by the symbol '|'</value>
            [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
			public string Addresses { get; set; }

            /// <value>The response format (son, xml)</value>
			[HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
			public string Format { get; set; }
		}

        /// <summary>
        /// The response for the rapid street data request
        /// </summary>
		[DataContract()]
		private sealed class RapidStreetResponse
		{
            /// <value>The zip code</value>
            [DataMember(Name = "zipcode")]
			public string Zipcode { get; set; }

            /// <value>The street name</value>
			[DataMember(Name = "street_name")]
			public string StreetName { get; set; }
		}

        /// <summary>
        /// Geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The geocoded addresses</returns>
		public string Geocoding(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest
			{
				Addresses = geoParams.Addresses,
				Format = geoParams.Format
			};

			var response = GetXmlObjectFromAPI<string>(request, R4MEInfrastructureSettings.Geocoder, 
                HttpMethodType.Post, (HttpContent)null, true, out errorString);

			if (response == null) return errorString; else return response.ToString();
		}

        /// <summary>
        /// Batch geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The geocoded addresses</returns>
		public string BatchGeocoding(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest { };

			var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("strExportFormat", geoParams.Format),
                new KeyValuePair<string, string>("addresses", geoParams.Addresses)
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

            return GetJsonObjectFromAPI<string>(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, httpContent, true, out errorString);
		}

        /// <summary>
        /// The response from the addresses uploading process to temporary storage.
        /// </summary>
		[DataContract]
		public sealed class uploadAddressesToTemporaryStorageResponse : GenericParameters
		{
            /// <value>The optimization problem ID</value>
            [DataMember(Name = "optimization_problem_id", IsRequired = false)]
			public string optimization_problem_id { get; set; }

            /// <value>The temporary addresses storage ID</value>
			[DataMember(Name = "temporary_addresses_storage_id", IsRequired = false)]
			public string temporary_addresses_storage_id { get; set; }

            /// <value>Number of the uploaded addresses</value>
			[DataMember(Name = "address_count", IsRequired = false)]
			public uint address_count { get; set; }

            /// <value>Status of the process: true, false</value>
			[DataMember(Name = "status", IsRequired = false)]
			public bool status { get; set; }
		}

        /// <summary>
        /// Uploads the addresses to temporary storage.
        /// </summary>
        /// <param name="jsonAddresses">The addresses, JSON formatted</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The uploadAddressesToTemporaryStorageResponse type object</returns>
		public uploadAddressesToTemporaryStorageResponse uploadAddressesToTemporaryStorage(string jsonAddresses, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest { };

			HttpContent content = new StringContent(jsonAddresses);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			Tuple<uploadAddressesToTemporaryStorageResponse, string> result = GetJsonObjectFromAPIAsync<uploadAddressesToTemporaryStorageResponse>(request,
															   R4MEInfrastructureSettings.FastGeocoder,
															   HttpMethodType.Post,
															   content, false).GetAwaiter().GetResult();

			Thread.SpinWait(5000);

            errorString = result.Item2;

			return result.Item1;
		}

        /// <summary>
        /// Asynchronous bath geocoding of the addresses.
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The geocoded addresses</returns>
		public string BatchGeocodingAsync(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest { };

			var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("strExportFormat", geoParams.ExportFormat),
                new KeyValuePair<string, string>("addresses", geoParams.Addresses)
            };

			HttpContent httpContent = new FormUrlEncodedContent(keyValues);

			Task<Tuple<string, string>> result = GetJsonObjectFromAPIAsync<string>(request,
																R4MEInfrastructureSettings.Geocoder,
																HttpMethodType.Post,
																httpContent, true);

			result.Wait();

            errorString = (result.IsFaulted || result.IsCanceled) ? result.Result.Item2 : "";

			return result.Result.Item1;
		}

        /// <summary>
        /// Returns rapid street data
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the street data</returns>
		public ArrayList RapidStreetData(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest
			{
				Addresses = geoParams.Addresses,
				Format = geoParams.Format
			};

			string url = R4MEInfrastructureSettings.RapidStreetData;

			ArrayList result = new ArrayList();

			if (geoParams.Pk > 0)
			{
				url = url + "/" + geoParams.Pk + "/";

				RapidStreetResponse response = GetJsonObjectFromAPI<RapidStreetResponse>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);
				Dictionary<string, string> dresult = new Dictionary<string, string>();

				if ((response != null))
				{
					dresult["zipcode"] = response.Zipcode;
					dresult["street_name"] = response.StreetName;
					result.Add(dresult);
				}
			}
			else
			{
				if (geoParams.Offset > 0 | geoParams.Limit > 0)
				{
					url = url + "/" + geoParams.Offset + "/" + geoParams.Limit + "/";
				}

				RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, HttpMethodType.Get, (HttpContent)null, false, out errorString);

                if ((response != null))
				{
					foreach (var resp1 in response)
					{
						Dictionary<string, string> dresult = new Dictionary<string, string>();
						dresult["zipcode"] = resp1.Zipcode;
						dresult["street_name"] = resp1.StreetName;
						result.Add(dresult);
					}
				}
			}

			return result;
		}

        /// <summary>
        /// Return the rapid street zip codes
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the street zipcodes</returns>
		public ArrayList RapidStreetZipcode(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest
			{
				Addresses = geoParams.Addresses,
				Format = geoParams.Format
			};

			string url = R4MEInfrastructureSettings.RapidStreetZipcode;

			ArrayList result = new ArrayList();

			if (geoParams.Zipcode != null)
			{
				url = url + "/" + geoParams.Zipcode + "/";
			}
			else
			{
				errorString = "Zipcode is not defined!...";
				return result;
			}

			if (geoParams.Offset > 0 | geoParams.Limit > 0)
			{
				url = url + geoParams.Offset + "/" + geoParams.Limit + "/";
			}

			RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, 
                                        HttpMethodType.Get, (HttpContent)null, false, out errorString);

            if ((response != null))
			{
				foreach (var resp1 in response)
				{
					Dictionary<string, string> dresult = new Dictionary<string, string>();
					dresult["zipcode"] = resp1.Zipcode;
					dresult["street_name"] = resp1.StreetName;
					result.Add(dresult);
				}
			}

			return result;
		}

        /// <summary>
        /// Return the array of rapid street services
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the street services</returns>
		public ArrayList RapidStreetService(GeocodingParameters geoParams, out string errorString)
		{
			GeocodingRequest request = new GeocodingRequest
			{
				Addresses = geoParams.Addresses,
				Format = geoParams.Format
			};

			string url = R4MEInfrastructureSettings.RapidStreetService;

			ArrayList result = new ArrayList();

			if (geoParams.Zipcode != null)
			{
				url = url + "/" + geoParams.Zipcode + "/";
			}
			else
			{
				errorString = "Zipcode is not defined!...";
				return result;
			}

			if (geoParams.Housenumber != null)
			{
				url = url + geoParams.Housenumber + "/";
			}
			else
			{
				errorString = "Housenumber is not defined!...";
				return result;
			}

			if (geoParams.Offset > 0 | geoParams.Limit > 0)
			{
				url = url + geoParams.Offset + "/" + geoParams.Limit + "/";
			}

			RapidStreetResponse[] response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, 
                                        HttpMethodType.Get, (HttpContent)null, false, out errorString);

            if ((response != null))
			{
				foreach (var resp1 in response)
				{
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
        /// Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The created vehicle </returns>
        public VehicleV4CreateResponse CreateVehicle(VehicleV4Parameters vehicle, out string errorString)
		{
            return GetJsonObjectFromAPI<VehicleV4CreateResponse>(vehicle, R4MEInfrastructureSettings.Vehicle_V4_API, HttpMethodType.Post, out errorString);
		}

        /// <summary>
        /// Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <param name="total"> out: Total number of the vehicles </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The VehiclesPaginated type object containing an array of the vehicles</returns>
        public VehiclesPaginated GetVehicles(VehicleParameters vehParams, out string errorString)
		{
            return GetJsonObjectFromAPI<VehiclesPaginated>(vehParams, R4MEInfrastructureSettings.Vehicle_V4,
															                       HttpMethodType.Get,
															                       out errorString);
		}

        /// <summary>
        /// Returns a vehicle
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> A vehicle </returns>
        public VehicleV4Response GetVehicle(VehicleParameters vehParams, out string errorString)
		{
            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams, R4MEInfrastructureSettings.Vehicle_V4,
															                       HttpMethodType.Get,
															                       out errorString);
		}

        /// <summary>
        /// Updates a vehicle
        /// </summary>
        /// <param name="vehParams">The VehicleV4Parameters type object as the request payload</param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The updated vehicle</returns>
		public VehicleV4Response updateVehicle(VehicleV4Parameters vehParams, string vehicleId, out string errorString)
		{

            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams, R4MEInfrastructureSettings.Vehicle_V4+ @"/"+ vehicleId, 
															                      HttpMethodType.Put,
															                      out errorString);
		}

        /// <summary>
        /// Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The removed vehicle</returns>
		public VehicleV4Response deleteVehicle(VehicleV4Parameters vehParams, out string errorString)
		{
            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams, R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId, 
															                      HttpMethodType.Delete,
															                      out errorString);
		}

        /// <summary>
        /// Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams">The VehicleParameters type object as the query parameters containing parameter VehicleId</param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The removed vehicle</returns>
		public VehicleV4Response deleteVehicle(VehicleParameters vehParams, out string errorString)
		{
            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams, R4MEInfrastructureSettings.Vehicle_V4,
															                      HttpMethodType.Get,
															                      out errorString);
		}


        #endregion

        #region Territories
        /// <summary>
        /// Creates a territory
        /// </summary>
        /// <param name="avoidanceZoneParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The Territory type object </returns>
        public TerritoryZone CreateTerritory(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
		{
            return GetJsonObjectFromAPI<TerritoryZone>(avoidanceZoneParameters, R4MEInfrastructureSettings.Territory, 
                                                                                HttpMethodType.Post, 
                                                                                out errorString);
		}

        /// <summary>
        /// Gets the territories by parameters
        /// </summary>
        /// <param name="avoidanceZoneQuery"> >The AvoidanceZoneQuery type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The teritories </returns>
        public AvoidanceZone[] GetTerritories(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
		{
            return GetJsonObjectFromAPI<AvoidanceZone[]>(avoidanceZoneQuery, R4MEInfrastructureSettings.Territory, 
                                                                                HttpMethodType.Get, 
                                                                                out errorString);
		}

        /// <summary>
        /// Gets a territory by query parameters (TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The TerritoryQuery type object as query parmaeters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> A TerritoryZone type object </returns>
        public TerritoryZone GetTerritory(TerritoryQuery territoryQuery, out string errorString)
		{
            return GetJsonObjectFromAPI<TerritoryZone>(territoryQuery, R4MEInfrastructureSettings.Territory, 
                                                                                HttpMethodType.Get, 
                                                                                out errorString);

		}

        /// <summary>
        /// Removes a trritory (by TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The AvoidanceZoneQuery type object as query parmaeters (TerritoryId) </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status: true/false </returns>
        public bool RemoveTerritory(AvoidanceZoneQuery territoryQuery, out string errorString)
		{
			var result = GetJsonObjectFromAPI<StatusResponse>(territoryQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.Delete, out errorString);

			return result.status;
		}

        /// <summary>
        /// Updates a territory
        /// </summary>
        /// <param name="tereritoryParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Territory Object </returns>
        public AvoidanceZone UpdateTerritory(AvoidanceZoneParameters tereritoryParameters, out string errorString)
		{
            return GetJsonObjectFromAPI<AvoidanceZone>(tereritoryParameters, R4MEInfrastructureSettings.Territory, 
                                                                                HttpMethodType.Put, 
                                                                                out errorString);
		}

        #endregion

        #region Telematics Vendors

        /// <summary>
        /// Returns the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The telematics vendors</returns>
        public TelematicsVendorsResponse GetAllTelematicsVendors(TelematicsVendorParameters vendorParams, out string errorString)
		{
            return GetJsonObjectFromAPI<TelematicsVendorsResponse>(vendorParams, R4MEInfrastructureSettings.TeleamticsVendorsHost,
															                   HttpMethodType.Get,
															                   out errorString);
		}

        /// <summary>
        /// Returns a telematics vendor
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters (vendorID) </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>A telematics vendor</returns>
		public TelematicsVendorResponse GetTelematicsVendor(TelematicsVendorParameters vendorParams, out string errorString)
		{
            return GetJsonObjectFromAPI<TelematicsVendorResponse>(vendorParams, R4MEInfrastructureSettings.TeleamticsVendorsHost,
															                   HttpMethodType.Get,
															                   out errorString);
		}

        /// <summary>
        /// Searches for the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The TelematicsVendorsSearchResponse type object containing found telematics vendors</returns>
		public TelematicsVendorsSearchResponse SearchTelematicsVendors(TelematicsVendorParameters vendorParams, out string errorString)
		{
            return GetJsonObjectFromAPI<TelematicsVendorsSearchResponse>(vendorParams, R4MEInfrastructureSettings.TeleamticsVendorsHost,
															                   HttpMethodType.Get,
															                   out errorString);
		}

		#endregion

		#endregion

		#region Generic Methods

		public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
											   string url,
											   HttpMethodType httpMethod,
											   out string errorMessage)
		{
			string result = GetJsonObjectFromAPI<string>(optimizationParameters,
														 url,
														 httpMethod,
														 true,
														 out errorMessage);

			return result;
		}

		public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
										 string url,
										 HttpMethodType httpMethod,
										 out string errorMessage)
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
										  string url,
										  HttpMethodType httpMethod,
										  bool isString,
										  out string errorMessage)
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

		private async Task<Tuple<T, string>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
										string url,
										HttpMethodType httpMethod,
										bool isString)
		where T : class
		{
			return await Task.Run(() =>
			{
				Task<Tuple<T, string>> result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
											   url,
											   httpMethod,
											   (HttpContent)null,
											   isString);

				return result;
			});


		}

		private async Task<Tuple<T, string>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
									   string url,
									   HttpMethodType httpMethod,
									   HttpContent httpContent,
									   bool isString)
	   where T : class
		{
			//out string errorMessage return this parameter in the tuple

			T result = default(T);
			string errorMessage = string.Empty;

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
									content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
								}

								HttpResponseMessage response = null;
								if (isPut)
								{
									response = await httpClient.PutAsync(parametersURI, content);
								}
								else if (isDelete)
								{
									HttpRequestMessage request = new HttpRequestMessage
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
								else
								{
									var streamTask = await ((StreamContent)response.Content).ReadAsStreamAsync();
									ErrorResponse errorResponse = null;
									try
									{
										errorResponse = streamTask.ReadObject<ErrorResponse>();
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
										var responseStream = await response.Content.ReadAsStringAsync();
										String responseString = responseStream;
										if (responseString != null)
											errorMessage = "Response: " + responseString;
									}
								}

								break;
							}
					}
				}
			}
			catch (HttpListenerException e)
			{
				errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;

				errorMessage = e.Message + " --- " + errorMessage;
				result = null;
			}
			catch (Exception e)
			{
				errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;
				result = default(T);
			}

			return new Tuple<T, string>(result, errorMessage);
		}


		private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
											  string url,
											  HttpMethodType httpMethod,
											  HttpContent httpContent,
											  bool isString,
											  out string errorMessage)
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
									content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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
			catch (HttpListenerException e)
			{
				errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;

				errorMessage = e.Message + " --- " + errorMessage;
				result = null;
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
										result = streamTask.Result.ReadString();
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
			catch (HttpListenerException e)
			{
				errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;

				errorMessage = e.Message + " --- " + errorMessage;
				result = null;
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

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

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

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
			HttpClient result = new HttpClient(handler) { BaseAddress = new Uri(url) };

			result.Timeout = m_DefaultTimeOut;
			result.DefaultRequestHeaders.Accept.Clear();
			result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return result;
		}

		#endregion

		#endregion
	}
}
