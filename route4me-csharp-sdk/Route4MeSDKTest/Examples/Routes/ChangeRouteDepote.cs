using Route4MeSDK.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Change a route depot.
        /// </summary>
        public void ChangeRouteDepote()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            RunSingleDriverRoundTrip();
            OptimizationsToRemove = new List<string>()
            {
                SDRT_optimization_problem_id
            };

            string routeId = SDRT_route_id;

            var initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(SDRT_route);

            SDRT_route.Addresses[0].IsDepot = false;
            int? addressId0 = SDRT_route.Addresses[0].RouteDestinationId;
            SDRT_route.Addresses[0].Alias = addressId0.ToString();
            initialRoute.Addresses[0].Alias = addressId0.ToString();

            SDRT_route.Addresses[1].IsDepot = true;
            int? addressId1 = SDRT_route.Addresses[1].RouteDestinationId;
            SDRT_route.Addresses[1].Alias = addressId1.ToString();
            initialRoute.Addresses[1].Alias = addressId1.ToString();

            var dataObject = route4Me.UpdateRoute(
                SDRT_route,
                initialRoute,
                out string errorString
            );

            #region Print Results

            PrintExampleRouteResult(dataObject, errorString);

            var address0 = dataObject.Addresses
                .Where(x => x.Alias == addressId0.ToString())
                .FirstOrDefault();

            PrintExampleDestination(address0);

            var address1 = dataObject.Addresses
                .Where(x => x.Alias == addressId1.ToString())
                .FirstOrDefault();

            PrintExampleDestination(address1);

            #endregion

            RemoveTestOptimizations();
        }
    }
}
