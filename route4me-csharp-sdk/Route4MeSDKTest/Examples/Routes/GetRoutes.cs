using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get limited number of the routes.
        /// </summary>
        public void GetRoutes()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParametersQuery()
            {
                Limit = 10,
                Offset = 5
            };

            // Run the query
            DataObjectRoute[] dataObjects = route4Me.GetRoutes(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObjects, errorString);
        }
    }
}
