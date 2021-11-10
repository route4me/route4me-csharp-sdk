using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the routes by list of the route IDs.
        /// </summary>
        public void GetRoutesByIDs()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            #region // Retrieve first 3 routes

            var routesParameters = new RouteParametersQuery()
            {
                Offset = 0,
                Limit = 3
            };

            DataObjectRoute[] threeRoutes = route4Me.GetRoutes(
                routesParameters,
                out string errorString
            );

            #endregion

            #region // Retrieve 2 route by their IDs

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = threeRoutes[0].RouteId + "," + threeRoutes[1].RouteId
            };

            var twoRoutes = route4Me.GetRoutes(routeParameters, out errorString);

            #endregion

            PrintExampleRouteResult(twoRoutes, errorString);
        }
    }
}
