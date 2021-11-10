using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets the routes from specified date range.
        /// </summary>
        public void GetRoutesFromDateRange()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            TimeSpan t10days = new TimeSpan(10, 0, 0, 0);
            DateTime dtNow = DateTime.Now;

            var routeParameters = new RouteParametersQuery()
            {
                StartDate = (dtNow - t10days).ToString("yyyy-MM-dd"),
                EndDate = dtNow.ToString("yyyy-MM-dd")
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
