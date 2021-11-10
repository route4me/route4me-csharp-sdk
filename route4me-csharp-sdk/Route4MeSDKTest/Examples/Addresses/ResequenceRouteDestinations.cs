using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Resequence the route destinations.
        /// </summary>
        /// <param name="route">A route object</param>
        public void ResequenceRouteDestinations(DataObjectRoute route = null)
        {
            bool isInnerExample = route == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                route = SD10Stops_route;
            }

            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Switch 2 addresses after departure address:

            AddressesOrderInfo addressesOrderInfo = new AddressesOrderInfo();
            addressesOrderInfo.RouteId = route.RouteId;
            addressesOrderInfo.Addresses = new AddressInfo[0];

            for (int i = 0; i < route.Addresses.Length; i++)
            {
                Address address = route.Addresses[i];
                AddressInfo addressInfo = new AddressInfo();

                addressInfo.DestinationId = address.RouteDestinationId.Value;

                addressInfo.SequenceNo = i;

                addressInfo.SequenceNo = i == 1 ? 2 : 1;

                addressInfo.IsDepot = (addressInfo.SequenceNo == 0);

                var addressesList = new List<AddressInfo>(addressesOrderInfo.Addresses);

                addressesList.Add(addressInfo);

                addressesOrderInfo.Addresses = addressesList.ToArray();
            }

            // Run the query
            DataObjectRoute route1 = route4Me
                .GetJsonObjectFromAPI<DataObjectRoute>(addressesOrderInfo,
                                    R4MEInfrastructureSettings.RouteHost,
                                    HttpMethodType.Put,
                                    out string errorString1);

            // Output the result
            PrintExampleRouteResult(route1, errorString1);

            if (isInnerExample) RemoveTestOptimizations();
        }

        [DataContract]
        private class AddressInfo : GenericParameters
        {
            [DataMember(Name = "route_destination_id")]
            public int DestinationId { get; set; }

            [DataMember(Name = "sequence_no")]
            public int SequenceNo { get; set; }

            [DataMember(Name = "is_depot")]
            public bool IsDepot { get; set; }
        }

        [DataContract]
        private class AddressesOrderInfo : GenericParameters
        {
            [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
            public string RouteId { get; set; }

            [DataMember(Name = "addresses")]
            public AddressInfo[] Addresses { get; set; }
        }

    }
}
